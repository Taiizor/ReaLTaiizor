#region Imports

using System;
using System.Linq;
using System.Drawing;
using ReaLTaiizor.Colors;
using System.Windows.Forms;
using System.Collections.Generic;
using ReaLTaiizor.Extension.Crown;

#endregion

namespace ReaLTaiizor.Util
{
    #region CrownUtil

    public sealed class Consts
    {
        public static int Padding = 10;

        public static int ScrollBarSize = 15;
        public static int ArrowButtonSize = 15;
        public static int MinimumThumbSize = 11;

        public static int CheckBoxSize = 12;
        public static int RadioButtonSize = 12;

        public const int ToolWindowHeaderSize = 25;
        public const int DocumentTabAreaSize = 24;
        public const int ToolWindowTabAreaSize = 21;
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

            if (ItemsAdded != null)
            {
                ItemsAdded(this, new ObservableListModified<T>(new List<T> { item }));
            }
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            List<T> list = collection.ToList();

            base.AddRange(list);

            if (ItemsAdded != null)
            {
                ItemsAdded(this, new ObservableListModified<T>(list));
            }
        }

        public new void Remove(T item)
        {
            base.Remove(item);

            if (ItemsRemoved != null)
            {
                ItemsRemoved(this, new ObservableListModified<T>(new List<T> { item }));
            }
        }

        public new void Clear()
        {
            ObservableList<T> thisis = this;
            ObservableListModified<T> removed = new ObservableListModified<T>(thisis.ToList<T>());
            base.Clear();

            if (removed.Items.Count() > 0 && ItemsRemoved != null)
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

            toolStrip.BackColor = CrownColors.GreyBackground;
            toolStrip.ForeColor = CrownColors.LightText;
        }

        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);

            item.BackColor = CrownColors.GreyBackground;
            item.ForeColor = CrownColors.LightText;

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
            using (SolidBrush b = new SolidBrush(CrownColors.GreyBackground))
            {
                g.FillRectangle(b, e.AffectedBounds);
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);

            using (Pen p = new Pen(CrownColors.LightBorder))
            {
                g.DrawRectangle(p, rect);
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(e.ImageRectangle.Left - 2, e.ImageRectangle.Top - 2,
                                         e.ImageRectangle.Width + 4, e.ImageRectangle.Height + 4);

            using (SolidBrush b = new SolidBrush(CrownColors.LightBorder))
            {
                g.FillRectangle(b, rect);
            }

            using (Pen p = new Pen(CrownColors.BlueHighlight))
            {
                Rectangle modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
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

            Rectangle rect = new Rectangle(1, 3, e.Item.Width, 1);

            using (SolidBrush b = new SolidBrush(CrownColors.LightBorder))
            {
                g.FillRectangle(b, rect);
            }
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = CrownColors.LightText;
            e.ArrowRectangle = new Rectangle(new Point(e.ArrowRectangle.Left, e.ArrowRectangle.Top - 1), e.ArrowRectangle.Size);

            base.OnRenderArrow(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            e.Item.ForeColor = e.Item.Enabled ? CrownColors.LightText : CrownColors.DisabledText;

            if (e.Item.Enabled)
            {

                Color bgColor = e.Item.Selected ? CrownColors.GreyHighlight : e.Item.BackColor;

                // Normal item
                Rectangle rect = new Rectangle(2, 0, e.Item.Width - 3, e.Item.Height);

                using (SolidBrush b = new SolidBrush(bgColor))
                {
                    g.FillRectangle(b, rect);
                }

                // Header item on open menu
                if (e.Item.GetType() == typeof(ToolStripMenuItem))
                {
                    if (((ToolStripMenuItem)e.Item).DropDown.Visible && e.Item.IsOnDropDown == false)
                    {
                        using (SolidBrush b = new SolidBrush(CrownColors.GreySelection))
                        {
                            g.FillRectangle(b, rect);
                        }
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
                item.Size = new Size(24, 24);
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
                using (Pen p = new Pen(CrownColors.GreyBackground))
                {
                    Rectangle rect = new Rectangle(e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
                    g.DrawRectangle(p, rect);
                }
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

            Rectangle rect = new Rectangle(0, 1, e.Item.Width, e.Item.Height - 2);

            if (e.Item.Selected || e.Item.Pressed)
            {
                using (SolidBrush b = new SolidBrush(CrownColors.GreySelection))
                {
                    g.FillRectangle(b, rect);
                }
            }

            if (e.Item.GetType() == typeof(ToolStripButton))
            {
                ToolStripButton castItem = (ToolStripButton)e.Item;

                if (castItem.Checked)
                {
                    using (SolidBrush b = new SolidBrush(CrownColors.GreySelection))
                    {
                        g.FillRectangle(b, rect);
                    }
                }

                if (castItem.Checked && castItem.Selected)
                {
                    Rectangle modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
                    using (Pen p = new Pen(CrownColors.GreyHighlight))
                    {
                        g.DrawRectangle(p, modRect);
                    }
                }
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(0, 1, e.Item.Width, e.Item.Height - 2);

            if (e.Item.Selected || e.Item.Pressed)
            {
                using (SolidBrush b = new SolidBrush(CrownColors.GreySelection))
                {
                    g.FillRectangle(b, rect);
                }
            }
        }

        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            if (e.GripStyle == ToolStripGripStyle.Hidden)
            {
                return;
            }

            Graphics g = e.Graphics;

            using (Bitmap img = Properties.Resources.grip.SetColor(CrownColors.LightBorder))
            {
                g.DrawImageUnscaled(img, new Point(e.AffectedBounds.Left, e.AffectedBounds.Top));
            }
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

            Rectangle rect = new Rectangle(3, 3, 2, e.Item.Height - 4);

            using (Pen p = new Pen(CrownColors.DarkBorder))
            {
                g.DrawLine(p, rect.Left, rect.Top, rect.Left, rect.Height);
            }

            using (Pen p = new Pen(CrownColors.LightBorder))
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
            /*var g = e.Graphics;

            var rect = new Rectangle(1, 0, e.Item.Width - 5, e.Item.Height);

            var castItem = (ToolStripOverflowButton)e.Item;

            var bgColor = BasicColors.White;
            if (castItem.Selected)
                bgColor = StyleColors.Weak(style);
            if (castItem.Pressed)
                bgColor = StyleColors.Medium(style);

            using (var b = new SolidBrush(bgColor))
            {
                g.FillRectangle(b, rect);
            }

            var fgColor = BasicColors.Grey;
            if (castItem.Selected)
                fgColor = StyleColors.Medium(style);
            if (castItem.Pressed)
                fgColor = StyleColors.Strong(style);

            using (var p = new Pen(fgColor))
            {
                var modRect = new Rectangle(1, 0, e.Item.Width - 6, e.Item.Height - 1);
                g.DrawRectangle(p, modRect);
            }

            using (var img = MenuIcons.overflow.SetColor(BasicColors.MediumGrey))
            {
                g.DrawImageUnscaled(img, e.Item.Width - 13, e.Item.Height - 9);
            }*/
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