#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeRadioButton

    public class HopeRadioButton : System.Windows.Forms.RadioButton
    {
        #region Variables
        Color EnabledCheckedColor;
        Color EnabledUnCheckedColor = ColorTranslator.FromHtml("#9c9ea1");
        Color DisabledColor = ColorTranslator.FromHtml("#c4c6ca");
        Color EnabledStringColor = ColorTranslator.FromHtml("#929292");
        Color DisabledStringColor = ColorTranslator.FromHtml("#babbbd");
        int SizeAnimationNum = 0;
        int PointAnimationNum = 10;
        Timer SizeAnimationTimer = new Timer { Interval = 35 };
        bool enterFalg = false;
        #endregion

        private Color _checkedColor = HopeColors.PrimaryColor;

        #region Settings
        public Color CheckedColor
        {
            get { return _checkedColor; }
            set
            {
                _checkedColor = value;
                Invalidate();
            }
        }
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
            Width = 25 + (int)CreateGraphics().MeasureString(Text, Font).Width;
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            Height = 20;
            Width = 25 + (int)CreateGraphics().MeasureString(Text, Font).Width;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Height = 20;
            Width = 25 + (int)CreateGraphics().MeasureString(Text, Font).Width;
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            enterFalg = true;
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
            graphics.Clear(Parent.BackColor);

            Rectangle BGEllipse = new Rectangle(1, 1, 18, 18);
            EnabledCheckedColor = _checkedColor;
            SolidBrush BG = new SolidBrush(Enabled ? (Checked || enterFalg ? EnabledCheckedColor : EnabledUnCheckedColor) : DisabledColor);

            graphics.FillEllipse(BG, BGEllipse);
            graphics.FillEllipse(new SolidBrush(Color.White), new Rectangle(3, 3, 14, 14));

            graphics.FillEllipse(BG, new Rectangle(PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum));

            graphics.DrawString(Text, Font, new SolidBrush(Checked ? _checkedColor : ForeColor), new RectangleF(22, 0, Width - 22, Height), HopeStringAlign.Center);
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
            Font = new Font("Segoe UI", 12);
            ForeColor = Color.Black;
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}