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
    #region HopeTabPage

    public class HopeTabPage : System.Windows.Forms.TabControl
    {
        #region Variables
        int enterIndex;
        bool enterFlag = false;
        private Color _baseColor = Color.FromArgb(44, 55, 66);
        private Color _themeColorA = HopeColors.PrimaryColor;
        private Color _themeColorB = Color.FromArgb(150, HopeColors.PrimaryColor);
        private Color _foreColorA = Color.Silver;
        private Color _foreColorB = Color.Black;
        private Color _foreColorC = Color.FromArgb(150, Color.White);
        #endregion

        #region Settings
        public Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                _baseColor = value;
                Invalidate();
            }
        }
        public Color ThemeColorA
        {
            get { return _themeColorA; }
            set
            {
                _themeColorA = value;
                Invalidate();
            }
        }
        public Color ThemeColorB
        {
            get { return _themeColorB; }
            set
            {
                _themeColorB = value;
                Invalidate();
            }
        }
        public Color ForeColorA
        {
            get { return _foreColorA; }
            set
            {
                _foreColorA = value;
                Invalidate();
            }
        }
        public Color ForeColorB
        {
            get { return _foreColorB; }
            set
            {
                _foreColorB = value;
                Invalidate();
            }
        }
        public Color ForeColorC
        {
            get { return _foreColorC; }
            set
            {
                _foreColorC = value;
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
            graphics.Clear(_baseColor);

            for (int i = 0; i < TabCount; i++)
            {
                if (i == SelectedIndex)
                {
                    graphics.FillRectangle(new SolidBrush(_themeColorA), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                    graphics.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(_foreColorA), GetTabRect(i), HopeStringAlign.Center);
                }
                else
                {
                    if (i == enterIndex && enterFlag)
                    {
                        graphics.FillRectangle(new SolidBrush(_themeColorB), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                        graphics.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(_foreColorC), GetTabRect(i), HopeStringAlign.Center);
                    }
                    else
                        graphics.DrawString(TabPages[i].Text.ToUpper(), Font, new SolidBrush(_foreColorB), GetTabRect(i), HopeStringAlign.Center);
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