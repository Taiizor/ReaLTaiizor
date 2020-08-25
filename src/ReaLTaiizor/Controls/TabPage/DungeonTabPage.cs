#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonTabPage

    public class DungeonTabPage : System.Windows.Forms.TabControl
    {
        private Color _BaseColor = Color.Transparent;
        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        private Color _DeactivePageTextColor = Color.FromArgb(80, 76, 76);
        public Color DeactivePageTextColor
        {
            get { return _DeactivePageTextColor; }
            set { _DeactivePageTextColor = value; }
        }

        private Color _PageEdgeColor = Color.FromArgb(247, 246, 246);
        public Color PageEdgeColor
        {
            get { return _PageEdgeColor; }
            set { _PageEdgeColor = value; }
        }

        private Color _PageEdgeBorderColor = Color.FromArgb(201, 198, 195);
        public Color PageEdgeBorderColor
        {
            get { return _PageEdgeBorderColor; }
            set { _PageEdgeBorderColor = value; }
        }

        private Color _ActivePageBorderColor = Color.FromArgb(201, 198, 195);
        public Color ActivePageBorderColor
        {
            get { return _ActivePageBorderColor; }
            set { _ActivePageBorderColor = value; }
        }

        private Color _ActivePageBackColor = Color.FromArgb(247, 246, 246);
        public Color ActivePageBackColor
        {
            get { return _ActivePageBackColor; }
            set { _ActivePageBackColor = value; }
        }

        private Color _PageBackColor = Color.FromArgb(247, 246, 246);
        public Color PageBackColor
        {
            get { return _PageBackColor; }
            set { _PageBackColor = value; }
        }

        private Color _ActivePageTextColor = Color.FromArgb(80, 76, 76);
        public Color ActivePageTextColor
        {
            get { return _ActivePageTextColor; }
            set { _ActivePageTextColor = value; }
        }

        public DungeonTabPage()
        {
            SetStyle((ControlStyles)(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint), true);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            ItemSize = new Size(80, 24);
            Alignment = TabAlignment.Top;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle ItemBoundsRect = new Rectangle();
            G.Clear(_BaseColor);
            for (int TabIndex = 0; TabIndex <= TabCount - 1; TabIndex++)
            {
                ItemBoundsRect = GetTabRect(TabIndex);
                if (!(TabIndex == SelectedIndex))
                {
                    G.DrawString(TabPages[TabIndex].Text, new Font(Font.Name, Font.Size - 2, FontStyle.Bold), new SolidBrush(_DeactivePageTextColor), new Rectangle(GetTabRect(TabIndex).Location, GetTabRect(TabIndex).Size), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                }
            }

            // Draw container rectangle
            G.FillPath(new SolidBrush(_PageEdgeColor), RoundRectangle.RoundRect(0, 23, Width - 1, Height - 24, 2));
            G.DrawPath(new Pen(_PageEdgeBorderColor), RoundRectangle.RoundRect(0, 23, Width - 1, Height - 24, 2));

            for (int ItemIndex = 0; ItemIndex <= TabCount - 1; ItemIndex++)
            {
                ItemBoundsRect = GetTabRect(ItemIndex);
                if (ItemIndex == SelectedIndex)
                {

                    // Draw header tabs
                    G.DrawPath(new Pen(_ActivePageBorderColor), RoundRectangle.RoundedTopRect(new Rectangle(new Point(ItemBoundsRect.X - 2, ItemBoundsRect.Y - 2), new Size(ItemBoundsRect.Width + 3, ItemBoundsRect.Height)), 7));
                    G.FillPath(new SolidBrush(_ActivePageBackColor), RoundRectangle.RoundedTopRect(new Rectangle(new Point(ItemBoundsRect.X - 1, ItemBoundsRect.Y - 1), new Size(ItemBoundsRect.Width + 2, ItemBoundsRect.Height)), 7));

                    try
                    {
                        G.DrawString(TabPages[ItemIndex].Text, new Font(Font.Name, Font.Size - 1, FontStyle.Bold), new SolidBrush(_ActivePageTextColor), new Rectangle(GetTabRect(ItemIndex).Location, GetTabRect(ItemIndex).Size), new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        });
                        TabPages[ItemIndex].BackColor = _PageBackColor;
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