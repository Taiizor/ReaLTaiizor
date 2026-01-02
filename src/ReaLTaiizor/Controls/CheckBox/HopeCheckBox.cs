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
    #region HopeCheckBox

    public class HopeCheckBox : System.Windows.Forms.CheckBox
    {
        #region Variables
        private readonly Timer AnimationTimer = new() { Interval = 15 };
        private int SizeAnimationNum = 14;
        private int PointAnimationNum = 3;
        private bool enterFlag = false;
        #endregion

        #region Settings
        public bool Enable
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        public Color EnabledCheckedColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HopeColors.PrimaryColor;

        public Color EnabledUncheckedColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#9c9ea1");

        public Color DisabledColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#c4c6ca");

        public Color EnabledStringColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#999999");

        public Color DisabledStringColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#babbbd");

        public Color CheckedColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HopeColors.PrimaryColor;
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
            Width = 25 + TextRenderer.MeasureText(Text, Font).Width;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Height = 20;
            //Width = 25 + (int)CreateGraphics().MeasureString(Text, Font).Width;
            Width = 25 + TextRenderer.MeasureText(Text, Font).Width;
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            enterFlag = true;

            if (Enable)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }

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

            GraphicsPath checkmarkPath = RoundRectangle.CreateRoundRect(2, 2, 16, 16, 1);
            //Rectangle checkMarkLine = new(3, 3, 14, 14);

            SolidBrush BG = new(Enable ? (Checked || enterFlag ? EnabledCheckedColor : EnabledUncheckedColor) : DisabledColor);
            Pen Pen = new(BG.Color);

            graphics.FillPath(BG, checkmarkPath);
            graphics.DrawPath(Pen, checkmarkPath);

            graphics.DrawLines(new(Color.White, 2), new PointF[]
            {
                new(5, 9),new(9, 13), new(15, 6)
            });

            graphics.FillRectangle(new SolidBrush(Color.White), PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum);

            graphics.DrawString(Text, Font, new SolidBrush(Enable ? (Checked ? CheckedColor : ForeColor) : DisabledStringColor), new RectangleF(22, 0, Width - 22, Height), HopeStringAlign.Center);
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
            Font = new("Segoe UI", 12);
            ForeColor = HopeColors.MainText;
            Size = new(147, 20);
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}