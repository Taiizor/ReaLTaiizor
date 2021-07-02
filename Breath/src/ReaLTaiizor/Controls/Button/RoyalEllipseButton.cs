#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalEllipseButton

    public class RoyalEllipseButton : RoyalButton
    {
        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public RoyalEllipseButton() : base()
        {
            Size = new(120, 120);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color foreColor = ForeColor;
            Color backColor = BackColor;
            //Color borderColor = BackColor;

            if (HotTracked && !Pressed)
            {
                backColor = HotTrackColor;
                //borderColor = backColor;
            }
            else if (Pressed)
            {
                foreColor = PressedForeColor;
                backColor = PressedColor;
                //borderColor = backColor;
            }

            if (DrawBorder)
            {
                //borderColor = BorderColor;
            }

            e.Graphics.SmoothingMode = SmoothingType;
            e.Graphics.FillEllipse(new SolidBrush(backColor), new Rectangle(2, 2, Width - (BorderThickness + 1), Height - (BorderThickness + 1)));
            e.Graphics.DrawEllipse(new(BorderColor, BorderThickness), new Rectangle(1, 1, Width - BorderThickness, Height - BorderThickness));

            if (Image != null)
            {
                if (LayoutFlags == RoyalLayoutFlags.ImageOnly)
                {
                    e.Graphics.DrawImage(Image, new Point((Width - Image.Width) / 2, (Height - Image.Height) / 2));
                }
            }
            else
            {
                TextRenderer.DrawText(e.Graphics, Text, Font, e.ClipRectangle, foreColor,
                    Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            //base.OnPaint(e);
        }
    }

    #endregion
}