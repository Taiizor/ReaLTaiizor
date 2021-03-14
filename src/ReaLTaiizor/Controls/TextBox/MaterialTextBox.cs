﻿#region Imports

using ReaLTaiizor.Extension;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialTextBox

    public class MaterialTextBox : RichTextBox, MaterialControlI
    {
        //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialManager SkinManager => MaterialManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        [Category("Material"), DefaultValue(false)]
        public bool Password { get; set; }

        private bool _UseTallSize;

        [Category("Material"), DefaultValue(true), Description("Using a larger size enables the hint to always be visible")]
        public bool UseTallSize
        {
            get => _UseTallSize;
            set
            {
                _UseTallSize = value;
                HEIGHT = UseTallSize ? 50 : 36;
                Size = new(Size.Width, HEIGHT);
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

        private const int HINT_TEXT_SMALL_SIZE = 18;
        private const int HINT_TEXT_SMALL_Y = 4;
        private const int BOTTOM_PADDING = 3;
        private int HEIGHT = 50;
        private int LINE_Y;

        private bool hasHint;

        private readonly AnimationManager _animationManager;

        public MaterialTextBox()
        {
            // Material Properties
            Hint = "";
            Password = false;
            UseAccent = true;
            UseTallSize = true;

            // Properties
            TabStop = true;
            Multiline = false;
            BorderStyle = BorderStyle.None;

            // Animations
            _animationManager = new AnimationManager
            {
                Increment = 0.08,
                AnimationType = AnimationType.EaseInOut
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            MaterialContextMenuStrip cms = new MaterialTextBoxContextMenuStrip();
            cms.Opening += ContextMenuStripOnOpening;
            cms.OnItemClickStart += ContextMenuStripOnItemClickStart;

            ContextMenuStrip = cms;

            MaxLength = 50;
        }

        private const int EM_SETPASSWORDCHAR = 0x00cc;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.Font = SkinManager.GetFontByType(MaterialManager.FontType.Subtitle1);
            Font = SkinManager.GetFontByType(MaterialManager.FontType.Subtitle1);
            base.AutoSize = false;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            if (Password)
            {
                SendMessage(Handle, EM_SETPASSWORDCHAR, 'T', 0);
            }

            // Size and padding
            HEIGHT = UseTallSize ? 50 : 36;
            Size = new(Size.Width, HEIGHT);
            LINE_Y = HEIGHT - BOTTOM_PADDING;

            // Position the "real" text field
            Rectangle rect = new(SkinManager.FORM_PADDING, UseTallSize ? hasHint ?
                    (HINT_TEXT_SMALL_Y + HINT_TEXT_SMALL_SIZE) : // Has hint and it's tall
                    (int)(LINE_Y / 3.5) : // No hint and tall
                    Height / 5, // not tall
                    ClientSize.Width - (SkinManager.FORM_PADDING * 2), LINE_Y);
            RECT rc = new(rect);
            _ = SendMessageRefRect(Handle, EM_SETRECT, 0, ref rc);

            // events
            MouseState = MaterialMouseState.OUT;
            LostFocus += (sender, args) => _animationManager.StartNewAnimation(AnimationDirection.Out);
            GotFocus += (sender, args) =>
            {
                _animationManager.StartNewAnimation(AnimationDirection.In);
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
            HScroll += (sender, args) =>
            {
                SendMessage(Handle, EM_GETSCROLLPOS, 0, ref scrollPos);
                Invalidate();
            };
            KeyDown += (sender, args) =>
            {
                SendMessage(Handle, EM_GETSCROLLPOS, 0, ref scrollPos);
                Invalidate();
            };
            TextChanged += (sender, args) =>
            {
                SendMessage(Handle, EM_GETSCROLLPOS, 0, ref scrollPos);
                Invalidate();
            };
        }

        private Point scrollPos = Point.Empty;
        private const int EM_GETSCROLLPOS = WM_USER + 221;
        private const int WM_USER = 0x400;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Point lParam);

        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size(proposedSize.Width, HEIGHT);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;

            g.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SystemColors.Control : Parent.Parent.BackColor) : Parent.BackColor);

            SolidBrush backBrush = new(BlendColor(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SystemColors.Control : Parent.Parent.BackColor) : Parent.BackColor, SkinManager.BackgroundAlternativeColor, SkinManager.BackgroundAlternativeColor.A));

            g.FillRectangle(
                !Enabled ? SkinManager.BackgroundDisabledBrush : // Disabled
                Focused ? SkinManager.BackgroundFocusBrush :  // Focused
                MouseState == MaterialMouseState.HOVER ? SkinManager.BackgroundHoverBrush : // Hover
                backBrush, // Normal
                ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, LINE_Y);

            // HintText
            bool userTextPresent = !string.IsNullOrEmpty(Text);
            Color textColor = Enabled ? Focused ?
                            UseAccent ? SkinManager.ColorScheme.AccentColor : SkinManager.ColorScheme.PrimaryColor : // Focused
                            SkinManager.TextHighEmphasisColor : // Inactive
                            SkinManager.TextDisabledOrHintColor; // Disabled
            Rectangle hintRect = new(SkinManager.FORM_PADDING, ClientRectangle.Y, Width, LINE_Y);
            int hintTextSize = 16;

            // bottom line base
            g.FillRectangle(SkinManager.DividersAlternativeBrush, 0, LINE_Y, Width, 1);

            if (!_animationManager.IsAnimating())
            {
                // No animation
                if (hasHint && UseTallSize && (Focused || userTextPresent))
                {
                    // hint text
                    hintRect = new(SkinManager.FORM_PADDING, HINT_TEXT_SMALL_Y, Width, HINT_TEXT_SMALL_SIZE);
                    hintTextSize = 12;
                }

                // bottom line
                if (Focused)
                {
                    g.FillRectangle(UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush, 0, LINE_Y, Width, 2);
                }
            }
            else
            {
                // Animate - Focus got/lost
                double animationProgress = _animationManager.GetProgress();

                // hint Animation
                if (hasHint && UseTallSize)
                {
                    hintRect = new(
                        SkinManager.FORM_PADDING,
                        userTextPresent ? (HINT_TEXT_SMALL_Y) : ClientRectangle.Y + (int)((HINT_TEXT_SMALL_Y - ClientRectangle.Y) * animationProgress),
                        Width,
                        userTextPresent ? (HINT_TEXT_SMALL_SIZE) : (int)(LINE_Y + (HINT_TEXT_SMALL_SIZE - LINE_Y) * animationProgress));
                    hintTextSize = userTextPresent ? 12 : (int)(16 + (12 - 16) * animationProgress);
                }

                // Line Animation
                int LineAnimationWidth = (int)(Width * animationProgress);
                int LineAnimationX = (Width / 2) - (LineAnimationWidth / 2);
                g.FillRectangle(UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush, LineAnimationX, LINE_Y, LineAnimationWidth, 2);
            }

            // Text stuff:
            string textToDisplay = Password ? Text.ToSecureString() : Text;
            string textSelected;
            Rectangle textSelectRect;

            // Calc text Rect
            Rectangle textRect = new(
                SkinManager.FORM_PADDING,
                hasHint && UseTallSize ? (hintRect.Y + hintRect.Height) - 2 : ClientRectangle.Y,
                ClientRectangle.Width - SkinManager.FORM_PADDING * 2 + scrollPos.X,
                hasHint && UseTallSize ? LINE_Y - (hintRect.Y + hintRect.Height) : LINE_Y);

            g.Clip = new(textRect);
            textRect.X -= scrollPos.X;

            using (MaterialNativeTextRenderer NativeText = new(g))
            {
                // Selection rects calc
                string textBeforeSelection = textToDisplay.Substring(0, SelectionStart);
                textSelected = textToDisplay.Substring(SelectionStart, SelectionLength);

                int selectX = NativeText.MeasureLogString(textBeforeSelection, SkinManager.GetLogFontByType(MaterialManager.FontType.Subtitle1)).Width;
                int selectWidth = NativeText.MeasureLogString(textSelected, SkinManager.GetLogFontByType(MaterialManager.FontType.Subtitle1)).Width;

                textSelectRect = new(
                    textRect.X + selectX, UseTallSize ? hasHint ?
                     textRect.Y + BOTTOM_PADDING : // tall and hint
                     LINE_Y / 3 - BOTTOM_PADDING : // tall and no hint
                     BOTTOM_PADDING, // not tall
                    selectWidth,
                    UseTallSize ? hasHint ?
                    textRect.Height - BOTTOM_PADDING * 2 : // tall and hint
                    (int)(LINE_Y / 2) : // tall and no hint
                    LINE_Y - BOTTOM_PADDING * 2); // not tall

                // Draw user text
                NativeText.DrawTransparentText(
                    textToDisplay,
                    SkinManager.GetLogFontByType(MaterialManager.FontType.Subtitle1),
                    Enabled ? SkinManager.TextHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                    textRect.Location,
                    textRect.Size,
                    MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }

            if (Focused)
            {
                // Draw Selection Rectangle
                g.FillRectangle(UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.DarkPrimaryBrush, textSelectRect);

                // Draw Selected Text
                using MaterialNativeTextRenderer NativeText = new(g);
                NativeText.DrawTransparentText(
                    textSelected,
                    SkinManager.GetLogFontByType(MaterialManager.FontType.Subtitle1),
                    SkinManager.ColorScheme.TextColor,
                    textSelectRect.Location,
                    textSelectRect.Size,
                    MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }

            g.Clip = new(ClientRectangle);

            // Draw hint text
            if (hasHint && (UseTallSize || string.IsNullOrEmpty(Text)))
            {
                using MaterialNativeTextRenderer NativeText = new(g);
                NativeText.DrawTransparentText(
                Hint,
                SkinManager.GetTextBoxFontBySize(hintTextSize),
                Enabled ? Focused ? UseAccent ?
                SkinManager.ColorScheme.AccentColor : // Focus Accent
                SkinManager.ColorScheme.PrimaryColor : // Focus Primary
                SkinManager.TextMediumEmphasisColor : // not focused
                SkinManager.TextDisabledOrHintColor, // Disabled
                hintRect.Location,
                hintRect.Size,
                MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(Width, HEIGHT);
            LINE_Y = HEIGHT - BOTTOM_PADDING;
        }

        private void ContextMenuStripOnItemClickStart(object sender, ToolStripItemClickedEventArgs toolStripItemClickedEventArgs)
        {
            switch (toolStripItemClickedEventArgs.ClickedItem.Text)
            {
                case "Cut":
                    Cut();
                    break;
                case "Copy":
                    Copy();
                    break;
                case "Paste":
                    Paste();
                    break;
                case "Delete":
                    SelectedText = string.Empty;
                    break;
                case "Select All":
                    SelectAll();
                    break;
            }
        }

        private void ContextMenuStripOnOpening(object sender, CancelEventArgs cancelEventArgs)
        {
            if (sender is MaterialTextBoxContextMenuStrip strip)
            {
                strip.Cut.Enabled = !string.IsNullOrEmpty(SelectedText);
                strip.Copy.Enabled = !string.IsNullOrEmpty(SelectedText);
                strip.Paste.Enabled = Clipboard.ContainsText();
                strip.Delete.Enabled = !string.IsNullOrEmpty(SelectedText);
                strip.SelectAll.Enabled = !string.IsNullOrEmpty(Text);
            }
        }

        // Cursor flickering fix
        private const int WM_SETCURSOR = 0x0020;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SETCURSOR)
            {
                Cursor.Current = Cursor;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        // Padding
        private const int EM_SETRECT = 0xB3;

        [DllImport(@"User32.dll", EntryPoint = @"SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessageRefRect(IntPtr hWnd, uint msg, int wParam, ref RECT rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            private RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }
        }
    }

    public class MaterialTextBoxContextMenuStrip : MaterialContextMenuStrip
    {
        public readonly ToolStripItem SelectAll = new MaterialToolStripMenuItem { Text = "Select All" };
        public readonly ToolStripItem Separator2 = new ToolStripSeparator();
        public readonly ToolStripItem Paste = new MaterialToolStripMenuItem { Text = "Paste" };
        public readonly ToolStripItem Copy = new MaterialToolStripMenuItem { Text = "Copy" };
        public readonly ToolStripItem Cut = new MaterialToolStripMenuItem { Text = "Cut" };
        public readonly ToolStripItem Delete = new MaterialToolStripMenuItem { Text = "Delete" };

        public MaterialTextBoxContextMenuStrip()
        {
            Items.AddRange
            (
                new[]
                {
                    Cut,
                    Copy,
                    Paste,
                    Delete,
                    Separator2,
                    SelectAll,
                }
            );
        }
    }

    #endregion
}