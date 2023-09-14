#region Imports

using ReaLTaiizor.Colors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region RoyalUtil

    public abstract class ControlRoyalBase : Control
    {
        private Thread animationThread;
        private readonly double framesPerSecond = 1000 / 15;

        protected delegate void MoveControlDelegate(Point location);
        protected delegate void ResizeControlDelegate(Size size);
        protected delegate void SetControlBackColorDelegate(Color c);
        protected delegate void RefreshControlDelegate();

        public event EventHandler EffectStarted;
        public event EventHandler EffectEnded;

        public bool AnimateBackColorChange = false;
        public double SecondsToChange = 0.5;

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (AnimateBackColorChange)
                {
                    SetBackgroundColor(value, SecondsToChange);
                }
                else
                {
                    base.BackColor = value;
                }
            }
        }

        public ControlRoyalBase()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            EffectStarted = new EventHandler(OnEffectStarted);
            EffectEnded = new EventHandler(OnEffectEnded);
        }

        public new void Move(Point location, double seconds)
        {
            animationThread = new(new ThreadStart(delegate ()
            {
                MoveControl(location, seconds);
            }));
            animationThread.Start();
        }

        public new void Resize(Size size, double seconds)
        {
            animationThread = new(new ThreadStart(delegate ()
            {
                ResizeControl(size, seconds);
            }));
            animationThread.Start();
        }

        public void SetBackgroundColor(Color backColor, double seconds)
        {
            animationThread = new(new ThreadStart(delegate ()
            {
                FadeToColor(backColor, seconds);
            }));
            animationThread.Start();
        }

        private void MoveControl(Point location, double seconds)
        {
            double x = location.X - Location.X;
            double y = location.Y - Location.Y;

            double xStepDist = x / (framesPerSecond * seconds);
            double yStepDist = y / (framesPerSecond * seconds);

            double ox = Location.X;
            double oy = Location.Y;

            try
            {
                for (int i = 0; i < (seconds * framesPerSecond); i++)
                {
                    ox += xStepDist;
                    oy += yStepDist;

                    Point p = new((int)ox, (int)oy);

                    if (InvokeRequired)
                    {
                        Invoke(new MoveControlDelegate(SetControlLocation), p);
                        Invoke(new RefreshControlDelegate(RefreshControl));
                    }
                    else
                    {
                        Location = p;
                        Refresh();
                    }

                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ResizeControl(Size size, double seconds)
        {
            double x = size.Width - Width;
            double y = size.Height - Height;

            double xStepDist = x / (framesPerSecond * seconds);
            double yStepDist = y / (framesPerSecond * seconds);

            double ox = Width;
            double oy = Height;

            try
            {
                for (int i = 0; i < (seconds * framesPerSecond); i++)
                {
                    ox += xStepDist;
                    oy += yStepDist;

                    Size s = new((int)ox, (int)oy);

                    if (InvokeRequired)
                    {
                        Invoke(new ResizeControlDelegate(SetControlSize), s);
                        Invoke(new RefreshControlDelegate(RefreshControl));
                    }
                    else
                    {
                        Size = s;
                        Refresh();
                    }

                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FadeToColor(Color color, double seconds)
        {
            double r = color.R - BackColor.R;
            double g = color.G - BackColor.G;
            double b = color.B - BackColor.B;

            double rStep = r / (framesPerSecond * seconds);
            double gStep = g / (framesPerSecond * seconds);
            double bStep = b / (framesPerSecond * seconds);

            double or = BackColor.R;
            double og = BackColor.G;
            double ob = BackColor.B;

            try
            {
                for (int i = 0; i < (seconds * framesPerSecond); i++)
                {
                    or += rStep;
                    og += gStep;
                    ob += gStep;

                    Color c = Color.FromArgb((int)or, (int)og, (int)ob);

                    if (InvokeRequired)
                    {
                        Invoke(new SetControlBackColorDelegate(SetControlBackColor), c);
                        Invoke(new RefreshControlDelegate(RefreshControl));
                    }
                    else
                    {
                        BackColor = c;
                        Refresh();
                    }

                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void SetControlLocation(Point location)
        {
            Location = location;
        }

        protected void SetControlSize(Size size)
        {
            Size = size;
        }

        protected void SetControlBackColor(Color c)
        {
            BackColor = c;
        }

        protected void RefreshControl()
        {
            Refresh();
        }

        protected virtual void OnEffectStarted(object sender, EventArgs e)
        {

        }

        protected virtual void OnEffectEnded(object sender, EventArgs e)
        {

        }
    }

    public class RoyalListBoxItemCollection : Collection<object>
    {
        public event EventHandler ItemAdded;
        public event EventHandler ItemRemoved;

        public RoyalListBoxItemCollection()
        {
            ItemAdded = new EventHandler(OnItemAdded);
            ItemRemoved = new EventHandler(OnItemRemoved);
        }

        public void AddRange(IEnumerable<object> items)
        {
            foreach (object item in items)
            {
                Items.Add(item);
            }
        }

        protected override void InsertItem(int index, object item)
        {
            base.InsertItem(index, item);
            ItemAdded(this, EventArgs.Empty);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            ItemRemoved(this, EventArgs.Empty);
        }

        protected virtual void OnItemAdded(object sender, EventArgs e)
        {

        }

        protected virtual void OnItemRemoved(object sender, EventArgs e)
        {

        }
    }

    public class RoyalListBoxSelectedItemCollection : Collection<object>
    {
        public RoyalListBoxSelectedItemCollection()
        {

        }
    }

    public class RoyalListBoxSelectedIndexCollection : Collection<int>
    {
        public RoyalListBoxSelectedIndexCollection()
        {

        }
    }

    public class RoyalToolStripRenderer : ToolStripRenderer
    {
        public static Color ForeColor
        {
            get => RoyalColors.ForeColor;
            set => RoyalColors.ForeColor = value;
        }

        public static Color PressedForeColor
        {
            get => RoyalColors.PressedForeColor;
            set => RoyalColors.PressedForeColor = value;
        }

        public static Color BackColor
        {
            get => RoyalColors.BackColor;
            set => RoyalColors.BackColor = value;
        }

        public static Color SelectedColor
        {
            get => RoyalColors.HotTrackColor;
            set => RoyalColors.HotTrackColor = value;
        }

        public static Color PressedColor
        {
            get => RoyalColors.AccentColor;
            set => RoyalColors.AccentColor = value;
        }

        public RoyalToolStripRenderer()
        {

        }

        protected override void Initialize(ToolStrip toolStrip)
        {
            toolStrip.ForeColor = RoyalColors.ForeColor;
            toolStrip.BackColor = RoyalColors.BackColor;

            base.Initialize(toolStrip);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            //e.Graphics.FillRectangle(new SolidBrush(foreColor), e.ArrowRectangle);

            base.OnRenderArrow(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);

            Rectangle rect = new(0, 0, e.Item.Width, e.Item.Height);
            Color color = BackColor;

            if (e.Item.Selected && !e.Item.Pressed)
            {
                color = SelectedColor;
            }
            else if (e.Item.Pressed)
            {
                color = PressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(color), rect);
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderDropDownButtonBackground(e);

            Rectangle rect = new(0, 0, e.Item.Width, e.Item.Height);
            Color color = BackColor;

            if (e.Item.Selected && !e.Item.Pressed)
            {
                color = SelectedColor;
            }
            else if (e.Item.Pressed)
            {
                color = PressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(color), rect);
        }

        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderItemBackground(e);

            Rectangle rect = new(0, 0, e.Item.Width, e.Item.Height);
            Color color = BackColor;

            if (e.Item.Selected && !e.Item.Pressed)
            {
                color = SelectedColor;
            }
            else if (e.Item.Pressed)
            {
                color = PressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(color), rect);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);

            Rectangle rect = new(0, 0, e.Item.Width, e.Item.Height);
            Color color = BackColor;

            if (e.Item.Selected && !e.Item.Pressed)
            {
                color = SelectedColor;
            }
            else if (e.Item.Pressed)
            {
                color = PressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(color), rect);
        }

        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderLabelBackground(e);

            Rectangle rect = new(0, 0, e.Item.Width, e.Item.Height);
            Color color = BackColor;

            if (e.Item.Selected && !e.Item.Pressed)
            {
                color = SelectedColor;
            }
            else if (e.Item.Pressed)
            {
                color = PressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(color), rect);
        }

        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            base.OnRenderGrip(e);

            e.Graphics.DrawString("GRIP", SystemFonts.MenuFont, Brushes.Black, e.GripBounds);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
            e.Graphics.FillRectangle(new SolidBrush(BackColor), e.AffectedBounds);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            base.OnRenderItemText(e);

            Color foreColor = RoyalColors.ForeColor;

            if (e.Item.Pressed)
            {
                foreColor = RoyalColors.PressedForeColor;
            }

            TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle, foreColor,
                Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            //base.OnRenderItemCheck(e);

            e.Graphics.DrawImage(Properties.Resources.Check, e.ImageRectangle);
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemImage(e);
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderOverflowButtonBackground(e);

            Color color = BackColor;

            if (e.Item.Selected && !e.Item.Pressed)
            {
                color = SelectedColor;
            }
            else if (e.Item.Pressed)
            {
                color = PressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(color), e.Item.Bounds);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            Rectangle itemRect = e.Item.Bounds;

            if (e.Vertical)
            {
                e.Graphics.DrawLine(Pens.Gainsboro, new Point(0, 0), new Point(0, itemRect.Height));
                e.Graphics.DrawLine(Pens.WhiteSmoke, new Point(1, 0), new Point(1, itemRect.Height));
            }
            else
            {
                //e.Graphics.DrawLine(Pens.Gainsboro, new Point(0, 0), new Point(itemRect.Width, 0));
                //e.Graphics.DrawLine(Pens.WhiteSmoke, new Point(0, 1), new Point(itemRect.Width, 1));
                e.Graphics.DrawLine(new(Color.Gainsboro, 2), new Point(0, 0), new Point(itemRect.Width, 0));
            }
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderSplitButtonBackground(e);

            Color color = BackColor;

            if (e.Item.Selected && !e.Item.Pressed)
            {
                color = SelectedColor;
            }
            else if (e.Item.Pressed)
            {
                color = PressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(color), e.Item.Bounds);
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            base.OnRenderStatusStripSizingGrip(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);

            if (e.ToolStrip.IsDropDown)
            {
                Rectangle rect = new(0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
                e.Graphics.DrawRectangle(Pens.Gainsboro, rect);
            }
        }

        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            base.OnRenderToolStripContentPanelBackground(e);
        }

        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            base.OnRenderToolStripPanelBackground(e);
        }

        protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderToolStripStatusLabelBackground(e);
        }
    }

    public enum RoyalLayoutFlags
    {
        TextOnly,
        ImageOnly,
        ImageBeforeText,
        TextBeforeImage,
        TextAboveImage,
        ImageAboveText
    };

    #endregion
}