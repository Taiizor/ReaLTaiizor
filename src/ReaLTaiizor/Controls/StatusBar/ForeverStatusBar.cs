#region Imports

using ReaLTaiizor.Colors;
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
    #region ForeverStatusBar

    public class ForeverStatusBar : Control
    {
        private int W;
        private int H;

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Dock = DockStyle.Bottom;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(45, 47, 49);

        [Category("Colors")]
        public Color TextColor { get; set; } = Color.White;

        [Category("Colors")]
        public Color TimeColor { get; set; } = Color.White;

        [Category("Colors")]
        public Color RectColor { get; set; } = ForeverLibrary.ForeverColor;

        [Category("Options")]
        public bool ShowTimeDate { get; set; } = false;

        [Category("Options")]
        public string TimeFormat { get; set; } = "dd.MM.yyyy - HH:mm:ss";

        [Category("Options")]
        public string TimeFormatDefault => "dd.MM.yyyy - HH:mm:ss";

        public string GetTimeFormat(string TF)
        {
            try
            {
                return DateTime.Now.ToString(TF);
            }
            catch
            {
                TimeFormat = TimeFormatDefault;
                TimeFormat = TimeFormatDefault;
                return DateTime.Now.ToString(TimeFormatDefault);
            }
        }

        public ForeverStatusBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 8);
            ForeColor = Color.White;
            TimeFormat = TimeFormat;
            Size = new(Width, 20);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width;
            H = Height;

            Rectangle Base = new(0, 0, W, H);

            Graphics _with21 = G;
            _with21.SmoothingMode = SmoothingMode.HighQuality;
            _with21.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with21.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with21.Clear(BaseColor);

            //-- Base
            _with21.FillRectangle(new SolidBrush(BaseColor), Base);

            //-- Text
            _with21.DrawString(Text, Font, new SolidBrush(TextColor), new Rectangle(10, 4, W, H), ForeverLibrary.NearSF);

            //-- Rectangle
            _with21.FillRectangle(new SolidBrush(RectColor), new Rectangle(4, 4, 4, 14));

            //-- TimeDate
            if (ShowTimeDate)
            {
                string Time = GetTimeFormat(TimeFormat);
                _with21.DrawString(Time, Font, new SolidBrush(TimeColor), new Rectangle(-4, 2, W, H), new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                });
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        private void UpdateColors()
        {
            ForeverColors Colors = ForeverLibrary.GetColors(this);

            RectColor = Colors.Forever;
        }
    }

    #endregion
}