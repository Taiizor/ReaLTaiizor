#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Util;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxNotification

    public class FoxNotification : Util.FoxBase.NotifyFoxBase
    {
        public FoxNotification() : base()
        {
            Size = new Size(130, 40);
            Font = new Font("Segoe UI", 10);
        }

        public Styles Style { get; set; }

        private Graphics G;

        private Color Background;
        private Color TextColor;
        private Color LeftBar;

        private Color _GreenBackColor = FoxLibrary.ColorFromHex("#DFF0D6");
        public Color GreenBackColor
        {
            get => _GreenBackColor;
            set => _GreenBackColor = value;
        }

        private Color _GreenTextColor = FoxLibrary.ColorFromHex("#4E8C45");
        public Color GreenTextColor
        {
            get => _GreenTextColor;
            set => _GreenTextColor = value;
        }

        private Color _GreenBarColor = FoxLibrary.ColorFromHex("#CEE5B6");
        public Color GreenBarColor
        {
            get => _GreenBarColor;
            set => _GreenBarColor = value;
        }

        private Color _BlueBackColor = FoxLibrary.ColorFromHex("#D9EDF8");
        public Color BlueBackColor
        {
            get => _BlueBackColor;
            set => _BlueBackColor = value;
        }

        private Color _BlueTextColor = FoxLibrary.ColorFromHex("#498FB8");
        public Color BlueTextColor
        {
            get => _BlueTextColor;
            set => _BlueTextColor = value;
        }

        private Color _BlueBarColor = FoxLibrary.ColorFromHex("#AFD9F0");
        public Color BlueBarColor
        {
            get => _BlueBarColor;
            set => _BlueBarColor = value;
        }

        private Color _YellowBackColor = FoxLibrary.ColorFromHex("#FCF8E1");
        public Color YellowBackColor
        {
            get => _YellowBackColor;
            set => _YellowBackColor = value;
        }

        private Color _YellowTextColor = FoxLibrary.ColorFromHex("#908358");
        public Color YellowTextColor
        {
            get => _YellowTextColor;
            set => _YellowTextColor = value;
        }

        private Color _YellowBarColor = FoxLibrary.ColorFromHex("#FAEBC8");
        public Color YellowBarColor
        {
            get => _YellowBarColor;
            set => _YellowBarColor = value;
        }

        private Color _RedBackColor = FoxLibrary.ColorFromHex("#F2DEDE");
        public Color RedBackColor
        {
            get => _RedBackColor;
            set => _RedBackColor = value;
        }

        private Color _RedTextColor = FoxLibrary.ColorFromHex("#C2635E");
        public Color RedTextColor
        {
            get => _RedTextColor;
            set => _RedTextColor = value;
        }

        private Color _RedBarColor = FoxLibrary.ColorFromHex("#EBCCD1");
        public Color RedBarColor
        {
            get => _RedBarColor;
            set => _RedBarColor = value;
        }

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
                    Background = _GreenBackColor;
                    TextColor = _GreenTextColor;
                    LeftBar = _GreenBarColor;
                    break;
                case Styles.Blue:
                    Background = _BlueBackColor;
                    TextColor = _BlueTextColor;
                    LeftBar = _BlueBarColor;
                    break;
                case Styles.Yellow:
                    Background = _YellowBackColor;
                    TextColor = _YellowTextColor;
                    LeftBar = _YellowBarColor;
                    break;
                case Styles.Red:
                    Background = _RedBackColor;
                    TextColor = _RedTextColor;
                    LeftBar = _RedBarColor;
                    break;
            }

            using (SolidBrush Back = new SolidBrush(Background))
            {
                using (SolidBrush TC = new SolidBrush(TextColor))
                {
                    using (SolidBrush LB = new SolidBrush(LeftBar))
                    {
                        G.FillRectangle(Back, FoxLibrary.FullRectangle(Size, true));
                        G.SmoothingMode = SmoothingMode.None;
                        G.FillRectangle(LB, new Rectangle(0, 1, 6, Height - 2));
                        G.SmoothingMode = SmoothingMode.HighQuality;
                        G.DrawString(Text, Font, TC, new Point(20, 11));
                    }
                }
            }

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(Width, 40);
        }

    }

    #endregion
}