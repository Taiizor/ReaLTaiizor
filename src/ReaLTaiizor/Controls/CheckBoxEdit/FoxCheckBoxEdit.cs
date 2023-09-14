#region Imports

using ReaLTaiizor.Util;
using ReaLTaiizor.Util.FoxBase;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxCheckBoxEdit

    [DefaultEvent("CheckedChanged")]
    public class FoxCheckBoxEdit : CheckControlEdit
    {
        private Graphics G;

        private readonly string B64E = "iVBORw0KGgoAAAANSUhEUgAAAAwAAAAMCAMAAABhq6zVAAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAABfVBMVEUAAAArntgsm9srm9sgn98gnuE1m9gsndoqnNkvmdYpnNkqm9gvl9YtnNksntosnNorm9o2mtoumtg/mb8rmtotm9snn9s/kMIqnNosm9ksoNssndksndklndwyl9gqn9krm9ornNksm9ksm9srm9srm9ssnNornNkgnd4xm9ksnNosnNorm9orm9ssnNosnNoalf8rnNkrm9ksnNosm9opnNkpnNkrnNkqm9ksnNosnNktmdcqm9ssnNosnNornNssnNksnNornNornNosm9srnNodnNYsmtosnNornNovmtkqnNornNosm9osnNoqmtorm9opntYim9Msm9osnNorm9osnNorm9oum9oAr9U1mNYqndosnNosnNosnNoxmNEont4qndoqnNosnNosnNosm9krm9kpnNksndornNkrnNksntsulc8pnt0snNosnNosnNosnNosnNosnNosnNosnNosnNosnNosnNosnNosnNosnNosnNornNn///9BLcc1AAAAbnRSTlMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWz8agABKOiVAAOt7jYBAgEABFr/fAABAhjRyg0VZhQDi/tMApWzBUD6kwJtgMTcHgIABKTkuvtkBAAAGtb7vwEBAkPz9zABAAJwdAEAANmSOx8AAAABYktHRH4/uEFzAAAAB3RJTUUH3wscBzYVoy2+dwAAAJxJREFUCNdjYAABWTl5RgVFJTBbmUmFWVUtTx3EZmHV0GTTytfWYdDV02c3MOQwMi4w4WTg4jI1M+e2sCy0suZhsLG147XncyhydOJ3ZnApdnUTcPco8RT0EmLwLi3zEfYt9/MXCQhkCAquCAkNqwwXjYiMYhCLjqmKrY4Tj09gYWFIlEhKrklJlUzjSmdgkMqQzqzNksnOyWVgAABtEB7gG6KeHgAAACV0RVh0ZGF0ZTpjcmVhdGUAMjAxNS0xMS0yOFQwNzo1NDoyMS0wNTowMCRACR8AAAAldEVYdGRhdGU6bW9kaWZ5ADIwMTUtMTEtMjhUMDc6NTQ6MjEtMDU6MDBVHbGjAAAAAElFTkSuQmCC";
        private readonly string B64D = "iVBORw0KGgoAAAANSUhEUgAAAAwAAAAMCAMAAABhq6zVAAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAABfVBMVEUAAAC2tbW3s7O2tLS/r6/Brq6vtra1s7O2tLS1srK2s7O2tLSxsLC1s7O3t7e2s7O2tLS1ubm0s7OroqK2tLS3s7O2tbWxqKi2s7O1s7O2t7e2tbW2s7O6sbGxtra4tLS2tLS2tLS1tLS3s7O2tLS2tLS2tLS2s7O9r6+ytbW1s7O2tLS2s7O2tLS2tLS1s7P1ioq1tLS1tLS2tLS2tLS2s7O2s7O2tLS2s7O2tLS1s7OzsbG2tLS2tLS1s7O2tbW2s7O2tLS2s7O2s7O2tLS2tLS6srK2tLS2tLS2s7O1tbW2s7O1s7O2tLS2tLS2s7O2tLSys7OssLC2tLS2tLS1tLS2tLS2s7O2tLTGoKCut7e0tbW2tLS2tLS2tLS1rq63tra3s7O3s7O2tLS2tLS1s7O1s7O2s7O2tbW1tLS1tLS3tLSwsbG3tra2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS2tLS1tLT///9wiMU7AAAAbnRSTlMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWz8agABKOiVAAOt7jYBAgEABFr/fAABAhjRyg0VZhQDi/tMApWzBUD6kwJtgMTcHgIABKTkuvtkBAAAGtb7vwEBAkPz9zABAAJwdAEAANmSOx8AAAABYktHRH4/uEFzAAAAB3RJTUUH3wscBzg0cceDpwAAAJxJREFUCNdjYAABWTl5RgVFJTBbmUmFWVUtTx3EZmHV0GTTytfWYdDV02c3MOQwMi4w4WTg4jI1M+e2sCy0suZhsLG147XncyhydOJ3ZnApdnUTcPco8RT0EmLwLi3zEfYt9/MXCQhkCAquCAkNqwwXjYiMYhCLjqmKrY4Tj09gYWFIlEhKrklJlUzjSmdgkMqQzqzNksnOyWVgAABtEB7gG6KeHgAAACV0RVh0ZGF0ZTpjcmVhdGUAMjAxNS0xMS0yOFQwNzo1Njo1Mi0wNTowMBuYyqYAAAAldEVYdGRhdGU6bW9kaWZ5ADIwMTUtMTEtMjhUMDc6NTY6NTItMDU6MDBqxXIaAAAAAElFTkSuQmCC";

        public Color BorderColor { get; set; } = FoxLibrary.ColorFromHex("#C8C8C8");
        public Color HoverBorderColor { get; set; } = FoxLibrary.ColorFromHex("#2C9CDA");
        public Color DisabledBorderColor { get; set; } = FoxLibrary.ColorFromHex("#E6E6E6");
        public Color DisabledTextColor { get; set; } = FoxLibrary.ColorFromHex("#A6B2BE");

        public FoxCheckBoxEdit() : base()
        {
            Font = new("Segoe UI", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            G.Clear(BackColor);

            if (Enabled)
            {
                switch (State)
                {
                    case FoxLibrary.MouseState.None:
                        using (Pen Border = new(BorderColor))
                        {
                            G.DrawPath(Border, FoxLibrary.RoundRect(new Rectangle(0, 0, 20, 20), 2));
                        }

                        break;
                    default:
                        using (Pen Border = new(HoverBorderColor))
                        {
                            G.DrawPath(Border, FoxLibrary.RoundRect(new Rectangle(0, 0, 20, 20), 2));
                        }

                        break;
                }

                using SolidBrush TextColor = new(ForeColor);
                G.DrawString(Text, Font, TextColor, new Point(27, 1));
            }
            else
            {
                using (Pen Border = new(DisabledBorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(new Rectangle(0, 0, 20, 20), 2));
                }

                using SolidBrush TextColor = new(DisabledTextColor);
                G.DrawString(Text, Font, TextColor, new Point(27, 1));
            }

            if (Checked)
            {

                if (Enabled)
                {
                    using Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64E)));
                    G.DrawImage(I, new Rectangle(5, 4, 12, 12));
                }
                else
                {
                    using Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64D)));
                    G.DrawImage(I, new Rectangle(5, 4, 12, 12));
                }
            }

            base.OnPaint(e);
        }

    }

    #endregion
}