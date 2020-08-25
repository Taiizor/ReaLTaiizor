#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeToggle

    public class HopeToggle : System.Windows.Forms.CheckBox
    {
        #region Variables

        Timer AnimationTimer = new Timer { Interval = 1 };
        int PointAnimationNum = 4;

        private Color _BaseColor = Color.FromArgb(44, 55, 66);
        private Color _BaseColorA = HopeColors.OneLevelBorder;
        private Color _BaseColorB = Color.FromArgb(100, HopeColors.PrimaryColor);
        private Color _HeadColorA = HopeColors.OneLevelBorder;
        private Color _HeadColorB = Color.White;
        private Color _HeadColorC = HopeColors.PrimaryColor;
        private Color _HeadColorD = HopeColors.PrimaryColor;

        #endregion

        #region Settings
        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }
        public Color BaseColorA
        {
            get { return _BaseColorA; }
            set { _BaseColorA = value; }
        }
        public Color BaseColorB
        {
            get { return _BaseColorB; }
            set { _BaseColorB = value; }
        }
        public Color HeadColorA
        {
            get { return _HeadColorA; }
            set { _HeadColorA = value; }
        }
        public Color HeadColorB
        {
            get { return _HeadColorB; }
            set { _HeadColorB = value; }
        }
        public Color HeadColorC
        {
            get { return _HeadColorC; }
            set { _HeadColorC = value; }
        }
        public Color HeadColorD
        {
            get { return _HeadColorD; }
            set { _HeadColorD = value; }
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
            graphics.Clear(BaseColor);

            var roundRectangle = new GraphicsPath();
            var radius = 9;
            roundRectangle.AddArc(11, 5, radius - 1, radius, 180, 90);
            roundRectangle.AddArc(Width - 21, 5, radius - 1, radius, -90, 90);
            roundRectangle.AddArc(Width - 21, Height - 14, radius - 1, radius, 0, 90);
            roundRectangle.AddArc(11, Height - 14, radius - 1, radius, 90, 90);
            roundRectangle.CloseAllFigures();

            graphics.FillPath(new SolidBrush(Checked ? _BaseColorB : _BaseColorA), roundRectangle);

            graphics.FillEllipse(new SolidBrush(Checked ? _HeadColorC : _HeadColorA), new RectangleF(PointAnimationNum, 1, 18, 18));
            graphics.FillEllipse(new SolidBrush(Checked ? _HeadColorD : _HeadColorB), new RectangleF(PointAnimationNum + 2, 3, 14, 14));
        }

        public HopeToggle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 20; Width = 47;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
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