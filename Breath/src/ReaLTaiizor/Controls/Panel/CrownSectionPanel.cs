#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownSectionPanel

    public class CrownSectionPanel : System.Windows.Forms.Panel
    {
        #region Field Region

        private string _sectionHeader;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding => base.Padding;

        [Category("Appearance")]
        [Description("The section header text associated with this control.")]
        public string SectionHeader
        {
            get => _sectionHeader;
            set
            {
                _sectionHeader = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor Region

        public CrownSectionPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            base.Padding = new Padding(1, 25, 1, 1);
        }

        #endregion

        #region Event Handler Region

        protected override void OnEnter(System.EventArgs e)
        {
            base.OnEnter(e);

            Invalidate();
        }

        protected override void OnLeave(System.EventArgs e)
        {
            base.OnLeave(e);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (Controls.Count > 0)
            {
                Controls[0].Focus();
            }
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = ClientRectangle;

            // Fill body
            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, rect);
            }

            // Draw header
            Color bgColor = ContainsFocus ? ThemeProvider.Theme.Colors.BlueBackground : ThemeProvider.Theme.Colors.HeaderBackground;
            Color darkColor = ContainsFocus ? ThemeProvider.Theme.Colors.DarkBlueBorder : ThemeProvider.Theme.Colors.DarkBorder;
            Color lightColor = ContainsFocus ? ThemeProvider.Theme.Colors.LightBlueBorder : ThemeProvider.Theme.Colors.LightBorder;

            using (SolidBrush b = new(bgColor))
            {
                Rectangle bgRect = new(0, 0, rect.Width, 25);
                g.FillRectangle(b, bgRect);
            }

            using (Pen p = new(darkColor))
            {
                g.DrawLine(p, rect.Left, 0, rect.Right, 0);
                g.DrawLine(p, rect.Left, 25 - 1, rect.Right, 25 - 1);
            }

            using (Pen p = new(lightColor))
            {
                g.DrawLine(p, rect.Left, 1, rect.Right, 1);
            }

            int xOffset = 3;

            using (SolidBrush b = new(ThemeProvider.Theme.Colors.LightText))
            {
                Rectangle textRect = new(xOffset, 0, rect.Width - 4 - xOffset, 25);

                StringFormat format = new()
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(SectionHeader, Font, b, textRect, format);
            }

            // Draw border
            using (Pen p = new(ThemeProvider.Theme.Colors.DarkBorder, 1))
            {
                Rectangle modRect = new(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

                g.DrawRectangle(p, modRect);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Absorb event
        }

        #endregion
    }

    #endregion
}