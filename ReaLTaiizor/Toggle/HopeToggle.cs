#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeToggle

    public class HopeToggle : System.Windows.Forms.CheckBox
    {
        #region Variables

        Timer AnimationTimer = new Timer { Interval = 1 };
        int PointAnimationNum = 4;

        #endregion

        #region Settings
        #endregion

        #region Events

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Height = 20; Width = 48;
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            var roundRectangle = new GraphicsPath();
            var radius = 9;
            roundRectangle.AddArc(11, 5, radius - 1, radius, 180, 90);
            roundRectangle.AddArc(Width - 21, 5, radius - 1, radius, -90, 90);
            roundRectangle.AddArc(Width - 21, Height - 14, radius - 1, radius, 0, 90);
            roundRectangle.AddArc(11, Height - 14, radius - 1, radius, 90, 90);
            roundRectangle.CloseAllFigures();

            graphics.FillPath(new SolidBrush(Checked ? Color.FromArgb(100, BackColor) : HopeColors.OneLevelBorder), roundRectangle);

            graphics.FillEllipse(new SolidBrush(Checked ? BackColor : HopeColors.OneLevelBorder), new RectangleF(PointAnimationNum, 1, 18, 18));
            graphics.FillEllipse(new SolidBrush(Checked ? BackColor : Color.White), new RectangleF(PointAnimationNum + 2, 3, 14, 14));
        }

        public HopeToggle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 20; Width = 47;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            BackColor = HopeColors.PrimaryColor;
            Cursor = Cursors.Hand;
        }

        void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (PointAnimationNum < 24)
                {
                    PointAnimationNum += 2;
                    Invalidate();
                }
            }
            else if (PointAnimationNum > 4)
            {
                PointAnimationNum -= 2;
                Invalidate();
            }
        }
    }

    #endregion
}