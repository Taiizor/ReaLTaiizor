#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalListBox

    public class RoyalListBox : Control
    {
        RoyalScrollBar scrollBar;

        RoyalListBoxItemCollection items;
        public RoyalListBoxItemCollection Items
        {
            get { return items; }
        }

        RoyalListBoxSelectedItemCollection selectedItems;
        public RoyalListBoxSelectedItemCollection SelectedItems
        {
            get { return selectedItems; }
        }

        RoyalListBoxSelectedIndexCollection selectedIndicies;
        public RoyalListBoxSelectedIndexCollection SelectedIndicies
        {
            get { return selectedIndicies; }
        }

        bool multiSelection;
        public bool MultiSelection
        {
            get { return multiSelection; }
            set { multiSelection = value; Invalidate(); }
        }

        bool multiSelectKeyDown = false;

        int itemHeight;
        public int ItemHeight
        {
            get { return itemHeight; }
            set { itemHeight = value; Invalidate(); }
        }

        Color hotLightColor;
        public Color HotLightColor
        {
            get { return hotLightColor; }
            set { hotLightColor = value; Invalidate(); }
        }

        Color selectedColor;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; Invalidate(); }
        }

        int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; Invalidate(); }
        }

        public object SelectedValue
        {
            get { return null; }
        }

        int hotLightedIndex;
        public int HotLightedIndex
        {
            get { return hotLightedIndex; }
            set { hotLightedIndex = value; Invalidate(); }
        }

        public object HotLightedItem
        {
            get { return null; }
        }

        public RoyalListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            selectedIndicies = new RoyalListBoxSelectedIndexCollection();
            selectedItems = new RoyalListBoxSelectedItemCollection();

            items = new RoyalListBoxItemCollection();
            items.ItemAdded += new EventHandler(Items_ItemAdded);
            items.ItemRemoved += new EventHandler(Items_ItemRemoved);

            ItemHeight = 30;
            HotLightColor = RoyalColors.HotTrackColor;
            SelectedColor = RoyalColors.AccentColor;
            SelectedIndex = -1;
            HotLightedIndex = -1;

            scrollBar = new RoyalScrollBar();
            scrollBar.GutterColor = RoyalColors.HotTrackColor;
            scrollBar.ThumbColor = RoyalColors.AccentColor;
            scrollBar.Location = new Point(Width - 5, 0);
            scrollBar.Size = new Size(5, Height);
            scrollBar.Orientation = Orientation.Vertical;
            scrollBar.ValueChanged += new EventHandler(ScrollBar_ValueChanged);
            scrollBar.SmallChange = ItemHeight;
            scrollBar.LargeChange = ItemHeight * 3;
            scrollBar.Show();

            Controls.Add(scrollBar);
            Controls.SetChildIndex(scrollBar, 0);
        }

        public int IndexFromPoint(Point p)
        {
            int index = (scrollBar.Value / ItemHeight) + (p.Y / ItemHeight);

            if (index >= Items.Count)
                index = -1;

            return index;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int index = IndexFromPoint(e.Location);

            if (index >= 0 && index < Items.Count)
            {
                HotLightedIndex = index;
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HotLightedIndex = -1;
            Refresh();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int index = IndexFromPoint(e.Location);

            if (index >= 0 && index < Items.Count)
            {
                if (multiSelection && multiSelectKeyDown)
                {
                    selectedIndicies.Add(index);
                    selectedItems.Add(items[index]);
                    Refresh();
                }
                else
                {
                    selectedIndicies.Clear();
                    selectedItems.Clear();

                    SelectedIndex = index;
                    Refresh();
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            scrollBar.Value -= (e.Delta / 4);
            base.OnMouseWheel(e);
        }

        protected override void OnResize(EventArgs e)
        {
            scrollBar.Location = new Point(Width - 8, 0);
            scrollBar.Size = new Size(8, Height);

            base.OnResize(e);
        }

        protected void DrawItem(DrawItemEventArgs e)
        {
            Color foreColor = RoyalColors.ForeColor;
            Color backColor = RoyalColors.BackColor;
            string text = Items[e.Index].ToString();

            if (e.State == DrawItemState.HotLight)
                backColor = RoyalColors.HotTrackColor;
            else if (e.State == DrawItemState.Selected)
            {
                foreColor = RoyalColors.PressedForeColor;
                backColor = SelectedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);
            TextRenderer.DrawText(e.Graphics, text, Font, e.Bounds, foreColor, Color.Transparent,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.LeftAndRightPadding);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int firstVisibleItem = (scrollBar.Value / ItemHeight);
            int lastVisibleItem = (scrollBar.Value / ItemHeight) + (Height / ItemHeight) + 1;

            if (firstVisibleItem < 0)
                firstVisibleItem = 0;

            if (lastVisibleItem > Items.Count)
                lastVisibleItem = Items.Count;

            for (int i = firstVisibleItem; i < lastVisibleItem; i++)
            {
                DrawItemState state = DrawItemState.Default;

                if (multiSelection && selectedIndicies.Count > 0)
                {
                    if (i == HotLightedIndex && !selectedIndicies.Contains(i))
                        state = DrawItemState.HotLight;
                    else if (selectedIndicies.Contains(i))
                        state = DrawItemState.Selected;
                }
                else
                {
                    if (i == HotLightedIndex && i != SelectedIndex)
                        state = DrawItemState.HotLight;
                    else if (i == SelectedIndex)
                        state = DrawItemState.Selected;
                }

                Rectangle rect = new Rectangle(0, ((i - firstVisibleItem) * ItemHeight), Width, ItemHeight);
                DrawItemEventArgs de = new DrawItemEventArgs(e.Graphics, Font, rect, i, state);

                DrawItem(de);
            }
            base.OnPaint(e);
        }

        void Items_ItemAdded(object sender, EventArgs e)
        {
            scrollBar.Max = (Items.Count * ItemHeight);
            scrollBar.Refresh();
        }

        void Items_ItemRemoved(object sender, EventArgs e)
        {
            scrollBar.Max = (Items.Count * ItemHeight);
            scrollBar.Refresh();
        }

        void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    };

    #endregion
}