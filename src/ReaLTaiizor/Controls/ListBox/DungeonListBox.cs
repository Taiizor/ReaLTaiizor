#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonListBox

    public class DungeonListBox : System.Windows.Forms.ListBox
    {

        public DungeonListBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            IntegralHeight = false;
            ItemHeight = 18;
            Font = new("Seoge UI", 11, FontStyle.Regular);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            e.DrawBackground();
            LinearGradientBrush LGB = new(e.Bounds, Color.FromArgb(246, 132, 85), Color.FromArgb(231, 108, 57), 90.0F);
            if (Convert.ToInt32(e.State & DrawItemState.Selected) == (int)DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(LGB, e.Bounds);
            }

            using (SolidBrush b = new(e.ForeColor))
            {
                if (base.Items.Count == 0)
                {
                    return;
                }
                else
                {
                    e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), e.Font, b, e.Bounds);
                }
            }

            LGB.Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Region MyRegion = new(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(BackColor), MyRegion);

            if (Items.Count > 0)
            {
                for (int i = 0; i <= Items.Count - 1; i++)
                {
                    System.Drawing.Rectangle RegionRect = GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(RegionRect))
                    {
                        if ((SelectionMode == SelectionMode.One && SelectedIndex == i) || (SelectionMode == SelectionMode.MultiSimple && SelectedIndices.Contains(i)) || (SelectionMode == SelectionMode.MultiExtended && SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, Font, RegionRect, i, DrawItemState.Selected, ForeColor, BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, Font, RegionRect, i, DrawItemState.Default, Color.FromArgb(60, 60, 60), BackColor));
                        }

                        MyRegion.Complement(RegionRect);
                    }
                }
            }
        }
    }

    #endregion
}