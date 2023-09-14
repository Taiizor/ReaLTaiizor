#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeRoundPrgoressBar

    public class HopeRoundProgressBar : Control
    {

        private readonly int tempValue = 0;
        private int _valueNumber = 0;
        public int ValueNumber
        {
            get => _valueNumber;
            set
            {
                _valueNumber = value > 100 ? 100 : (value < 0 ? 0 : value);
                Invalidate();
            }
        }

        private readonly float _roundWidth = 6;

        private bool _isError = false;
        public bool IsError
        {
            get => _isError;
            set
            {
                _isError = value;
                Invalidate();
            }
        }

        public string PercentText { get; set; } = "%";
        public Color BorderColor { get; set; } = HopeColors.OneLevelBorder;
        public Color DangerColor { get; set; } = HopeColors.Danger;
        public Color DangerTextColorA { get; set; } = HopeColors.Danger;
        public Color DangerTextColorB { get; set; } = HopeColors.Danger;
        public Color FullTextColorA { get; set; } = HopeColors.Success;
        public Color FullTextColorB { get; set; } = HopeColors.Success;
        public Color BarColor { get; set; } = HopeColors.PrimaryColor;
        public Color FullBarColor { get; set; } = HopeColors.Success;

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Width = Height;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            graphics.FillEllipse(new SolidBrush(BorderColor), new Rectangle(0, 0, Width, Height));

            if (_isError)
            {
                graphics.FillPie(new SolidBrush(DangerColor), new Rectangle(0, 0, Width, Width), 0, _valueNumber * 3.6f);

                graphics.FillEllipse(new SolidBrush(BackColor), new RectangleF(_roundWidth, _roundWidth, Width - (_roundWidth * 2), Width - (_roundWidth * 2)));
                graphics.DrawLine(new(DangerTextColorA, 2f), (Width / 2) - 6, (Height / 2) - 6, (Width / 2) + 6, (Height / 2) + 6);
                graphics.DrawLine(new(DangerTextColorB, 2f), (Width / 2) - 6, (Height / 2) + 6, (Width / 2) + 6, (Height / 2) - 6);
            }
            else
            {
                if (_valueNumber == 100)
                {
                    graphics.FillPie(new SolidBrush(FullBarColor), new Rectangle(0, 0, Width, Width), 0, _valueNumber * 3.6f);
                    graphics.FillEllipse(new SolidBrush(BackColor), new RectangleF(_roundWidth, _roundWidth, Width - (_roundWidth * 2), Width - (_roundWidth * 2)));
                    graphics.DrawLine(new(FullTextColorA, 2f), (Width / 2) - 6, Height / 2, (Width / 2) - 3, (Height / 2) + 6);
                    graphics.DrawLine(new(FullTextColorB, 2f), (Width / 2) + 6, (Height / 2) - 6, (Width / 2) - 3, (Height / 2) + 6);
                }
                else
                {
                    graphics.FillPie(new SolidBrush(BarColor), new Rectangle(0, 0, Width, Width), 0, _valueNumber * 3.6f);
                    graphics.FillEllipse(new SolidBrush(BackColor), new RectangleF(_roundWidth, _roundWidth, Width - (_roundWidth * 2), Width - (_roundWidth * 2)));
                    graphics.DrawString(_valueNumber.ToString() + PercentText, Font, new SolidBrush(ForeColor), new RectangleF(_roundWidth, _roundWidth, Width - (_roundWidth * 2), Width - (_roundWidth * 2)), HopeStringAlign.Center);
                }
            }
        }

        public HopeRoundProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12f);
            BackColor = Color.White;
            ForeColor = HopeColors.PrimaryColor;
        }
    }

    #endregion
}