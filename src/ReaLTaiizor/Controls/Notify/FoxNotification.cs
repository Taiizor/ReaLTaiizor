#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxNotification

    public class FoxNotification : Util.FoxBase.NotifyFoxBase
    {
        public FoxNotification() : base()
        {
            Size = new(130, 40);
            Font = new("Segoe UI", 10);
        }

        public Styles Style { get; set; }

        private Graphics G;

        private Color Background;
        private Color TextColor;
        private Color LeftBar;

        public Color GreenBackColor { get; set; } = FoxLibrary.ColorFromHex("#DFF0D6");
        public Color GreenTextColor { get; set; } = FoxLibrary.ColorFromHex("#4E8C45");
        public Color GreenBarColor { get; set; } = FoxLibrary.ColorFromHex("#CEE5B6");
        public Color BlueBackColor { get; set; } = FoxLibrary.ColorFromHex("#D9EDF8");
        public Color BlueTextColor { get; set; } = FoxLibrary.ColorFromHex("#498FB8");
        public Color BlueBarColor { get; set; } = FoxLibrary.ColorFromHex("#AFD9F0");
        public Color YellowBackColor { get; set; } = FoxLibrary.ColorFromHex("#FCF8E1");
        public Color YellowTextColor { get; set; } = FoxLibrary.ColorFromHex("#908358");
        public Color YellowBarColor { get; set; } = FoxLibrary.ColorFromHex("#FAEBC8");
        public Color RedBackColor { get; set; } = FoxLibrary.ColorFromHex("#F2DEDE");
        public Color RedTextColor { get; set; } = FoxLibrary.ColorFromHex("#C2635E");
        public Color RedBarColor { get; set; } = FoxLibrary.ColorFromHex("#EBCCD1");

        public enum Styles : byte
        {
            Green = 0,
            Blue = 1,
            Yellow = 2,
            Red = 3
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (Style)
            {
                case Styles.Green:
                    Background = GreenBackColor;
                    TextColor = GreenTextColor;
                    LeftBar = GreenBarColor;
                    break;
                case Styles.Blue:
                    Background = BlueBackColor;
                    TextColor = BlueTextColor;
                    LeftBar = BlueBarColor;
                    break;
                case Styles.Yellow:
                    Background = YellowBackColor;
                    TextColor = YellowTextColor;
                    LeftBar = YellowBarColor;
                    break;
                case Styles.Red:
                    Background = RedBackColor;
                    TextColor = RedTextColor;
                    LeftBar = RedBarColor;
                    break;
            }

            using (SolidBrush Back = new(Background))
            {
                using SolidBrush TC = new(TextColor);
                using SolidBrush LB = new(LeftBar);
                G.FillRectangle(Back, FoxLibrary.FullRectangle(Size, true));
                G.SmoothingMode = SmoothingMode.None;
                G.FillRectangle(LB, new Rectangle(0, 1, 6, Height - 2));
                G.SmoothingMode = SmoothingMode.HighQuality;
                G.DrawString(Text, Font, TC, new Point(20, 11));
            }

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(Width, 40);
        }

    }

    #endregion
}