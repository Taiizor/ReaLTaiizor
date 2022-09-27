#region Imports

using ReaLTaiizor.Util;
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
    #region FoxCheckBox

    [DefaultEvent("CheckedChanged")]
    public class FoxCheckBox : Util.FoxBase.CheckControlBox
    {
        private Graphics G;

        private readonly string B64C = "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAQAAAAnOwc2AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QA/4ePzL8AAAAHdElNRQffCxwIKTQpQueKAAAAn0lEQVQI1yXKMU4CQRxG8TczW5nYWRCX+4it1/AUVvacArkGBQkBLmKUkBB3ne/b+VNs9ZKXXwKAOicT8cR3mVejUbo0scpf/NKSypRE7Sr1VReFdgx55D+rE3Wlq0J798SD3qeFqC+6KHR2b9BGoa3e9KPQwUvjgtYKNY0KnfxsVCr84Q+FQsdZGcOQB/ypgxezqhgi3VIr02PDyRgDd6AdcPpYOg4ZAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE1LTExLTI4VDA4OjQxOjUyLTA1OjAwH7rbKgAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNS0xMS0yOFQwODo0MTo1Mi0wNTowMG7nY5YAAAAASUVORK5CYII=";
        private readonly string B64U = "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAQAAAAnOwc2AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QA/4ePzL8AAAAHdElNRQffCxwIKir4YIkqAAAAgUlEQVQI122OMQrCQAAENxoMxz3Aj8Y3WAv6jtzVAYPYKah/8AtC5AZdm1TqFss0y6xGseXoxb26yA172iKx5o1JDg4kzMhK9JgnJpMn6uVIwoCn7hx1lmsSplAwyfVJs2Wlr8wlR7qfOYc/Ina8MNnBgTxdeogNg5ubrnLDQFv0AXVYjzifEiowAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE1LTExLTI4VDA4OjQyOjQyLTA1OjAwOCdgtwAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNS0xMS0yOFQwODo0Mjo0Mi0wNTowMEl62AsAAAAASUVORK5CYII=";

        public Color CheckedBorderColorA { get; set; } = FoxLibrary.ColorFromHex("#C8C8C8");
        public Color CheckedBorderColorB { get; set; } = FoxLibrary.ColorFromHex("#2A8AC1");
        public Color UncheckedBorderColorA { get; set; } = FoxLibrary.ColorFromHex("#C8C8C8");
        public Color UncheckedBorderColorB { get; set; } = FoxLibrary.ColorFromHex("#DC8400");
        public Color CheckedColor { get; set; } = FoxLibrary.ColorFromHex("#2C9CDA");
        public Color UncheckedColor { get; set; } = FoxLibrary.ColorFromHex("#FF9500");
        public Color DisabledCheckedBorderColorA { get; set; } = FoxLibrary.ColorFromHex("#E6E6E6");
        public Color DisabledCheckedBorderColorB { get; set; } = FoxLibrary.ColorFromHex("#7CA6BF");
        public Color DisabledCheckedColor { get; set; } = FoxLibrary.ColorFromHex("#7DB7D8");
        public Color DisabledUncheckedBorderColorA { get; set; } = FoxLibrary.ColorFromHex("#E6E6E6");
        public Color DisabledUncheckedBorderColorB { get; set; } = FoxLibrary.ColorFromHex("#E2BD85");
        public Color DisabledUncheckedColor { get; set; } = FoxLibrary.ColorFromHex("#FFCB7C");

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                if (Checked)
                {
                    using Pen Border = new(CheckedBorderColorA);
                    using SolidBrush Background = new(CheckedColor);
                    using Pen BackBorder = new(CheckedBorderColorB);
                    using Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64C)));
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

                    G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));
                    G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));

                    G.DrawImage(I, new Point(9, 9));
                }
                else
                {
                    using Pen Border = new(UncheckedBorderColorA);
                    using SolidBrush Background = new(UncheckedColor);
                    using Pen BackBorder = new(UncheckedBorderColorB);
                    using Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64U)));
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

                    G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));
                    G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));

                    G.DrawImage(I, new Point(Width - 19, 9));
                }
            }
            else
            {
                if (Checked)
                {
                    using Pen Border = new(DisabledCheckedBorderColorA);
                    using SolidBrush Background = new(DisabledCheckedColor);
                    using Pen BackBorder = new(DisabledCheckedBorderColorB);
                    using Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64C)));
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

                    G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));
                    G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));

                    G.DrawImage(I, new Point(9, 9));
                }
                else
                {
                    using Pen Border = new(DisabledUncheckedBorderColorA);
                    using SolidBrush Background = new(DisabledUncheckedColor);
                    using Pen BackBorder = new(DisabledUncheckedBorderColorB);
                    using Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64U)));
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

                    G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));
                    G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));

                    G.DrawImage(I, new Point(Width - 19, 9));
                }
            }

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(55, 28);
        }
    }

    #endregion
}