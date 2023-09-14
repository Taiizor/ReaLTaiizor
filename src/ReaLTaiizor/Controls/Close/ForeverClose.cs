#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverClose

    public class ForeverClose : Control
    {
        private MouseStateForever State = MouseStateForever.None;
        private int x;

        #region Properties

        private bool _DefaultLocation = true;
        public bool DefaultLocation
        {
            get => _DefaultLocation;
            set
            {
                _DefaultLocation = value;
                Invalidate();
            }
        }

        #endregion

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            try
            {
                if (DefaultLocation)
                {
                    Location = new(Parent.Width - Width - 12, 16);
                }
            }
            catch (Exception)
            {
                //
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStateForever.Over;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseStateForever.Down;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStateForever.None;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseStateForever.Over;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            x = e.X;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Parent.FindForm().Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(18, 18);
        }

        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(45, 47, 49);

        [Category("Colors")]
        public Color OverColor { get; set; } = Color.FromArgb(30, 255, 255, 255);

        [Category("Colors")]
        public Color DownColor { get; set; } = Color.FromArgb(30, 0, 0, 0);

        [Category("Colors")]
        public Color TextColor { get; set; } = Color.FromArgb(243, 243, 243);

        public ForeverClose()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.White;
            Size = new(18, 18);
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Font = new("Marlett", 10);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Rectangle Base = new(0, 0, Width, Height);

            Graphics _with3 = G;
            _with3.SmoothingMode = SmoothingMode.HighQuality;
            _with3.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with3.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with3.Clear(BackColor);

            //-- Base
            _with3.FillRectangle(new SolidBrush(BaseColor), Base);

            //-- X
            _with3.DrawString("r", Font, new SolidBrush(TextColor), new Rectangle(0, 0, Width, Height), ForeverLibrary.CenterSF);

            //-- Hover/down
            switch (State)
            {
                case MouseStateForever.Over:
                    _with3.FillRectangle(new SolidBrush(OverColor), Base);
                    break;
                case MouseStateForever.Down:
                    _with3.FillRectangle(new SolidBrush(DownColor), Base);
                    break;
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
    }

    #endregion
}