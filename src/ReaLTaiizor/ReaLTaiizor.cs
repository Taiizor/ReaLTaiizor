#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

// |---------DO-NOT-REMOVE---------|
//
//     Creator: Taiizor
//     Website: www.Vegalya.com
//     Created: 15.May.2019
//     Changed: 14.Nov.2023
//     Version: 3.7.9.8
//
// |---------DO-NOT-REMOVE---------|

namespace ReaLTaiizor
{
    #region Core

    #region RoundRectangle

    public sealed class RoundRectangle
    {
        public static GraphicsPath RoundRect(Rectangle Rectangle, int Curve)
        {
            GraphicsPath GP = new();

            int ArcRectangleWidth = Curve * 2;

            GP.AddArc(new(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90);
            GP.AddArc(new(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90);
            GP.AddArc(new(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90);
            GP.AddArc(new(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90);
            GP.AddLine(new Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));

            return GP;
        }

        public static GraphicsPath RoundRect(int X, int Y, int Width, int Height, int Curve)
        {
            Rectangle Rectangle = new(X, Y, Width, Height);

            GraphicsPath GP = new();

            int EndArcWidth = Curve * 2;

            GP.AddArc(new(Rectangle.X, Rectangle.Y, EndArcWidth, EndArcWidth), -180, 90);
            GP.AddArc(new(Rectangle.Width - EndArcWidth + Rectangle.X, Rectangle.Y, EndArcWidth, EndArcWidth), -90, 90);
            GP.AddArc(new(Rectangle.Width - EndArcWidth + Rectangle.X, Rectangle.Height - EndArcWidth + Rectangle.Y, EndArcWidth, EndArcWidth), 0, 90);
            GP.AddArc(new(Rectangle.X, Rectangle.Height - EndArcWidth + Rectangle.Y, EndArcWidth, EndArcWidth), 90, 90);
            GP.AddLine(new Point(Rectangle.X, Rectangle.Height - EndArcWidth + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));

            return GP;
        }

        public static GraphicsPath RoundedTopRect(Rectangle Rectangle, int Curve)
        {
            GraphicsPath GP = new();

            int ArcRectangleWidth = Curve * 2;

            GP.AddArc(new(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90);
            GP.AddArc(new(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90);
            GP.AddLine(new Point(Rectangle.X + Rectangle.Width, Rectangle.Y + ArcRectangleWidth), new Point(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height - 1));
            GP.AddLine(new Point(Rectangle.X, Rectangle.Height - 1 + Rectangle.Y), new Point(Rectangle.X, Rectangle.Y + Curve));

            return GP;
        }

        public static GraphicsPath CreateRoundRect(float X, float Y, float Width, float Height, float Radius)
        {
            GraphicsPath GP = new();
            GP.AddLine(X + Radius, Y, X + Width - (Radius * 2), Y);
            GP.AddArc(X + Width - (Radius * 2), Y, Radius * 2, Radius * 2, 270, 90);

            GP.AddLine(X + Width, Y + Radius, X + Width, Y + Height - (Radius * 2));
            GP.AddArc(X + Width - (Radius * 2), Y + Height - (Radius * 2), Radius * 2, Radius * 2, 0, 90);

            GP.AddLine(X + Width - (Radius * 2), Y + Height, X + Radius, Y + Height);
            GP.AddArc(X, Y + Height - (Radius * 2), Radius * 2, Radius * 2, 90, 90);

            GP.AddLine(X, Y + Height - (Radius * 2), X, Y + Radius);
            GP.AddArc(X, Y, Radius * 2, Radius * 2, 180, 90);

            GP.CloseFigure();

            return GP;
        }

        public static GraphicsPath CreateUpRoundRect(float X, float Y, float Width, float Height, float Radius)
        {
            GraphicsPath GP = new();

            GP.AddLine(X + Radius, Y, X + Width - (Radius * 2), Y);
            GP.AddArc(X + Width - (Radius * 2), Y, Radius * 2, Radius * 2, 270, 90);

            GP.AddLine(X + Width, Y + Radius, X + Width, Y + Height - (Radius * 2) + 1);
            GP.AddArc(X + Width - (Radius * 2), Y + Height - (Radius * 2), Radius * 2, 2, 0, 90);

            GP.AddLine(X + Width, Y + Height, X + Radius, Y + Height);
            GP.AddArc(X, Y + Height - (Radius * 2) + 1, Radius * 2, 1, 90, 90);

            GP.AddLine(X, Y + Height, X, Y + Radius);
            GP.AddArc(X, Y, Radius * 2, Radius * 2, 180, 90);

            GP.CloseFigure();

            return GP;
        }

        public static GraphicsPath CreateLeftRoundRect(float X, float Y, float Width, float Height, float Radius)
        {
            GraphicsPath GP = new();
            GP.AddLine(X + Radius, Y, X + Width - (Radius * 2), Y);
            GP.AddArc(X + Width - (Radius * 2), Y, Radius * 2, Radius * 2, 270, 90);

            GP.AddLine(X + Width, Y + 0, X + Width, Y + Height);
            GP.AddArc(X + Width - (Radius * 2), Y + Height - 1, Radius * 2, 1, 0, 90);

            GP.AddLine(X + Width - (Radius * 2), Y + Height, X + Radius, Y + Height);
            GP.AddArc(X, Y + Height - (Radius * 2), Radius * 2, Radius * 2, 90, 90);

            GP.AddLine(X, Y + Height - (Radius * 2), X, Y + Radius);
            GP.AddArc(X, Y, Radius * 2, Radius * 2, 180, 90);

            GP.CloseFigure();

            return GP;
        }

        public static Color BlendColor(Color BackgroundColor, Color FrontColor)
        {
            double Ratio = 0 / 255d;
            double InvRatio = 1d - Ratio;

            int R = (int)((BackgroundColor.R * InvRatio) + (FrontColor.R * Ratio));
            int G = (int)((BackgroundColor.G * InvRatio) + (FrontColor.G * Ratio));
            int B = (int)((BackgroundColor.B * InvRatio) + (FrontColor.B * Ratio));

            return Color.FromArgb(R, G, B);
        }

        public static Color BackColor = ColorTranslator.FromHtml("#DADCDF"); //BCBFC4
        public static Color DarkBackColor = ColorTranslator.FromHtml("#90949A");
        public static Color LightBackColor = ColorTranslator.FromHtml("#F5F5F5");
    }

    #endregion

    #region ControlRenderer

    #region Color Table

    public abstract class XColorTable
    {
        public abstract Color TextColor { get; }
        public abstract Color Background { get; }
        public abstract Color SelectionBorder { get; }
        public abstract Color SelectionTopGradient { get; }
        public abstract Color SelectionMidGradient { get; }
        public abstract Color SelectionBottomGradient { get; }
        public abstract Color PressedBackground { get; }
        public abstract Color CheckedBackground { get; }
        public abstract Color CheckedSelectedBackground { get; }
        public abstract Color DropdownBorder { get; }
        public abstract Color Arrow { get; }
        public abstract Color OverflowBackground { get; }
    }

    public abstract class ColorTable
    {
        public abstract XColorTable CommonColorTable { get; }
        public abstract Color BackgroundTopGradient { get; }
        public abstract Color BackgroundBottomGradient { get; }
        public abstract Color DroppedDownItemBackground { get; }
        public abstract Color DropdownTopGradient { get; }
        public abstract Color DropdownBottomGradient { get; }
        public abstract Color Separator { get; }
        public abstract Color ImageMargin { get; }
    }

    public class MSColorTable : ColorTable
    {

        private readonly XColorTable _CommonColorTable;

        public MSColorTable()
        {
            _CommonColorTable = new DefaultCColorTable();
        }

        public override XColorTable CommonColorTable => _CommonColorTable;

        public override Color BackgroundTopGradient => Color.FromArgb(246, 246, 246);

        public override Color BackgroundBottomGradient => Color.FromArgb(226, 226, 226);

        public override Color DropdownTopGradient => Color.FromArgb(246, 246, 246);

        public override Color DropdownBottomGradient => Color.FromArgb(246, 246, 246);

        public override Color DroppedDownItemBackground => Color.FromArgb(240, 240, 240);

        public override Color Separator => Color.FromArgb(190, 195, 203);

        public override Color ImageMargin => Color.FromArgb(240, 240, 240);
    }

    public class DefaultCColorTable : XColorTable
    {

        public override Color CheckedBackground => Color.FromArgb(230, 230, 230);

        public override Color CheckedSelectedBackground => Color.FromArgb(230, 230, 230);

        public override Color SelectionBorder => Color.FromArgb(180, 180, 180);

        public override Color SelectionTopGradient => Color.FromArgb(240, 240, 240);

        public override Color SelectionMidGradient => Color.FromArgb(235, 235, 235);

        public override Color SelectionBottomGradient => Color.FromArgb(230, 230, 230);

        public override Color PressedBackground => Color.FromArgb(232, 232, 232);

        public override Color TextColor => Color.FromArgb(80, 80, 80);

        public override Color Background => Color.FromArgb(188, 199, 216);

        public override Color DropdownBorder => Color.LightGray;

        public override Color Arrow => Color.Black;

        public override Color OverflowBackground => Color.FromArgb(213, 220, 232);
    }

    #endregion

    #region Renderer

    public class ControlRenderer : ToolStripProfessionalRenderer
    {
        public ControlRenderer() : this(new MSColorTable()) { }

        public ControlRenderer(ColorTable ColorTable)
        {
            ControlRenderer thisis = this;
            thisis.ColorTable = ColorTable;
        }

        private ColorTable _ColorTable;
        public new ColorTable ColorTable
        {
            get
            {
                if (_ColorTable == null)
                {
                    _ColorTable = new MSColorTable();
                }

                return _ColorTable;
            }
            set => _ColorTable = value;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            // Menu strip bar gradient
            using LinearGradientBrush LGB = new(e.AffectedBounds, ColorTable.BackgroundTopGradient, ColorTable.BackgroundBottomGradient, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(LGB, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.Parent == null)
            {
                // Draw border around the menu drop-down
                Rectangle Rect = new(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                using (Pen P1 = new(ColorTable.CommonColorTable.DropdownBorder))
                {
                    e.Graphics.DrawRectangle(P1, Rect);
                }

                // Fill the gap between menu drop-down and owner item
                using SolidBrush B1 = new(ColorTable.DroppedDownItemBackground);
                e.Graphics.FillRectangle(B1, e.ConnectedArea);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                if (e.Item.Selected)
                {
                    if (!e.Item.IsOnDropDown)
                    {
                        Rectangle SelRect = new(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                        RectDrawing.DrawSelection(e.Graphics, ColorTable.CommonColorTable, SelRect);
                    }
                    else
                    {
                        Rectangle SelRect = new(2, 0, e.Item.Width - 4, e.Item.Height - 1);
                        RectDrawing.DrawSelection(e.Graphics, ColorTable.CommonColorTable, SelRect);
                    }
                }

                if (((ToolStripMenuItem)e.Item).DropDown.Visible && !e.Item.IsOnDropDown)
                {
                    Rectangle BorderRect = new(0, 0, e.Item.Width - 1, e.Item.Height);
                    // Fill the background
                    Rectangle BackgroundRect = new(1, 1, e.Item.Width - 2, e.Item.Height + 2);
                    using (SolidBrush B1 = new(ColorTable.DroppedDownItemBackground))
                    {
                        e.Graphics.FillRectangle(B1, BackgroundRect);
                    }

                    // Draw border
                    using Pen P1 = new(ColorTable.CommonColorTable.DropdownBorder);
                    RectDrawing.DrawRoundedRectangle(e.Graphics, P1, Convert.ToSingle(BorderRect.X), Convert.ToSingle(BorderRect.Y), Convert.ToSingle(BorderRect.Width), Convert.ToSingle(BorderRect.Height), 2);
                }
                e.Item.ForeColor = ColorTable.CommonColorTable.TextColor;
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = ColorTable.CommonColorTable.TextColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            Rectangle rect = new(3, 1, e.Item.Height - 3, e.Item.Height - 3);
            Color c = default;

            if (e.Item.Selected)
            {
                c = ColorTable.CommonColorTable.CheckedSelectedBackground;
            }
            //else

            using (SolidBrush b = new(c))
            {
                e.Graphics.FillRectangle(b, rect);
            }

            using (Pen p = new(ColorTable.CommonColorTable.SelectionBorder))
            {
                e.Graphics.DrawRectangle(p, rect);
            }

            e.Graphics.DrawString("ü", new Font("Wingdings", 13, FontStyle.Regular), Brushes.Black, new Point(4, 2));
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            int PT1 = 28;
            int PT2 = Convert.ToInt32(e.Item.Width);
            int Y = 3;
            using Pen P1 = new(ColorTable.Separator);
            e.Graphics.DrawLine(P1, PT1, Y, PT2, Y);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            Rectangle BackgroundRect = new(0, -1, e.ToolStrip.Width, e.ToolStrip.Height + 1);
            using (LinearGradientBrush LGB = new(BackgroundRect, ColorTable.DropdownTopGradient, ColorTable.DropdownBottomGradient, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(LGB, BackgroundRect);
            }

            using SolidBrush B1 = new(ColorTable.ImageMargin);
            e.Graphics.FillRectangle(B1, e.AffectedBounds);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            bool @checked = Convert.ToBoolean(((ToolStripButton)e.Item).Checked);
            bool drawBorder = false;

            if (@checked)
            {
                drawBorder = true;

                if (e.Item.Selected && !e.Item.Pressed)
                {
                    using SolidBrush b = new(ColorTable.CommonColorTable.CheckedSelectedBackground);
                    e.Graphics.FillRectangle(b, rect);
                }
                else
                {
                    using SolidBrush b = new(ColorTable.CommonColorTable.CheckedBackground);
                    e.Graphics.FillRectangle(b, rect);
                }

            }
            else
            {

                if (e.Item.Pressed)
                {
                    drawBorder = true;
                    using SolidBrush b = new(ColorTable.CommonColorTable.PressedBackground);
                    e.Graphics.FillRectangle(b, rect);
                }
                else if (e.Item.Selected)
                {
                    drawBorder = true;
                    RectDrawing.DrawSelection(e.Graphics, ColorTable.CommonColorTable, rect);
                }

            }

            if (drawBorder)
            {
                using Pen p = new(ColorTable.CommonColorTable.SelectionBorder);
                e.Graphics.DrawRectangle(p, rect);
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            bool drawBorder = false;

            if (e.Item.Pressed)
            {
                drawBorder = true;
                using SolidBrush b = new(ColorTable.CommonColorTable.PressedBackground);
                e.Graphics.FillRectangle(b, rect);
            }
            else if (e.Item.Selected)
            {
                drawBorder = true;
                RectDrawing.DrawSelection(e.Graphics, ColorTable.CommonColorTable, rect);
            }

            if (drawBorder)
            {
                using Pen p = new(ColorTable.CommonColorTable.SelectionBorder);
                e.Graphics.DrawRectangle(p, rect);
            }
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderSplitButtonBackground(e);
            bool drawBorder = false;
            bool drawSeparator = true;
            ToolStripSplitButton item = (ToolStripSplitButton)e.Item;
            checked
            {
                Rectangle btnRect = new(0, 0, item.ButtonBounds.Width - 1, item.ButtonBounds.Height - 1);
                Rectangle borderRect = new(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);
                bool flag = item.DropDownButtonPressed;
                if (flag)
                {
                    drawBorder = true;
                    drawSeparator = false;
                    SolidBrush b = new(ColorTable.CommonColorTable.PressedBackground);
                    try
                    {
                        e.Graphics.FillRectangle(b, borderRect);
                    }
                    finally
                    {
                        flag = b != null;
                        if (flag)
                        {
                            b.Dispose();
                        }
                    }
                }
                else
                {
                    flag = item.DropDownButtonSelected;
                    if (flag)
                    {
                        drawBorder = true;
                        RectDrawing.DrawSelection(e.Graphics, ColorTable.CommonColorTable, borderRect);
                    }
                }
                flag = item.ButtonPressed;
                if (flag)
                {
                    SolidBrush b2 = new(ColorTable.CommonColorTable.PressedBackground);
                    try
                    {
                        e.Graphics.FillRectangle(b2, btnRect);
                    }
                    finally
                    {
                        flag = b2 != null;
                        if (flag)
                        {
                            b2.Dispose();
                        }
                    }
                }
                flag = drawBorder;
                if (flag)
                {
                    Pen p = new(ColorTable.CommonColorTable.SelectionBorder);
                    try
                    {
                        e.Graphics.DrawRectangle(p, borderRect);
                        flag = drawSeparator;
                        if (flag)
                        {
                            e.Graphics.DrawRectangle(p, btnRect);
                        }
                    }
                    finally
                    {
                        flag = p != null;
                        if (flag)
                        {
                            p.Dispose();
                        }
                    }
                    DrawCustomArrow(e.Graphics, item);
                }
            }
        }

        private void DrawCustomArrow(Graphics g, ToolStripSplitButton item)
        {
            int dropWidth = Convert.ToInt32(item.DropDownButtonBounds.Width - 1);
            int dropHeight = Convert.ToInt32(item.DropDownButtonBounds.Height - 1);
            float triangleWidth = (dropWidth / 2.0F) + 1;
            float triangleLeft = Convert.ToSingle(item.DropDownButtonBounds.Left + ((dropWidth - triangleWidth) / 2.0F));
            float triangleHeight = triangleWidth / 2.0F;
            float triangleTop = Convert.ToSingle(item.DropDownButtonBounds.Top + ((dropHeight - triangleHeight) / 2.0F) + 1);
            RectangleF arrowRect = new(triangleLeft, triangleTop, triangleWidth, triangleHeight);

            DrawCustomArrow(g, item, Rectangle.Round(arrowRect));
        }

        private void DrawCustomArrow(Graphics g, ToolStripItem item, Rectangle rect)
        {
            ToolStripArrowRenderEventArgs arrowEventArgs = new(g, item, rect, ColorTable.CommonColorTable.Arrow, ArrowDirection.Down);
            base.OnRenderArrow(arrowEventArgs);
        }

        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new(0, 0, e.Item.Width - 1, e.Item.Height - 2);
            Rectangle rectEnd = new(rect.X - 5, rect.Y, rect.Width - 5, rect.Height);

            if (e.Item.Pressed)
            {
                using SolidBrush b = new(ColorTable.CommonColorTable.PressedBackground);
                e.Graphics.FillRectangle(b, rect);
            }
            else if (e.Item.Selected)
            {
                RectDrawing.DrawSelection(e.Graphics, ColorTable.CommonColorTable, rect);
            }
            else
            {
                using SolidBrush b = new(ColorTable.CommonColorTable.OverflowBackground);
                e.Graphics.FillRectangle(b, rect);
            }

            using (Pen P1 = new(ColorTable.CommonColorTable.Background))
            {
                RectDrawing.DrawRoundedRectangle(e.Graphics, P1, Convert.ToSingle(rectEnd.X), Convert.ToSingle(rectEnd.Y), Convert.ToSingle(rectEnd.Width), Convert.ToSingle(rectEnd.Height), 3);
            }

            // Icon
            int w = Convert.ToInt32(rect.Width - 1);
            int h = Convert.ToInt32(rect.Height - 1);
            float triangleWidth = (w / 2.0F) + 1;
            float triangleLeft = Convert.ToSingle(rect.Left + ((w - triangleWidth) / 2.0F) + 3);
            float triangleHeight = triangleWidth / 2.0F;
            float triangleTop = Convert.ToSingle(rect.Top + ((h - triangleHeight) / 2.0F) + 7);
            RectangleF arrowRect = new(triangleLeft, triangleTop, triangleWidth, triangleHeight);
            DrawCustomArrow(e.Graphics, e.Item, Rectangle.Round(arrowRect));

            using Pen p = new(ColorTable.CommonColorTable.Arrow);
            e.Graphics.DrawLine(p, triangleLeft + 2, triangleTop - 2, triangleLeft + triangleWidth - 2, triangleTop - 2);
        }
    }

    #endregion

    #region Drawing

    public class RectDrawing
    {
        public static void DrawSelection(Graphics G, XColorTable ColorTable, Rectangle Rect)
        {
            Rectangle FillRect = new(Rect.X + 1, Rect.Y + 1, Rect.Width - 1, Rect.Height - 1);

            Rectangle TopRect = FillRect;
            TopRect.Height -= Convert.ToInt32(TopRect.Height / 2);
            Rectangle BottomRect = new(TopRect.X, TopRect.Bottom, TopRect.Width, FillRect.Height - TopRect.Height);

            // Top gradient
            using (LinearGradientBrush LGB = new(TopRect, ColorTable.SelectionTopGradient, ColorTable.SelectionMidGradient, LinearGradientMode.Vertical))
            {
                G.FillRectangle(LGB, TopRect);
            }

            // Bottom
            using (SolidBrush B1 = new(ColorTable.SelectionBottomGradient))
            {
                G.FillRectangle(B1, BottomRect);
            }

            // Border
            using Pen P1 = new(ColorTable.SelectionBorder);
            RectDrawing.DrawRoundedRectangle(G, P1, Convert.ToSingle(Rect.X), Convert.ToSingle(Rect.Y), Convert.ToSingle(Rect.Width), Convert.ToSingle(Rect.Height), 2);
        }

        public static void DrawRoundedRectangle(Graphics G, Pen P, float X, float Y, float W, float H, float Rad)
        {
            using GraphicsPath GP = new();
            GP.AddLine(X + Rad, Y, X + W - (Rad * 2), Y);
            GP.AddArc(X + W - (Rad * 2), Y, Rad * 2, Rad * 2, 270, 90);
            GP.AddLine(X + W, Y + Rad, X + W, Y + H - (Rad * 2));
            GP.AddArc(X + W - (Rad * 2), Y + H - (Rad * 2), Rad * 2, Rad * 2, 0, 90);
            GP.AddLine(X + W - (Rad * 2), Y + H, X + Rad, Y + H);
            GP.AddArc(X, Y + H - (Rad * 2), Rad * 2, Rad * 2, 90, 90);
            GP.AddLine(X, Y + H - (Rad * 2), X, Y + Rad);
            GP.AddArc(X, Y, Rad * 2, Rad * 2, 180, 90);
            GP.CloseFigure();

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.DrawPath(P, GP);
            G.SmoothingMode = SmoothingMode.Default;

        }
    }

    #endregion

    #endregion

    #region PaintHelper

    public abstract class PaintHelperA
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        private const int WM_SETREDRAW = 11;

        public static void Suspend(Control Parent)
        {
            _ = SendMessage(Parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void Resume(Control Parent)
        {
            _ = SendMessage(Parent.Handle, WM_SETREDRAW, true, 0);
            Parent.Refresh();
        }
    }

    public abstract class PaintHelperB
    {
        private const int WM_SETREDRAW = 0x000B;

        public static void Suspend(Control Parent)
        {
            Message msgSuspendUpdate = Message.Create(Parent.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(Parent.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        public static void Resume(Control Parent)
        {
            // Create a C "true" boolean as an IntPtr
            IntPtr wparam = new(1);
            Message msgResumeUpdate = Message.Create(Parent.Handle, WM_SETREDRAW, wparam, IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(Parent.Handle);
            window.DefWndProc(ref msgResumeUpdate);

            Parent.Invalidate();
        }
    }

    #endregion

    #endregion
}