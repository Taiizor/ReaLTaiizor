#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region BigTextBox

    [DefaultEvent("TextChanged")]
    public class BigTextBox : Control
    {
        #region Variables

        public TextBox RT_TB = new();
        private GraphicsPath Shape;
        private int _maxchars = 32767;
        private bool _ReadOnly;
        private bool _Multiline;
        private Image _Image;
        private Size _ImageSize;
        private HorizontalAlignment ALNType;
        private bool isPasswordMasked = false;
        private readonly Pen P1;
        private readonly SolidBrush B1;

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
                        RT_TB.Height = Height - 23;
                    }
                    else
                    {
                        Height = RT_TB.Height + 23;
                    }
                }

            }
        }

        public Image Image
        {
            get => _Image;
            set
            {
                if (value == null)
                {
                    _ImageSize = Size.Empty;
                }
                else
                {
                    _ImageSize = value.Size;
                }

                _Image = value;

                if (Image == null)
                {
                    RT_TB.Location = new(8, 10);
                }
                else
                {
                    RT_TB.Location = new(35, 11);
                }
                Invalidate();
            }
        }

        protected Size ImageSize => _ImageSize;

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
                RT_TB.Height = Height - 23;
            }
            else
            {
                Height = RT_TB.Height + 23;
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
            _TB.Location = new(7, 10);
            _TB.Text = string.Empty;
            _TB.BorderStyle = BorderStyle.None;
            _TB.TextAlign = HorizontalAlignment.Left;
            _TB.Font = new("Tahoma", 11);
            _TB.UseSystemPasswordChar = UseSystemPasswordChar;
            _TB.Multiline = false;
            RT_TB.KeyDown += _OnKeyDown;
            RT_TB.TextChanged += OnBaseTextChanged;
        }

        public BigTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddTextBox();
            Controls.Add(RT_TB);

            P1 = new(Color.FromArgb(180, 180, 180)); // P1 = Border color
            B1 = new(Color.White); // B1 = Rect Background color
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;

            Text = null;
            Font = new("Tahoma", 11);
            Size = new(100, 43);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.HighQuality;

            if (Image == null)
            {
                RT_TB.Width = Width - 18;
            }
            else
            {
                RT_TB.Width = Width - 45;
            }

            RT_TB.TextAlign = TextAlignment;
            RT_TB.UseSystemPasswordChar = UseSystemPasswordChar;

            G.Clear(Color.Transparent);
            G.FillPath(B1, Shape); // Draw background
            G.DrawPath(P1, Shape); // Draw border


            if (Image != null)
            {
                G.DrawImage(_Image, 5, 8, 24, 24);
                // 24x24 is the perfect size of the image
            }

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}