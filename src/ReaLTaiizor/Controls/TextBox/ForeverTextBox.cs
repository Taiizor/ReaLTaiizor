#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverTextBox

    [DefaultEvent("TextChanged")]
    public class ForeverTextBox : Control
    {
        private int W;
        private int H;
        private MouseStateForever State = MouseStateForever.None;
        private readonly System.Windows.Forms.TextBox TB;

        [Category("Options")]
        public HorizontalAlignment TextAlign
        {
            get;
            set
            {
                field = value;
                if (TB != null)
                {
                    TB.TextAlign = value;
                }
            }
        } = HorizontalAlignment.Left;

        [Category("Options")]
        public int MaxLength
        {
            get;
            set
            {
                field = value;
                if (TB != null)
                {
                    TB.MaxLength = value;
                }
            }
        } = 32767;

        [Category("Options")]
        public bool ReadOnly
        {
            get;
            set
            {
                field = value;
                if (TB != null)
                {
                    TB.ReadOnly = value;
                }
            }
        }

        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get;
            set
            {
                field = value;
                if (TB != null)
                {
                    TB.UseSystemPasswordChar = value;
                }
            }
        }

        [Category("Options")]
        public bool Multiline
        {
            get;
            set
            {
                field = value;
                if (TB != null)
                {
                    TB.Multiline = value;

                    if (value)
                    {
                        TB.Height = Height - 11;
                    }
                    else
                    {
                        Height = TB.Height + 11;
                    }
                }
            }
        }

        [Category("Options")]
        public bool FocusOnHover { get; set; } = false;

        [Category("Options")]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                if (TB != null)
                {
                    TB.Text = value;
                }
            }
        }

        [Category("Options")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                if (TB != null)
                {
                    TB.Font = value;
                    TB.Location = new(3, 5);
                    TB.Width = Width - 6;

                    if (!Multiline)
                    {
                        Height = TB.Height + 11;
                    }
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(TB))
            {
                Controls.Add(TB);
            }
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = TB.Text;
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                TB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                TB.Copy();
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            TB.Location = new(5, 5);
            TB.Width = Width - 10;

            if (Multiline)
            {
                TB.Height = Height - 11;
            }
            else
            {
                Height = TB.Height + 11;
            }

            base.OnResize(e);
        }

        [Category("Colors")]
        public Color BorderColor { get; set; } = ForeverLibrary.ForeverColor;
        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(45, 47, 49);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseStateForever.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseStateForever.Over;
            TB.Focus();
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStateForever.Over;
            if (FocusOnHover)
            {
                TB.Focus();
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStateForever.None;
            Invalidate();
        }

        public ForeverTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            ForeColor = Color.FromArgb(192, 192, 192);

            TB = new System.Windows.Forms.TextBox
            {
                Font = new("Segoe UI", 10),
                Text = Text,
                BackColor = BaseColor,
                ForeColor = ForeColor,
                MaxLength = MaxLength,
                Multiline = Multiline,
                ReadOnly = ReadOnly,
                UseSystemPasswordChar = UseSystemPasswordChar,
                BorderStyle = BorderStyle.None,
                Location = new(6, 6),
                Width = Width - 10,

                Cursor = Cursors.IBeam
            };

            if (Multiline)
            {
                TB.Height = Height - 11;
            }
            else
            {
                Height = TB.Height + 11;
            }

            Width = 103;

            TB.TextChanged += OnBaseTextChanged;
            TB.KeyDown += OnBaseKeyDown;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            BackColor = Color.Transparent;

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width - 2;
            H = Height - 2;

            Rectangle Base = new(1, 1, W, H);

            Graphics _with12 = G;
            _with12.SmoothingMode = SmoothingMode.HighQuality;
            _with12.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with12.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with12.Clear(BorderColor);

            //-- Colors
            TB.BackColor = BaseColor;
            TB.ForeColor = ForeColor;

            //-- Base
            _with12.FillRectangle(new SolidBrush(BaseColor), Base);

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        #region TB Events

        public new event KeyEventHandler KeyUp
        {
            add => TB.KeyUp += value;
            remove => TB.KeyUp -= value;
        }

        public new event KeyEventHandler KeyDown
        {
            add => TB.KeyDown += value;
            remove => TB.KeyDown -= value;
        }

        public new event KeyPressEventHandler KeyPress
        {
            add => TB.KeyPress += value;
            remove => TB.KeyPress -= value;
        }

        #endregion

        private void UpdateColors()
        {
            ForeverColors Colors = ForeverLibrary.GetColors(this);

            BorderColor = Colors.Forever;
        }
    }

    #endregion
}