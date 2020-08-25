#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Utils
{
    #region HopeLibrary

    public static class HopeStringAlign
    {
        public static StringFormat TopLeft { get => new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near }; }

        public static StringFormat TopCenter { get => new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near }; }

        public static StringFormat TopRight { get => new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near }; }

        public static StringFormat Left { get => new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }; }

        public static StringFormat Center { get => new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }; }

        public static StringFormat Right { get => new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center }; }

        public static StringFormat BottomLeft { get => new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far }; }

        public static StringFormat BottomCenter { get => new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far }; }

        public static StringFormat BottomRight { get => new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far }; }
    }

    public enum HopeButtonType
    {
        Default = 0,
        Primary = 1,
        Success = 2,
        Info = 3,
        Warning = 4,
        Danger = 5
    }

    namespace HopeBase
    {
        public class ToolStripRender : ToolStripProfessionalRenderer
        {
            public ToolStripRender()
            {
                base.RoundedEdges = true;
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                e.ToolStrip.ForeColor = HopeColors.MainText;
                if (e.ToolStrip is ToolStripDropDown)
                {
                    var g = e.Graphics;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.Clear(Color.White);
                    var bg = RoundRectangle.CreateRoundRect(0.5f, 0.5f, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1, 3);
                    g.DrawPath(new Pen(HopeColors.OneLevelBorder, 1), bg);
                    g.FillPath(new SolidBrush(Color.White), bg);
                }
                else
                {
                    base.OnRenderToolStripBackground(e);
                }
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.ToolStrip is MenuStrip)
                {
                    if (e.Item.Selected || e.Item.Pressed)
                        e.Graphics.FillRectangle(new SolidBrush(HopeColors.FourLevelBorder), 0, 0, e.Item.Size.Width, e.Item.Height);
                    else
                        base.OnRenderMenuItemBackground(e);
                }
                else if (e.ToolStrip is ToolStripDropDown)
                {
                    if (e.Item.Selected)
                        e.Graphics.FillRectangle(new SolidBrush(HopeColors.FourLevelBorder), 0, 0, e.Item.Size.Width, e.Item.Height);
                }
                else
                    base.OnRenderMenuItemBackground(e);
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                e.Graphics.DrawLine(new Pen(HopeColors.OneLevelBorder, 1.5f), 5, 2.75f, e.Item.Width - 5, 2.75f);
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                e.Graphics.Clear(Color.White);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                var g = e.Graphics;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                var itemRect = GetItemRect(e.Item);
                var textRect = new Rectangle(0, itemRect.Y, itemRect.Width, itemRect.Height);
                g.DrawString(e.Text, new Font("Segoe UI", 11f), new SolidBrush(e.Item.Selected ? HopeColors.PrimaryColor : HopeColors.RegularText), textRect, HopeStringAlign.Center);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                var g = e.Graphics;
                g.DrawRectangle(new Pen(HopeColors.OneLevelBorder), new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = HopeColors.PlaceholderText;
                base.OnRenderArrow(e);
            }

            private Rectangle GetItemRect(ToolStripItem item)
            {
                return new Rectangle(0, item.ContentRectangle.Y, item.ContentRectangle.Width + 4, item.ContentRectangle.Height);
            }
        }

        public class DateRectHopeBase
        {
            public RectangleF Rect;
            public bool Drawn = false;
            public DateTime Date;

            public DateRectHopeBase(RectangleF pRect)
            {
                Rect = pRect;
            }
        }
    }

    #endregion
}