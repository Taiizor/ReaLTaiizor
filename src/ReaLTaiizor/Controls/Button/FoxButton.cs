#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxButton

    public class FoxButton : Util.FoxBase.ButtonFoxBase
    {

        private Graphics G;

        public Color BaseColor { get; set; } = FoxLibrary.ColorFromHex("#F9F9F9");
        public Color OverColor { get; set; } = FoxLibrary.ColorFromHex("#F2F2F2");
        public Color DownColor { get; set; } = FoxLibrary.ColorFromHex("#E8E8E8");
        public Color BorderColor { get; set; } = FoxLibrary.ColorFromHex("#C1C1C1");
        public Color DisabledBaseColor { get; set; } = FoxLibrary.ColorFromHex("#F9F9F9");
        public Color DisabledTextColor { get; set; } = FoxLibrary.ColorFromHex("#A6B2BE");
        public Color DisabledBorderColor { get; set; } = FoxLibrary.ColorFromHex("#D1D1D1");

        public FoxButton() : base()
        {
            Font = new("Segoe UI", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            G.Clear(Parent.BackColor);

            if (Enabled)
            {
                switch (State)
                {
                    case FoxLibrary.MouseState.None:
                        using (SolidBrush Background = new(BaseColor))
                        {
                            G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                        }

                        break;
                    case FoxLibrary.MouseState.Over:

                        using (SolidBrush Background = new(OverColor))
                        {
                            G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                        }

                        break;
                    case FoxLibrary.MouseState.Down:
                        using (SolidBrush Background = new(DownColor))
                        {
                            G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                        }

                        break;
                }

                using (Pen Border = new(BorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                }

                using SolidBrush TextColor = new(ForeColor);
                FoxLibrary.CenterString(G, Text, Font, TextColor.Color, new Rectangle(3, 0, Width, Height));
            }
            else
            {
                using (SolidBrush Background = new(DisabledBaseColor))
                {
                    G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                }

                using (SolidBrush TextColor = new(DisabledTextColor))
                {
                    FoxLibrary.CenterString(G, Text, Font, TextColor.Color, new Rectangle(3, 0, Width, Height));
                }

                using Pen Border = new(DisabledBorderColor);
                G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
            }

            base.OnPaint(e);
        }

    }

    #endregion
}