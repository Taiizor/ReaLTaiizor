#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeTabPage

    public class HopeTabPage : TabControl
    {
        #region Variables
        int enterIndex;
        bool enterFlag = false;

        #endregion

        #region Settings

        private Color _themeColor = HopeColors.PrimaryColor;
        public Color ThemeColor
        {
            get { return _themeColor; }
            set
            {
                _themeColor = value;
                Invalidate();
            }
        }
        #endregion


        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 4, rect.Top - 4, rect.Width + 8, rect.Height + 8);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            enterFlag = true;
            for (int i = 0; i < TabCount; i++)
            {
                var tempRect = GetTabRect(i);
                if (tempRect.Contains(e.Location))
                    enterIndex = i;
            }
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            enterFlag = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            for (int i = 0; i < TabCount; i++)
            {
                if (i == SelectedIndex)
                {
                    graphics.FillRectangle(new SolidBrush(_themeColor), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                    graphics.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(_themeColor), GetTabRect(i), HopeStringAlign.Center);
                }
                else
                {
                    if (i == enterIndex && enterFlag)
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, _themeColor)), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);

                    graphics.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(Color.Black), GetTabRect(i), HopeStringAlign.Center);
                }
            }
        }

        public HopeTabPage()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12F);
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(120, 40);
        }
    }

    #endregion
}