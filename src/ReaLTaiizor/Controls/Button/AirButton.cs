#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AirButton

    public class AirButton : AirControl
    {
        public AirButton()
        {
            Font = new("Segoe UI", 9);
            SetColor("Gradient top normal", 237, 237, 237);
            SetColor("Gradient top over", 242, 242, 242);
            SetColor("Gradient top down", 235, 235, 235);
            SetColor("Gradient bottom normal", 230, 230, 230);
            SetColor("Gradient bottom over", 235, 235, 235);
            SetColor("Gradient bottom down", 223, 223, 223);
            SetColor("Border", 167, 167, 167);
            SetColor("Text normal", 60, 60, 60);
            SetColor("Text down/over", 20, 20, 20);
            SetColor("Text disabled", Color.Gray);
            Size = new(100, 45);
            Cursor = Cursors.Hand;
        }

        private Color GTN, GTO, GTD, GBN, GBO, GBD, Bo, TN, TD, TDO;

        protected override void ColorHook()
        {
            GTN = GetColor("Gradient top normal");
            GTO = GetColor("Gradient top over");
            GTD = GetColor("Gradient top down");
            GBN = GetColor("Gradient bottom normal");
            GBO = GetColor("Gradient bottom over");
            GBD = GetColor("Gradient bottom down");
            Bo = GetColor("Border");
            TN = GetColor("Text normal");
            TDO = GetColor("Text down/over");
            TD = GetColor("Text disabled");
        }

        protected override void PaintHook()
        {
            G.Clear(BackColor);
            G.SmoothingMode = SmoothingMode.HighQuality;


            LinearGradientBrush LGB = State switch
            {
                MouseStateAir.None => new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 1), GTN, GBN, 90f),
                MouseStateAir.Over => new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 1), GTO, GBO, 90f),
                _ => new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 1), GTD, GBD, 90f),
            };

            if (!Enabled)
            {
                LGB = new(new Rectangle(0, 0, Width - 1, Height - 1), GTN, GBN, 90f);
            }

            GraphicsPath buttonpath = CreateRound(Rectangle.Round(LGB.Rectangle), 3);
            G.FillPath(LGB, CreateRound(Rectangle.Round(LGB.Rectangle), 3));
            
            if (!Enabled)
            {
                G.FillPath(new SolidBrush(Color.FromArgb(50, Color.White)), CreateRound(Rectangle.Round(LGB.Rectangle), 3));
            }

            G.SetClip(buttonpath);
            LGB = new(new Rectangle(0, 0, Width, Height / 6), Color.FromArgb(80, Color.White), Color.Transparent, 90f);
            G.FillRectangle(LGB, Rectangle.Round(LGB.Rectangle));

            G.ResetClip();
            G.DrawPath(new(Bo), buttonpath);

            if (Enabled)
            {
                switch (State)
                {
                    case MouseStateAir.None:
                        DrawText(new SolidBrush(TN), HorizontalAlignment.Center, 1, 0);
                        break;
                    default:
                        DrawText(new SolidBrush(TDO), HorizontalAlignment.Center, 1, 0);
                        break;
                }
            }
            else
            {
                DrawText(new SolidBrush(TD), HorizontalAlignment.Center, 1, 0);
            }
        }
    }

    #endregion
}