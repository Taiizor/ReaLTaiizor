#region Imports

using ReaLTaiizor.Extension.Crown;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Util
{
    #region CrownUtil

    public class Sizes
    {
        public int Padding { get; set; } = 10;

        public int ScrollBarSize { get; set; } = 15;
        public int ArrowButtonSize { get; set; } = 15;
        public int MinimumThumbSize { get; set; } = 11;

        public int CheckBoxSize { get; set; } = 12;
        public int RadioButtonSize { get; set; } = 12;

        public int ToolWindowHeaderSize { get; set; } = 25;
        public int DocumentTabAreaSize { get; set; } = 24;
        public int ToolWindowTabAreaSize { get; set; } = 21;
    }

    public class ObservableListModified<T> : EventArgs
    {
        public IEnumerable<T> Items { get; private set; }

        public ObservableListModified(IEnumerable<T> items)
        {
            Items = items;
        }
    }

    public class ObservableList<T> : List<T>, IDisposable
    {
        #region Field Region

        private bool _disposed;

        #endregion

        #region Event Region

        public event EventHandler<ObservableListModified<T>> ItemsAdded;
        public event EventHandler<ObservableListModified<T>> ItemsRemoved;

        #endregion

        #region Destructor Region

        ~ObservableList()
        {
            Dispose(false);
        }

        #endregion

        #region Dispose Region

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (ItemsAdded != null)
                {
                    ItemsAdded = null;
                }

                if (ItemsRemoved != null)
                {
                    ItemsRemoved = null;
                }

                _disposed = true;
            }
        }

        #endregion

        #region Method Region

        public new void Add(T item)
        {
            base.Add(item);

            ItemsAdded?.Invoke(this, new ObservableListModified<T>(new List<T> { item }));
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            List<T> list = collection.ToList();

            base.AddRange(list);

            ItemsAdded?.Invoke(this, new ObservableListModified<T>(list));
        }

        public new void Remove(T item)
        {
            base.Remove(item);

            ItemsRemoved?.Invoke(this, new ObservableListModified<T>(new List<T> { item }));
        }

        public new void Clear()
        {
            ObservableList<T> thisis = this;
            ObservableListModified<T> removed = new(thisis.ToList<T>());
            base.Clear();

            if (removed.Items.Any() && ItemsRemoved != null)
            {
                ItemsRemoved(this, removed);
            }
        }

        #endregion
    }

    public class MenuRenderer : System.Windows.Forms.ToolStripRenderer
    {
        #region Initialisation Region

        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);

            toolStrip.BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            toolStrip.ForeColor = ThemeProvider.Theme.Colors.LightText;
        }

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            item.BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            item.ForeColor = ThemeProvider.Theme.Colors.LightText;

            if (item.GetType() == typeof(ToolStripSeparator))
            {
                item.Margin = new Padding(0, 0, 0, 1);
            }
        }

        #endregion

        #region Render Region

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            using SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground);
            g.FillRectangle(b, e.AffectedBounds);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);

            using Pen p = new(ThemeProvider.Theme.Colors.LightBorder);
            g.DrawRectangle(p, rect);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new(e.ImageRectangle.Left - 2, e.ImageRectangle.Top - 2,
                                         e.ImageRectangle.Width + 4, e.ImageRectangle.Height + 4);

            using (SolidBrush b = new(ThemeProvider.Theme.Colors.LightBorder))
            {
                g.FillRectangle(b, rect);
            }

            using (Pen p = new(ThemeProvider.Theme.Colors.BlueHighlight))
            {
                Rectangle modRect = new(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
                g.DrawRectangle(p, modRect);
            }

            if (e.Item.ImageIndex == -1 && string.IsNullOrEmpty(e.Item.ImageKey) && e.Item.Image == null)
            {
                g.DrawImageUnscaled(Properties.Resources.tick, new Point(e.ImageRectangle.Left, e.ImageRectangle.Top));
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new(1, 3, e.Item.Width, 1);

            using SolidBrush b = new(ThemeProvider.Theme.Colors.LightBorder);
            g.FillRectangle(b, rect);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = ThemeProvider.Theme.Colors.LightText;
            e.ArrowRectangle = new(new Point(e.ArrowRectangle.Left, e.ArrowRectangle.Top - 1), e.ArrowRectangle.Size);

            base.OnRenderArrow(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            e.Item.ForeColor = e.Item.Enabled ? ThemeProvider.Theme.Colors.LightText : ThemeProvider.Theme.Colors.DisabledText;

            if (e.Item.Enabled)
            {

                Color bgColor = e.Item.Selected ? ThemeProvider.Theme.Colors.GreyHighlight : e.Item.BackColor;

                // Normal item
                Rectangle rect = new(2, 0, e.Item.Width - 3, e.Item.Height);

                using (SolidBrush b = new(bgColor))
                {
                    g.FillRectangle(b, rect);
                }

                // Header item on open menu
                if (e.Item.GetType() == typeof(ToolStripMenuItem))
                {
                    if (((ToolStripMenuItem)e.Item).DropDown.Visible && e.Item.IsOnDropDown == false)
                    {
                        using SolidBrush b = new(ThemeProvider.Theme.Colors.GreySelection);
                        g.FillRectangle(b, rect);
                    }
                }
            }
        }

        #endregion
    }

    public class ToolStripRenderer : MenuRenderer
    {
        #region Initialisation Region

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            if (item.GetType() == typeof(ToolStripSeparator))
            {
                ToolStripSeparator castItem = (ToolStripSeparator)item;
                if (!castItem.IsOnDropDown)
                {
                    item.Margin = new Padding(0, 0, 2, 0);
                }
            }

            if (item.GetType() == typeof(ToolStripButton))
            {
                item.AutoSize = false;
                item.Size = new(24, 24);
            }
        }

        #endregion

        #region Render Region

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            Graphics g = e.Graphics;

            if (e.ToolStrip.GetType() == typeof(ToolStripOverflow))
            {
                using Pen p = new(ThemeProvider.Theme.Colors.GreyBackground);
                Rectangle rect = new(e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
                g.DrawRectangle(p, rect);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.GetType() != typeof(ToolStrip))
            {
                base.OnRenderToolStripBorder(e);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new(0, 1, e.Item.Width, e.Item.Height - 2);

            if (e.Item.Selected || e.Item.Pressed)
            {
                using SolidBrush b = new(ThemeProvider.Theme.Colors.GreySelection);
                g.FillRectangle(b, rect);
            }

            if (e.Item.GetType() == typeof(ToolStripButton))
            {
                ToolStripButton castItem = (ToolStripButton)e.Item;

                if (castItem.Checked)
                {
                    using SolidBrush b = new(ThemeProvider.Theme.Colors.GreySelection);
                    g.FillRectangle(b, rect);
                }

                if (castItem.Checked && castItem.Selected)
                {
                    Rectangle modRect = new(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
                    using Pen p = new(ThemeProvider.Theme.Colors.GreyHighlight);
                    g.DrawRectangle(p, modRect);
                }
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new(0, 1, e.Item.Width, e.Item.Height - 2);

            if (e.Item.Selected || e.Item.Pressed)
            {
                using SolidBrush b = new(ThemeProvider.Theme.Colors.GreySelection);
                g.FillRectangle(b, rect);
            }
        }

        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            if (e.GripStyle == ToolStripGripStyle.Hidden)
            {
                return;
            }

            Graphics g = e.Graphics;

            using Bitmap img = Properties.Resources.grip.SetColor(ThemeProvider.Theme.Colors.LightBorder);
            g.DrawImageUnscaled(img, new Point(e.AffectedBounds.Left, e.AffectedBounds.Top));
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            ToolStripSeparator castItem = (ToolStripSeparator)e.Item;
            if (castItem.IsOnDropDown)
            {
                base.OnRenderSeparator(e);
                return;
            }

            Rectangle rect = new(3, 3, 2, e.Item.Height - 4);

            using (Pen p = new(ThemeProvider.Theme.Colors.DarkBorder))
            {
                g.DrawLine(p, rect.Left, rect.Top, rect.Left, rect.Height);
            }

            using (Pen p = new(ThemeProvider.Theme.Colors.LightBorder))
            {
                g.DrawLine(p, rect.Left + 1, rect.Top, rect.Left + 1, rect.Height);
            }
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            if (e.Image == null)
            {
                return;
            }

            if (e.Item.Enabled)
            {
                g.DrawImageUnscaled(e.Image, new Point(e.ImageRectangle.Left, e.ImageRectangle.Top));
            }
            else
            {
                ControlPaint.DrawImageDisabled(g, e.Image, e.ImageRectangle.Left, e.ImageRectangle.Top, Color.Transparent);
            }
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            /*
                var g = e.Graphics;

                var rect = new(1, 0, e.Item.Width - 5, e.Item.Height);

                var castItem = (ToolStripOverflowButton)e.Item;

                var bgColor = BasicThemeProvider.Theme.Colors.White;
                if (castItem.Selected)
                    bgColor = BasicThemeProvider.Theme.Colors.Weak(style);
                if (castItem.Pressed)
                    bgColor = BasicThemeProvider.Theme.Colors.Medium(style);

                using (var b = new(bgColor))
                {
                    g.FillRectangle(b, rect);
                }

                var fgColor = BasicThemeProvider.Theme.Colors.Grey;
                if (castItem.Selected)
                    fgColor = BasicThemeProvider.Theme.Colors.Medium(style);
                if (castItem.Pressed)
                    fgColor = BasicThemeProvider.Theme.Colors.Strong(style);

                using (var p = new(fgColor))
                {
                    var modRect = new(1, 0, e.Item.Width - 6, e.Item.Height - 1);
                    g.DrawRectangle(p, modRect);
                }

                using (var img = Properties.Resources.overflow.SetColor(BasicThemeProvider.Theme.Colors.MediumGrey))
                {
                    g.DrawImageUnscaled(img, e.Item.Width - 13, e.Item.Height - 9);
                }
            */
        }

        #endregion
    }

    public class ScrollValueEventArgs : EventArgs
    {
        public int Value { get; private set; }

        public ScrollValueEventArgs(int value)
        {
            Value = value;
        }
    }

    #endregion
}