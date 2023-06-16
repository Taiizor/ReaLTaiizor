#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverPalette

    public class ForeverPalette : Control
    {
        private int W;
        private int H;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Width = 180;
            Height = 80;
        }

        [Category("Colors")]
        public Color Red { get; set; } = Color.FromArgb(220, 85, 96);

        [Category("Colors")]
        public Color Cyan { get; set; } = Color.FromArgb(10, 154, 157);

        [Category("Colors")]
        public Color Blue { get; set; } = Color.FromArgb(0, 128, 255);

        [Category("Colors")]
        public Color LimeGreen { get; set; } = Color.FromArgb(35, 168, 109);

        [Category("Colors")]
        public Color Orange { get; set; } = Color.FromArgb(253, 181, 63);

        [Category("Colors")]
        public Color Purple { get; set; } = Color.FromArgb(155, 88, 181);

        [Category("Colors")]
        public Color Black { get; set; } = Color.FromArgb(45, 47, 49);

        [Category("Colors")]
        public Color Gray { get; set; } = Color.FromArgb(63, 70, 73);

        [Category("Colors")]
        public Color White { get; set; } = Color.FromArgb(243, 243, 243);

        [Category("Options")]
        public string String { get; set; } = "Color Palette";

        [Category("Colors")]
        public Color StringColor { get; set; } = Color.White;

        public ForeverPalette()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.FromArgb(60, 70, 73);
            Size = new(160, 80);
            Font = new("Segoe UI", 12);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            W = Width - 1;
            H = Height - 1;

            Graphics _with6 = G;
            _with6.SmoothingMode = SmoothingMode.HighQuality;
            _with6.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with6.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with6.Clear(BackColor);

            //-- Colors 
            _with6.FillRectangle(new SolidBrush(Red), new Rectangle(0, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(Cyan), new Rectangle(20, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(Blue), new Rectangle(40, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(LimeGreen), new Rectangle(60, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(Orange), new Rectangle(80, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(Purple), new Rectangle(100, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(Black), new Rectangle(120, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(Gray), new Rectangle(140, 0, 20, 40));
            _with6.FillRectangle(new SolidBrush(White), new Rectangle(160, 0, 20, 40));

            //-- Text
            _with6.DrawString(String, Font, new SolidBrush(StringColor), new Rectangle(0, 22, W, H), ForeverLibrary.CenterSF);

            base.OnPaint(e);

            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
    }

    #endregion
}