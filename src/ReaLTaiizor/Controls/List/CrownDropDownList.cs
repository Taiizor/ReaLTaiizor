#region Imports

using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Enum.Crown;
using ReaLTaiizor.Extension.Crown;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownDropDownList

    public class CrownDropDownList : Control
    {
        #region Event Region

        public event EventHandler SelectedItemChanged;

        #endregion

        #region Field Region


        private CrownDropDownItem _selectedItem;

        private readonly CrownContextMenuStrip _menu = new();
        private bool _menuOpen = false;

        private bool _showBorder = true;

        private int _itemHeight = 22;
        private int _maxHeight = 130;

        private readonly int _iconSize = 16;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<CrownDropDownItem> Items { get; } = new();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDropDownItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SelectedItemChanged?.Invoke(this, new EventArgs());
            }
        }

        [Category("Appearance")]
        [Description("Determines whether a border is drawn around the control.")]
        [DefaultValue(true)]
        public bool ShowBorder
        {
            get => _showBorder;
            set
            {
                _showBorder = value;
                Invalidate();
            }
        }

        protected override Size DefaultSize => new(100, 26);

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ControlState ControlState { get; private set; } = ControlState.Normal;

        [Category("Appearance")]
        [Description("Determines the height of the individual list view items.")]
        [DefaultValue(22)]
        public int ItemHeight
        {
            get => _itemHeight;
            set
            {
                _itemHeight = value;
                ResizeMenu();
            }
        }

        [Category("Appearance")]
        [Description("Determines the maximum height of the dropdown panel.")]
        [DefaultValue(130)]
        public int MaxHeight
        {
            get => _maxHeight;
            set
            {
                _maxHeight = value;
                ResizeMenu();
            }
        }

        [Category("Behavior")]
        [Description("Determines what location the dropdown list appears.")]
        [DefaultValue(ToolStripDropDownDirection.Default)]
        public ToolStripDropDownDirection DropdownDirection { get; set; } = ToolStripDropDownDirection.Default;

        #endregion

        #region Constructor Region

        public CrownDropDownList()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.Selectable |
                ControlStyles.UserMouse,
                    true
            );

            _menu.AutoSize = false;
            _menu.Closed += Menu_Closed;

            Items.CollectionChanged += Items_CollectionChanged;
            SelectedItemChanged += CrownDropDownList_SelectedItemChanged;

            SetControlState(ControlState.Normal);
        }

        #endregion

        #region Method Region

        private ToolStripMenuItem GetMenuItem(CrownDropDownItem item)
        {
            foreach (ToolStripMenuItem menuItem in _menu.Items)
            {
                if ((CrownDropDownItem)menuItem.Tag == item)
                {
                    return menuItem;
                }
            }

            return null;
        }

        private void SetControlState(ControlState controlState)
        {
            if (_menuOpen)
            {
                return;
            }

            if (ControlState != controlState)
            {
                ControlState = controlState;
                Invalidate();
            }
        }

        private void ShowMenu()
        {
            if (_menu.Visible)
            {
                return;
            }

            SetControlState(ControlState.Pressed);

            _menuOpen = true;

            Point pos = new(0, ClientRectangle.Bottom);

            if (DropdownDirection is ToolStripDropDownDirection.AboveLeft or ToolStripDropDownDirection.AboveRight)
            {
                pos.Y = 0;
            }

            _menu.Show(this, pos, DropdownDirection);

            if (SelectedItem != null)
            {
                ToolStripMenuItem selectedItem = GetMenuItem(SelectedItem);
                selectedItem.Select();
            }
        }

        private void ResizeMenu()
        {
            int width = ClientRectangle.Width;
            int height = (_menu.Items.Count * _itemHeight) + 4;

            if (height > _maxHeight)
            {
                height = _maxHeight;
            }

            // Dirty: Check what the autosized items are
            foreach (ToolStripMenuItem item in _menu.Items)
            {
                item.AutoSize = true;

                if (item.Size.Width > width)
                {
                    width = item.Size.Width;
                }

                item.AutoSize = false;
            }

            // Force the size
            foreach (ToolStripMenuItem item in _menu.Items)
            {
                item.Size = new(width - 1, _itemHeight);
            }

            _menu.Size = new(width, height);
        }

        #endregion

        #region Event Handler Region

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (CrownDropDownItem item in e.NewItems)
                {
                    ToolStripMenuItem menuItem = new(item.Text)
                    {
                        Image = item.Icon,
                        AutoSize = false,
                        Height = _itemHeight,
                        Font = Font,
                        Tag = item,
                        TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                    };

                    _menu.Items.Add(menuItem);
                    menuItem.Click += Item_Select;

                    if (SelectedItem == null)
                    {
                        SelectedItem = item;
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (CrownDropDownItem item in e.OldItems)
                {
                    foreach (ToolStripMenuItem menuItem in _menu.Items)
                    {
                        if ((CrownDropDownItem)menuItem.Tag == item)
                        {
                            _menu.Items.Remove(menuItem);
                        }
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _menu.Items.Clear();
                SelectedItem = null;
            }

            ResizeMenu();
        }

        private void Item_Select(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem menuItem)
            {
                return;
            }

            CrownDropDownItem dropdownItem = (CrownDropDownItem)menuItem.Tag;
            if (_selectedItem != dropdownItem)
            {
                SelectedItem = dropdownItem;
            }
        }

        private void CrownDropDownList_SelectedItemChanged(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in _menu.Items)
            {
                if ((CrownDropDownItem)item.Tag == SelectedItem)
                {
                    item.BackColor = ThemeProvider.Theme.Colors.DarkBlueBackground;
                    item.Font = new(Font, FontStyle.Bold);
                }
                else
                {
                    item.BackColor = ThemeProvider.Theme.Colors.GreyBackground;
                    item.Font = new(Font, FontStyle.Regular);
                }
            }

            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ResizeMenu();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(e.Location))
                {
                    SetControlState(ControlState.Pressed);
                }
                else
                {
                    SetControlState(ControlState.Hover);
                }
            }
            else
            {
                SetControlState(ControlState.Hover);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            ShowMenu();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            SetControlState(ControlState.Normal);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            SetControlState(ControlState.Normal);
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            Point location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
            {
                SetControlState(ControlState.Normal);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Point location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
            {
                SetControlState(ControlState.Normal);
            }
            else
            {
                SetControlState(ControlState.Hover);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                ShowMenu();
            }
        }

        private void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            _menuOpen = false;

            if (!ClientRectangle.Contains(MousePosition))
            {
                SetControlState(ControlState.Normal);
            }
            else
            {
                SetControlState(ControlState.Hover);
            }
        }

        #endregion

        #region Render Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw background
            using (SolidBrush b = new(ThemeProvider.Theme.Colors.MediumBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            // Draw normal state
            if (ControlState == ControlState.Normal)
            {
                if (ShowBorder)
                {
                    using Pen p = new(ThemeProvider.Theme.Colors.LightBorder, 1);
                    Rectangle modRect = new(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                    g.DrawRectangle(p, modRect);
                }
            }

            // Draw hover state
            if (ControlState == ControlState.Hover)
            {
                using (SolidBrush b = new(ThemeProvider.Theme.Colors.DarkBorder))
                {
                    g.FillRectangle(b, ClientRectangle);
                }

                using (SolidBrush b = new(ThemeProvider.Theme.Colors.DarkBackground))
                {
                    Rectangle arrowRect = new(ClientRectangle.Right - Properties.Resources.small_arrow.Width - 8, ClientRectangle.Top, Properties.Resources.small_arrow.Width + 8, ClientRectangle.Height);
                    g.FillRectangle(b, arrowRect);
                }

                using Pen p = new(ThemeProvider.Theme.Colors.BlueSelection, 1);
                Rectangle modRect = new(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width - 1 - Properties.Resources.small_arrow.Width - 8, ClientRectangle.Height - 1);
                g.DrawRectangle(p, modRect);
            }

            // Draw pressed state
            if (ControlState == ControlState.Pressed)
            {
                using (SolidBrush b = new(ThemeProvider.Theme.Colors.DarkBorder))
                {
                    g.FillRectangle(b, ClientRectangle);
                }

                using (SolidBrush b = new(ThemeProvider.Theme.Colors.BlueSelection))
                {
                    Rectangle arrowRect = new(ClientRectangle.Right - Properties.Resources.small_arrow.Width - 8, ClientRectangle.Top, Properties.Resources.small_arrow.Width + 8, ClientRectangle.Height);
                    g.FillRectangle(b, arrowRect);
                }
            }

            // Draw dropdown arrow
            using (Bitmap img = Properties.Resources.small_arrow.SetColor(ThemeProvider.Theme.Colors.LightText))
            {
                g.DrawImageUnscaled(img, ClientRectangle.Right - img.Width - 4, ClientRectangle.Top + (ClientRectangle.Height / 2) - (img.Height / 2));
            }

            // Draw selected item
            if (SelectedItem != null)
            {
                // Draw Icon
                bool hasIcon = SelectedItem.Icon != null;

                if (hasIcon)
                {
                    g.DrawImageUnscaled(SelectedItem.Icon, new Point(ClientRectangle.Left + 5, ClientRectangle.Top + (ClientRectangle.Height / 2) - (_iconSize / 2)));
                }

                // Draw Text
                using SolidBrush b = new(ThemeProvider.Theme.Colors.LightText);
                StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };

                Rectangle rect = new(ClientRectangle.Left + 2, ClientRectangle.Top, ClientRectangle.Width - 16, ClientRectangle.Height);

                if (hasIcon)
                {
                    rect.X += _iconSize + 7;
                    rect.Width -= _iconSize + 7;
                }

                g.DrawString(SelectedItem.Text, Font, b, rect, stringFormat);
            }
        }

        #endregion
    }

    #endregion
}