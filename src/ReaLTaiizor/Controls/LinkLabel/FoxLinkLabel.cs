﻿#region Imports

using System.Drawing;
using ReaLTaiizor.Util;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ReaLTaiizor.Util.FoxBase;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxLinkLabel

    public class FoxLinkLabel : ButtonFoxBase
    {
        private Graphics G;

        private Color _DownColor = FoxLibrary.ColorFromHex("#FF9500");
        public Color DownColor
        {
            get => _DownColor;
            set => _DownColor = value;
        }

        private Color _OverColor = FoxLibrary.ColorFromHex("#178CE5");
        public Color OverColor
        {
            get => _OverColor;
            set => _OverColor = value;
        }

        public FoxLinkLabel() : base()
        {
            Size = new(82, 18);
            Font = new("Segoe UI", 10);
            ForeColor = FoxLibrary.ColorFromHex("#0095DD");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (State)
            {
                case FoxLibrary.MouseState.Over:
                    using (SolidBrush TextColor = new(_OverColor))
                    {
                        using Font TextFont = new(Font.FontFamily, Font.Size, FontStyle.Underline);
                        G.DrawString(Text, TextFont, TextColor, new Point(0, 0));
                    }
                    break;
                case FoxLibrary.MouseState.Down:
                    using (SolidBrush TextColor = new(_DownColor))
                    {
                        G.DrawString(Text, Font, TextColor, new Point(0, 0));
                    }

                    break;
                default:
                    using (SolidBrush TextColor = new(ForeColor))
                    {
                        G.DrawString(Text, Font, TextColor, new Point(0, 0));
                    }

                    break;
            }

            base.OnPaint(e);
        }

    }

    #endregion
}