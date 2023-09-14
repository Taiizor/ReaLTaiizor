#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverTreeView

    public class ForeverTreeView : TreeView
    {
        private readonly TreeNodeStates State;

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            try
            {
                Rectangle Bounds = new(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Width, e.Bounds.Height);
                //e.Node.Nodes.Item.
                switch (State)
                {
                    case TreeNodeStates.Default:
                        e.Graphics.FillRectangle(Brushes.Red, Bounds);
                        e.Graphics.DrawString(e.Node.Text, Font, Brushes.LimeGreen, new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);
                        Invalidate();
                        break;
                    case TreeNodeStates.Checked:
                        e.Graphics.FillRectangle(Brushes.Green, Bounds);
                        e.Graphics.DrawString(e.Node.Text, Font, Brushes.Black, new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);
                        Invalidate();
                        break;
                    case TreeNodeStates.Selected:
                        e.Graphics.FillRectangle(Brushes.Green, Bounds);
                        e.Graphics.DrawString(e.Node.Text, Font, Brushes.Black, new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);
                        Invalidate();
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            base.OnDrawNode(e);
        }

        private readonly Color _BaseColor = Color.FromArgb(45, 47, 49);
        private readonly Color _LineColor = Color.FromArgb(25, 27, 29);

        public ForeverTreeView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);

            SetStyle(ControlStyles.UserPaint, value: false);
            SetStyle(ControlStyles.StandardClick, value: false);
            SetStyle(ControlStyles.UseTextForAccessibility, value: false);

            DoubleBuffered = true;

            BackColor = _BaseColor;
            ForeColor = Color.White;
            LineColor = _LineColor;
            DrawMode = TreeViewDrawMode.Normal;

            Font = new("Segoe UI", 8);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Rectangle Base = new(0, 0, Width, Height);

            Graphics _with22 = G;
            _with22.SmoothingMode = SmoothingMode.HighQuality;
            _with22.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with22.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with22.Clear(BackColor);

            _with22.FillRectangle(new SolidBrush(_BaseColor), Base);
            _with22.DrawString(Text, Font, new SolidBrush(_LineColor), new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);

            base.OnPaint(e);

            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
    }

    #endregion
}