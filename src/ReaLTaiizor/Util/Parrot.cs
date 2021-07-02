#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region ParrotUtil

    #region Percantage

    public static class Percentage
    {
        public static int IntToPercent(int number, int total)
        {
            return Convert.ToInt32(Math.Round((double)(100 * number) / (double)total));
        }

        public static int PercentToInt(int number, int total)
        {
            return Convert.ToInt32(Math.Round((double)(total / 100) * (double)number));
        }
    }

    #endregion

    #region StripeRemoval

    public class StripeRemoval : ToolStripSystemRenderer
    {
        public StripeRemoval(Color borderColor)
        {
            BorderColor = borderColor;
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(BorderColor, 1f), new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1));
        }

        public Color BorderColor;
    }

    #endregion

    #region Widget

    public class Widget
    {
        public void SetWidget(Control C)
        {
            C.MouseDown += WidgetDown;
            C.MouseUp += WidgetUp;
            C.MouseMove += WidgetMove;
        }

        private void WidgetDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
        }

        private void WidgetUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void WidgetMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                ((Control)sender).Location = new Point(e.X, e.Y);
            }
        }

        private bool isDragging;
    }

    #endregion

    #endregion
}