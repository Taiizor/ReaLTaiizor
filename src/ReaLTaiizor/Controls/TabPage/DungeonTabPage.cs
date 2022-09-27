#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonTabPage

    public class DungeonTabPage : TabControl
    {
        public Color BaseColor { get; set; } = Color.Transparent;
        public Color DeactivePageTextColor { get; set; } = Color.FromArgb(80, 76, 76);
        public Color PageEdgeColor { get; set; } = Color.FromArgb(247, 246, 246);
        public Color PageEdgeBorderColor { get; set; } = Color.FromArgb(201, 198, 195);
        public Color ActivePageBorderColor { get; set; } = Color.FromArgb(201, 198, 195);
        public Color ActivePageBackColor { get; set; } = Color.FromArgb(247, 246, 246);
        public Color PageBackColor { get; set; } = Color.FromArgb(247, 246, 246);
        public Color ActivePageTextColor { get; set; } = Color.FromArgb(80, 76, 76);

        public DungeonTabPage()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            ItemSize = new(80, 24);
            Alignment = TabAlignment.Top;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle ItemBoundsRect = new();
            G.Clear(BaseColor);
            for (int TabIndex = 0; TabIndex <= TabCount - 1; TabIndex++)
            {
                ItemBoundsRect = GetTabRect(TabIndex);
                if (!(TabIndex == SelectedIndex))
                {
                    G.DrawString(TabPages[TabIndex].Text, new Font(Font.Name, Font.Size - 2, FontStyle.Bold), new SolidBrush(DeactivePageTextColor), new Rectangle(GetTabRect(TabIndex).Location, GetTabRect(TabIndex).Size), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                }
            }

            // Draw container rectangle
            G.FillPath(new SolidBrush(PageEdgeColor), RoundRectangle.RoundRect(0, 23, Width - 1, Height - 24, 2));
            G.DrawPath(new(PageEdgeBorderColor), RoundRectangle.RoundRect(0, 23, Width - 1, Height - 24, 2));

            for (int ItemIndex = 0; ItemIndex <= TabCount - 1; ItemIndex++)
            {
                ItemBoundsRect = GetTabRect(ItemIndex);
                if (ItemIndex == SelectedIndex)
                {

                    // Draw header tabs
                    G.DrawPath(new(ActivePageBorderColor), RoundRectangle.RoundedTopRect(new Rectangle(new Point(ItemBoundsRect.X - 2, ItemBoundsRect.Y - 2), new Size(ItemBoundsRect.Width + 3, ItemBoundsRect.Height)), 7));
                    G.FillPath(new SolidBrush(ActivePageBackColor), RoundRectangle.RoundedTopRect(new Rectangle(new Point(ItemBoundsRect.X - 1, ItemBoundsRect.Y - 1), new Size(ItemBoundsRect.Width + 2, ItemBoundsRect.Height)), 7));

                    try
                    {
                        G.DrawString(TabPages[ItemIndex].Text, new Font(Font.Name, Font.Size - 1, FontStyle.Bold), new SolidBrush(ActivePageTextColor), new Rectangle(GetTabRect(ItemIndex).Location, GetTabRect(ItemIndex).Size), new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        });
                        TabPages[ItemIndex].BackColor = PageBackColor;
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    #endregion
}