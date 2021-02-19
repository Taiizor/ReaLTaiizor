#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SmallTextBox

    [DefaultEvent("TextChanged")]
    public class SmallTextBox : Control
    {
        #region Variables

        public TextBox RT_TB = new();
        private GraphicsPath Shape;
        private int _maxchars = 32767;
        private bool _ReadOnly;
        private bool _Multiline;
        private HorizontalAlignment ALNType;
        private bool isPasswordMasked = false;
        private readonly Pen P1;
        private readonly SolidBrush B1;

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        private Color _BorderColor = Color.FromArgb(180, 180, 180);
        private Color _CustomBGColor = Color.White;

        #endregion

        #region Properties

        public HorizontalAlignment TextAlignment
        {
            get => ALNType;
            set
            {
                ALNType = value;
                Invalidate();
            }
        }

        public int MaxLength
        {
            get => _maxchars;
            set
            {
                _maxchars = value;
                RT_TB.MaxLength = MaxLength;
                Invalidate();
            }
        }

        public bool UseSystemPasswordChar
        {
            get => isPasswordMasked;
            set
            {
                RT_TB.UseSystemPasswordChar = UseSystemPasswordChar;
                isPasswordMasked = value;
                Invalidate();
            }
        }

        public bool ReadOnly
        {
            get => _ReadOnly;
            set
            {
                _ReadOnly = value;
                if (RT_TB != null)
                {
                    RT_TB.ReadOnly = value;
                }
            }
        }

        public bool Multiline
        {
            get => _Multiline;
            set
            {
                _Multiline = value;
                if (RT_TB != null)
                {
                    RT_TB.Multiline = value;

                    if (value)
                    {
                        RT_TB.Height = Height - 10;
                    }
                    else
                    {
                        Height = RT_TB.Height + 10;
                    }
                }
            }
        }

        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        public Color CustomBGColor
        {
            get => _CustomBGColor;
            set
            {
                _CustomBGColor = value;
                Invalidate();
            }
        }

        #endregion

        #region EventArgs

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            RT_TB.Text = Text;
            Invalidate();
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = RT_TB.Text;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            RT_TB.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RT_TB.Font = Font;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void _OnKeyDown(object Obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                RT_TB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                RT_TB.Copy();
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_Multiline)
            {
                RT_TB.Height = Height - 10;
            }
            else
            {
                Height = RT_TB.Height + 10;
            }

            Shape = new();
            GraphicsPath _with1 = Shape;
            _with1.AddArc(0, 0, 10, 10, 180, 90);
            _with1.AddArc(Width - 11, 0, 10, 10, -90, 90);
            _with1.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            _with1.AddArc(0, Height - 11, 10, 10, 90, 90);
            _with1.CloseAllFigures();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            RT_TB.Focus();
        }

        #endregion

        public void AddTextBox()
        {
            TextBox _TB = RT_TB;
            _TB.Size = new(Width - 10, 33);
            _TB.Location = new(7, 5);
            _TB.Text = string.Empty;
            _TB.BorderStyle = BorderStyle.None;
            _TB.TextAlign = HorizontalAlignment.Left;
            _TB.Font = new("Tahoma", 11);
            _TB.UseSystemPasswordChar = UseSystemPasswordChar;
            _TB.Multiline = false;
            RT_TB.KeyDown += _OnKeyDown;
            RT_TB.TextChanged += OnBaseTextChanged;
        }

        public SmallTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddTextBox();
            Controls.Add(RT_TB);

            P1 = new(BorderColor); // P1 = Border color
            B1 = new(CustomBGColor); // B1 = Rect Background color
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;

            Text = null;
            Font = new("Tahoma", 11);
            Size = new(110, 33);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingType;

            TextBox _TB = RT_TB;
            _TB.Width = Width - 10;
            _TB.TextAlign = TextAlignment;
            _TB.UseSystemPasswordChar = UseSystemPasswordChar;

            G.Clear(BackColor);
            G.FillPath(B1, Shape); // Draw background
            G.DrawPath(P1, Shape); // Draw border

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}