#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonComboBox

    public class DungeonComboBox : ComboBox
    {
        #region Variables

        private int _StartIndex = 0;
        private Color _HoverSelectionColor; // VBConversions Note: Initial value cannot be assigned here since it is non-static. Assignment has been moved to the class constructors.

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

        public Color ColorA { get; set; } = Color.FromArgb(246, 132, 85);

        public Color ColorB { get; set; } = Color.FromArgb(231, 108, 57);

        public Color ColorC { get; set; } = Color.FromArgb(242, 241, 240);

        public Color ColorD { get; set; } = Color.FromArgb(253, 252, 252);

        public Color ColorE { get; set; } = Color.FromArgb(239, 237, 236);

        public Color ColorF { get; set; } = Color.FromArgb(180, 180, 180);

        public Color ColorG { get; set; } = Color.FromArgb(119, 119, 118);

        public Color ColorH { get; set; } = Color.FromArgb(224, 222, 220);

        public Color ColorI { get; set; } = Color.FromArgb(250, 249, 249);

        #endregion

        #region EventArgs

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            LinearGradientBrush LGB = new(e.Bounds, ColorA, ColorB, 90.0F);

            if (Convert.ToInt32(e.State & DrawItemState.Selected) == (int)DrawItemState.Selected)
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(LGB, e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.WhiteSmoke, e.Bounds);
                }
            }
            else
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(new SolidBrush(ColorC), e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.DimGray, e.Bounds);
                }
            }
            LGB.Dispose();
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!Focused)
            {
                SelectionLength = 0;
            }
        }

        #endregion

        public DungeonComboBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;

            BackColor = Color.FromArgb(246, 246, 246);
            ForeColor = Color.FromArgb(76, 76, 97);
            Size = new(135, 26);
            ItemHeight = 20;
            DropDownHeight = 100;
            Font = new("Segoe UI", 10, FontStyle.Regular);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            LinearGradientBrush LGB = default;
            GraphicsPath GP = default;

            e.Graphics.Clear(Parent.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create a curvy border
            GP = RoundRectangle.RoundRect(0, 0, Width - 1, Height - 1, 5);
            // Fills the body of the rectangle with a gradient
            LGB = new(ClientRectangle, ColorD, ColorE, 90.0F);

            e.Graphics.SetClip(GP);
            e.Graphics.FillRectangle(LGB, ClientRectangle);
            e.Graphics.ResetClip();

            // Draw rectangle border
            e.Graphics.DrawPath(new(ColorF), GP);
            // Draw string
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(3, 0, Width - 20, Height), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            });
            e.Graphics.DrawString("6", new Font("Marlett", 13, FontStyle.Regular), new SolidBrush(ColorG), new Rectangle(3, 0, Width - 4, Height), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Far
            });
            e.Graphics.DrawLine(new(ColorH), Width - 24, 4, Width - 24, Height - 5);
            e.Graphics.DrawLine(new(ColorI), Width - 25, 4, Width - 25, Height - 5);

            GP.Dispose();
            LGB.Dispose();
        }
    }

    #endregion
}