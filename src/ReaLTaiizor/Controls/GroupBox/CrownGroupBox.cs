#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownGroupBox

    public class CrownGroupBox : System.Windows.Forms.GroupBox
    {
        #region Properties

        private Color _borderColor = ThemeProvider.Theme.Colors.DarkBorder;

        [Category("Appearance")]
        [Description("Determines the color of the border.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor

        public CrownGroupBox()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );

            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        #endregion

        #region Event

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            SizeF stringSize = g.MeasureString(Text, Font);

            Color textColor = ThemeProvider.Theme.Colors.LightText;
            Color fillColor = ThemeProvider.Theme.Colors.GreyBackground;

            using (SolidBrush b = new SolidBrush(fillColor))
            {
                g.FillRectangle(b, rect);
            }

            using (Pen p = new Pen(BorderColor, 1))
            {
                Rectangle borderRect = new Rectangle(0, (int)stringSize.Height / 2, rect.Width - 1, rect.Height - ((int)stringSize.Height / 2) - 1);
                g.DrawRectangle(p, borderRect);
            }

            Rectangle textRect = new Rectangle(rect.Left + ThemeProvider.Theme.Sizes.Padding, rect.Top, rect.Width - (ThemeProvider.Theme.Sizes.Padding * 2), (int)stringSize.Height);

            using (SolidBrush b2 = new SolidBrush(fillColor))
            {
                Rectangle modRect = new Rectangle(textRect.Left, textRect.Top, Math.Min(textRect.Width, (int)stringSize.Width), textRect.Height);
                g.FillRectangle(b2, modRect);
            }

            using (SolidBrush b = new SolidBrush(textColor))
            {
                StringFormat stringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(Text, Font, b, textRect, stringFormat);
            }
        }

        #endregion
    }

    #endregion
}