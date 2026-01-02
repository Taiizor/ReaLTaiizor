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
    #region HopeRadioButton

    public class HopeRadioButton : System.Windows.Forms.RadioButton
    {
        #region Variables
        private int SizeAnimationNum = 0;
        private int PointAnimationNum = 10;
        private readonly Timer SizeAnimationTimer = new() { Interval = 35 };
        private bool enterFalg = false;
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
        } = ColorTranslator.FromHtml("#929292");

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
            SizeAnimationTimer.Start();
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
            enterFalg = true;

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
            enterFalg = false;
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

            Rectangle BGEllipse = new(1, 1, 18, 18);
            SolidBrush BG = new(Enable ? (Checked || enterFalg ? EnabledCheckedColor : EnabledUncheckedColor) : DisabledColor);

            graphics.FillEllipse(BG, BGEllipse);
            graphics.FillEllipse(new SolidBrush(Color.White), new Rectangle(3, 3, 14, 14));

            graphics.FillEllipse(BG, new Rectangle(PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum));

            graphics.DrawString(Text, Font, new SolidBrush(Enable ? (Checked ? CheckedColor : ForeColor) : DisabledStringColor), new RectangleF(22, 0, Width - 22, Height), HopeStringAlign.Center);
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (SizeAnimationNum < 8)
                {
                    SizeAnimationNum += 2;
                    PointAnimationNum -= 1;
                    Invalidate();
                }
            }
            else if (SizeAnimationNum != 0)
            {
                SizeAnimationNum -= 2;
                PointAnimationNum += 1;
                Invalidate();
            }
        }

        public HopeRadioButton()
        {
            SizeAnimationTimer.Tick += new EventHandler(AnimationTick);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            ForeColor = Color.Black;
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}