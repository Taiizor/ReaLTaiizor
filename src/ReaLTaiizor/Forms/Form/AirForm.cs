#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Forms
{
    #region AirForm

    public class AirForm : AirLibrary
    {
        public AirForm()
        {
            TransparencyKey = Color.Fuchsia;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9);
            SetColor("Title color", Color.Black);
            SetColor("X-color", 90, 90, 90);
            SetColor("X-ellipse", 114, 114, 114);
            MinimumSize = new Size(112, 35);
        }

        Color TitleColor;
        Color Xcolor;
        Color Xellipse;
        protected override void ColorHook()
        {
            TitleColor = GetColor("Title color");
            Xcolor = GetColor("X-color");
            Xellipse = GetColor("X-ellipse");
        }

        int X;

        int Y;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            X = e.Location.X;
            Y = e.Location.Y;
            base.OnMouseMove(e);
            Invalidate();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnClick(e);
            Rectangle r = new Rectangle(Width - 22, 5, 15, 15);
            if (r.Contains(new Point(e.X, e.Y)) || r.Contains(new Point(X, Y)) && e.Button == MouseButtons.Left)
                FindForm().Close();
        }

        protected override void PaintHook()
        {
            G.Clear(BackColor);
            DrawCorners(Color.Fuchsia);
            DrawCorners(Color.Fuchsia, 1, 0, Width - 2, Height);
            DrawCorners(Color.Fuchsia, 0, 1, Width, Height - 2);
            DrawCorners(Color.Fuchsia, 2, 0, Width - 4, Height);
            DrawCorners(Color.Fuchsia, 0, 2, Width, Height - 4);

            G.SmoothingMode = SmoothingMode.HighQuality;
            if (new Rectangle(Width - 22, 5, 15, 15).Contains(new Point(X, Y)))
            {
                G.FillEllipse(new SolidBrush(Xellipse), new Rectangle(Width - 24, 6, 16, 16));
                G.DrawString("r", new Font("Webdings", 8), new SolidBrush(BackColor), new Point(Width - 23, 5));
            }
            else
                G.DrawString("r", new Font("Webdings", 8), new SolidBrush(Xcolor), new Point(Width - 23, 5));

            DrawText(new SolidBrush(TitleColor), new Point(8, 7));
        }
    }

    #endregion
}