#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region  DungeonListBox

    public class DungeonListBox : ListBox
    {

        public DungeonListBox()
        {
            this.SetStyle((ControlStyles)(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint), true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            IntegralHeight = false;
            ItemHeight = 18;
            Font = new Font("Seoge UI", 11, FontStyle.Regular);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            e.DrawBackground();
            LinearGradientBrush LGB = new LinearGradientBrush(e.Bounds, Color.FromArgb(246, 132, 85), Color.FromArgb(231, 108, 57), 90.0F);
            if (Convert.ToInt32((e.State & DrawItemState.Selected)) == (int)DrawItemState.Selected)
                e.Graphics.FillRectangle(LGB, e.Bounds);
            using (SolidBrush b = new SolidBrush(e.ForeColor))
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
            Region MyRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), MyRegion);

            if (this.Items.Count > 0)
            {
                for (int i = 0; i <= this.Items.Count - 1; i++)
                {
                    System.Drawing.Rectangle RegionRect = this.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(RegionRect))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i) || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i)) || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, RegionRect, i, DrawItemState.Selected, this.ForeColor, this.BackColor));
                        else
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, RegionRect, i, DrawItemState.Default, Color.FromArgb(60, 60, 60), this.BackColor));
                        MyRegion.Complement(RegionRect);
                    }
                }
            }
        }
    }

    #endregion
}