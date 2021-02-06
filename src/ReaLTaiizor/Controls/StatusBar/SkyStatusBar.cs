#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyStatusBar

    public class SkyStatusBar : Control
    {
        #region Variables
        private Color _BackColorA = Color.FromArgb(245, 245, 245);
        private Color _BackColorB = Color.FromArgb(230, 230, 230);
        private Color _BorderColorA = Color.FromArgb(200, 252, 252, 252);
        private Color _BorderColorB = Color.FromArgb(200, 249, 249, 249);
        private Color _BorderColorC = Color.FromArgb(189, 189, 189);
        #endregion

        #region Settings
        public Color BackColorA
        {
            get => _BackColorA;
            set => _BackColorA = value;
        }

        public Color BackColorB
        {
            get => _BackColorB;
            set => _BackColorB = value;
        }

        public Color BorderColorA
        {
            get => _BorderColorA;
            set => _BorderColorA = value;
        }

        public Color BorderColorB
        {
            get => _BorderColorB;
            set => _BorderColorB = value;
        }

        public Color BorderColorC
        {
            get => _BorderColorC;
            set => _BorderColorC = value;
        }
        #endregion

        #region Events
        protected override void CreateHandle()
        {
            base.CreateHandle();
            Dock = DockStyle.Bottom;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        #endregion

        public SkyStatusBar() : base()
        {
            Font = new("Verdana", 6.75f, FontStyle.Bold);
            ForeColor = Color.FromArgb(27, 94, 137);
            Size = new(Width, 20);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            base.OnPaint(e);

            LinearGradientBrush bodyGradNone = new(new Rectangle(0, 1, Width, Height - 1), BackColorA, BackColorB, 90);
            G.FillRectangle(bodyGradNone, bodyGradNone.Rectangle);
            LinearGradientBrush bodyInBorderNone = new(new Rectangle(1, 1, Width - 3, Height - 3), BorderColorA, BorderColorB, 90);
            G.DrawRectangle(new(bodyInBorderNone), new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawRectangle(new(BorderColorC), new Rectangle(0, 0, Width - 1, Height - 1));

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(4, 4), StringFormat.GenericDefault);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}