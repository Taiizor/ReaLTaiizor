#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

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

        private Style _style = Style.ToolTip;
        public Style ProgressBarStyle
        {
            get { return _style; }
            set
            {
                _style = value;
                Invalidate();
            }
        }

        private bool _isError = false;
        public bool IsError
        {
            get { return _isError; }
            set
            {
                _isError = value;
                Invalidate();
            }
        }

        private int _valueNumber = 0;
        public int ValueNumber
        {
            get { return _valueNumber; }
            set
            {
                _valueNumber = value > 100 ? 100 : (value < 0 ? 0 : value);
                Invalidate();
            }
        }

        private Color _DangerColor = HopeColors.Danger;
        public Color DangerColor
        {
            get { return _DangerColor; }
            set { _DangerColor = value; }
        }

        private Color _BaseColor = HopeColors.PrimaryColor;
        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        private Color _FullBallonColor = HopeColors.Success;
        public Color FullBallonColor
        {
            get { return _FullBallonColor; }
            set { _FullBallonColor = value; }
        }

        private Color _FullBarColor = HopeColors.Success;
        public Color FullBarColor
        {
            get { return _FullBarColor; }
            set { _FullBarColor = value; }
        }

        private Color _BarColor = HopeColors.OneLevelBorder;
        public Color BarColor
        {
            get { return _BarColor; }
            set { _BarColor = value; }
        }

        private string _FullBallonText = "Ok!";
        public string FullBallonText
        {
            get { return _FullBallonText; }
            set { _FullBallonText = value; }
        }

        #endregion

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            switch (_style)
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

            var tempColor = _isError ? _DangerColor : _BaseColor;

            switch (_style)
            {
                case Style.ToolTip:
                    var x = _valueNumber * (Width - 32) / 100 + 16f;
                    var y = 25;
                    graphics.FillPolygon(new SolidBrush(_valueNumber == 100 && !_isError ? _FullBallonColor : tempColor), new PointF[]
                    {
                         new PointF(x,y),new PointF(x+5,y-5),new PointF(x+16,y-5),new PointF(x+16,y-25),new PointF(x-16,y-25),new PointF(x-16,y-5),new PointF(x-5,y-5)
                    });
                    graphics.DrawString(_valueNumber != 100 ? _valueNumber.ToString() + "%" : _FullBallonText, Font, new SolidBrush(ForeColor), new RectangleF(x - 16, y - 25, 32, 20), HopeStringAlign.Center);

                    graphics.FillRectangle(new SolidBrush(_BarColor), new RectangleF(16, 25, Width - 32, Height - 25));
                    graphics.FillRectangle(new SolidBrush(_valueNumber == 100 && !_isError ? _FullBarColor : tempColor), new RectangleF(16, 25, x - 16, Height - 25));
                    break;

                case Style.ValueInSide:
                    var path1 = new GraphicsPath();
                    path1.AddArc(new RectangleF(0, 0, Height, Height), 90, 180);
                    path1.AddArc(new RectangleF(Width - Height, 0, Height, Height), -90, 180);
                    path1.CloseAllFigures();
                    graphics.FillPath(new SolidBrush(_BarColor), path1);

                    if (_valueNumber == 0)
                        graphics.DrawString("0%", new Font("Segoe UI", 9f), new SolidBrush(ForeColor), new RectangleF(5, 0, 50, Height), HopeStringAlign.Left);
                    else
                    {
                        var path2 = new GraphicsPath();
                        path2.AddArc(new RectangleF(0, 0, Height, Height), 90, 180);
                        path2.AddArc(new RectangleF(_valueNumber * (Width - Height) / 100, 0, Height, Height), -90, 180);
                        path2.CloseAllFigures();
                        graphics.FillPath(new SolidBrush(_valueNumber == 100 && !_isError ? _FullBarColor : tempColor), path2);

                        graphics.DrawString(_valueNumber.ToString() + "%", new Font("Segoe UI", 9f), new SolidBrush(ForeColor), new RectangleF(_valueNumber * (Width - Height) / 100 - 33, 0, 45, Height), HopeStringAlign.Right);
                    }
                    break;
                case Style.ValueOutSide:
                    var path3 = new GraphicsPath();
                    path3.AddArc(new RectangleF(0, 4, Height - 8, Height - 8), 90, 180);
                    path3.AddArc(new RectangleF(Width - 50, 4, Height - 8, Height - 8), -90, 180);
                    path3.CloseAllFigures();
                    graphics.FillPath(new SolidBrush(_BarColor), path3);

                    if (_valueNumber != 0)
                    {
                        var path4 = new GraphicsPath();
                        path4.AddArc(new RectangleF(0, 4, Height - 8, Height - 8), 90, 180);
                        path4.AddArc(new RectangleF(_valueNumber * (Width - 50) / 100, 4, Height - 8, Height - 8), -90, 180);
                        path4.CloseAllFigures();
                        graphics.FillPath(new SolidBrush(_valueNumber == 100 && !_isError ? _FullBarColor : tempColor), path4);
                    }

                    if (_isError)
                    {
                        graphics.FillEllipse(new SolidBrush(_DangerColor), new RectangleF(Width - 40, 0, Height, Height));
                        var a = Width - 40 + 4;
                        var b = Height - 4;
                        graphics.DrawLine(new Pen(ForeColor), a, b - 6, a + 6, b);
                        graphics.DrawLine(new Pen(ForeColor), a + 6, b - 6, a, b);
                    }
                    else
                    {
                        if (_valueNumber == 100)
                        {
                            graphics.FillEllipse(new SolidBrush(_FullBarColor), new RectangleF(Width - 40, 0, Height, Height));
                            var a = Width - 40 + 4;
                            var b = Height - 4;
                            graphics.DrawLine(new Pen(ForeColor), a, b - 3, a + 3, b);
                            graphics.DrawLine(new Pen(ForeColor), a + 3, b, a + 6, b - 6);
                        }
                        else
                            graphics.DrawString(_valueNumber.ToString() + "%", new Font("Segoe UI", 10f), new SolidBrush(ForeColor), new RectangleF(Width - 40, 0, 50, Height), HopeStringAlign.Left);
                    }
                    break;
            }
        }

        public HopeProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 10);
            ForeColor = HopeColors.FourLevelBorder;
        }
    }

    #endregion
}