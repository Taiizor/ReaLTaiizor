#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Diagnostics;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AloneTextBox

    [DefaultEvent("TextChanged")]
    public class AloneTextBox : Control
    {
        public enum MouseState : byte
        {
            None,
            Over,
            Down
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never), AccessedThroughProperty("TB"), CompilerGenerated]
        private System.Windows.Forms.TextBox _TB;

        private Graphics G;

        private AloneTextBox.MouseState State;

        private bool IsDown;

        private bool _EnabledCalc;

        private bool _allowpassword;

        private int _maxChars;

        private HorizontalAlignment _textAlignment;

        private bool _multiLine;

        private bool _readOnly;

        public virtual System.Windows.Forms.TextBox TB
        {
            [CompilerGenerated]
            get
            {
                return _TB;
            }
            [CompilerGenerated]
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler value2 = delegate (object a0, EventArgs a1)
                {
                    TextChangeTb();
                };
                System.Windows.Forms.TextBox tB = _TB;
                if (tB != null)
                    tB.TextChanged -= value2;
                _TB = value;
                tB = _TB;
                if (tB != null)
                    tB.TextChanged += value2;
            }
        }

        public new bool Enabled
        {
            get
            {
                return EnabledCalc;
            }
            set
            {
                TB.Enabled = value;
                _EnabledCalc = value;
                base.Invalidate();
            }
        }

        [DisplayName("Enabled")]
        public bool EnabledCalc
        {
            get
            {
                return _EnabledCalc;
            }
            set
            {
                Enabled = value;
                base.Invalidate();
            }
        }

        public bool UseSystemPasswordChar
        {
            get
            {
                return _allowpassword;
            }
            set
            {
                TB.UseSystemPasswordChar = UseSystemPasswordChar;
                _allowpassword = value;
                base.Invalidate();
            }
        }

        public int MaxLength
        {
            get
            {
                return _maxChars;
            }
            set
            {
                _maxChars = value;
                TB.MaxLength = MaxLength;
                base.Invalidate();
            }
        }

        public HorizontalAlignment TextAlign
        {
            get
            {
                return _textAlignment;
            }
            set
            {
                _textAlignment = value;
                base.Invalidate();
            }
        }

        public bool MultiLine
        {
            get
            {
                return _multiLine;
            }
            set
            {
                _multiLine = value;
                TB.Multiline = value;
                OnResize(EventArgs.Empty);
                base.Invalidate();
            }
        }

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                bool flag = TB != null;
                if (flag)
                    TB.ReadOnly = value;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            base.Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            base.Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            TB.ForeColor = ForeColor;
            base.Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            TB.Font = Font;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            TB.Focus();
        }

        private void TextChangeTb()
        {
            Text = TB.Text;
        }

        private void TextChng()
        {
            TB.Text = Text;
        }

        public void NewTextBox()
        {
            System.Windows.Forms.TextBox tB = TB;
            tB.Text = string.Empty;
            tB.BackColor = BackColor;
            tB.ForeColor = ForeColor;
            tB.TextAlign = HorizontalAlignment.Left;
            tB.BorderStyle = BorderStyle.None;
            tB.Location = new Point(3, 3);
            tB.Font = Font;
            tB.Size = checked(new Size(base.Width - 3, base.Height - 3));
            tB.UseSystemPasswordChar = UseSystemPasswordChar;
        }

        public AloneTextBox()
        {
            base.TextChanged += delegate (object a0, EventArgs a1)
            {
                TextChng();
            };
            TB = new System.Windows.Forms.TextBox();
            _allowpassword = false;
            _maxChars = 32767;
            _multiLine = false;
            _readOnly = false;
            NewTextBox();
            base.Controls.Add(TB);
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            TextAlign = HorizontalAlignment.Left;
            ForeColor = AloneLibrary.ColorFromHex("#7C858E");
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9f);
            base.Size = new Size(97, 29);
            Enabled = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            G.Clear(BackColor);
            bool enabled = Enabled;
            if (enabled)
            {
                TB.ForeColor = ForeColor;
                bool flag = State == AloneTextBox.MouseState.Down;
                if (flag)
                {
                    using (Pen pen = new Pen(AloneLibrary.ColorFromHex("#78B7E6")))
                        G.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 12, AloneLibrary.RoundingStyle.All));
                }
                else
                {
                    using (Pen pen2 = new Pen(AloneLibrary.ColorFromHex("#D0D5D9")))
                        G.DrawPath(pen2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 12, AloneLibrary.RoundingStyle.All));
                }
            }
            else
            {
                TB.ForeColor = ForeColor;
                using (Pen pen3 = new Pen(AloneLibrary.ColorFromHex("#E1E1E2")))
                    G.DrawPath(pen3, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 12, AloneLibrary.RoundingStyle.All));
            }
            TB.TextAlign = TextAlign;
            TB.UseSystemPasswordChar = UseSystemPasswordChar;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            bool flag = !MultiLine;
            checked
            {
                if (flag)
                {
                    int height = TB.Height;
                    TB.Location = new Point(10, (int)Math.Round(unchecked((double)base.Height / 2.0 - (double)height / 2.0 - 0.0)));
                    TB.Size = new Size(base.Width - 20, height);
                }
                else
                {
                    TB.Location = new Point(10, 10);
                    TB.Size = new Size(base.Width - 20, base.Height - 20);
                }
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            State = AloneTextBox.MouseState.Down;
            base.Invalidate();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            State = AloneTextBox.MouseState.None;
            base.Invalidate();
        }
    }

    #endregion
}