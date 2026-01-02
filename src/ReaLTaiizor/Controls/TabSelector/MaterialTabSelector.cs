#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialTabSelector

    public class MaterialTabSelector : Control, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        public enum Alignment
        {
            Left,
            Center,
            Right
        }

        [Category("Material"), Browsable(true)]
        public Alignment HeadAlignment
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Alignment.Left;

        [Category("Behavior")]
        public string[] SelectorHideTabName
        {
            get;
            set
            {
                field = value;

                if (BaseTabControl != null && field.Any())
                {
                    foreach (System.Windows.Forms.TabPage TB in BaseTabControl.TabPages)
                    {
                        if (field.Contains(TB.Name))
                        {
                            BaseTabControl.TabPages.Remove(TB);
                        }
                    }

                    Refresh();
                    Invalidate();
                }
            }
        } = new List<string>().ToArray();

        [Category("Behavior")]
        public System.Windows.Forms.TabPage[] SelectorNonClickTabPage { get; set; } = new List<System.Windows.Forms.TabPage>().ToArray();

        //[Browsable(false)]
        public enum CustomCharacterCasing
        {
            [Description("Text will be used as user inserted, no alteration")]
            Normal,
            [Description("Text will be converted to UPPER case")]
            Upper,
            [Description("Text will be converted to lower case")]
            Lower,
            [Description("Text will be converted to Proper case (aka Title case)")]
            Proper
        }

        TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

        [Category("Material"), Browsable(true)]
        public MaterialTabControl BaseTabControl
        {
            get;
            set
            {
                field = value;
                if (field == null)
                {
                    return;
                }

                UpdateTabRects();

                _previousSelectedTabIndex = field.SelectedIndex;
                field.Deselected += (sender, args) =>
                {
                    _previousSelectedTabIndex = field.SelectedIndex;
                };
                field.SelectedIndexChanged += (sender, args) =>
                {
                    _animationManager.SetProgress(0);
                    _animationManager.StartNewAnimation(AnimationDirection.In);
                };
                field.ControlAdded += delegate
                {
                    Invalidate();
                };
                field.ControlRemoved += delegate
                {
                    Invalidate();
                };
            }
        }

        private int _previousSelectedTabIndex;

        private Point _animationSource;

        private readonly AnimationManager _animationManager;

        private List<Rectangle> _tabRects;

        private const int ICON_SIZE = 24;
        private const int FIRST_TAB_PADDING = 50;
        private const int TAB_HEADER_PADDING = 24;
        private const int TAB_WIDTH_MIN = 160;
        private const int TAB_WIDTH_MAX = 264;

        private int _tab_over_index = -1;

        [Category("Appearance")]
        public CustomCharacterCasing CharacterCasing
        {
            get;
            set
            {
                field = value;
                BaseTabControl.Invalidate();
                Invalidate();
            }
        }

        [Category("Material"), Browsable(true), DisplayName("Tab Indicator Height"), DefaultValue(2)]
        public int TabIndicatorHeight
        {
            get;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Tab Indicator Height", value, "Value should be > 0");
                }
                else
                {
                    field = value;
                    Refresh();
                }
            }
        }

        public enum TabLabelStyle
        {
            Text,
            Icon,
            IconAndText,
        }

        [Category("Material"), Browsable(true), DisplayName("Tab Label"), DefaultValue(TabLabelStyle.Text)]
        public TabLabelStyle TabLabel
        {
            get;
            set
            {
                field = value;
                if (field == TabLabelStyle.IconAndText)
                {
                    Height = 72;
                }
                else
                {
                    Height = 48;
                }

                UpdateTabRects();
                Invalidate();
            }
        }


        public MaterialTabSelector()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            TabIndicatorHeight = 2;
            TabLabel = TabLabelStyle.Text;

            Size = new Size(480, 48);

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
            Font = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(SkinManager.ColorScheme.PrimaryColor);

            if (BaseTabControl == null || BaseTabControl.TabCount == 0)
            {
                return;
            }

            if (!_animationManager.IsAnimating() || _tabRects == null || _tabRects.Count != BaseTabControl.TabCount)
            {
                UpdateTabRects();
            }

            double animationProgress = _animationManager.GetProgress();

            //Click feedback
            if (_animationManager.IsAnimating())
            {
                SolidBrush rippleBrush = new(Color.FromArgb((int)(51 - (animationProgress * 50)), Color.White));
                int rippleSize = (int)(animationProgress * _tabRects[BaseTabControl.SelectedIndex].Width * 1.75);

                g.SetClip(_tabRects[BaseTabControl.SelectedIndex]);
                g.FillEllipse(rippleBrush, new Rectangle(_animationSource.X - (rippleSize / 2), _animationSource.Y - (rippleSize / 2), rippleSize, rippleSize));
                g.ResetClip();
                rippleBrush.Dispose();
            }

            //Draw tab headers
            if (_tab_over_index >= 0)
            {
                //Change mouse over tab background color
                g.FillRectangle(SkinManager.BackgroundHoverBrush, _tabRects[_tab_over_index].X, _tabRects[_tab_over_index].Y, _tabRects[_tab_over_index].Width, _tabRects[_tab_over_index].Height - TabIndicatorHeight);
            }

            foreach (System.Windows.Forms.TabPage tabPage in BaseTabControl.TabPages)
            {
                int currentTabIndex = BaseTabControl.TabPages.IndexOf(tabPage);

                if (TabLabel != TabLabelStyle.Icon)
                {
                    // Text
                    using MaterialNativeTextRenderer NativeText = new(g);
                    Size textSize = TextRenderer.MeasureText(BaseTabControl.TabPages[currentTabIndex].Text, Font);
                    Rectangle textLocation = new(_tabRects[currentTabIndex].X + (TAB_HEADER_PADDING / 2), _tabRects[currentTabIndex].Y, _tabRects[currentTabIndex].Width - TAB_HEADER_PADDING, _tabRects[currentTabIndex].Height);

                    if (TabLabel == TabLabelStyle.IconAndText)
                    {
                        textLocation.Y = 46;
                        textLocation.Height = 10;
                    }

                    if ((TAB_HEADER_PADDING * 2) + textSize.Width < TAB_WIDTH_MAX)
                    {
                        NativeText.DrawTransparentText(
                        CharacterCasing == CustomCharacterCasing.Upper ? tabPage.Text.ToUpper() :
                        CharacterCasing == CustomCharacterCasing.Lower ? tabPage.Text.ToLower() :
                        CharacterCasing == CustomCharacterCasing.Proper ? textInfo.ToTitleCase(tabPage.Text.ToLower()) : tabPage.Text,
                        Font,
                        Color.FromArgb(CalculateTextAlpha(currentTabIndex, animationProgress), SkinManager.ColorScheme.TextColor),
                        textLocation.Location,
                        textLocation.Size,
                        MaterialNativeTextRenderer.TextAlignFlags.Center | MaterialNativeTextRenderer.TextAlignFlags.Middle);
                    }
                    else
                    {
                        if (TabLabel == TabLabelStyle.IconAndText)
                        {
                            textLocation.Y = 40;
                            textLocation.Height = 26;
                        }
                        NativeText.DrawMultilineTransparentText(
                        CharacterCasing == CustomCharacterCasing.Upper ? tabPage.Text.ToUpper() :
                        CharacterCasing == CustomCharacterCasing.Lower ? tabPage.Text.ToLower() :
                        CharacterCasing == CustomCharacterCasing.Proper ? textInfo.ToTitleCase(tabPage.Text.ToLower()) : tabPage.Text,
                        SkinManager.GetFontByType(MaterialSkinManager.FontType.Body2),
                        Color.FromArgb(CalculateTextAlpha(currentTabIndex, animationProgress), SkinManager.ColorScheme.TextColor),
                        textLocation.Location,
                        textLocation.Size,
                        MaterialNativeTextRenderer.TextAlignFlags.Center | MaterialNativeTextRenderer.TextAlignFlags.Middle);
                    }
                }

                if (TabLabel != TabLabelStyle.Text)
                {
                    // Icons
                    if (BaseTabControl.ImageList != null && (!string.IsNullOrEmpty(tabPage.ImageKey) | tabPage.ImageIndex > -1))
                    {
                        Rectangle iconRect = new(
                            _tabRects[currentTabIndex].X + (_tabRects[currentTabIndex].Width / 2) - (ICON_SIZE / 2),
                            _tabRects[currentTabIndex].Y + (_tabRects[currentTabIndex].Height / 2) - (ICON_SIZE / 2),
                            ICON_SIZE, ICON_SIZE);
                        if (TabLabel == TabLabelStyle.IconAndText)
                        {
                            iconRect.Y = 12;
                        }
                        g.DrawImage(!string.IsNullOrEmpty(tabPage.ImageKey) ? BaseTabControl.ImageList.Images[tabPage.ImageKey] : BaseTabControl.ImageList.Images[tabPage.ImageIndex], iconRect);
                    }
                }
            }

            //Animate tab indicator
            int previousSelectedTabIndexIfHasOne = _previousSelectedTabIndex == -1 ? BaseTabControl.SelectedIndex : _previousSelectedTabIndex;
            Rectangle previousActiveTabRect = _tabRects[previousSelectedTabIndexIfHasOne];
            Rectangle activeTabPageRect = _tabRects[BaseTabControl.SelectedIndex];

            int y = activeTabPageRect.Bottom - TabIndicatorHeight;
            int x = previousActiveTabRect.X + (int)((activeTabPageRect.X - previousActiveTabRect.X) * animationProgress);
            int width = previousActiveTabRect.Width + (int)((activeTabPageRect.Width - previousActiveTabRect.Width) * animationProgress);

            g.FillRectangle(SkinManager.ColorScheme.AccentBrush, x, y, width, TabIndicatorHeight);
        }

        private int CalculateTextAlpha(int tabIndex, double animationProgress)
        {
            int primaryA = SkinManager.TextHighEmphasisColor.A;
            int secondaryA = SkinManager.TextMediumEmphasisColor.A;

            if (tabIndex == BaseTabControl.SelectedIndex && !_animationManager.IsAnimating())
            {
                return primaryA;
            }
            if (tabIndex != _previousSelectedTabIndex && tabIndex != BaseTabControl.SelectedIndex)
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
                    if (SelectorNonClickTabPage == null || !SelectorNonClickTabPage.Contains(BaseTabControl.TabPages[i]))
                    {
                        BaseTabControl.SelectedIndex = i;
                    }
                }
            }

            _animationSource = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode)
            {
                return;
            }

            if (_tabRects == null)
            {
                UpdateTabRects();
            }

            int old_tab_over_index = _tab_over_index;
            _tab_over_index = -1;
            for (int i = 0; i < _tabRects.Count; i++)
            {
                if (_tabRects[i].Contains(e.Location))
                {
                    if (SelectorNonClickTabPage == null || !SelectorNonClickTabPage.Contains(BaseTabControl.TabPages[i]))
                    {
                        Cursor = Cursors.Hand;
                        _tab_over_index = i;
                        break;
                    }
                    else
                    {
                        Cursor = Cursors.No;
                        return;
                    }
                }
            }
            if (_tab_over_index == -1)
            {
                Cursor = Cursors.Arrow;
            }

            if (old_tab_over_index != _tab_over_index)
            {
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (DesignMode)
            {
                return;
            }

            if (_tabRects == null)
            {
                UpdateTabRects();
            }

            Cursor = Cursors.Arrow;
            _tab_over_index = -1;
            Invalidate();
        }

        private void UpdateTabRects()
        {
            _tabRects = [];

            //If there isn't a base tab control, the rects shouldn't be calculated
            //If there aren't tab pages in the base tab control, the list should just be empty which has been set already; exit the void
            if (BaseTabControl == null || BaseTabControl.TabCount == 0)
            {
                return;
            }

            //Calculate the bounds of each tab header specified in the base tab control
            using Bitmap b = new(1, 1);
            using Graphics g = Graphics.FromImage(b);
            using Graphics gs = Graphics.FromImage(b);
            using MaterialNativeTextRenderer NativeText = new(g);

            int TitleLenght = 0;

            if (HeadAlignment != Alignment.Left)
            {
                foreach (System.Windows.Forms.TabPage TP in BaseTabControl.TabPages)
                {
                    TitleLenght += (TAB_HEADER_PADDING * 2) + (int)gs.MeasureString(TP.Text, Font).Width;
                }
            }

            switch (HeadAlignment)
            {
                case Alignment.Center:
                    int CenterLocation = (Width / 2) - (TitleLenght / 2);
                    _tabRects.Add(new Rectangle(CenterLocation, 0, (TAB_HEADER_PADDING * 2) + (int)gs.MeasureString(BaseTabControl.TabPages[0].Text, Font).Width, Height));
                    for (int i = 1; i < BaseTabControl.TabPages.Count; i++)
                    {
                        _tabRects.Add(new Rectangle(_tabRects[i - 1].Right, 0, (TAB_HEADER_PADDING * 2) + (int)gs.MeasureString(BaseTabControl.TabPages[i].Text, Font).Width, Height));
                    }

                    break;
                case Alignment.Right:
                    _tabRects.Add(new Rectangle(Width - TitleLenght - SkinManager.FORM_PADDING, 0, (TAB_HEADER_PADDING * 2) + (int)gs.MeasureString(BaseTabControl.TabPages[0].Text, Font).Width, Height));
                    for (int i = 1; i < BaseTabControl.TabPages.Count; i++)
                    {
                        _tabRects.Add(new Rectangle(_tabRects[i - 1].Right, 0, (TAB_HEADER_PADDING * 2) + (int)gs.MeasureString(BaseTabControl.TabPages[i].Text, Font).Width, Height));
                    }

                    break;
                default:
                    for (int i = 0; i < BaseTabControl.TabPages.Count; i++)
                    {
                        Size textSize = TextRenderer.MeasureText(BaseTabControl.TabPages[i].Text, Font);
                        if (TabLabel == TabLabelStyle.Icon)
                        {
                            textSize.Width = ICON_SIZE;
                        }

                        int TabWidth = (TAB_HEADER_PADDING * 2) + textSize.Width;
                        if (TabWidth > TAB_WIDTH_MAX)
                        {
                            TabWidth = TAB_WIDTH_MAX;
                        }
                        else if (TabWidth < TAB_WIDTH_MIN)
                        {
                            TabWidth = TAB_WIDTH_MIN;
                        }

                        if (i == 0)
                        {
                            _tabRects.Add(new Rectangle(FIRST_TAB_PADDING - TAB_HEADER_PADDING, 0, TabWidth, Height));
                        }
                        else
                        {
                            _tabRects.Add(new Rectangle(_tabRects[i - 1].Right, 0, TabWidth, Height));
                        }
                    }

                    break;
            }
        }
    }

    #endregion
}