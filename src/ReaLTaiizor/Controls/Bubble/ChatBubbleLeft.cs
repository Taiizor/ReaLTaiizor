#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ChatBubbleLeft

    public class ChatBubbleLeft : Control
    {
        #region Variables

        private GraphicsPath Shape;
        private Color _TextColor = Color.FromArgb(52, 52, 52);
        private Color _BubbleColor = Color.FromArgb(217, 217, 217);
        private bool _DrawBubbleArrow = true;
        private bool _SizeAuto = true;
        private bool _SizeAutoW = true;
        private bool _SizeAutoH = true;

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
            get => _BubbleColor;
            set
            {
                _BubbleColor = value;
                Invalidate();
            }
        }

        public bool DrawBubbleArrow
        {
            get => _DrawBubbleArrow;
            set
            {
                _DrawBubbleArrow = value;
                Invalidate();
            }
        }

        public bool SizeAuto
        {
            get => _SizeAuto;
            set
            {
                _SizeAuto = value;
                Invalidate();
            }
        }

        public bool SizeAutoW
        {
            get => _SizeAutoW;
            set
            {
                _SizeAutoW = value;
                Invalidate();
            }
        }

        public bool SizeAutoH
        {
            get => _SizeAutoH;
            set
            {
                _SizeAutoH = value;
                Invalidate();
            }
        }

        #endregion

        public ChatBubbleLeft()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new(120, 40);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(52, 52, 52);
            Font = new("Segoe UI", 10);
        }

        protected override void OnResize(EventArgs e)
        {
            Shape = new();

            GraphicsPath _Shape = Shape;
            _Shape.AddArc(9, 0, 10, 10, 180, 90);
            _Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            _Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            _Shape.AddArc(9, Height - 11, 10, 10, 90, 90);
            _Shape.CloseAllFigures();

            Invalidate();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_SizeAuto)
            {
                if (_SizeAutoW && _SizeAutoH)
                {
                    Width = TextRenderer.MeasureText(Text, Font).Width + 15;
                    Height = TextRenderer.MeasureText(Text, Font).Height + 15;
                }
                else if (_SizeAutoW)
                {
                    Width = TextRenderer.MeasureText(Text, Font).Width + 15;
                }
                else
                {
                    Height = TextRenderer.MeasureText(Text, Font).Height + 15;
                }
            }

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Graphics _G = G;
            _G.SmoothingMode = SmoothingMode.HighQuality;
            _G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _G.Clear(BackColor);

            // Fill the body of the bubble with the specified color
            _G.FillPath(new SolidBrush(_BubbleColor), Shape);
            // Draw the string specified in 'Text' property
            _G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(15, 7, Width - 17, Height - 5));

            // Draw a polygon on the right side of the bubble
            if (_DrawBubbleArrow == true)
            {
                Point[] p =
                {
                    new Point(9, Height - 19),
                    new Point(0, Height - 25),
                    new Point(9, Height - 30)
                };
                _G.FillPolygon(new SolidBrush(_BubbleColor), p);
                _G.DrawPolygon(new(new SolidBrush(_BubbleColor)), p);
            }
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
    }

    #endregion
}