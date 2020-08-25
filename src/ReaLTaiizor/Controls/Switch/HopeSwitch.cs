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
    #region HopeSwitch

    public class HopeSwitch : System.Windows.Forms.CheckBox
    {
        #region Variables

        Timer AnimationTimer = new Timer { Interval = 1 };
        int PointAnimationNum = 3;

        Color _BaseColor = Color.White;
        Color _BaseOnColor = HopeColors.PrimaryColor;
        Color _BaseOffColor = HopeColors.OneLevelBorder;

        #endregion

        #region Settings

        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        public Color BaseOnColor
        {
            get { return _BaseOnColor; }
            set { _BaseOnColor = value; }
        }

        public Color BaseOffColor
        {
            get { return _BaseOffColor; }
            set { _BaseOffColor = value; }
        }

        #endregion

        #region Events

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Height = 20; Width = 40;
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.Clear(Parent.BackColor);

            var backRect = new GraphicsPath();
            backRect.AddArc(new RectangleF(0.5f, 0.5f, Height - 1, Height - 1), 90, 180);
            backRect.AddArc(new RectangleF(Width - Height + 0.5f, 0.5f, Height - 1, Height - 1), 270, 180);
            backRect.CloseAllFigures();

            graphics.FillPath(new SolidBrush(Checked ? _BaseOnColor : _BaseOffColor), backRect);
            graphics.FillEllipse(new SolidBrush(_BaseColor), new RectangleF(PointAnimationNum, 2, 16, 16));
        }

        public HopeSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 20; Width = 42;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            Cursor = Cursors.Hand;
        }

        void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (PointAnimationNum < 21)
                {
                    PointAnimationNum += 2;
                    Invalidate();
                }
            }
            else if (PointAnimationNum > 3)
            {
                PointAnimationNum -= 2;
                Invalidate();
            }
        }
    }

    #endregion
}