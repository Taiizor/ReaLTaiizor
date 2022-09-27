#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AloneNotice

    public class AloneNotice : System.Windows.Forms.TextBox
    {
        private Graphics G;

        private readonly string B64;

        public Color BorderColor { get; set; } = Color.White;

        public AloneNotice()
        {
            B64 = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABL0lEQVQ4T5VT0VGDQBB9e2cBdGBSgTIDEr9MCw7pI0kFtgB9yFiC+KWMmREqMOnAAuDWOfAiudzhyA/svtvH7Xu7BOv5eH2atVKtwbwk0LWGGVyDqLzoRB7e3u/HJTQOdm+PGYjWNuk4ZkIW36RbkzsS7KqiBnB1Usw49DHh8oQEXMfJKhwgAM4/Mw7RIp0NeLG3ScCcR4vVhnTPnVCf9rUZeImTdKnz71VREnBnn5FKzMnX95jA2V6vLufkBQFESTq0WBXsEla7owmcoC6QJMKW2oCUePY5M0lAjK0iBAQ8TBGc2/d7+uvnM/AQNF4Rp4bpiGkRfTb2Gigx12+XzQb3D9JfBGaQzHWm7HS000RJ2i/av5fJjPDZMplErwl1GxDpMTbL1YC5lCwze52/AQFekh7wKBpGAAAAAElFTkSuQmCC";
            DoubleBuffered = true;
            base.Enabled = false;
            base.ReadOnly = true;
            base.BorderStyle = BorderStyle.None;
            Multiline = true;
            BackColor = AloneLibrary.ColorFromHex("#FFFDE8");
            ForeColor = AloneLibrary.ColorFromHex("#B9B595");
            Cursor = Cursors.Default;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            G.Clear(BorderColor);
            using (SolidBrush solidBrush = new(BackColor))
            {
                using Pen pen = new(BorderColor);
                using SolidBrush solidBrush2 = new(ForeColor);
                using Font font = new("Segoe UI", 9f);
                G.FillPath(solidBrush, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                G.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                G.DrawString(Text, font, solidBrush2, new Point(30, 6));
            }
            using Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64)));
            G.DrawImage(image, new Rectangle(8, checked((int)Math.Round(unchecked((Height / 2.0) - 8.0))), 16, 16));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }

    #endregion
}