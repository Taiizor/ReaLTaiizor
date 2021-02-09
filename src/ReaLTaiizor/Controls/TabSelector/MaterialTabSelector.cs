#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Util;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using static ReaLTaiizor.Util.MaterialAnimations;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialTabSelector

    public class MaterialTabSelector : Control, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialManager SkinManager => MaterialManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        public enum TextState
        {
            Upper,
            Lower,
            Normal
        }

        private TextState _TitleTextState = TextState.Normal;
        public TextState TitleTextState
        {
            get => _TitleTextState;
            set
            {
                _TitleTextState = value;
                Invalidate();
            }
        }

        public enum Alignment
        {
            Left,
            Center,
            Right
        }

        private Alignment _HeadAlignment = Alignment.Left;
        public Alignment HeadAlignment
        {
            get => _HeadAlignment;
            set
            {
                _HeadAlignment = value;
                Invalidate();
            }
        }

        private string TitleText(string Text)
        {
            return TitleTextState switch
            {
                TextState.Upper => Text.ToUpperInvariant(),
                TextState.Lower => Text.ToLowerInvariant(),
                _ => Text,
            };
        }

        private MaterialTabControl _baseTabControl;

        public MaterialTabControl BaseTabControl
        {
            get => _baseTabControl;
            set
            {
                _baseTabControl = value;
                if (_baseTabControl == null)
                {
                    return;
                }

                _previousSelectedTabIndex = _baseTabControl.SelectedIndex;
                _baseTabControl.Deselected += (sender, args) =>
                {
                    _previousSelectedTabIndex = _baseTabControl.SelectedIndex;
                };
                _baseTabControl.SelectedIndexChanged += (sender, args) =>
                {
                    _animationManager.SetProgress(0);
                    _animationManager.StartNewAnimation(AnimationDirection.In);
                };
                _baseTabControl.ControlAdded += delegate
                {
                    Invalidate();
                };
                _baseTabControl.ControlRemoved += delegate
                {
                    Invalidate();
                };
            }
        }

        private int _previousSelectedTabIndex;

        private Point _animationSource;

        private readonly AnimationManager _animationManager;

        private List<Rectangle> _tabRects;

        private const int TAB_HEADER_PADDING = 24;

        private const int TAB_INDICATOR_HEIGHT = 2;

        public MaterialTabSelector()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            Height = 48;

            _animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseOut,
                Increment = 0.04
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = SkinManager.getFontByType(MaterialManager.fontType.Body1);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(SkinManager.ColorScheme.PrimaryColor);

            if (_baseTabControl != null && _baseTabControl.TabPages.Count > 0)
            {
                if (!_animationManager.IsAnimating() || _tabRects == null || _tabRects.Count != _baseTabControl.TabCount)
                {
                    UpdateTabRects();
                }

                double animationProgress = _animationManager.GetProgress();

                //Click feedback
                if (_animationManager.IsAnimating())
                {
                    SolidBrush rippleBrush = new(Color.FromArgb((int)(51 - (animationProgress * 50)), Color.White));
                    int rippleSize = (int)(animationProgress * _tabRects[_baseTabControl.SelectedIndex].Width * 1.75);

                    g.SetClip(_tabRects[_baseTabControl.SelectedIndex]);
                    g.FillEllipse(rippleBrush, new Rectangle(_animationSource.X - rippleSize / 2, _animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    g.ResetClip();
                    rippleBrush.Dispose();
                }

                //Draw tab headers
                foreach (System.Windows.Forms.TabPage tabPage in _baseTabControl.TabPages)
                {
                    int currentTabIndex = _baseTabControl.TabPages.IndexOf(tabPage);

                    using MaterialNativeTextRenderer NativeText = new(g);
                    Rectangle textLocation = _tabRects[currentTabIndex];
                    NativeText.DrawTransparentText(
                        TitleText(tabPage.Text),
                        SkinManager.getLogFontByType(MaterialManager.fontType.Button),
                        Color.FromArgb(CalculateTextAlpha(currentTabIndex, animationProgress), SkinManager.ColorScheme.TextColor),
                        textLocation.Location,
                        textLocation.Size,
                        MaterialNativeTextRenderer.TextAlignFlags.Center | MaterialNativeTextRenderer.TextAlignFlags.Middle);
                }

                try
                {
                    //Animate tab indicator
                    int previousSelectedTabIndexIfHasOne = _previousSelectedTabIndex == -1 ? _baseTabControl.SelectedIndex : _previousSelectedTabIndex;
                    Rectangle previousActiveTabRect = _tabRects[previousSelectedTabIndexIfHasOne];
                    Rectangle activeTabPageRect = _tabRects[_baseTabControl.SelectedIndex];

                    int y = activeTabPageRect.Bottom - 2;
                    int x = previousActiveTabRect.X + (int)((activeTabPageRect.X - previousActiveTabRect.X) * animationProgress);
                    int width = previousActiveTabRect.Width + (int)((activeTabPageRect.Width - previousActiveTabRect.Width) * animationProgress);

                    g.FillRectangle(SkinManager.ColorScheme.AccentBrush, x, y, width, TAB_INDICATOR_HEIGHT);
                }
                catch
                {
                    //
                }
            }
        }

        private int CalculateTextAlpha(int tabIndex, double animationProgress)
        {
            int primaryA = SkinManager.TextHighEmphasisColor.A;
            int secondaryA = SkinManager.TextMediumEmphasisColor.A;

            if (tabIndex == _baseTabControl.SelectedIndex && !_animationManager.IsAnimating())
            {
                return primaryA;
            }

            if (tabIndex != _previousSelectedTabIndex && tabIndex != _baseTabControl.SelectedIndex)
            {
                return secondaryA;
            }

            if (tabIndex == _previousSelectedTabIndex)
            {
                return primaryA - (int)((primaryA - secondaryA) * animationProgress);
            }

            return secondaryA + (int)((primaryA - secondaryA) * animationProgress);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_tabRects == null)
            {
                UpdateTabRects();
            }

            for (int i = 0; i < _tabRects.Count; i++)
            {
                if (_tabRects[i].Contains(e.Location))
                {
                    _baseTabControl.SelectedIndex = i;
                }
            }

            _animationSource = e.Location;
        }

        private void UpdateTabRects()
        {
            _tabRects = new List<Rectangle>();
            //If there isn't a base tab control, the rects shouldn't be calculated
            //If there aren't tab pages in the base tab control, the list should just be empty which has been set already; exit the void
            if (_baseTabControl == null || _baseTabControl.TabCount == 0)
            {
                return;
            }

            //Calculate the bounds of each tab header specified in the base tab control
            using Bitmap b = new(1, 1);
            using Graphics g = Graphics.FromImage(b);
            if (_baseTabControl.TabPages.Count > 0)
            {
                int TitleLenght = 0;
                foreach (System.Windows.Forms.TabPage TP in _baseTabControl.TabPages)
                {
                    TitleLenght += TAB_HEADER_PADDING * 2 + (int)g.MeasureString(TP.Text, Font).Width;
                }

                switch (HeadAlignment)
                {
                    case Alignment.Center:
                        int CenterLocation = (Width / 2) - (TitleLenght / 2);
                        _tabRects.Add(new Rectangle(CenterLocation, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(_baseTabControl.TabPages[0].Text, Font).Width, Height));
                        for (int i = 1; i < _baseTabControl.TabPages.Count; i++)
                        {
                            _tabRects.Add(new Rectangle(_tabRects[i - 1].Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(_baseTabControl.TabPages[i].Text, Font).Width, Height));
                        }

                        break;
                    case Alignment.Right:
                        _tabRects.Add(new Rectangle(Width - TitleLenght - SkinManager.FORM_PADDING, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(_baseTabControl.TabPages[0].Text, Font).Width, Height));
                        for (int i = 1; i < _baseTabControl.TabPages.Count; i++)
                        {
                            _tabRects.Add(new Rectangle(_tabRects[i - 1].Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(_baseTabControl.TabPages[i].Text, Font).Width, Height));
                        }

                        break;
                    default:
                        _tabRects.Add(new Rectangle(SkinManager.FORM_PADDING, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(_baseTabControl.TabPages[0].Text, Font).Width, Height));
                        for (int i = 1; i < _baseTabControl.TabPages.Count; i++)
                        {
                            _tabRects.Add(new Rectangle(_tabRects[i - 1].Right, 0, TAB_HEADER_PADDING * 2 + (int)g.MeasureString(_baseTabControl.TabPages[i].Text, Font).Width, Height));
                        }

                        break;
                }
            }
        }
    }

    #endregion
}