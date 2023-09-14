#region Imports

using ReaLTaiizor.Child.Material;
using ReaLTaiizor.Enum.Material;
using ReaLTaiizor.Helper;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialListBox

    [DefaultProperty("Items")]
    [DefaultEvent("SelectedIndexChanged")]
    [ComVisible(true)]
    public class MaterialListBox : Control, MaterialControlI
    {
        #region Internal Vars

        private List<object> _indicates;
        private bool _multiSelect;
        private int _selectedIndex;
        private MaterialListBoxItem _selectedItem;
        private bool _showScrollBar;
        private bool _multiKeyDown;
        private int _hoveredItem;
        private MaterialScrollBar _scrollBar;
        private bool _updating = false;
        private int _itemHeight;
        private bool _showBorder;
        private Color _borderColor;
        private Font _primaryFont;
        private Font _secondaryFont;

        private const int _leftrightPadding = 16;
        private int _primaryTextBottomPadding = 0;
        private int _secondaryTextTopPadding = 0;
        private int _secondaryTextBottomPadding = 0;

        public enum ListBoxStyle
        {
            SingleLine,
            TwoLine,
            ThreeLine
        }
        private ListBoxStyle _style = ListBoxStyle.SingleLine;

        public enum MaterialItemDensity
        {
            Default,
            Dense
        }

        private MaterialItemDensity _density;

        #endregion Internal Vars

        #region Properties

        //Properties for managing the material design properties
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        private bool useAccentColor;

        [Category("Material"), DefaultValue(false), DisplayName("Use Accent Color")]
        public bool UseAccentColor
        {
            get => useAccentColor;
            set { useAccentColor = value; _scrollBar.UseAccentColor = value; Invalidate(); }
        }

        [TypeConverter(typeof(CollectionConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(MaterialItemCollectionEditor), typeof(UITypeEditor))]
        [Category("Material"), Description("Gets the items of the ListBox.")]
        public ObservableCollection<MaterialListBoxItem> Items { get; } = new ObservableCollection<MaterialListBoxItem>();

        [Browsable(false)]
        [Category("Material"), Description("Gets a collection containing the currently selected items in the ListBox.")]
        public List<object> SelectedItems { get; private set; }

        [Browsable(false), Category("Material"), Description("Gets or sets the currently selected item in the ListBox.")]
        public MaterialListBoxItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                _selectedIndex = Items.IndexOf(_selectedItem);
                update_selection();
                Invalidate();
            }
        }

        [Browsable(false), Category("Material"),
         Description("Gets the currently selected Text in the ListBox.")]
        public string SelectedText
        {
            get; private set;
            //set
            //{
            //    _selectedText = value;
            //    Invalidate();
            //}
        }

        [Browsable(false), Category("Material"), Description("Gets or sets the zero-based index of the currently selected item in a ListBox.")]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                update_selection();
                Invalidate();
            }
        }

        [Browsable(true), Category("Material"), Description("Gets the value of the member property specified by the ValueMember property.")]
        public object SelectedValue
        {
            get; private set;
            //set
            //{
            //    _selectedValue = value;
            //    Invalidate();
            //}
        }

        [Category("Material"), DefaultValue(false), Description("Gets or sets a value indicating whether the ListBox supports multiple rows.")]
        public bool MultiSelect
        {
            get => _multiSelect;
            set
            {
                _multiSelect = value;

                if (SelectedItems.Count > 1)
                {
                    SelectedItems.RemoveRange(1, SelectedItems.Count - 1);
                }

                Invalidate();
            }
        }

        [Browsable(false)]
        public int Count => Items.Count;

        [Category("Material"), DefaultValue(false), Description("Gets or sets a value indicating whether the vertical scroll bar be shown or not.")]
        public bool ShowScrollBar
        {
            get => _showScrollBar;
            set
            {
                _showScrollBar = value;
                _scrollBar.Visible = value;
                Invalidate();
            }
        }

        [Category("Material"), DefaultValue(true), Description("Gets or sets a value indicating whether the border shown or not.")]
        public bool ShowBorder
        {
            get => _showBorder;
            set
            {
                _showBorder = value;
                Refresh();
            }
        }

        [Category("Material"), Description("Gets or sets backcolor used by the control.")]
        public override Color BackColor { get; set; }

        [Category("Material"), Description("Gets or sets forecolor used by the control.")]
        public override Color ForeColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text { get => base.Text; set => base.Text = value; }

        [Category("Material"), Description("Gets or sets border color used by the control.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Refresh();
            }
        }

        [Category("Material"), DefaultValue(ListBoxStyle.SingleLine)]
        [Description("Gets or sets the control style.")]
        public ListBoxStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateItemSpecs();

                InvalidateScroll(this, null);
                Refresh();
            }
        }

        [Category("Material"), DefaultValue(MaterialItemDensity.Dense)]
        [Description("Gets or sets list density")]
        public MaterialItemDensity Density
        {
            get => _density;
            set
            {
                _density = value;
                UpdateItemSpecs();
                Invalidate();
            }
        }

        #endregion Properties

        #region Constructors

        public MaterialListBox()
        {
            SetStyle
            (
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.Selectable |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            base.BackColor = Color.Transparent;
            base.Font = SkinManager.GetFontByType(MaterialSkinManager.FontType.Subtitle1);
            _secondaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
            SetDefaults();
            ShowBorder = true;
            ShowScrollBar = false;
            MultiSelect = false;
            UseAccentColor = false;
            ForeColor = SkinManager.TextHighEmphasisColor; // Color.Black;
            BackColor = Color.White;
            BorderColor = Color.LightGray;
            UpdateProperties();
        }

        private void SetDefaults()
        {
            SelectedIndex = -1;
            _hoveredItem = -1;
            _showScrollBar = false;
            Items.CollectionChanged += InvalidateScroll;
            SelectedItems = new List<object>();
            _indicates = new List<object>();
            _multiKeyDown = false;
            _scrollBar = new MaterialScrollBar()
            {
                Orientation = MateScrollOrientation.Vertical,
                Size = new Size(12, Height),
                Maximum = Items.Count * _itemHeight,
                SmallChange = _itemHeight,
                LargeChange = _itemHeight
            };
            _scrollBar.Scroll += HandleScroll;
            _scrollBar.MouseDown += VS_MouseDown;
            _scrollBar.BackColor = Color.Transparent;
            if (!Controls.Contains(_scrollBar))
            {
                Controls.Add(_scrollBar);
            }

            Style = ListBoxStyle.SingleLine;
            Density = MaterialItemDensity.Dense;
        }

        #endregion Constructors

        #region ApplyTheme

        private void UpdateProperties()
        {
            Invalidate();
        }

        private void UpdateItemSpecs()
        {
            if (_style == ListBoxStyle.TwoLine)
            {
                _secondaryTextTopPadding = 4;
                if (_density == MaterialItemDensity.Dense)
                {
                    _itemHeight = 60;
                    _secondaryTextBottomPadding = 10;
                    _primaryTextBottomPadding = 2;
                    _primaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
                    _secondaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body2);
                }
                else
                {
                    _itemHeight = 72;
                    _secondaryTextBottomPadding = 16;
                    _primaryTextBottomPadding = 4;
                    _primaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Subtitle1);
                    _secondaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
                }
            }
            else if (_style == ListBoxStyle.ThreeLine)
            {
                _primaryTextBottomPadding = 4;
                _secondaryTextTopPadding = 4;
                if (_density == MaterialItemDensity.Dense)
                {
                    _itemHeight = 76;
                    _secondaryTextBottomPadding = 16;
                    _primaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
                    _secondaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body2);
                }
                else
                {
                    _itemHeight = 88;
                    _secondaryTextBottomPadding = 12;
                    _primaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Subtitle1);
                    _secondaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
                }
            }
            else
            {
                //SingleLine
                if (_density == MaterialItemDensity.Dense)
                {
                    _itemHeight = 40;
                }
                else
                {
                    _itemHeight = 48;
                }

                _primaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Subtitle1);
                _secondaryFont = SkinManager.GetFontByType(MaterialSkinManager.FontType.Body1);
            }

        }

        #endregion ApplyTheme

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_updating == true)
            {
                return;
            }

            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Rectangle mainRect = new(0, 0, Width - (ShowBorder ? 1 : 0), Height - (ShowBorder ? 1 : 0));

            int lastItem = (_scrollBar.Value / _itemHeight) + (Height / _itemHeight) + 1 > Items.Count ? Items.Count : (_scrollBar.Value / _itemHeight) + (Height / _itemHeight) + 1;
            int firstItem = _scrollBar.Value / _itemHeight < 0 ? 0 : (_scrollBar.Value / _itemHeight);

            g.FillRectangle(Enabled ? SkinManager.BackgroundBrush : SkinManager.BackgroundDisabledBrush, mainRect);

            //Set TextAlignFlags
            MaterialNativeTextRenderer.TextAlignFlags primaryTextAlignFlags;
            MaterialNativeTextRenderer.TextAlignFlags secondaryTextAlignFlags = MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Top;
            if (_style is ListBoxStyle.TwoLine or ListBoxStyle.ThreeLine)
            {
                primaryTextAlignFlags = MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Bottom;
            }
            else
            {
                //SingleLine
                primaryTextAlignFlags = MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle;
            }

            //Set color and brush
            Color SelectedColor = new();
            if (UseAccentColor)
            {
                SelectedColor = SkinManager.ColorScheme.AccentColor;
            }
            else
            {
                SelectedColor = SkinManager.ColorScheme.PrimaryColor;
            }

            SolidBrush SelectedBrush = new(SelectedColor);

            //Draw items
            for (int i = firstItem; i < lastItem; i++)
            {
                string itemText = Items[i].Text;
                string itemSecondaryText = Items[i].SecondaryText;

                Rectangle itemRect = new(0, (i - firstItem) * _itemHeight, Width - (_showScrollBar && _scrollBar.Visible ? _scrollBar.Width : 0), _itemHeight);

                if (MultiSelect && _indicates.Count != 0)
                {
                    if (i == _hoveredItem && !_indicates.Contains(i))
                    {
                        g.FillRectangle(SkinManager.BackgroundHoverBrush, itemRect);
                    }
                    else if (_indicates.Contains(i))
                    {
                        g.FillRectangle(Enabled ?
                            SelectedBrush :
                            new SolidBrush(BlendColor(SelectedColor, SkinManager.SwitchOffDisabledThumbColor, 197)),
                            itemRect);
                    }
                }
                else
                {
                    if (i == _hoveredItem && i != SelectedIndex)
                    {
                        g.FillRectangle(SkinManager.BackgroundHoverBrush, itemRect);
                    }
                    else if (i == SelectedIndex)
                    {
                        g.FillRectangle(Enabled ?
                            SelectedBrush :
                            new SolidBrush(BlendColor(SelectedColor, SkinManager.SwitchOffDisabledThumbColor, 197)),
                            itemRect);
                    }
                }

                //Define primary & secondary Text Rect
                Rectangle primaryTextRect = new(itemRect.X + _leftrightPadding, itemRect.Y, itemRect.Width - (2 * _leftrightPadding), itemRect.Height);
                Rectangle secondaryTextRect = new();

                if (_style == ListBoxStyle.TwoLine)
                {
                    primaryTextRect.Height = (primaryTextRect.Height / 2) - _primaryTextBottomPadding;
                }
                else if (_style == ListBoxStyle.ThreeLine)
                {
                    if (_density == MaterialItemDensity.Default)
                    {
                        primaryTextRect.Height = 36 - _primaryTextBottomPadding;
                    }
                    else
                    {
                        primaryTextRect.Height = 30 - _primaryTextBottomPadding;
                    }
                }
                secondaryTextRect = new Rectangle(primaryTextRect.X, primaryTextRect.Y + primaryTextRect.Height + _primaryTextBottomPadding + _secondaryTextTopPadding, primaryTextRect.Width, _itemHeight - _secondaryTextBottomPadding - primaryTextRect.Height - (_primaryTextBottomPadding + _secondaryTextTopPadding));

                using MaterialNativeTextRenderer NativeText = new(g);
                NativeText.DrawTransparentText(
                itemText,
                _primaryFont,
                Enabled ? (i != SelectedIndex || UseAccentColor) ?
                SkinManager.TextHighEmphasisColor :
                SkinManager.ColorScheme.TextColor :
                SkinManager.TextDisabledOrHintColor, // Disabled
                primaryTextRect.Location,
                primaryTextRect.Size,
                primaryTextAlignFlags);
                if (_style == ListBoxStyle.TwoLine)
                {
                    NativeText.DrawTransparentText(
                    itemSecondaryText,
                    _secondaryFont,
                    Enabled ? (i != SelectedIndex || UseAccentColor) ?
                    SkinManager.TextDisabledOrHintColor :
                    SkinManager.ColorScheme.TextColor.Darken(0.25f) :
                    SkinManager.TextDisabledOrHintColor, // Disabled
                    secondaryTextRect.Location,
                    secondaryTextRect.Size,
                    secondaryTextAlignFlags);
                }
                else if (_style == ListBoxStyle.ThreeLine)
                {
                    NativeText.DrawMultilineTransparentText(
                    itemSecondaryText,
                    _secondaryFont,
                    Enabled ? (i != SelectedIndex || UseAccentColor) ?
                    SkinManager.TextDisabledOrHintColor :
                    SkinManager.ColorScheme.TextColor.Darken(0.25f) :
                    SkinManager.TextDisabledOrHintColor, // Disabled
                    secondaryTextRect.Location,
                    secondaryTextRect.Size,
                    secondaryTextAlignFlags);
                }

            }
            if (ShowBorder)
            {
                g.DrawRectangle(Pens.LightGray, mainRect);
            }
        }

        #endregion Draw Control

        #region Methods

        public void AddItem(MaterialListBoxItem newItem)
        {
            Items.Add(newItem);
            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        public void AddItem(string newItem)
        {
            MaterialListBoxItem _newitemMLBI = new(newItem);
            Items.Add(_newitemMLBI);
            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        public void AddItems(MaterialListBoxItem[] newItems)
        {
            _updating = true;
            foreach (MaterialListBoxItem str in newItems)
            {
                AddItem(str);
            }
            _updating = false;

            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        public void AddItems(string[] newItems)
        {
            _updating = true;
            foreach (string str in newItems)
            {
                AddItem(str);
            }
            _updating = false;

            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        public void RemoveItemAt(int index)
        {
            if (index <= _selectedIndex)
            {
                _selectedIndex -= 1;
                update_selection();
            }
            Items.RemoveAt(index);
            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        public void RemoveItem(MaterialListBoxItem item)
        {
            if (Items.IndexOf(item) <= _selectedIndex)
            {
                _selectedIndex -= 1;
                update_selection();
            }
            Items.Remove(item);
            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        public int IndexOf(MaterialListBoxItem value)
        {
            return Items.IndexOf(value);
        }

        public void RemoveItems(MaterialListBoxItem[] itemsToRemove)
        {
            _updating = true;
            foreach (MaterialListBoxItem item in itemsToRemove)
            {
                if (Items.IndexOf(item) <= _selectedIndex)
                {
                    _selectedIndex -= 1;
                    update_selection();
                }
                Items.Remove(item);
            }
            _updating = false;

            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        private void update_selection()
        {
            if (_selectedIndex >= 0)
            {
                _selectedItem = Items[_selectedIndex];
                SelectedValue = Items[_selectedIndex];
                SelectedText = Items[_selectedIndex].ToString();
            }
            else
            {
                _selectedItem = null;
                SelectedValue = null;
                SelectedText = null;
            }
        }

        public void Clear()
        {
            _updating = true;
            for (int i = Items.Count - 1; i >= 0; i += -1)
            {
                Items.RemoveAt(i);
            }
            _updating = false;
            _selectedIndex = -1;
            update_selection();

            InvalidateScroll(this, null);
            ItemsCountChanged?.Invoke(this, new EventArgs());
        }

        public void BeginUpdate()
        {
            _updating = true;
        }

        public void EndUpdate()
        {
            _updating = false;
        }

        #endregion Methods

        #region Events

        [Category("Behavior")]
        [Description("Occurs when selected index change.")]
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;

        public delegate void SelectedIndexChangedEventHandler(object sender, MaterialListBoxItem selectedItem);

        [Category("Behavior")]
        [Description("Occurs when selected value change.")]
        public event SelectedValueEventHandler SelectedValueChanged;

        public delegate void SelectedValueEventHandler(object sender, MaterialListBoxItem selectedItem);

        [Category("Behavior")]
        [Description("Occurs when item is added or removed.")]
        public event EventHandler ItemsCountChanged;

        #endregion Events

        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateScroll(this, e);
            InvalidateLayout();
            base.OnSizeChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (e.Button == MouseButtons.Left)
            {
                int index = (_scrollBar.Value / _itemHeight) + (e.Location.Y / _itemHeight);
                if (index >= 0 && index < Items.Count)
                {
                    if (MultiSelect && _multiKeyDown)
                    {
                        _indicates.Add(index);
                        SelectedItems.Add(Items[index]);
                    }
                    else
                    {
                        _indicates.Clear();
                        SelectedItems.Clear();
                        _selectedItem = Items[index];
                        _selectedIndex = index;
                        SelectedValue = Items[index];
                        SelectedText = Items[index].ToString();
                        SelectedIndexChanged?.Invoke(this, _selectedItem);
                        SelectedValueChanged?.Invoke(this, _selectedItem);
                    }
                }
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        private void HandleScroll(object sender, ScrollEventArgs e)
        {
            if (_scrollBar.Maximum < _scrollBar.Value + Height)
            {
                _scrollBar.Value = _scrollBar.Maximum - Height;
            }

            Invalidate();
        }

        private void InvalidateScroll(object sender, EventArgs e)
        {
            _scrollBar.Maximum = Items.Count * _itemHeight;
            _scrollBar.SmallChange = _itemHeight;
            _scrollBar.LargeChange = Height;
            _scrollBar.Visible = (Items.Count * _itemHeight) > Height;
            if (Items.Count == 0)
            { _scrollBar.Value = 0; }
            Invalidate();
        }

        private void VS_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
        }

        private void InvalidateLayout()
        {
            _scrollBar.Size = new Size(12, Height - (ShowBorder ? 2 : 0));
            _scrollBar.Location = new Point(Width - (_scrollBar.Width + (ShowBorder ? 1 : 0)), ShowBorder ? 1 : 0);
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_scrollBar.Visible == true)
            {
                if (_scrollBar.Minimum > _scrollBar.Value - (e.Delta / 2))
                {
                    _scrollBar.Value = _scrollBar.Minimum;
                }
                else if (_scrollBar.Maximum < _scrollBar.Value + Height)
                {
                    if (e.Delta > 0)
                    {
                        _scrollBar.Value -= e.Delta / 2;
                    }
                    else
                    { } //Do nothing, maximum reached
                }
                else
                {
                    _scrollBar.Value -= e.Delta / 2;
                }

                _updateHoveredItem(e);

                Invalidate();
                base.OnMouseWheel(e);
                ((HandledMouseEventArgs)e).Handled = true;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    try
                    {
                        SelectedItems.Remove(Items[SelectedIndex]);
                        SelectedIndex += 1;
                        SelectedItems.Add(Items[SelectedIndex]);
                    }
                    catch
                    {
                        //
                    }
                    break;

                case Keys.Up:
                    try
                    {
                        SelectedItems.Remove(Items[SelectedIndex]);
                        SelectedIndex -= 1;
                        SelectedItems.Add(Items[SelectedIndex]);
                    }
                    catch
                    {
                        //
                    }
                    break;
            }
            Invalidate();
            return base.IsInputKey(keyData);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = Cursors.Hand;
            _updateHoveredItem(e);

            Invalidate();
        }

        private void _updateHoveredItem(MouseEventArgs e)
        {
            int index = (_scrollBar.Value / _itemHeight) + (e.Location.Y / _itemHeight);

            if (index >= Items.Count)
            {
                index = -1;
            }

            if (index >= 0 && index < Items.Count)
            {
                _hoveredItem = index;
            }

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hoveredItem = -1;
            Cursor = Cursors.Default;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _scrollBar.Size = new Size(12, Height - (ShowBorder ? 2 : 0));
            _scrollBar.Location = new Point(Width - (_scrollBar.Width + (ShowBorder ? 1 : 0)), ShowBorder ? 1 : 0);
            InvalidateScroll(this, e);
        }

        public const int WM_SETCURSOR = 0x0020;
        public const int IDC_HAND = 32649;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SETCURSOR)
            {
                SetCursor(LoadCursor(IntPtr.Zero, IDC_HAND));
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }
    }

    #endregion
}