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
    #region HopeProgressBar

    public class HopeProgressBar : Control
    {
        #region Variable

        public enum Style
        {
            ToolTip = 0,
            ValueInSide = 1,
            ValueOutSide = 2
        }

        #endregion

        #region Settings

        public Style ProgressBarStyle
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Style.ToolTip;

        public bool IsError
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

        public int ValueNumber
        {
            get;
            set
            {
                field = value > 100 ? 100 : (value < 0 ? 0 : value);
                Invalidate();
            }
        } = 0;

        public Color DangerColor { get; set; } = HopeColors.Danger;
        public Color BaseColor { get; set; } = HopeColors.PrimaryColor;
        public Color FullBallonColor { get; set; } = HopeColors.Success;
        public Color FullBarColor { get; set; } = HopeColors.Success;
        public Color BarColor { get; set; } = HopeColors.OneLevelBorder;
        public string FullBallonText { get; set; } = "Ok!";

        #endregion

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            switch (ProgressBarStyle)
            {
                case Style.ToolTip:
                    Height = 32;
                    break;
                case Style.ValueInSide:
                    Height = 14;
                    break;
                case Style.ValueOutSide:
                    Height = 14;
                    break;
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(BackColor);

            Color tempColor = IsError ? DangerColor : BaseColor;

            switch (ProgressBarStyle)
            {
                case Style.ToolTip:
                    float x = (ValueNumber * (Width - 32) / 100) + 16f;
                    int y = 25;
                    graphics.FillPolygon(new SolidBrush(ValueNumber == 100 && !IsError ? FullBallonColor : tempColor), new PointF[]
                    {
                         new(x,y),new(x+5,y-5),new(x+16,y-5),new(x+16,y-25),new(x-16,y-25),new(x-16,y-5),new(x-5,y-5)
                    });
                    graphics.DrawString(ValueNumber != 100 ? ValueNumber.ToString() + "%" : FullBallonText, Font, new SolidBrush(ForeColor), new RectangleF(x - 16, y - 25, 32, 20), HopeStringAlign.Center);

                    graphics.FillRectangle(new SolidBrush(BarColor), new RectangleF(16, 25, Width - 32, Height - 25));
                    graphics.FillRectangle(new SolidBrush(ValueNumber == 100 && !IsError ? FullBarColor : tempColor), new RectangleF(16, 25, x - 16, Height - 25));
                    break;

                case Style.ValueInSide:
                    GraphicsPath path1 = new();
                    path1.AddArc(new RectangleF(0, 0, Height, Height), 90, 180);
                    path1.AddArc(new RectangleF(Width - Height, 0, Height, Height), -90, 180);
                    path1.CloseAllFigures();
                    graphics.FillPath(new SolidBrush(BarColor), path1);

                    if (ValueNumber == 0)
                    {
                        graphics.DrawString("0%", new Font("Segoe UI", 9f), new SolidBrush(ForeColor), new RectangleF(5, 0, 50, Height), HopeStringAlign.Left);
                    }
                    else
                    {
                        GraphicsPath path2 = new();
                        path2.AddArc(new RectangleF(0, 0, Height, Height), 90, 180);
                        path2.AddArc(new RectangleF(ValueNumber * (Width - Height) / 100, 0, Height, Height), -90, 180);
                        path2.CloseAllFigures();
                        graphics.FillPath(new SolidBrush(ValueNumber == 100 && !IsError ? FullBarColor : tempColor), path2);

                        graphics.DrawString(ValueNumber.ToString() + "%", new Font("Segoe UI", 9f), new SolidBrush(ForeColor), new RectangleF((ValueNumber * (Width - Height) / 100) - 33, 0, 45, Height), HopeStringAlign.Right);
                    }
                    break;
                case Style.ValueOutSide:
                    GraphicsPath path3 = new();
                    path3.AddArc(new RectangleF(0, 4, Height - 8, Height - 8), 90, 180);
                    path3.AddArc(new RectangleF(Width - 50, 4, Height - 8, Height - 8), -90, 180);
                    path3.CloseAllFigures();
                    graphics.FillPath(new SolidBrush(BarColor), path3);

                    if (ValueNumber != 0)
                    {
                        GraphicsPath path4 = new();
                        path4.AddArc(new RectangleF(0, 4, Height - 8, Height - 8), 90, 180);
                        path4.AddArc(new RectangleF(ValueNumber * (Width - 50) / 100, 4, Height - 8, Height - 8), -90, 180);
                        path4.CloseAllFigures();
                        graphics.FillPath(new SolidBrush(ValueNumber == 100 && !IsError ? FullBarColor : tempColor), path4);
                    }

                    if (IsError)
                    {
                        graphics.FillEllipse(new SolidBrush(DangerColor), new RectangleF(Width - 40, 0, Height, Height));
                        int a = Width - 40 + 4;
                        int b = Height - 4;
                        graphics.DrawLine(new(ForeColor), a, b - 6, a + 6, b);
                        graphics.DrawLine(new(ForeColor), a + 6, b - 6, a, b);
                    }
                    else
                    {
                        if (ValueNumber == 100)
                        {
                            graphics.FillEllipse(new SolidBrush(FullBarColor), new RectangleF(Width - 40, 0, Height, Height));
                            int a = Width - 40 + 4;
                            int b = Height - 4;
                            graphics.DrawLine(new(ForeColor), a, b - 3, a + 3, b);
                            graphics.DrawLine(new(ForeColor), a + 3, b, a + 6, b - 6);
                        }
                        else
                        {
                            graphics.DrawString(ValueNumber.ToString() + "%", new Font("Segoe UI", 10f), new SolidBrush(ForeColor), new RectangleF(Width - 40, 0, 50, Height), HopeStringAlign.Left);
                        }
                    }
                    break;
            }
        }

        public HopeProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 10);
            ForeColor = HopeColors.FourLevelBorder;
        }
    }

    #endregion
}