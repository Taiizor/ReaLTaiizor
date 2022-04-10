#region Imports

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ProgressIndicator

    public class ProgressIndicator : Control
    {
        #region Variables

        private readonly SolidBrush BaseColor = new(Color.DarkGray);
        private readonly SolidBrush AnimationColor = new(Color.DimGray);

        private readonly Timer AnimationSpeed = new();
        private PointF[] FloatPoint;
        private BufferedGraphics BuffGraphics;
        private int IndicatorIndex;
        private readonly BufferedGraphicsContext GraphicsContext = BufferedGraphicsManager.Current;

        #endregion

        #region Custom Properties

        public Color P_BaseColor
        {
            get => BaseColor.Color;
            set => BaseColor.Color = value;
        }

        public Color P_AnimationColor
        {
            get => AnimationColor.Color;
            set => AnimationColor.Color = value;
        }

        public int P_AnimationSpeed
        {
            get => AnimationSpeed.Interval;
            set => AnimationSpeed.Interval = value;
        }

        #endregion

        #region EventArgs

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetStandardSize();
            UpdateGraphics();
            SetPoints();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            AnimationSpeed.Enabled = Enabled;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationSpeed.Tick += AnimationSpeed_Tick;
            AnimationSpeed.Start();
        }

        private void AnimationSpeed_Tick(object sender, EventArgs e)
        {
            if (IndicatorIndex.Equals(0))
            {
                IndicatorIndex = FloatPoint.Length - 1;
            }
            else
            {
                IndicatorIndex -= 1;
            }

            Invalidate(false);
        }

        #endregion

        public ProgressIndicator()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);

            Size = new(80, 80);
            Text = string.Empty;
            MinimumSize = new(50, 50);
            SetPoints();
            AnimationSpeed.Interval = 100;
        }

        private void SetStandardSize()
        {
            int _Size = Math.Max(Width, Height);
            Size = new(_Size, _Size);
        }

        private void SetPoints()
        {
            Stack<PointF> stack = new();
            PointF startingFloatPoint = new(Width / 2f, Height / 2f);
            for (float i = 0f; i < 360f; i += 45f)
            {
                SetValue(startingFloatPoint, (int)Math.Round((double)((Width / 2.0) - 15.0)), (double)i);
                PointF endPoint = EndPoint;
                endPoint = new(endPoint.X - 7.5f, endPoint.Y - 7.5f);
                stack.Push(endPoint);
            }
            FloatPoint = stack.ToArray();
        }

        private void UpdateGraphics()
        {
            if (Width > 0 && Height > 0)
            {
                Size size2 = new(Width + 1, Height + 1);
                GraphicsContext.MaximumBuffer = size2;
                BuffGraphics = GraphicsContext.Allocate(CreateGraphics(), ClientRectangle);
                BuffGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BuffGraphics.Graphics.Clear(BackColor);
            int num2 = FloatPoint.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                if (IndicatorIndex == i)
                {
                    BuffGraphics.Graphics.FillEllipse(AnimationColor, FloatPoint[i].X, FloatPoint[i].Y, 15f, 15f);
                }
                else
                {
                    BuffGraphics.Graphics.FillEllipse(BaseColor, FloatPoint[i].X, FloatPoint[i].Y, 15f, 15f);
                }
            }
            BuffGraphics.Render(e.Graphics);
        }

        private double Rise;
        private double Run;
        private PointF _StartingFloatPoint;

        private X AssignValues<X>(ref X Run, X Length)
        {
            Run = Length;
            return Length;
        }

        private void SetValue(PointF StartingFloatPoint, int Length, double Angle)
        {
            double CircleRadian = Math.PI * Angle / 180.0;

            _StartingFloatPoint = StartingFloatPoint;
            Rise = AssignValues(ref Run, Length);
            Rise = Math.Sin(CircleRadian) * Rise;
            Run = Math.Cos(CircleRadian) * Run;
        }

        private PointF EndPoint
        {
            get
            {
                float LocationX = Convert.ToSingle(_StartingFloatPoint.Y + Rise);
                float LocationY = Convert.ToSingle(_StartingFloatPoint.X + Run);

                return new PointF(LocationY, LocationX);
            }
        }
    }

    #endregion
}