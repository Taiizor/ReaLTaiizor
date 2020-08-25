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
    #region HopeCheckBox

    public class HopeCheckBox : System.Windows.Forms.CheckBox
    {
        #region Variables
        Color _EnabledCheckedColor = HopeColors.PrimaryColor;
        Color _EnabledUncheckedColor = ColorTranslator.FromHtml("#9c9ea1");
        Color _DisabledColor = ColorTranslator.FromHtml("#c4c6ca");
        Color _EnabledStringColor = ColorTranslator.FromHtml("#999999");
        Color _DisabledStringColor = ColorTranslator.FromHtml("#babbbd");
        Color _CheckedColor = HopeColors.PrimaryColor;
        Timer AnimationTimer = new Timer { Interval = 15 };
        int SizeAnimationNum = 14;
        int PointAnimationNum = 3;
        bool enterFlag = false;
        bool _Enable = true;
        #endregion

        #region Settings
        public bool Enable
        {
            get { return _Enable; }
            set
            {
                _Enable = value;
                Invalidate();
            }
        }

        public Color EnabledCheckedColor
        {
            get { return _EnabledCheckedColor; }
            set
            {
                _EnabledCheckedColor = value;
                Invalidate();
            }
        }

        public Color EnabledUncheckedColor
        {
            get { return _EnabledUncheckedColor; }
            set
            {
                _EnabledUncheckedColor = value;
                Invalidate();
            }
        }

        public Color DisabledColor
        {
            get { return _DisabledColor; }
            set
            {
                _DisabledColor = value;
                Invalidate();
            }
        }

        public Color EnabledStringColor
        {
            get { return _EnabledStringColor; }
            set
            {
                _EnabledStringColor = value;
                Invalidate();
            }
        }

        public Color DisabledStringColor
        {
            get { return _DisabledStringColor; }
            set
            {
                _DisabledStringColor = value;
                Invalidate();
            }
        }

        public Color CheckedColor
        {
            get { return _CheckedColor; }
            set
            {
                _CheckedColor = value;
                Invalidate();
            }
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
            Height = 20;
            //Width = 25 + (int)CreateGraphics().MeasureString(Text, Font).Width;
            Width = 25 + (int)TextRenderer.MeasureText(Text, Font).Width;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Height = 20;
            //Width = 25 + (int)CreateGraphics().MeasureString(Text, Font).Width;
            Width = 25 + (int)TextRenderer.MeasureText(Text, Font).Width;
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            enterFlag = true;

            if (_Enable)
                Cursor = Cursors.Hand;
            else
                Cursor = Cursors.Default;

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            enterFlag = false;
            Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(BackColor);

            var checkmarkPath = RoundRectangle.CreateRoundRect(2, 2, 16, 16, 1);
            var checkMarkLine = new Rectangle(3, 3, 14, 14);

            SolidBrush BG = new SolidBrush(_Enable ? (Checked || enterFlag ? _EnabledCheckedColor : _EnabledUncheckedColor) : _DisabledColor);
            Pen Pen = new Pen(BG.Color);

            graphics.FillPath(BG, checkmarkPath);
            graphics.DrawPath(Pen, checkmarkPath);

            graphics.DrawLines(new Pen(Color.White, 2), new PointF[]
            {
                new PointF(5, 9),new PointF(9, 13), new PointF(15, 6)
            });
            graphics.FillRectangle(new SolidBrush(Color.White), PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum);

            graphics.DrawString(Text, Font, new SolidBrush(_Enable ? (Checked ? _CheckedColor : ForeColor) : _DisabledStringColor), new RectangleF(22, 0, Width - 22, Height), HopeStringAlign.Center);
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (SizeAnimationNum > 0)
                {
                    SizeAnimationNum -= 2;
                    PointAnimationNum += 1;
                    Invalidate();
                }
            }
            else
            {
                if (SizeAnimationNum < 14)
                {
                    SizeAnimationNum += 2;
                    PointAnimationNum -= 1;
                    Invalidate();
                }
            }
        }

        public HopeCheckBox()
        {
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);
            ForeColor = HopeColors.MainText;
            Size = new Size(147, 20);
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}