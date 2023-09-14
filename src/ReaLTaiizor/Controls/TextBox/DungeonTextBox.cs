#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonTextBox

    [DefaultEvent("TextChanged")]
    public class DungeonTextBox : Control
    {
        #region Variables

        public TextBox DungeonTB = new();
        private GraphicsPath Shape;
        private int _maxchars = 32767;
        private bool _ReadOnly;
        private bool _Multiline;
        private HorizontalAlignment ALNType;
        private bool isPasswordMasked = false;
        private Pen P1;
        private readonly SolidBrush B1;

        #endregion

        #region Properties

        public Color BorderColor { get; set; } = Color.FromArgb(180, 180, 180);

        public Color EdgeColor { get; set; } = Color.White;

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
                DungeonTB.MaxLength = MaxLength;
                Invalidate();
            }
        }

        public bool UseSystemPasswordChar
        {
            get => isPasswordMasked;
            set
            {
                DungeonTB.UseSystemPasswordChar = UseSystemPasswordChar;
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
                if (DungeonTB != null)
                {
                    DungeonTB.ReadOnly = value;
                }
            }
        }

        public bool Multiline
        {
            get => _Multiline;
            set
            {
                _Multiline = value;
                if (DungeonTB != null)
                {
                    DungeonTB.Multiline = value;

                    if (value)
                    {
                        DungeonTB.Height = Height - 10;
                    }
                    else
                    {
                        Height = DungeonTB.Height + 10;
                    }
                }
            }
        }

        #endregion

        #region EventArgs

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            DungeonTB.Text = Text;
            Invalidate();
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = DungeonTB.Text;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            DungeonTB.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            DungeonTB.Font = Font;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void _OnKeyDown(object Obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                DungeonTB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                DungeonTB.Copy();
                e.SuppressKeyPress = true;
            }
        }

        private void _Enter(object Obj, EventArgs e)
        {
            P1 = new(Color.FromArgb(205, 87, 40));
            Refresh();
        }

        private void _Leave(object Obj, EventArgs e)
        {
            P1 = new(Color.FromArgb(180, 180, 180));
            Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_Multiline)
            {
                DungeonTB.Height = Height - 10;
            }
            else
            {
                Height = DungeonTB.Height + 10;
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
            DungeonTB.Focus();
        }

        #endregion

        public void AddTextBox()
        {
            TextBox _TB = DungeonTB;
            _TB.Size = new(Width - 10, 33);
            _TB.Location = new(7, 4);
            _TB.Text = string.Empty;
            _TB.BorderStyle = BorderStyle.None;
            _TB.TextAlign = HorizontalAlignment.Left;
            _TB.Font = Font;
            _TB.UseSystemPasswordChar = UseSystemPasswordChar;
            _TB.Multiline = false;
            DungeonTB.KeyDown += _OnKeyDown;
            DungeonTB.Enter += _Enter;
            DungeonTB.Leave += _Leave;
            DungeonTB.TextChanged += OnBaseTextChanged;

        }

        public DungeonTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddTextBox();
            Controls.Add(DungeonTB);

            P1 = new(BorderColor); // P1 = Border color
            B1 = new(EdgeColor); // B1 = Rect Background color
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;

            Text = null;
            Font = new("Tahoma", 11);
            Size = new(135, 33);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.AntiAlias;

            TextBox _TB = DungeonTB;
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