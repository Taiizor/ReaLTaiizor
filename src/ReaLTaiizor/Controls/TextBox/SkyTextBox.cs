#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyTextBox

    public class SkyTextBox : Control
    {
        private readonly TextBox txtbox = new();

        #region " Control Help - Properties & Flicker Control "

        public bool UseSystemPasswordChar
        {
            get;
            set
            {
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
                field = value;
                Invalidate();
            }
        } = false;

        public int MaxLength
        {
            get;
            set
            {
                field = value;
                txtbox.MaxLength = MaxLength;
                Invalidate();
            }
        } = 32767;

        public HorizontalAlignment TextAlignment
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        public bool MultiLine
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            txtbox.BackColor = BackColor;
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            txtbox.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            txtbox.Font = Font;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            txtbox.Focus();
        }

        public void TextChngTxtBox(object system, EventArgs e)
        {
            Text = txtbox.Text;
        }

        public void TextChng(object system, EventArgs e)
        {
            txtbox.Text = Text;
        }

        public void NewTextBox()
        {
            {
                txtbox.Multiline = false;
                txtbox.BackColor = UnknownBackColor;
                txtbox.ForeColor = ForeColor;
                txtbox.Text = string.Empty;
                txtbox.TextAlign = HorizontalAlignment.Center;
                txtbox.BorderStyle = BorderStyle.None;
                txtbox.Location = new(5, 4);
                txtbox.Font = new("Trebuchet MS", 8.25f, FontStyle.Bold);
                txtbox.Size = new(Width - 10, Height - 11);
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

        }
        #endregion

        #region Variables
        #endregion

        #region Settings
        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighQuality;

        public Color BorderColorA { get; set; } = Color.FromArgb(220, 220, 220);

        public Color BorderColorB { get; set; } = Color.FromArgb(228, 228, 228);

        public Color BorderColorC { get; set; } = Color.FromArgb(191, 191, 191);

        public Color BorderColorD { get; set; } = Color.FromArgb(254, 254, 254);

        public Color BaseColor { get; set; } = Color.Transparent;

        public Color UnknownBackColor { get; set; } = Color.FromArgb(43, 43, 43);
        #endregion

        public SkyTextBox() : base()
        {
            NewTextBox();
            Controls.Add(txtbox);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Text = "";
            BackColor = Color.FromArgb(233, 233, 233);
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new("Verdana", 6.75f, FontStyle.Bold);
            Size = new(75, 35);
            DoubleBuffered = true;
            txtbox.TextChanged += new EventHandler(TextChngTxtBox);
            base.TextChanged += new EventHandler(TextChng);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingType;

            Height = txtbox.Height + 10;
            {
                txtbox.Width = Width - 10;
                txtbox.TextAlign = TextAlignment;
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

            G.Clear(BaseColor);

            LinearGradientBrush innerBorderBrush = new(new Rectangle(1, 1, Width - 3, Height - 3), BorderColorA, BorderColorB, 90);
            Pen innerBorderPen = new(innerBorderBrush);
            G.DrawRectangle(innerBorderPen, new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawLine(new(BorderColorC), new Point(1, 1), new Point(Width - 3, 1));

            G.DrawRectangle(new(BorderColorD), new Rectangle(0, 0, Width - 1, Height - 1));
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}