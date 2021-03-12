#region Imports

using ReaLTaiizor.Colors;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region HopeUtil

    public static class HopeStringAlign
    {
        public static StringFormat TopLeft => new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };

        public static StringFormat TopCenter => new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };

        public static StringFormat TopRight => new() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };

        public static StringFormat Left => new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

        public static StringFormat Center => new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        public static StringFormat Right => new() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        public static StringFormat BottomLeft => new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };

        public static StringFormat BottomCenter => new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };

        public static StringFormat BottomRight => new() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };
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
                    Graphics g = e.Graphics;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.Clear(Color.White);
                    GraphicsPath bg = RoundRectangle.CreateRoundRect(0.5f, 0.5f, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1, 3);
                    g.DrawPath(new(HopeColors.OneLevelBorder, 1), bg);
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
                    {
                        e.Graphics.FillRectangle(new SolidBrush(HopeColors.FourLevelBorder), 0, 0, e.Item.Size.Width, e.Item.Height);
                    }
                    else
                    {
                        base.OnRenderMenuItemBackground(e);
                    }
                }
                else if (e.ToolStrip is ToolStripDropDown)
                {
                    if (e.Item.Selected)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(HopeColors.FourLevelBorder), 0, 0, e.Item.Size.Width, e.Item.Height);
                    }
                }
                else
                {
                    base.OnRenderMenuItemBackground(e);
                }
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                e.Graphics.DrawLine(new(HopeColors.OneLevelBorder, 1.5f), 5, 2.75f, e.Item.Width - 5, 2.75f);
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                e.Graphics.Clear(Color.White);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                Graphics g = e.Graphics;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                Rectangle itemRect = GetItemRect(e.Item);
                Rectangle textRect = new(0, itemRect.Y, itemRect.Width, itemRect.Height);
                g.DrawString(e.Text, new Font("Segoe UI", 11f), new SolidBrush(e.Item.Selected ? HopeColors.PrimaryColor : HopeColors.RegularText), textRect, HopeStringAlign.Center);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                Graphics g = e.Graphics;
                g.DrawRectangle(new(HopeColors.OneLevelBorder), new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = HopeColors.PlaceholderText;
                base.OnRenderArrow(e);
            }

            private static Rectangle GetItemRect(ToolStripItem item)
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