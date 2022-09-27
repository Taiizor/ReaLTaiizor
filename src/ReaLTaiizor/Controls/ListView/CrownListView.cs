#region Imports

using ReaLTaiizor.Child.Crown;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownListView

    public class CrownListView : CrownScrollView
    {
        #region Event Region

        public event EventHandler SelectedIndicesChanged;

        #endregion

        #region Field Region

        private int _itemHeight = 20;
        private readonly int _iconSize = 16;

        private ObservableCollection<CrownListItem> _items;
        private int _anchoredItemStart = -1;
        private int _anchoredItemEnd = -1;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<CrownListItem> Items
        {
            get => _items;
            set
            {
                if (_items != null)
                {
                    _items.CollectionChanged -= Items_CollectionChanged;
                }

                _items = value;

                _items.CollectionChanged += Items_CollectionChanged;

                UpdateListBox();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> SelectedIndices { get; }

        [Category("Appearance")]
        [Description("Determines the height of the individual list view items.")]
        [DefaultValue(20)]
        public int ItemHeight
        {
            get => _itemHeight;
            set
            {
                _itemHeight = value;
                UpdateListBox();
            }
        }

        [Category("Behaviour")]
        [Description("Determines whether multiple list view items can be selected at once.")]
        [DefaultValue(false)]
        public bool MultiSelect { get; set; }

        [Category("Appearance")]
        [Description("Determines whether icons are rendered with the list items.")]
        [DefaultValue(false)]
        public bool ShowIcons { get; set; }

        #endregion

        #region Constructor Region

        public CrownListView()
        {
            Items = new ObservableCollection<CrownListItem>();
            SelectedIndices = new List<int>();
        }

        #endregion

        #region Event Handler Region

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                using (Graphics g = CreateGraphics())
                {
                    // Set the area size of all new items
                    foreach (CrownListItem item in e.NewItems)
                    {
                        item.TextChanged += Item_TextChanged;
                        UpdateItemSize(item, g);
                    }
                }

                // Find the starting index of the new item list and update anything past that
                if (e.NewStartingIndex < (Items.Count - 1))
                {
                    for (int i = e.NewStartingIndex; i <= Items.Count - 1; i++)
                    {
                        UpdateItemPosition(Items[i], i);
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (CrownListItem item in e.OldItems)
                {
                    item.TextChanged -= Item_TextChanged;
                }

                // Find the starting index of the old item list and update anything past that
                if (e.OldStartingIndex < (Items.Count - 1))
                {
                    for (int i = e.OldStartingIndex; i <= Items.Count - 1; i++)
                    {
                        UpdateItemPosition(Items[i], i);
                    }
                }
            }

            if (Items.Count == 0)
            {
                if (SelectedIndices.Count > 0)
                {
                    SelectedIndices.Clear();

                    SelectedIndicesChanged?.Invoke(this, null);
                }
            }

            UpdateContentSize();
        }

        private void Item_TextChanged(object sender, EventArgs e)
        {
            CrownListItem item = (CrownListItem)sender;

            UpdateItemSize(item);
            UpdateContentSize(item);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (Items.Count == 0)
            {
                return;
            }

            if (e.Button is not MouseButtons.Left and not MouseButtons.Right)
            {
                return;
            }

            Point pos = OffsetMousePosition;

            List<int> range = ItemIndexesInView().ToList();

            int top = range.Min();
            int bottom = range.Max();
            int width = Math.Max(ContentSize.Width, Viewport.Width);

            for (int i = top; i <= bottom; i++)
            {
                Rectangle rect = new(0, i * ItemHeight, width, ItemHeight);

                if (rect.Contains(pos))
                {
                    if (MultiSelect && ModifierKeys == Keys.Shift)
                    {
                        SelectAnchoredRange(i);
                    }
                    else if (MultiSelect && ModifierKeys == Keys.Control)
                    {
                        ToggleItem(i);
                    }
                    else
                    {
                        SelectItem(i);
                    }
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (Items.Count == 0)
            {
                return;
            }

            if (e.KeyCode is not Keys.Down and not Keys.Up)
            {
                return;
            }

            if (MultiSelect && ModifierKeys == Keys.Shift)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (_anchoredItemEnd - 1 >= 0)
                    {
                        SelectAnchoredRange(_anchoredItemEnd - 1);
                        EnsureVisible();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (_anchoredItemEnd + 1 <= Items.Count - 1)
                    {
                        SelectAnchoredRange(_anchoredItemEnd + 1);
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (_anchoredItemEnd - 1 >= 0)
                    {
                        SelectItem(_anchoredItemEnd - 1);
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (_anchoredItemEnd + 1 <= Items.Count - 1)
                    {
                        SelectItem(_anchoredItemEnd + 1);
                    }
                }
            }

            EnsureVisible();
        }

        #endregion

        #region Method Region

        public int GetItemIndex(CrownListItem item)
        {
            return Items.IndexOf(item);
        }

        public void SelectItem(int index)
        {
            if (index < 0 || index > Items.Count - 1)
            {
                throw new IndexOutOfRangeException($"Value '{index}' is outside of valid range.");
            }

            SelectedIndices.Clear();
            SelectedIndices.Add(index);

            SelectedIndicesChanged?.Invoke(this, null);

            _anchoredItemStart = index;
            _anchoredItemEnd = index;

            Invalidate();
        }

        public void SelectItems(IEnumerable<int> indexes)
        {
            SelectedIndices.Clear();

            List<int> list = indexes.ToList();

            foreach (int index in list)
            {
                if (index < 0 || index > Items.Count - 1)
                {
                    throw new IndexOutOfRangeException($"Value '{index}' is outside of valid range.");
                }

                SelectedIndices.Add(index);
            }

            SelectedIndicesChanged?.Invoke(this, null);

            _anchoredItemStart = list[list.Count - 1];
            _anchoredItemEnd = list[list.Count - 1];

            Invalidate();
        }

        public void ToggleItem(int index)
        {
            if (SelectedIndices.Contains(index))
            {
                SelectedIndices.Remove(index);

                // If we just removed both the anchor start AND end then reset them
                if (_anchoredItemStart == index && _anchoredItemEnd == index)
                {
                    if (SelectedIndices.Count > 0)
                    {
                        _anchoredItemStart = SelectedIndices[0];
                        _anchoredItemEnd = SelectedIndices[0];
                    }
                    else
                    {
                        _anchoredItemStart = -1;
                        _anchoredItemEnd = -1;
                    }
                }

                // If we just removed the anchor start then update it accordingly
                if (_anchoredItemStart == index)
                {
                    if (_anchoredItemEnd < index)
                    {
                        _anchoredItemStart = index - 1;
                    }
                    else if (_anchoredItemEnd > index)
                    {
                        _anchoredItemStart = index + 1;
                    }
                    else
                    {
                        _anchoredItemStart = _anchoredItemEnd;
                    }
                }

                // If we just removed the anchor end then update it accordingly
                if (_anchoredItemEnd == index)
                {
                    if (_anchoredItemStart < index)
                    {
                        _anchoredItemEnd = index - 1;
                    }
                    else if (_anchoredItemStart > index)
                    {
                        _anchoredItemEnd = index + 1;
                    }
                    else
                    {
                        _anchoredItemEnd = _anchoredItemStart;
                    }
                }
            }
            else
            {
                SelectedIndices.Add(index);
                _anchoredItemStart = index;
                _anchoredItemEnd = index;
            }

            SelectedIndicesChanged?.Invoke(this, null);

            Invalidate();
        }

        public void SelectItems(int startRange, int endRange)
        {
            SelectedIndices.Clear();

            if (startRange == endRange)
            {
                SelectedIndices.Add(startRange);
            }

            if (startRange < endRange)
            {
                for (int i = startRange; i <= endRange; i++)
                {
                    SelectedIndices.Add(i);
                }
            }
            else if (startRange > endRange)
            {
                for (int i = startRange; i >= endRange; i--)
                {
                    SelectedIndices.Add(i);
                }
            }

            SelectedIndicesChanged?.Invoke(this, null);

            Invalidate();
        }

        private void SelectAnchoredRange(int index)
        {
            _anchoredItemEnd = index;
            SelectItems(_anchoredItemStart, index);
        }

        private void UpdateListBox()
        {
            using (Graphics g = CreateGraphics())
            {
                for (int i = 0; i <= Items.Count - 1; i++)
                {
                    CrownListItem item = Items[i];
                    UpdateItemSize(item, g);
                    UpdateItemPosition(item, i);
                }
            }

            UpdateContentSize();
        }

        private void UpdateItemSize(CrownListItem item)
        {
            using Graphics g = CreateGraphics();
            UpdateItemSize(item, g);
        }

        private void UpdateItemSize(CrownListItem item, Graphics g)
        {
            SizeF size = g.MeasureString(item.Text, Font);
            size.Width++;

            if (ShowIcons)
            {
                size.Width += _iconSize + 8;
            }

            item.Area = new(item.Area.Left, item.Area.Top, (int)size.Width, item.Area.Height);
        }

        private void UpdateItemPosition(CrownListItem item, int index)
        {
            item.Area = new(2, index * ItemHeight, item.Area.Width, ItemHeight);
        }

        private void UpdateContentSize()
        {
            int highestWidth = 0;

            foreach (CrownListItem item in Items)
            {
                if (item.Area.Right + 1 > highestWidth)
                {
                    highestWidth = item.Area.Right + 1;
                }
            }

            int width = highestWidth;
            int height = Items.Count * ItemHeight;

            if (ContentSize.Width != width || ContentSize.Height != height)
            {
                ContentSize = new(width, height);
                Invalidate();
            }
        }

        private void UpdateContentSize(CrownListItem item)
        {
            int itemWidth = item.Area.Right + 1;

            if (itemWidth == ContentSize.Width)
            {
                UpdateContentSize();
                return;
            }

            if (itemWidth > ContentSize.Width)
            {
                ContentSize = new(itemWidth, ContentSize.Height);
                Invalidate();
            }
        }

        public void EnsureVisible()
        {
            if (SelectedIndices.Count == 0)
            {
                return;
            }

            int itemTop = -1;

            if (!MultiSelect)
            {
                itemTop = SelectedIndices[0] * ItemHeight;
            }
            else
            {
                itemTop = _anchoredItemEnd * ItemHeight;
            }

            int itemBottom = itemTop + ItemHeight;

            if (itemTop < Viewport.Top)
            {
                VScrollTo(itemTop);
            }

            if (itemBottom > Viewport.Bottom)
            {
                VScrollTo(itemBottom - Viewport.Height);
            }
        }

        private IEnumerable<int> ItemIndexesInView()
        {
            int top = (Viewport.Top / ItemHeight) - 1;

            if (top < 0)
            {
                top = 0;
            }

            int bottom = ((Viewport.Top + Viewport.Height) / ItemHeight) + 1;

            if (bottom > Items.Count)
            {
                bottom = Items.Count;
            }

            IEnumerable<int> result = Enumerable.Range(top, bottom - top);
            return result;
        }

        private IEnumerable<CrownListItem> ItemsInView()
        {
            IEnumerable<int> indexes = ItemIndexesInView();
            List<CrownListItem> result = indexes.Select(index => Items[index]).ToList();
            return result;
        }

        #endregion

        #region Paint Region

        protected override void PaintContent(Graphics g)
        {
            List<int> range = ItemIndexesInView().ToList();

            if (range.Count == 0)
            {
                return;
            }

            int top = range.Min();
            int bottom = range.Max();

            for (int i = top; i <= bottom; i++)
            {
                int width = Math.Max(ContentSize.Width, Viewport.Width);
                Rectangle rect = new(0, i * ItemHeight, width, ItemHeight);

                // Background
                bool odd = i % 2 != 0;
                Color bgColor = !odd ? ThemeProvider.Theme.Colors.HeaderBackground : ThemeProvider.Theme.Colors.GreyBackground;

                if (SelectedIndices.Count > 0 && SelectedIndices.Contains(i))
                {
                    bgColor = Focused ? ThemeProvider.Theme.Colors.BlueSelection : ThemeProvider.Theme.Colors.GreySelection;
                }

                using (SolidBrush b = new(bgColor))
                {
                    g.FillRectangle(b, rect);
                }

                // DEBUG: Border
                /*using (var p = new(ThemeProvider.Theme.Colors.DarkBorder))
                {
                    g.DrawLine(p, new Point(rect.Left, rect.Bottom - 1), new Point(rect.Right, rect.Bottom - 1));
                }*/

                // Icon
                if (ShowIcons && Items[i].Icon != null)
                {
                    g.DrawImageUnscaled(Items[i].Icon, new Point(rect.Left + 5, rect.Top + (rect.Height / 2) - (_iconSize / 2)));
                }

                // Text
                using (SolidBrush b = new(ThemeProvider.Theme.Colors.LightText)) //Items[i].TextColor
                {
                    StringFormat stringFormat = new()
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };

                    Font modFont = new(Font, Items[i].FontStyle);

                    Rectangle modRect = new(rect.Left + 2, rect.Top, rect.Width, rect.Height);

                    if (ShowIcons)
                    {
                        modRect.X += _iconSize + 8;
                    }

                    g.DrawString(Items[i].Text, modFont, b, modRect, stringFormat);
                }
            }
        }

        #endregion
    }

    #endregion
}