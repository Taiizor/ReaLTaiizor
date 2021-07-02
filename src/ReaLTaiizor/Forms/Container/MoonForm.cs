#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region MoonForm

    public class MoonForm : MoonLibrary
    {
        private Color G1;
        private Color G2;
        private Color BG;

        private Color _TitleColor = Color.Black;
        public Color TitleColor
        {
            get => _TitleColor;
            set
            {
                _TitleColor = value;
                Invalidate();
            }
        }

        private Color _BorderColor = Color.LightGray;
        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        private Color _FirstEdge = Color.LightGray;
        public Color FirstEdge
        {
            get => _FirstEdge;
            set
            {
                _FirstEdge = value;
                Invalidate();
            }
        }

        private Color _SecondEdge = Color.White;
        public Color SecondEdge
        {
            get => _SecondEdge;
            set
            {
                _SecondEdge = value;
                Invalidate();
            }
        }

        public MoonForm()
        {
            TransparencyKey = Color.Fuchsia;
            SetColor("G1", Color.White);
            SetColor("G2", Color.LightGray);
            SetColor("BG", Color.FromArgb(240, 240, 240));
            StartPosition = FormStartPosition.CenterScreen;
            Padding = new Padding(0, 0, 0, 0);
            Font = new("Segoe UI", 9);
            ForeColor = Color.Gray;
            TitleColor = _TitleColor;
            MinimumSize = new(100, 50);
        }

        protected override void ColorHook()
        {
            G1 = GetColor("G1");
            G2 = GetColor("G2");
            BG = GetColor("BG");
        }

        protected override void PaintHook()
        {
            G.Clear(BG);

            LinearGradientBrush LGB = new(new Rectangle(new Point(1, 1), new Size(Width - 2, 23)), G1, G2, 90f);
            G.FillRectangle(LGB, new Rectangle(new Point(1, 1), new Size(Width - 2, 23)));

            G.DrawLine(new(new SolidBrush(FirstEdge)), 1, 25, Width - 2, 25);
            G.DrawLine(new(new SolidBrush(SecondEdge)), 1, 26, Width - 2, 26);

            DrawCorners(TransparencyKey);
            DrawBorders(new(new SolidBrush(BorderColor)), 1);

            Rectangle IconRec = new(3, 3, 20, 20);
            G.DrawIcon(ParentForm.Icon, IconRec);

            G.DrawString(ParentForm.Text, Font, new SolidBrush(TitleColor), new Point(25, 5));
        }
    }

    #endregion
}