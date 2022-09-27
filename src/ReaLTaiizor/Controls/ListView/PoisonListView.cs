#region Imports

using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonListView

    public partial class PoisonListView : ListView, IPoisonControl
    {
        private ListViewColumnSorter lvwColumnSorter;
        private readonly Font stdFont = new("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Pixel);
        private readonly float _offset = 0.2F;

        #region Interface

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || poisonStyle != ColorStyle.Default)
                {
                    return poisonStyle;
                }

                if (StyleManager != null && poisonStyle == ColorStyle.Default)
                {
                    return StyleManager.Style;
                }

                if (StyleManager == null && poisonStyle == ColorStyle.Default)
                {
                    return PoisonDefaults.Style;
                }

                return poisonStyle;
            }
            set => poisonStyle = value;
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || poisonTheme != ThemeStyle.Default)
                {
                    return poisonTheme;
                }

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }

                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                {
                    return PoisonDefaults.Theme;
                }

                return poisonTheme;
            }
            set => poisonTheme = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager { get; set; } = null;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor { get; set; } = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor { get; set; } = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors { get; set; } = false;

        [Browsable(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }

        #endregion

        #region Scrollbar
        [StructLayout(LayoutKind.Sequential)]
        private struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        private enum ScrollInfoMask
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = SIF_RANGE + SIF_PAGE + SIF_POS + SIF_TRACKPOS
        }

        //fnBar values
        private enum SBTYPES
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }
        //lpsi values
        private enum LPCSCROLLINFO
        {
            SIF_RANGE = 0x0001,
            SIF_PAGE = 0x0002,
            SIF_POS = 0x0004,
            SIF_DISABLENOSCROLL = 0x0008,
            SIF_TRACKPOS = 0x0010,
            SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS
        }

        //ListView item information
        [StructLayoutAttribute(LayoutKind.Sequential)]
        private struct LVITEM
        {
            public uint mask;
            public int iItem;
            public int iSubItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
        }

        public enum ScrollBarCommands
        {
            SB_LINEUP = 0,
            SB_LINELEFT = 0,
            SB_LINEDOWN = 1,
            SB_LINERIGHT = 1,
            SB_PAGEUP = 2,
            SB_PAGELEFT = 2,
            SB_PAGEDOWN = 3,
            SB_PAGERIGHT = 3,
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_TOP = 6,
            SB_LEFT = 6,
            SB_BOTTOM = 7,
            SB_RIGHT = 7,
            SB_ENDSCROLL = 8
        }

        private const uint WM_VSCROLL = 0x0115;
        private const uint WM_NCCALCSIZE = 0x83;

        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_INSERTITEMA = LVM_FIRST + 7;
        private const uint LVM_INSERTITEMW = LVM_FIRST + 77;
        private const uint LVM_DELETEITEM = LVM_FIRST + 8;
        private const uint LVM_DELETEALLITEMS = LVM_FIRST + 9;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        public delegate void ScrollPositionChangedDelegate(PoisonListView listview, int pos);

        public event ScrollPositionChangedDelegate ScrollPositionChanged;
        public event Action<PoisonListView> ItemAdded;
        public event Action<PoisonListView> ItemsRemoved;

        private int _disableChangeEvents = 0;

        private readonly PoisonScrollBar _vScrollbar = new();

        private void BeginDisableChangeEvents()
        {
            _disableChangeEvents++;
        }

        private void EndDisableChangeEvents()
        {
            if (_disableChangeEvents > 0)
            {
                _disableChangeEvents--;
            }
        }

        private void _vScrollbar_ValueChanged(object sender, int newValue)
        {
            if (_disableChangeEvents > 0)
            {
                return;
            }

            SetScrollPosition(_vScrollbar.Value);
        }

        public void GetScrollPosition(out int min, out int max, out int pos, out int smallchange, out int largechange)
        {
            SCROLLINFO scrollinfo = new()
            {
                cbSize = (uint)Marshal.SizeOf(typeof(SCROLLINFO)),
                fMask = (int)ScrollInfoMask.SIF_ALL
            };
            if (GetScrollInfo(Handle, (int)SBTYPES.SB_VERT, ref scrollinfo))
            {
                min = scrollinfo.nMin;
                max = scrollinfo.nMax;
                pos = scrollinfo.nPos + 1;
                smallchange = 1;
                largechange = (int)scrollinfo.nPage;
            }
            else
            {
                min = 0;
                max = 0;
                pos = 0;
                smallchange = 0;
                largechange = 0;
            }
        }

        public void UpdateScrollbar()
        {
            if (_vScrollbar != null)
            {
                GetScrollPosition(out int min, out int max, out int pos, out int smallchange, out int largechange);

                BeginDisableChangeEvents();
                _vScrollbar.Value = pos == 1 ? 0 : pos;
                _vScrollbar.Maximum = max;// -largechange < largechange ? largechange : max - largechange;
                _vScrollbar.Minimum = min;
                _vScrollbar.SmallChange = smallchange;
                _vScrollbar.LargeChange = largechange;
                _vScrollbar.Visible = max > largechange;
                EndDisableChangeEvents();
            }
        }

        public void SetScrollPosition(int pos)
        {
            pos = Math.Min(Items.Count - 1, pos);

            if (pos < 0 || pos >= Items.Count)
            {
                return;
            }

            SuspendLayout();
            EnsureVisible(pos);

            if (View is View.Tile or View.LargeIcon or View.SmallIcon)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                if (TopItem != null && TopItem.Index != pos)
                {
                    TopItem = Items[pos];
                }
            }

            ResumeLayout();
        }

        protected void OnItemAdded()
        {
            if (_disableChangeEvents > 0)
            {
                return;
            }

            UpdateScrollbar();

            ItemAdded?.Invoke(this);
        }

        protected void OnItemsRemoved()
        {
            if (_disableChangeEvents > 0)
            {
                return;
            }

            UpdateScrollbar();

            ItemsRemoved?.Invoke(this);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_vScrollbar != null)
            {
                _vScrollbar.Value -= 3 * Math.Sign(e.Delta);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_VSCROLL)
            {
                GetScrollPosition(out int min, out int max, out int pos, out int smallchange, out int largechange);

                ScrollPositionChanged?.Invoke(this, pos);

                if (_vScrollbar != null)
                {
                    _vScrollbar.Value = pos;
                }
            }
            else if (m.Msg == WM_NCCALCSIZE) // WM_NCCALCSIZE
            {
                int style = GetWindowLong(Handle, GWL_STYLE);
                if ((style & WS_VSCROLL) == WS_VSCROLL)
                {
                    SetWindowLong(Handle, GWL_STYLE, style & ~WS_VSCROLL);
                }
            }

            else if (m.Msg is (int)LVM_INSERTITEMA or (int)LVM_INSERTITEMW)
            {
                OnItemAdded();
            }
            else if (m.Msg is (int)LVM_DELETEITEM or (int)LVM_DELETEALLITEMS)
            {
                OnItemsRemoved();
            }

            base.WndProc(ref m);
        }

        private const int GWL_STYLE = -16;
        private const int WS_VSCROLL = 0x00200000;

        public static int GetWindowLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return (int)GetWindowLong32(hWnd, nIndex);
            }
            else
            {
                return (int)(long)GetWindowLongPtr64(hWnd, nIndex);
            }
        }

        public static int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return (int)SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            else
            {
                return (int)(long)SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            }
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, int dwNewLong);
        #endregion

        public PoisonListView()
        {
            Font = new("Segoe UI", 12.0f);
            HideSelection = true;

            OwnerDraw = true;
            DrawColumnHeader += PoisonListView_DrawColumnHeader;
            DrawItem += PoisonListView_DrawItem;
            DrawSubItem += PoisonListView_DrawSubItem;
            Resize += PoisonListView_Resize;
            ColumnClick += PoisonListView_ColumnClick;
            SelectedIndexChanged += PoisonListView_SelectedIndexChanged;
            FullRowSelect = true;
            Controls.Add(_vScrollbar);
            _vScrollbar.Visible = false;
            _vScrollbar.Width = 15;
            _vScrollbar.Dock = DockStyle.Right;
            _vScrollbar.ValueChanged += _vScrollbar_ValueChanged;

            //DoubleBuffering(true);
        }

        private void PoisonListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateScrollbar();
        }

        private bool allowSorting = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        public bool AllowSorting
        {
            get => allowSorting;
            set
            {
                allowSorting = value;
                if (!value)
                {
                    lvwColumnSorter = null;
                    ListViewItemSorter = null;
                }
                else
                {
                    lvwColumnSorter = new ListViewColumnSorter();
                    ListViewItemSorter = lvwColumnSorter;
                }
            }
        }

        private void PoisonListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lvwColumnSorter == null)
            {
                return;
            }

            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            Sort();
        }

        private void PoisonListView_Resize(object sender, EventArgs e)
        {
            if (Columns.Count <= 0)
            {
                return;
            }
        }

        [Description("Set the font of the button caption")]
        [Browsable(false)]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        private void PoisonListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color itemForeColor = PoisonPaint.ForeColor.Button.Disabled(Theme);
            if (View == View.Details)
            {

                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ControlPaint.Light(PoisonPaint.GetStyleColor(Style), _offset)), e.Bounds);
                    itemForeColor = Color.White;
                }

                TextFormatFlags align = TextFormatFlags.Left;

                int _ded = 0, _left = 0;
                if (CheckBoxes && e.ColumnIndex == 0)
                {
                    _ded = 12; _left = 14;
                    int _top = (e.Bounds.Height / 2) - 6;
                    using (Pen p = new(itemForeColor))
                    {
                        Rectangle boxRect = new(e.Bounds.X + 2, e.Bounds.Y + _top, 12, 12);
                        e.Graphics.DrawRectangle(p, boxRect);
                    }

                    if (e.Item.Checked)
                    {
                        Color fillColor = PoisonPaint.GetStyleColor(Style);
                        if (e.Item.Selected)
                        {
                            fillColor = Color.White;
                        }

                        using SolidBrush b = new(fillColor);
                        _top = (e.Bounds.Height / 2) - 4;
                        Rectangle boxRect = new(e.Bounds.X + 4, e.Bounds.Y + _top, 9, 9);
                        e.Graphics.FillRectangle(b, boxRect);
                    }
                }

                if (SmallImageList != null)
                {
                    Image _img = null;
                    if (e.Item.ImageIndex > -1)
                    {
                        _img = SmallImageList.Images[e.Item.ImageIndex];
                    }

                    if (e.Item.ImageKey != "")
                    {
                        _img = SmallImageList.Images[e.Item.ImageKey];
                    }

                    if (_img != null)
                    {
                        _left += _left > 0 ? 4 : 2;
                        int _top = (e.Item.Bounds.Height - _img.Height) / 2;
                        e.Graphics.DrawImage(_img, new Rectangle(e.Item.Bounds.Left + _left, e.Item.Bounds.Top + _top, _img.Width, _img.Height));

                        _left += SmallImageList.ImageSize.Width;
                        _ded += SmallImageList.ImageSize.Width;
                    }
                }

                int _colWidth = e.Item.Bounds.Width;
                if (View == View.Details)
                {
                    _colWidth = Columns[e.ColumnIndex].Width;
                }

                using StringFormat sf = new();
                //TextFormatFlags flags = TextFormatFlags.Left;

                sf.Alignment = e.Header.TextAlign switch
                {
                    HorizontalAlignment.Center => StringAlignment.Center,
                    HorizontalAlignment.Right => StringAlignment.Far,
                    _ => StringAlignment.Near,
                };
                if (e.ColumnIndex > 0 && double.TryParse(e.SubItem.Text, NumberStyles.Currency, NumberFormatInfo.CurrentInfo, out double subItemValue))
                {
                    sf.Alignment = StringAlignment.Far;
                    //flags = TextFormatFlags.Right;
                }


                //TextFormatFlags align = TextFormatFlags.Left;
                Rectangle rect = new(e.Bounds.X + _left, e.Bounds.Y, _colWidth - _ded, e.Item.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, stdFont, rect, itemForeColor, align | TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void PoisonListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Color itemForeColor = PoisonPaint.ForeColor.Button.Disabled(Theme);
            if (View == View.Details | View == View.List | View == View.SmallIcon)
            {
                Color fillColor = PoisonPaint.GetStyleColor(Style);

                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ControlPaint.Light(PoisonPaint.GetStyleColor(Style), _offset)), e.Bounds);
                    itemForeColor = Color.White;
                    fillColor = Color.White;
                }

                TextFormatFlags align = TextFormatFlags.Left;

                int _ded = 0, _left = 0;
                if (CheckBoxes)
                {
                    _ded = 12; _left = 14;
                    int _top = (e.Bounds.Height / 2) - 6;
                    using (Pen p = new(itemForeColor))
                    {
                        Rectangle boxRect = new(e.Bounds.X + 2, e.Bounds.Y + _top, 12, 12);
                        e.Graphics.DrawRectangle(p, boxRect);
                    }

                    if (e.Item.Checked)
                    {
                        using SolidBrush b = new(fillColor);
                        _top = (e.Bounds.Height / 2) - 4;
                        Rectangle boxRect = new(e.Bounds.X + 4, e.Bounds.Y + _top, 9, 9);
                        e.Graphics.FillRectangle(b, boxRect);
                    }
                }

                if (SmallImageList != null)
                {
                    int _top = 0;
                    Image _img = null;
                    if (e.Item.ImageIndex > -1)
                    {
                        _img = SmallImageList.Images[e.Item.ImageIndex];
                    }

                    if (e.Item.ImageKey != "")
                    {
                        _img = SmallImageList.Images[e.Item.ImageKey];
                    }

                    if (_img != null)
                    {
                        _left += _left > 0 ? 4 : 2;
                        _top = (e.Item.Bounds.Height - _img.Height) / 2;
                        e.Graphics.DrawImage(_img, new Rectangle(e.Item.Bounds.Left + _left, e.Item.Bounds.Top + _top, _img.Width, _img.Height));

                        _left += SmallImageList.ImageSize.Width;
                        _ded += SmallImageList.ImageSize.Width;
                    }
                }

                if (View == View.Details)
                {
                    return;
                }

                int _colWidth = e.Item.Bounds.Width;
                if (View == View.Details)
                {
                    _colWidth = Columns[0].Width;
                }

                Rectangle rect = new(e.Bounds.X + _left, e.Bounds.Y, _colWidth - _ded, e.Item.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, e.Item.Text, stdFont, rect, itemForeColor, align | TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
            }

            else if (View == View.Tile)
            {
                int _left = 0;

                if (LargeImageList != null)
                {
                    int _top = 0;
                    _left = LargeImageList.ImageSize.Width + 2;

                    Image _img = null;
                    if (e.Item.ImageIndex > -1)
                    {
                        _img = LargeImageList.Images[e.Item.ImageIndex];
                    }

                    if (e.Item.ImageKey != "")
                    {
                        _img = LargeImageList.Images[e.Item.ImageKey];
                    }

                    if (_img != null)
                    {
                        _top = (e.Item.Bounds.Height - _img.Height) / 2;
                        e.Graphics.DrawImage(_img, new Rectangle(e.Item.Bounds.Left + _left, e.Item.Bounds.Top + _top, _img.Width, _img.Height));
                    }
                }

                if (e.Item.Selected)
                {
                    Rectangle rect = new(e.Item.Bounds.X + _left, e.Item.Bounds.Y, e.Item.Bounds.Width, e.Item.Bounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(250, 194, 87)), rect);
                }

                int _fill = 0;
                foreach (ListViewItem.ListViewSubItem item in e.Item.SubItems)
                {
                    if (_fill > 0 && !e.Item.Selected)
                    {
                        itemForeColor = Color.Silver;
                    }

                    int _y = e.Item.Bounds.Y + _fill + ((e.Item.Bounds.Height - (e.Item.SubItems.Count * 15)) / 2);

                    Rectangle rect = new(e.Item.Bounds.X + _left, e.Item.Bounds.Y + _fill, e.Item.Bounds.Width, e.Item.Bounds.Height);

                    TextFormatFlags align = TextFormatFlags.Left;
                    TextRenderer.DrawText(e.Graphics, item.Text, new Font("Segoe UI", 9.0f), rect, itemForeColor, align | TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.WordEllipsis);
                    _fill += 15;
                }
            }
            else
            {
                if (CheckBoxes)
                {
                    int _top = (e.Bounds.Height / 2) - 6;
                    using (Pen p = new(Color.Black))
                    {
                        Rectangle boxRect = new(e.Bounds.X + 6, e.Bounds.Y + _top, 12, 12);
                        e.Graphics.DrawRectangle(p, boxRect);
                    }

                    if (e.Item.Checked)
                    {
                        Color fillColor = PoisonPaint.GetStyleColor(Style);
                        if (e.Item.Selected)
                        {
                            fillColor = Color.White;
                        }

                        using SolidBrush b = new(fillColor);
                        _top = (e.Bounds.Height / 2) - 4;

                        Rectangle boxRect = new(e.Bounds.X + 8, e.Bounds.Y + _top, 9, 9);
                        e.Graphics.FillRectangle(b, boxRect);
                    }

                    Rectangle rect = new(e.Bounds.X + 23, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height);

                    e.Graphics.DrawString(e.Item.Text, stdFont, new SolidBrush(itemForeColor), rect);
                }

                Font = stdFont;
                e.DrawDefault = true;
            }
        }

        private void PoisonListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            Color _headColor = PoisonPaint.ForeColor.Button.Press(Theme);
            e.Graphics.FillRectangle(new SolidBrush(PoisonPaint.GetStyleColor(Style)), e.Bounds);

            using StringFormat sf = new();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(e.Header.Text, stdFont, new SolidBrush(_headColor), e.Bounds, sf);
        }
    }

    #endregion
}