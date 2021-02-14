#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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
            Rectangle rect = new(0, 0, ClientSize.Width, ClientSize.Height);
            SizeF stringSize = g.MeasureString(Text, Font);

            Color textColor = ThemeProvider.Theme.Colors.LightText;
            Color fillColor = ThemeProvider.Theme.Colors.GreyBackground;

            using (SolidBrush b = new(fillColor))
            {
                g.FillRectangle(b, rect);
            }

            using (Pen p = new(BorderColor, 1))
            {
                Rectangle borderRect = new(0, (int)stringSize.Height / 2, rect.Width - 1, rect.Height - ((int)stringSize.Height / 2) - 1);
                g.DrawRectangle(p, borderRect);
            }

            Rectangle textRect = new(rect.Left + ThemeProvider.Theme.Sizes.Padding, rect.Top, rect.Width - (ThemeProvider.Theme.Sizes.Padding * 2), (int)stringSize.Height);

            using (SolidBrush b2 = new(fillColor))
            {
                Rectangle modRect = new(textRect.Left, textRect.Top, Math.Min(textRect.Width, (int)stringSize.Width), textRect.Height);
                g.FillRectangle(b2, modRect);
            }

            using (SolidBrush b = new(textColor))
            {
                StringFormat stringFormat = new()
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