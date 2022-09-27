#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightButton

    public class NightButton : Control, IButtonControl
    {
        #region Fields

        private int _Radius = 20;

        private readonly Timer animationTimer;
        private int buttonGlow;
        private int stringGlow;
        private bool hoverButton;

        private int mouseState;

        private Color _NormalBackColor = ColorTranslator.FromHtml("#F25D59");
        private Color _PressedBackColor = Color.FromArgb(100, ColorTranslator.FromHtml("#F25D59"));
        private Color _HoverBackColor = Color.FromArgb(50, ColorTranslator.FromHtml("#F25D59"));
        private Color _PressedForeColor = Color.White;
        private Color _HoverForeColor = Color.White;

        private float margin, width, height;
        private Rectangle stringRect;
        private RectangleF buttonRect;
        private GraphicsPath roundRectPath;

        private InterpolationMode _InterpolationType = InterpolationMode.HighQualityBicubic;
        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;

        #endregion

        #region Properties

        [Browsable(true)]
        [Description("Sets the radius of curvature for the control.")]
        public int Radius
        {
            get => _Radius;
            set
            {
                if (value is not (< 1 or > 20))
                {
                    _Radius = value;
                }
                else
                {
                    throw new Exception("The entered value cannot be less than 1 or greater than 20.");
                }

                Invalidate();
            }
        }

        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }
        public InterpolationMode InterpolationType
        {
            get => _InterpolationType;
            set
            {
                _InterpolationType = value;
                Invalidate();
            }
        }

        public PixelOffsetMode PixelOffsetType
        {
            get => _PixelOffsetType;
            set
            {
                _PixelOffsetType = value;
                Invalidate();
            }
        }

        public Color PressedForeColor
        {
            get => _PressedForeColor;
            set
            {
                _PressedForeColor = value;
                Invalidate();
            }
        }

        public Color HoverForeColor
        {
            get => _HoverForeColor;
            set
            {
                _HoverForeColor = value;
                Invalidate();
            }
        }

        public Color NormalBackColor
        {
            get => _NormalBackColor;
            set
            {
                _NormalBackColor = value;
                Invalidate();
            }
        }

        public Color PressedBackColor
        {
            get => _PressedBackColor;
            set
            {
                _PressedBackColor = value;
                Invalidate();
            }
        }

        public Color HoverBackColor
        {
            get => _HoverBackColor;
            set
            {
                _HoverBackColor = value;
                Invalidate();
            }
        }

        #endregion

        #region IButtonControl

        private DialogResult dlgResult;

        [Browsable(false)]
        private bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DialogResult DialogResult
        {
            get => dlgResult;
            set
            {
                if (System.Enum.IsDefined(typeof(DialogResult), value))
                {
                    dlgResult = value;
                }
            }
        }

        public void NotifyDefault(bool value)
        {
            IsDefault = value;
        }

        public void PerformClick()
        {
            if (CanSelect) { OnClick(EventArgs.Empty); }
        }

        #endregion

        #region Create Round Rectangle

        // Snippet by RodStephens
        private GraphicsPath RoundedRect(RectangleF rect, float x_radius, float y_radius, bool round_upperLeft, bool round_upperRight, bool round_lowerRight, bool round_lowerLeft)
        {
            PointF point1, point2;
            GraphicsPath path = new();

            // Upper left corner
            if (round_upperLeft)
            {
                RectangleF corner = new(rect.X, rect.Y, 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 180, 90);
                point1 = new(rect.X + x_radius, rect.Y);
            }
            else
            {
                point1 = new(rect.X, rect.Y);
            }

            // Top side
            if (round_upperRight)
            {
                point2 = new(rect.Right - x_radius, rect.Y);
            }
            else
            {
                point2 = new(rect.Right, rect.Y);
                path.AddLine(point1, point2);
            }

            // Upper right corner
            if (round_upperRight)
            {
                RectangleF corner = new(rect.Right - (2 * x_radius), rect.Y, 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 270, 90);
                point1 = new(rect.Right, rect.Y + y_radius);
            }
            else
            {
                point1 = new(rect.Right, rect.Y);
            }

            // Right side
            if (round_lowerRight)
            {
                point2 = new(rect.Right, rect.Bottom - y_radius);
            }
            else
            {
                point2 = new(rect.Right, rect.Bottom);
                path.AddLine(point1, point2);
            }

            // Lower right corner
            if (round_lowerRight)
            {
                RectangleF corner = new(rect.Right - (2 * x_radius), rect.Bottom - (2 * y_radius), 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 0, 90);
                point1 = new(rect.Right - x_radius, rect.Bottom);
            }
            else
            {
                point1 = new(rect.Right, rect.Bottom);
            }

            // Bottom side
            if (round_lowerLeft)
            {
                point2 = new(rect.X + x_radius, rect.Bottom);
            }
            else
            {
                point2 = new(rect.X, rect.Bottom);
                path.AddLine(point1, point2);
            }

            // Lower left corner
            if (round_lowerLeft)
            {
                RectangleF corner = new(rect.X, rect.Bottom - (2 * y_radius), 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 90, 90);
                point1 = new(rect.X, rect.Bottom - y_radius);
            }
            else
            {
                point1 = new(rect.X, rect.Bottom);
            }

            // Left side
            if (round_upperLeft)
            {
                point2 = new(rect.X, rect.Y + y_radius);
            }
            else
            {
                point2 = new(rect.X, rect.Y);
                path.AddLine(point1, point2);
            }

            path.CloseFigure();
            return path;
        }

        #endregion

        #region EventArgs

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseState = 0;
            Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseState = 1;
            Invalidate();
            Focus();
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            mouseState = 3;
            hoverButton = true;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            mouseState = 0;
            hoverButton = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            animationTimer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            if (Width > 0 && Height > 0)
            {
                stringRect = new(0, 0, Width, Height);
            }

            Invalidate();
            base.OnResize(e);
        }

        private void OnAnimation(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            if (hoverButton)
            {
                if (buttonGlow < 242)
                {
                    buttonGlow += 15;
                }

                if (stringGlow < 160)
                {
                    stringGlow += 15;
                }
            }
            else
            {
                if (buttonGlow >= 15)
                {
                    buttonGlow -= 15;
                }

                if (stringGlow >= 15)
                {
                    stringGlow -= 15;
                }
            }

            Invalidate();
        }

        #endregion

        #region Native Hand Cursor

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == NativeConstants.WM_SETCURSOR)
            {
                IntPtr cursor = NativeMethods.LoadCursor(IntPtr.Zero, NativeConstants.IDC_HAND);
                NativeMethods.SetCursor(cursor);

                msg.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref msg);
        }

        #endregion

        public NightButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new("Segoe UI", 10);
            ForeColor = ColorTranslator.FromHtml("#F25D59");
            Size = new(144, 47);
            MinimumSize = new(144, 47);
            Cursor = Cursors.Hand;

            animationTimer = new Timer { Interval = 1 };
            animationTimer.Tick += OnAnimation;
        }

        private void FillButton(Graphics g)
        {
            using SolidBrush animBrush = new(Color.FromArgb(buttonGlow, ForeColor));
            g.FillPath(animBrush, roundRectPath);
        }

        private void DrawButton(Graphics g)
        {
            Color penColor = Color.Empty;
            Color brushColor = Color.Empty;

            switch (mouseState)
            {
                case 0: // Inactive state
                    penColor = NormalBackColor;
                    brushColor = ForeColor;
                    break;
                case 1: // Pressed state
                    penColor = PressedBackColor;
                    brushColor = PressedForeColor;
                    break;
                case 3: // Hover state
                    penColor = HoverBackColor;
                    brushColor = Color.FromArgb(80 + stringGlow, HoverForeColor);
                    break;
            }

            using Pen pathPen = new(penColor, 2f);
            using SolidBrush stringBrush = new(brushColor);
            using
                StringFormat sf = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
            g.DrawPath(pathPen, roundRectPath);
            g.DrawString(Text, Font, stringBrush, stringRect, sf);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = _SmoothingType;
            g.InterpolationMode = _InterpolationType;
            g.PixelOffsetMode = _PixelOffsetType;

            margin = 3;
            width = ClientSize.Width - (2 * margin);
            height = ClientSize.Height - 6;

            buttonRect = new(margin, margin, width, height);
            roundRectPath = RoundedRect(buttonRect, _Radius, _Radius, false, true, true, false);

            FillButton(g);
            DrawButton(g);

            base.OnPaint(e);
        }
    }

    #endregion
}