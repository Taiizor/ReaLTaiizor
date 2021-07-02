#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ComboBoxEdit

    public class ComboBoxEdit : ComboBox
    {

        #region Variables

        private int _StartIndex = 0;
        private Color _HoverSelectionColor = Color.FromArgb(241, 241, 241);

        #endregion
        #region Custom Properties

        public int StartIndex
        {
            get => _StartIndex;
            set
            {
                _StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch
                {
                }
                Invalidate();
            }
        }

        public Color HoverSelectionColor
        {
            get => _HoverSelectionColor;
            set
            {
                _HoverSelectionColor = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(_HoverSelectionColor), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            if (!(e.Index == -1))
            {
                e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.DimGray, e.Bounds);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            SuspendLayout();
            Update();
            ResumeLayout();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        #endregion

        public ComboBoxEdit()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;

            Cursor = Cursors.Hand;

            BackColor = Color.FromArgb(246, 246, 246);
            ForeColor = Color.FromArgb(142, 142, 142);
            Size = new(135, 26);
            ItemHeight = 20;
            DropDownHeight = 100;
            Font = new("Segoe UI", 10, FontStyle.Regular);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            LinearGradientBrush LGB = default;
            GraphicsPath GP = default;

            e.Graphics.Clear(BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create a curvy border
            GP = RoundRectangle.RoundRect(0, 0, Width - 1, Height - 1, 5);
            // Fills the body of the rectangle with a gradient
            LGB = new(ClientRectangle, Color.FromArgb(241, 241, 241), Color.FromArgb(241, 241, 241), 90f);

            e.Graphics.SetClip(GP);
            e.Graphics.FillRectangle(LGB, ClientRectangle);
            e.Graphics.ResetClip();

            // Draw rectangle border
            e.Graphics.DrawPath(new(Color.FromArgb(204, 204, 204)), GP);
            // Draw string
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(3, 0, Width - 20, Height), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            });

            // Draw the dropdown arrow
            e.Graphics.DrawLine(new(Color.FromArgb(160, 160, 160), 2), new Point(Width - 18, 10), new Point(Width - 14, 14));
            e.Graphics.DrawLine(new(Color.FromArgb(160, 160, 160), 2), new Point(Width - 14, 14), new Point(Width - 10, 10));
            e.Graphics.DrawLine(new(Color.FromArgb(160, 160, 160)), new Point(Width - 14, 15), new Point(Width - 14, 14));

            GP.Dispose();
            LGB.Dispose();
        }
    }

    #endregion
}