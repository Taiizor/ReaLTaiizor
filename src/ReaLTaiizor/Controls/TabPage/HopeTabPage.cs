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
    #region HopeTabPage

    public class HopeTabPage : TabControl
    {
        #region Variables
        private int enterIndex;
        private bool enterFlag = false;
        #endregion

        #region Settings
        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighQuality;

        public PixelOffsetMode PixelOffsetType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = PixelOffsetMode.HighQuality;

        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

        public Color BaseColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(44, 55, 66);

        public Color ThemeColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HopeColors.PrimaryColor;

        public Color ThemeColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(150, HopeColors.PrimaryColor);

        public Color ForeColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Silver;

        public Color ForeColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Gray;

        public Color ForeColorC
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(150, Color.White);

        public enum TextState
        {
            Upper,
            Lower,
            Normal
        }

        public TextState TitleTextState
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextState.Normal;

        #endregion

        #region Functions
        private string TitleText(string Text)
        {
            return TitleTextState switch
            {
                TextState.Upper => Text.ToUpperInvariant(),
                TextState.Lower => Text.ToLowerInvariant(),
                _ => Text,
            };
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
                Rectangle tempRect = GetTabRect(i);
                if (tempRect.Contains(e.Location))
                {
                    enterIndex = i;
                }
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
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingType;
            graphics.PixelOffsetMode = PixelOffsetType;
            graphics.TextRenderingHint = TextRenderingType;
            graphics.Clear(BaseColor);

            for (int i = 0; i < TabCount; i++)
            {
                if (i == SelectedIndex)
                {
                    graphics.FillRectangle(new SolidBrush(ThemeColorA), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                    graphics.DrawString(TitleText(TabPages[i].Text), Font, new SolidBrush(ForeColorA), GetTabRect(i), HopeStringAlign.Center);
                }
                else
                {
                    if (i == enterIndex && enterFlag)
                    {
                        graphics.FillRectangle(new SolidBrush(ThemeColorB), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                        graphics.DrawString(TitleText(TabPages[i].Text), Font, new SolidBrush(ForeColorC), GetTabRect(i), HopeStringAlign.Center);
                    }
                    else
                    {
                        graphics.DrawString(TitleText(TabPages[i].Text), Font, new SolidBrush(ForeColorB), GetTabRect(i), HopeStringAlign.Center);
                    }
                }
            }
        }

        public HopeTabPage()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12F);
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new(120, 40);
        }
    }

    #endregion
}