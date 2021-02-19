#region Imports

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region GroupBox

    public class GroupBox : ContainerControl
    {
        #region Variables

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        private Color _HeaderColor = Color.CornflowerBlue;
        private Color _BorderColorH = Color.FromArgb(182, 180, 186);
        private Color _BorderColorG = Color.FromArgb(159, 159, 161);
        private Color _BackGColor = Color.DodgerBlue;
        private Color _BaseColor = Color.Transparent;

        #endregion

        #region Custom Properties

        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color HeaderColor
        {
            get => _HeaderColor;
            set
            {
                _HeaderColor = value;
                Invalidate();
            }
        }

        public Color BorderColorH
        {
            get => _BorderColorH;
            set
            {
                _BorderColorH = value;
                Invalidate();
            }
        }

        public Color BorderColorG
        {
            get => _BorderColorG;
            set
            {
                _BorderColorG = value;
                Invalidate();
            }
        }

        public Color BackGColor
        {
            get => _BackGColor;
            set
            {
                _BackGColor = value;
                Invalidate();
            }
        }

        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        #endregion

        public GroupBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Size = new(212, 104);
            MinimumSize = new(136, 50);
            Padding = new Padding(5, 28, 5, 5);
            ForeColor = Color.FromArgb(53, 53, 53);
            Font = new("Tahoma", 9, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle TitleBox = new(51, 3, Width - 103, 18);
            Rectangle box = new(0, 0, Width - 1, Height - 10);

            G.Clear(BaseColor);
            G.SmoothingMode = SmoothingType;

            // Draw the body of the GroupBox
            G.FillPath(new SolidBrush(_BackGColor), RoundRectangle.RoundRect(new Rectangle(1, 12, Width - 3, box.Height - 1), 8));
            // Draw the border of the GroupBox
            G.DrawPath(new(_BorderColorG), RoundRectangle.RoundRect(new Rectangle(1, 12, Width - 3, Height - 13), 8));

            // Draw the background of the title box
            G.FillPath(new SolidBrush(_HeaderColor), RoundRectangle.RoundRect(TitleBox, 1));
            // Draw the border of the title box
            G.DrawPath(new(_BorderColorH), RoundRectangle.RoundRect(TitleBox, 4));
            // Draw the specified string from 'Text' property inside the title box
            G.DrawString(Text, Font, new SolidBrush(ForeColor), TitleBox, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}