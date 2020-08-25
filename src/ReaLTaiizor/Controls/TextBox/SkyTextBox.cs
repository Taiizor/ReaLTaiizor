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
        System.Windows.Forms.TextBox txtbox = new System.Windows.Forms.TextBox();

        #region " Control Help - Properties & Flicker Control "

        private bool _passmask = false;
        public bool UseSystemPasswordChar
        {
            get { return _passmask; }
            set
            {
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
                _passmask = value;
                Invalidate();
            }
        }

        private int _maxchars = 32767;
        public int MaxLength
        {
            get { return _maxchars; }
            set
            {
                _maxchars = value;
                txtbox.MaxLength = MaxLength;
                Invalidate();
            }
        }

        private HorizontalAlignment _align;
        public HorizontalAlignment TextAlignment
        {
            get { return _align; }
            set
            {
                _align = value;
                Invalidate();
            }
        }

        private bool _multiline = false;
        public bool MultiLine
        {
            get { return _multiline; }
            set
            {
                _multiline = value;
                Invalidate();
            }
        }

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
                txtbox.BackColor = Color.FromArgb(43, 43, 43);
                txtbox.ForeColor = ForeColor;
                txtbox.Text = string.Empty;
                txtbox.TextAlign = HorizontalAlignment.Center;
                txtbox.BorderStyle = BorderStyle.None;
                txtbox.Location = new Point(5, 4);
                txtbox.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
                txtbox.Size = new Size(Width - 10, Height - 11);
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

        }
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
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            Size = new Size(75, 35);
            DoubleBuffered = true;
            txtbox.TextChanged += new EventHandler(TextChngTxtBox);
            base.TextChanged += new EventHandler(TextChng);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighQuality;

            Height = txtbox.Height + 10;
            {
                txtbox.Width = Width - 10;
                txtbox.TextAlign = TextAlignment;
                txtbox.UseSystemPasswordChar = UseSystemPasswordChar;
            }

            G.Clear(Color.Transparent);

            LinearGradientBrush innerBorderBrush = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 3), Color.FromArgb(220, 220, 220), Color.FromArgb(228, 228, 228), 90);
            Pen innerBorderPen = new Pen(innerBorderBrush);
            G.DrawRectangle(innerBorderPen, new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawLine(new Pen(Color.FromArgb(191, 191, 191)), new Point(1, 1), new Point(Width - 3, 1));

            G.DrawRectangle(new Pen(Color.FromArgb(254, 254, 254)), new Rectangle(0, 0, Width - 1, Height - 1));
            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}