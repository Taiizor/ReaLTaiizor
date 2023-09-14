#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxRadioButton

    [DefaultEvent("CheckedChanged")]
    public class FoxRadioButton : Util.FoxBase.FoxBaseRadioButton
    {
        private Graphics G;

        public Color CheckedColor { get; set; } = FoxLibrary.ColorFromHex("#2C9CDA");
        public Color DisabledCheckedColor { get; set; } = FoxLibrary.ColorFromHex("#B6B4B4");
        public Color BorderColor { get; set; } = FoxLibrary.ColorFromHex("#C8C8C8");
        public Color DisabledBorderColor { get; set; } = FoxLibrary.ColorFromHex("#E6E6E6");
        public Color DisabledTextColor { get; set; } = FoxLibrary.ColorFromHex("#A6B2BE");
        public Color HoverBorderColor { get; set; } = FoxLibrary.ColorFromHex("#2C9CDA");

        public FoxRadioButton() : base()
        {
            Font = new("Segoe UI", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            G.Clear(BackColor);

            if (Enabled)
            {
                switch (State)
                {
                    case FoxLibrary.MouseState.None:
                        using (Pen Border = new(BorderColor))
                        {
                            G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
                        }

                        break;
                    default:
                        using (Pen Border = new(HoverBorderColor))
                        {
                            G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
                        }

                        break;
                }

                using SolidBrush TextColor = new(ForeColor);
                G.DrawString(Text, Font, TextColor, new Point(27, 1));
            }
            else
            {
                using (Pen Border = new(DisabledBorderColor))
                {
                    G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
                }

                using SolidBrush TextColor = new(DisabledTextColor);
                G.DrawString(Text, Font, TextColor, new Point(27, 1));
            }

            if (Checked)
            {
                if (Enabled)
                {
                    using SolidBrush FillColor = new(CheckedColor);
                    G.FillEllipse(FillColor, new Rectangle(4, 4, 12, 12));
                }
                else
                {
                    using SolidBrush FillColor = new(DisabledCheckedColor);
                    G.FillEllipse(FillColor, new Rectangle(4, 4, 12, 12));
                }
            }

            base.OnPaint(e);
        }
    }

    #endregion
}