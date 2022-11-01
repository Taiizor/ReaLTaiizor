#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialComboBox

    public class MaterialComboBox : ComboBox, MaterialControlI
    {
        // For some reason, even when overriding the AutoSize property, it doesn't appear on the properties panel, so we have to create a new one.
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Layout")]
        private bool _AutoResize;

        public bool AutoResize
        {
            get => _AutoResize;
            set
            {
                _AutoResize = value;
                recalculateAutoSize();
            }
        }

        //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        private bool _UseTallSize;

        [Category("Material"), DefaultValue(true), Description("Using a larger size enables the hint to always be visible")]
        public bool UseTallSize
        {
            get => _UseTallSize;
            set
            {
                _UseTallSize = value;
                setHeightVars();
                Invalidate();
            }
        }

        [Category("Material"), DefaultValue(true)]
        public bool UseAccent { get; set; }

        private string _hint = string.Empty;

        [Category("Material"), DefaultValue(""), Localizable(true)]
        public string Hint
        {
            get => _hint;
            set
            {
                _hint = value;
                hasHint = !string.IsNullOrEmpty(Hint);
                Invalidate();
            }
        }

        private int _startIndex;
        public int StartIndex
        {
            get => _startIndex;
            set
            {
                _startIndex = value;
                try
                {
                    if (base.Items.Count > 0)
                    {
                        base.SelectedIndex = value;
                    }
                }
                catch
                {
                }
                Invalidate();
            }
        }

        private const int TEXT_SMALL_SIZE = 18;
        private const int TEXT_SMALL_Y = 4;
        private const int BOTTOM_PADDING = 3;
        private int HEIGHT = 50;
        private int LINE_Y;

        private bool hasHint;

        private readonly AnimationManager _animationManager;

        public MaterialComboBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            // Material Properties
            Hint = "";
            UseAccent = true;
            UseTallSize = true;
            MaxDropDownItems = 4;

            Font = SkinManager.GetFontByType(MaterialSkinManager.FontType.Subtitle2);
            BackColor = SkinManager.BackgroundColor;
            ForeColor = SkinManager.TextHighEmphasisColor;
            DrawMode = DrawMode.OwnerDrawVariable;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DropDownWidth = Width;

            // Animations
            _animationManager = new AnimationManager(true)
            {
                Increment = 0.08,
                AnimationType = AnimationType.EaseInOut
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();
            _animationManager.OnAnimationFinished += sender => _animationManager.SetProgress(0);
            DropDownClosed += (sender, args) =>
            {
                MouseState = MaterialMouseState.OUT;
                if (SelectedIndex < 0 && !Focused)
                {
                    _animationManager.StartNewAnimation(AnimationDirection.Out);
                }
            };
            LostFocus += (sender, args) =>
            {
                MouseState = MaterialMouseState.OUT;
                if (SelectedIndex < 0)
                {
                    _animationManager.StartNewAnimation(AnimationDirection.Out);
                }
            };
            DropDown += (sender, args) =>
            {
                _animationManager.StartNewAnimation(AnimationDirection.In);
            };
            GotFocus += (sender, args) =>
            {
                _animationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseEnter += (sender, args) =>
            {
                MouseState = MaterialMouseState.HOVER;
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MaterialMouseState.OUT;
                Invalidate();
            };
            SelectedIndexChanged += (sender, args) =>
            {
                Invalidate();
            };
            KeyUp += (sender, args) =>
            {
                if (Enabled && DropDownStyle == ComboBoxStyle.DropDownList && (args.KeyCode == Keys.Delete || args.KeyCode == Keys.Back))
                {
                    SelectedIndex = -1;
                    Invalidate();
                }
            };
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            g.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor);
            g.FillRectangle(Enabled ? Focused ?
                SkinManager.BackgroundFocusBrush : // Focused
                MouseState == MaterialMouseState.HOVER ?
                SkinManager.BackgroundHoverBrush : // Hover
                SkinManager.BackgroundAlternativeBrush : // normal
                SkinManager.BackgroundDisabledBrush // Disabled
                , ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, LINE_Y);

            //Set color and brush
            Color SelectedColor = new();
            if (UseAccent)
            {
                SelectedColor = SkinManager.ColorScheme.AccentColor;
            }
            else
            {
                SelectedColor = SkinManager.ColorScheme.PrimaryColor;
            }

            SolidBrush SelectedBrush = new(SelectedColor);

            // Create and Draw the arrow
            GraphicsPath pth = new();
            PointF TopRight = new(this.Width - 0.5f - SkinManager.FORM_PADDING, (this.Height >> 1) - 2.5f);
            PointF MidBottom = new(this.Width - 4.5f - SkinManager.FORM_PADDING, (this.Height >> 1) + 2.5f);
            PointF TopLeft = new(this.Width - 8.5f - SkinManager.FORM_PADDING, (this.Height >> 1) - 2.5f);
            pth.AddLine(TopLeft, TopRight);
            pth.AddLine(TopRight, MidBottom);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillPath((SolidBrush)(Enabled ? DroppedDown || Focused ?
                SelectedBrush : //DroppedDown or Focused
                SkinManager.TextHighEmphasisBrush : //Not DroppedDown and not Focused
                new SolidBrush(BlendColor(SkinManager.TextHighEmphasisColor, SkinManager.SwitchOffDisabledThumbColor, 197))  //Disabled
                ), pth);
            g.SmoothingMode = SmoothingMode.None;

            // HintText
            bool userTextPresent = SelectedIndex >= 0;
            Rectangle hintRect = new(SkinManager.FORM_PADDING, ClientRectangle.Y, Width, LINE_Y);
            int hintTextSize = 16;

            // bottom line base
            g.FillRectangle(SkinManager.DividersAlternativeBrush, 0, LINE_Y, Width, 1);

            if (!_animationManager.IsAnimating())
            {
                // No animation
                if (hasHint && UseTallSize && (DroppedDown || Focused || SelectedIndex >= 0))
                {
                    // hint text
                    hintRect = new Rectangle(SkinManager.FORM_PADDING, TEXT_SMALL_Y, Width, TEXT_SMALL_SIZE);
                    hintTextSize = 12;
                }

                // bottom line
                if (DroppedDown || Focused)
                {
                    g.FillRectangle(SelectedBrush, 0, LINE_Y, Width, 2);
                }
            }
            else
            {
                // Animate - Focus got/lost
                double animationProgress = _animationManager.GetProgress();

                // hint Animation
                if (hasHint && UseTallSize)
                {
                    hintRect = new Rectangle(
                        SkinManager.FORM_PADDING,
                        userTextPresent && !_animationManager.IsAnimating() ? TEXT_SMALL_Y : ClientRectangle.Y + (int)((TEXT_SMALL_Y - ClientRectangle.Y) * animationProgress),
                        Width,
                        userTextPresent && !_animationManager.IsAnimating() ? TEXT_SMALL_SIZE : (int)(LINE_Y + ((TEXT_SMALL_SIZE - LINE_Y) * animationProgress)));
                    hintTextSize = userTextPresent && !_animationManager.IsAnimating() ? 12 : (int)(16 + ((12 - 16) * animationProgress));
                }

                // Line Animation
                int LineAnimationWidth = (int)(Width * animationProgress);
                int LineAnimationX = (Width / 2) - (LineAnimationWidth / 2);
                g.FillRectangle(SelectedBrush, LineAnimationX, LINE_Y, LineAnimationWidth, 2);
            }

            // Calc text Rect
            Rectangle textRect = new(
                SkinManager.FORM_PADDING,
                hasHint && UseTallSize ? hintRect.Y + hintRect.Height - 2 : ClientRectangle.Y,
                ClientRectangle.Width - (SkinManager.FORM_PADDING * 3) - 8,
                hasHint && UseTallSize ? LINE_Y - (hintRect.Y + hintRect.Height) : LINE_Y);

            g.Clip = new Region(textRect);

            using (MaterialNativeTextRenderer NativeText = new(g))
            {
                // Draw user text
                NativeText.DrawTransparentText(
                    Text,
                    SkinManager.GetLogFontByType(MaterialSkinManager.FontType.Subtitle1),
                    Enabled ? SkinManager.TextHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                    textRect.Location,
                    textRect.Size,
                    MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }

            g.ResetClip();

            // Draw hint text
            if (hasHint && (UseTallSize || string.IsNullOrEmpty(Text)))
            {
                using MaterialNativeTextRenderer NativeText = new(g);
                NativeText.DrawTransparentText(
                Hint,
                SkinManager.GetTextBoxFontBySize(hintTextSize),
                Enabled ? DroppedDown || Focused ?
                SelectedColor : // Focus 
                SkinManager.TextMediumEmphasisColor : // not focused
                SkinManager.TextDisabledOrHintColor, // Disabled
                hintRect.Location,
                hintRect.Size,
                MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }
        }

        private void CustomMeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = HEIGHT - 7;
        }

        private void CustomDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index > Items.Count || !Focused)
            {
                return;
            }

            Graphics g = e.Graphics;

            // Draw the background of the item.
            g.FillRectangle(SkinManager.BackgroundBrush, e.Bounds);

            // Hover
            if (e.State.HasFlag(DrawItemState.Focus)) // Focus == hover
            {
                g.FillRectangle(SkinManager.BackgroundHoverBrush, e.Bounds);
            }

            string Text = "";
            if (!string.IsNullOrWhiteSpace(DisplayMember))
            {
                if (!Items[e.Index].GetType().Equals(typeof(DataRowView)))
                {
                    object item = Items[e.Index].GetType().GetProperty(DisplayMember).GetValue(Items[e.Index]);
                    Text = item.ToString();
                }
                else
                {
                    DataTable table = ((DataRow)Items[e.Index].GetType().GetProperty("Row").GetValue(Items[e.Index])).Table;
                    Text = table.Rows[e.Index][DisplayMember].ToString();
                }
            }
            else
            {
                Text = Items[e.Index].ToString();
            }

            using MaterialNativeTextRenderer NativeText = new(g);
            NativeText.DrawTransparentText(
            Text,
            SkinManager.GetFontByType(MaterialSkinManager.FontType.Subtitle1),
            SkinManager.TextHighEmphasisNoAlphaColor,
            new Point(e.Bounds.Location.X + SkinManager.FORM_PADDING, e.Bounds.Location.Y),
            new Size(e.Bounds.Size.Width - (SkinManager.FORM_PADDING * 2), e.Bounds.Size.Height),
            MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle); ;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            MouseState = MaterialMouseState.OUT;
            MeasureItem += CustomMeasureItem;
            DrawItem += CustomDrawItem;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawVariable;
            recalculateAutoSize();
            setHeightVars();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            recalculateAutoSize();
            setHeightVars();
        }

        private void setHeightVars()
        {
            HEIGHT = UseTallSize ? 50 : 36;
            Size = new Size(Size.Width, HEIGHT);
            LINE_Y = HEIGHT - BOTTOM_PADDING;
            ItemHeight = HEIGHT - 7;
            DropDownHeight = (ItemHeight * MaxDropDownItems) + 2;
        }

        public void recalculateAutoSize()
        {
            if (!AutoResize)
            {
                return;
            }

            int w = DropDownWidth;
            int padding = SkinManager.FORM_PADDING * 3;
            int vertScrollBarWidth = (Items.Count > MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;

            Graphics g = CreateGraphics();
            using (MaterialNativeTextRenderer NativeText = new(g))
            {
                System.Collections.Generic.IEnumerable<string> itemsList = this.Items.Cast<object>().Select(item => item.ToString());
                foreach (string s in itemsList)
                {
                    int newWidth = NativeText.MeasureLogString(s, SkinManager.GetLogFontByType(MaterialSkinManager.FontType.Subtitle1)).Width + vertScrollBarWidth + padding;
                    if (w < newWidth)
                    {
                        w = newWidth;
                    }
                }
            }

            if (Width != w)
            {
                DropDownWidth = w;
                Width = w;
            }
        }
    }

    #endregion
}