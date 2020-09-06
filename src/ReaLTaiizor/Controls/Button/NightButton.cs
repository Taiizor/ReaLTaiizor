﻿#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightButton

    public class NightButton : Control, IButtonControl
    {
        #region Fields

        private readonly Timer animationTimer;
        private int buttonGlow;
        private int stringGlow;
        private bool hoverButton;

        private int mouseState;

        private float margin, width, height;
        private Rectangle stringRect;
        private RectangleF buttonRect;
        private GraphicsPath roundRectPath;

        #endregion

        #region Properties

        [Browsable(false)]
        [Description("The foreground color of this component, which is used to display text.")]
        public override Color ForeColor { get; set; }

        private int _Radius = 20;
        [Browsable(true)]
        [Description("Sets the radius of curvature for the control.")]
        public int Radius
        {
            get { return _Radius; }
            set
            {
                if (!(value < 1 || value > 20))
                    _Radius = value;
                else
                    throw new Exception("The entered value cannot be less than 1 or greater than 20.");

                Invalidate();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        public SmoothingMode SmoothingType
        {
            get { return _SmoothingType; }
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private InterpolationMode _InterpolationType = InterpolationMode.HighQualityBicubic;
        public InterpolationMode InterpolationType
        {
            get { return _InterpolationType; }
            set
            {
                _InterpolationType = value;
                Invalidate();
            }
        }

        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        public PixelOffsetMode PixelOffsetType
        {
            get { return _PixelOffsetType; }
            set
            {
                _PixelOffsetType = value;
                Invalidate();
            }
        }

        #endregion

        #region IButtonControl

        private bool _IsDefault;
        private DialogResult dlgResult;

        [Browsable(false)]
        private bool IsDefault
        {
            get { return _IsDefault; }
        }

        /// </summary>
        public DialogResult DialogResult
        {
            get { return dlgResult; }
            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                    dlgResult = value;
            }
        }

        public void NotifyDefault(bool value)
        {
            _IsDefault = value;
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
            var path = new GraphicsPath();

            // Upper left corner
            if (round_upperLeft)
            {
                var corner = new RectangleF(rect.X, rect.Y, 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 180, 90);
                point1 = new PointF(rect.X + x_radius, rect.Y);
            }
            else
                point1 = new PointF(rect.X, rect.Y);

            // Top side
            if (round_upperRight)
                point2 = new PointF(rect.Right - x_radius, rect.Y);
            else
            {
                point2 = new PointF(rect.Right, rect.Y);
                path.AddLine(point1, point2);
            }

            // Upper right corner
            if (round_upperRight)
            {
                var corner = new RectangleF(rect.Right - 2 * x_radius, rect.Y, 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 270, 90);
                point1 = new PointF(rect.Right, rect.Y + y_radius);
            }
            else
                point1 = new PointF(rect.Right, rect.Y);

            // Right side
            if (round_lowerRight)
                point2 = new PointF(rect.Right, rect.Bottom - y_radius);
            else
            {
                point2 = new PointF(rect.Right, rect.Bottom);
                path.AddLine(point1, point2);
            }

            // Lower right corner
            if (round_lowerRight)
            {
                var corner = new RectangleF(rect.Right - 2 * x_radius, rect.Bottom - 2 * y_radius, 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 0, 90);
                point1 = new PointF(rect.Right - x_radius, rect.Bottom);
            }
            else
                point1 = new PointF(rect.Right, rect.Bottom);

            // Bottom side
            if (round_lowerLeft)
                point2 = new PointF(rect.X + x_radius, rect.Bottom);
            else
            {
                point2 = new PointF(rect.X, rect.Bottom);
                path.AddLine(point1, point2);
            }

            // Lower left corner
            if (round_lowerLeft)
            {
                var corner = new RectangleF(rect.X, rect.Bottom - 2 * y_radius, 2 * x_radius, 2 * y_radius);
                path.AddArc(corner, 90, 90);
                point1 = new PointF(rect.X, rect.Bottom - y_radius);
            }
            else
                point1 = new PointF(rect.X, rect.Bottom);

            // Left side
            if (round_upperLeft)
                point2 = new PointF(rect.X, rect.Y + y_radius);
            else
            {
                point2 = new PointF(rect.X, rect.Y);
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
                stringRect = new Rectangle(0, 0, Width, Height);

            Invalidate();
            base.OnResize(e);
        }

        private void OnAnimation(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (hoverButton)
            {
                if (buttonGlow < 242) { buttonGlow += 15; }
                if (stringGlow < 160) { stringGlow += 15; }
            }
            else
            {
                if (buttonGlow >= 15) { buttonGlow -= 15; }
                if (stringGlow >= 15) { stringGlow -= 15; }
            }

            Invalidate();
        }

        #endregion

        public NightButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 10);
            ForeColor = ColorTranslator.FromHtml("#F25D59");
            Size = new Size(144, 47);
            MinimumSize = new Size(144, 47);
            Cursor = Cursors.Hand;

            animationTimer = new Timer { Interval = 1 };
            animationTimer.Tick += OnAnimation;
        }

        #region Native Hand Cursor

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == NativeConstants.WM_SETCURSOR)
            {
                var cursor = NativeMethods.LoadCursor(IntPtr.Zero, NativeConstants.IDC_HAND);
                NativeMethods.SetCursor(cursor);

                msg.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref msg);
        }

        #endregion

        private void FillButton(Graphics g)
        {
            using (var animBrush = new SolidBrush(Color.FromArgb(buttonGlow, ForeColor)))
                g.FillPath(animBrush, roundRectPath);
        }

        private void DrawButton(Graphics g)
        {
            var penColor = Color.Empty;
            var brushColor = Color.Empty;

            switch (mouseState)
            {
                case 0: // Inactive state
                    penColor = ForeColor;
                    brushColor = ForeColor;
                    break;
                case 1: // Pressed state
                    penColor = ForeColor;
                    brushColor = Color.White;
                    break;
                case 3: // Hover state
                    penColor = ForeColor;
                    brushColor = Color.FromArgb(80 + stringGlow, Color.White);
                    break;
            }

            using (var pathPen = new Pen(penColor, 2f))
            using (var stringBrush = new SolidBrush(brushColor))
            using
			(
				var sf = new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center
				}
			)
            {
                g.DrawPath(pathPen, roundRectPath);
                g.DrawString(Text, Font, stringBrush, stringRect, sf);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = _SmoothingType;
            g.InterpolationMode = _InterpolationType;
            g.PixelOffsetMode = _PixelOffsetType;

            margin = 3;
            width = ClientSize.Width - 2 * margin;
            height = ClientSize.Height - 6;

            buttonRect = new RectangleF(margin, margin, width, height);
            roundRectPath = RoundedRect(buttonRect, _Radius, _Radius, false, true, true, false);

            FillButton(g);
            DrawButton(g);

            base.OnPaint(e);
        }
    }

    #endregion
}