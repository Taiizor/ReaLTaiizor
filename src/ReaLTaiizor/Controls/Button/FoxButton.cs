#region Imports

using System.Drawing;
using ReaLTaiizor.Util;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxButton

    public class FoxButton : Util.FoxBase.ButtonFoxBase
    {

        private Graphics G;

        private Color _BaseColor = FoxLibrary.ColorFromHex("#F9F9F9");
        public Color BaseColor
        {
            get => _BaseColor;
            set => _BaseColor = value;
        }

        private Color _OverColor = FoxLibrary.ColorFromHex("#F2F2F2");
        public Color OverColor
        {
            get => _OverColor;
            set => _OverColor = value;
        }

        private Color _DownColor = FoxLibrary.ColorFromHex("#E8E8E8");
        public Color DownColor
        {
            get => _DownColor;
            set => _DownColor = value;
        }

        private Color _BorderColor = FoxLibrary.ColorFromHex("#C1C1C1");
        public Color BorderColor
        {
            get => _BorderColor;
            set => _BorderColor = value;
        }

        private Color _DisabledBaseColor = FoxLibrary.ColorFromHex("#F9F9F9");
        public Color DisabledBaseColor
        {
            get => _DisabledBaseColor;
            set => _DisabledBaseColor = value;
        }

        private Color _DisabledTextColor = FoxLibrary.ColorFromHex("#A6B2BE");
        public Color DisabledTextColor
        {
            get => _DisabledTextColor;
            set => _DisabledTextColor = value;
        }

        private Color _DisabledBorderColor = FoxLibrary.ColorFromHex("#D1D1D1");
        public Color DisabledBorderColor
        {
            get => _DisabledBorderColor;
            set => _DisabledBorderColor = value;
        }

        public FoxButton() : base()
        {
            Font = new Font("Segoe UI", 10);
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
                        using (SolidBrush Background = new(_BaseColor))
                        {
                            G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                        }

                        break;
                    case FoxLibrary.MouseState.Over:

                        using (SolidBrush Background = new(_OverColor))
                        {
                            G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                        }

                        break;
                    case FoxLibrary.MouseState.Down:
                        using (SolidBrush Background = new(_DownColor))
                        {
                            G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                        }

                        break;
                }

                using (Pen Border = new(_BorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                }

                using (SolidBrush TextColor = new(ForeColor))
                {
                    FoxLibrary.CenterString(G, Text, Font, TextColor.Color, new Rectangle(3, 0, Width, Height));
                }
            }
            else
            {
                using (SolidBrush Background = new(_DisabledBaseColor))
                {
                    G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                }

                using (SolidBrush TextColor = new(_DisabledTextColor))
                {
                    FoxLibrary.CenterString(G, Text, Font, TextColor.Color, new Rectangle(3, 0, Width, Height));
                }

                using (Pen Border = new(_DisabledBorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                }
            }

            base.OnPaint(e);
        }

    }

    #endregion
}