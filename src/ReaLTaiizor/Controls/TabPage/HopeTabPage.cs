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
        private Color _baseColor = Color.FromArgb(44, 55, 66);
        private Color _themeColorA = HopeColors.PrimaryColor;
        private Color _themeColorB = Color.FromArgb(150, HopeColors.PrimaryColor);
        private Color _foreColorA = Color.Silver;
        private Color _foreColorB = Color.Gray;
        private Color _foreColorC = Color.FromArgb(150, Color.White);

        private TextState _TitleTextState = TextState.Normal;
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        private TextRenderingHint _TextRenderingType = TextRenderingHint.ClearTypeGridFit;
        #endregion

        #region Settings
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public PixelOffsetMode PixelOffsetType
        {
            get => _PixelOffsetType;
            set
            {
                _PixelOffsetType = value;
                Invalidate();
            }
        }

        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        public Color BaseColor
        {
            get => _baseColor;
            set
            {
                _baseColor = value;
                Invalidate();
            }
        }

        public Color ThemeColorA
        {
            get => _themeColorA;
            set
            {
                _themeColorA = value;
                Invalidate();
            }
        }

        public Color ThemeColorB
        {
            get => _themeColorB;
            set
            {
                _themeColorB = value;
                Invalidate();
            }
        }

        public Color ForeColorA
        {
            get => _foreColorA;
            set
            {
                _foreColorA = value;
                Invalidate();
            }
        }

        public Color ForeColorB
        {
            get => _foreColorB;
            set
            {
                _foreColorB = value;
                Invalidate();
            }
        }

        public Color ForeColorC
        {
            get => _foreColorC;
            set
            {
                _foreColorC = value;
                Invalidate();
            }
        }

        public enum TextState
        {
            Upper,
            Lower,
            Normal
        }

        public TextState TitleTextState
        {
            get => _TitleTextState;
            set
            {
                _TitleTextState = value;
                Invalidate();
            }
        }

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
            graphics.Clear(_baseColor);

            for (int i = 0; i < TabCount; i++)
            {
                if (i == SelectedIndex)
                {
                    graphics.FillRectangle(new SolidBrush(_themeColorA), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                    graphics.DrawString(TitleText(TabPages[i].Text), Font, new SolidBrush(_foreColorA), GetTabRect(i), HopeStringAlign.Center);
                }
                else
                {
                    if (i == enterIndex && enterFlag)
                    {
                        graphics.FillRectangle(new SolidBrush(_themeColorB), GetTabRect(i).X + 3, ItemSize.Height - 3, ItemSize.Width - 6, 3);
                        graphics.DrawString(TitleText(TabPages[i].Text), Font, new SolidBrush(_foreColorC), GetTabRect(i), HopeStringAlign.Center);
                    }
                    else
                    {
                        graphics.DrawString(TitleText(TabPages[i].Text), Font, new SolidBrush(_foreColorB), GetTabRect(i), HopeStringAlign.Center);
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