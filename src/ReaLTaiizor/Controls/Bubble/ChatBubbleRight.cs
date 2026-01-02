#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ChatBubbleRight

    public class ChatBubbleRight : Control
    {
        #region Variables

        private GraphicsPath Shape;
        private Color _TextColor = Color.FromArgb(52, 52, 52);

        #endregion

        #region Properties

        public override Color ForeColor
        {
            get => _TextColor;
            set
            {
                _TextColor = value;
                Invalidate();
            }
        }

        public Color BubbleColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(192, 206, 215);

        public bool DrawBubbleArrow
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        public bool SizeAuto
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        public bool SizeAutoW
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        public bool SizeAutoH
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        public bool SizeWidthLeft
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

        #endregion

        public ChatBubbleRight()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new(130, 40);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(52, 52, 52);
            Font = new("Segoe UI", 10);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Shape = new();

            GraphicsPath _with1 = Shape;

            _with1.AddArc(0, 0, 10, 10, 180, 90);
            _with1.AddArc(Width - 18, 0, 10, 10, -90, 90);
            _with1.AddArc(Width - 18, Height - 11, 10, 10, 0, 90);
            _with1.AddArc(0, Height - 11, 10, 10, 90, 90);

            _with1.CloseAllFigures();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Size TS = TextRenderer.MeasureText(Text, Font);

            if (SizeAuto)
            {
                int WW = Width;
                if (SizeAutoW && SizeAutoH)
                {
                    Width = TS.Width + 15;
                    Height = TS.Height + 15;

                    if (SizeWidthLeft)
                    {
                        Location = new(Location.X - (Width - WW), Location.Y);
                    }
                }
                else if (SizeAutoW)
                {
                    Width = TS.Width + 15;

                    if (SizeWidthLeft)
                    {
                        Location = new(Location.X - (Width - WW), Location.Y);
                    }
                }
                else
                {
                    int TH = 0;

                    using (Graphics CG = CreateGraphics())
                    {
                        SizeF SF = CG.MeasureString(Text, Font, Width - 17);
                        TH = (int)SF.Height;
                    }

                    Height = TH + 15;
                    //Height = TS.Height + 15;
                }
            }

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Graphics _G = G;

            _G.SmoothingMode = SmoothingMode.HighQuality;
            _G.PixelOffsetMode = PixelOffsetMode.HighQuality;

            _G.Clear(BackColor);

            // Fill the body of the bubble with the specified color
            _G.FillPath(new SolidBrush(BubbleColor), Shape);

            // Draw the string specified in 'Text' property
            _G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(6, 7, Width - 15, Height));

            // Draw a polygon on the right side of the bubble
            if (DrawBubbleArrow == true)
            {
                Point[] p =
                {
                    new(Width - 8, Height - 19),
                    new(Width, Height - 25),
                    new(Width - 8, Height - 30)
                };

                _G.FillPolygon(new SolidBrush(BubbleColor), p);
                _G.DrawPolygon(new(new SolidBrush(BubbleColor)), p);
            }

            G.Dispose();

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);

            B.Dispose();
        }
    }

    #endregion
}