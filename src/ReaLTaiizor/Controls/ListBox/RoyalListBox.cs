#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalListBox

    public class RoyalListBox : Control
    {
        private readonly RoyalScrollBar scrollBar;

        public RoyalListBoxItemCollection Items { get; }

        public RoyalListBoxSelectedItemCollection SelectedItems { get; }

        public RoyalListBoxSelectedIndexCollection SelectedIndicies { get; }

        private bool multiSelection;
        public bool MultiSelection
        {
            get => multiSelection;
            set { multiSelection = value; Invalidate(); }
        }

        private readonly bool multiSelectKeyDown = false;
        private int itemHeight;
        public int ItemHeight
        {
            get => itemHeight;
            set { itemHeight = value; Invalidate(); }
        }

        private Color hotLightColor;
        public Color HotLightColor
        {
            get => hotLightColor;
            set { hotLightColor = value; Invalidate(); }
        }

        private Color selectedColor;
        public Color SelectedColor
        {
            get => selectedColor;
            set { selectedColor = value; Invalidate(); }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set { selectedIndex = value; Invalidate(); }
        }

        public object SelectedValue => null;

        private int hotLightedIndex;
        public int HotLightedIndex
        {
            get => hotLightedIndex;
            set { hotLightedIndex = value; Invalidate(); }
        }

        public object HotLightedItem => null;

        public RoyalListBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            SelectedIndicies = new RoyalListBoxSelectedIndexCollection();
            SelectedItems = new RoyalListBoxSelectedItemCollection();

            Items = new RoyalListBoxItemCollection();
            Items.ItemAdded += new EventHandler(Items_ItemAdded);
            Items.ItemRemoved += new EventHandler(Items_ItemRemoved);

            ItemHeight = 30;
            HotLightColor = RoyalColors.HotTrackColor;
            SelectedColor = RoyalColors.AccentColor;
            SelectedIndex = -1;
            HotLightedIndex = -1;

            scrollBar = new RoyalScrollBar
            {
                GutterColor = RoyalColors.HotTrackColor,
                ThumbColor = RoyalColors.AccentColor,
                Location = new(Width - 5, 0),
                Size = new(5, Height),
                Orientation = Orientation.Vertical
            };
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
            {
                index = -1;
            }

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
                    SelectedIndicies.Add(index);
                    SelectedItems.Add(Items[index]);
                    Refresh();
                }
                else
                {
                    SelectedIndicies.Clear();
                    SelectedItems.Clear();

                    SelectedIndex = index;
                    Refresh();
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            scrollBar.Value -= e.Delta / 4;
            base.OnMouseWheel(e);
        }

        protected override void OnResize(EventArgs e)
        {
            scrollBar.Location = new(Width - 8, 0);
            scrollBar.Size = new(8, Height);

            base.OnResize(e);
        }

        protected void DrawItem(DrawItemEventArgs e)
        {
            Color foreColor = RoyalColors.ForeColor;
            Color backColor = RoyalColors.BackColor;
            string text = Items[e.Index].ToString();

            if (e.State == DrawItemState.HotLight)
            {
                backColor = RoyalColors.HotTrackColor;
            }
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
            int firstVisibleItem = scrollBar.Value / ItemHeight;
            int lastVisibleItem = (scrollBar.Value / ItemHeight) + (Height / ItemHeight) + 1;

            if (firstVisibleItem < 0)
            {
                firstVisibleItem = 0;
            }

            if (lastVisibleItem > Items.Count)
            {
                lastVisibleItem = Items.Count;
            }

            for (int i = firstVisibleItem; i < lastVisibleItem; i++)
            {
                DrawItemState state = DrawItemState.Default;

                if (multiSelection && SelectedIndicies.Count > 0)
                {
                    if (i == HotLightedIndex && !SelectedIndicies.Contains(i))
                    {
                        state = DrawItemState.HotLight;
                    }
                    else if (SelectedIndicies.Contains(i))
                    {
                        state = DrawItemState.Selected;
                    }
                }
                else
                {
                    if (i == HotLightedIndex && i != SelectedIndex)
                    {
                        state = DrawItemState.HotLight;
                    }
                    else if (i == SelectedIndex)
                    {
                        state = DrawItemState.Selected;
                    }
                }

                Rectangle rect = new(0, (i - firstVisibleItem) * ItemHeight, Width, ItemHeight);
                DrawItemEventArgs de = new(e.Graphics, Font, rect, i, state);

                DrawItem(de);
            }
            base.OnPaint(e);
        }

        private void Items_ItemAdded(object sender, EventArgs e)
        {
            scrollBar.Max = Items.Count * ItemHeight;
            scrollBar.Refresh();
        }

        private void Items_ItemRemoved(object sender, EventArgs e)
        {
            scrollBar.Max = Items.Count * ItemHeight;
            scrollBar.Refresh();
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    };

    #endregion
}