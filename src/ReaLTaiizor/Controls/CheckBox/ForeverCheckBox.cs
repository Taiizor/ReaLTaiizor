#region Imports

using ReaLTaiizor.Colors;
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
    #region ForeverCheckBox

    [DefaultEvent("CheckedChanged")]
    public class ForeverCheckBox : Control
    {
        private int W;
        private int H;
        private MouseStateForever State = MouseStateForever.None;
        private bool _Checked;

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                Invalidate();
            }
        }

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        protected override void OnClick(EventArgs e)
        {
            _Checked = !_Checked;
            CheckedChanged?.Invoke(this);
            base.OnClick(e);
        }

        [Flags()]
        public enum _Options
        {
            Style1,
            Style2
        }

        [Category("Options")]
        public _Options Options { get; set; }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 22;
        }

        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(45, 47, 49);

        [Category("Colors")]
        public Color BorderColor { get; set; } = ForeverLibrary.ForeverColor;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseStateForever.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseStateForever.Over;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStateForever.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStateForever.None;
            Invalidate();
        }

        public ForeverCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.FromArgb(60, 70, 73);
            ForeColor = Color.FromArgb(243, 243, 243);
            Cursor = Cursors.Hand;
            Font = new("Segoe UI", 10);
            Size = new(130, 22);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width - 1;
            H = Height - 1;

            Rectangle Base = new(0, 2, Height - 5, Height - 5);

            Graphics _with11 = G;
            _with11.SmoothingMode = SmoothingMode.HighQuality;
            _with11.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with11.Clear(BackColor);
            switch (Options)
            {
                case _Options.Style1:
                    //-- Style 1
                    //-- Base
                    _with11.FillRectangle(new SolidBrush(BaseColor), Base);

                    switch (State)
                    {
                        case MouseStateForever.Over:
                            //-- Base
                            _with11.DrawRectangle(new(BorderColor), Base);
                            break;
                        case MouseStateForever.Down:
                            //-- Base
                            _with11.DrawRectangle(new(BorderColor), Base);
                            break;
                    }

                    //-- If Checked
                    if (Checked)
                    {
                        _with11.DrawString("ü", new Font("Wingdings", 18), new SolidBrush(BorderColor), new Rectangle(5, 7, H - 9, H - 9), ForeverLibrary.CenterSF);
                    }

                    //-- If Enabled
                    if (Enabled == false)
                    {
                        _with11.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), Base);
                        _with11.DrawString(Text, Font, new SolidBrush(Color.FromArgb(140, 142, 143)), new Rectangle(20, 2, W, H), ForeverLibrary.NearSF);
                    }

                    //-- Text
                    _with11.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(20, 2, W, H), ForeverLibrary.NearSF);
                    break;
                case _Options.Style2:
                    //-- Style 2
                    //-- Base
                    _with11.FillRectangle(new SolidBrush(BaseColor), Base);

                    switch (State)
                    {
                        case MouseStateForever.Over:
                            //-- Base
                            _with11.DrawRectangle(new(BorderColor), Base);
                            _with11.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
                            break;
                        case MouseStateForever.Down:
                            //-- Base
                            _with11.DrawRectangle(new(BorderColor), Base);
                            _with11.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
                            break;
                    }

                    //-- If Checked
                    if (Checked)
                    {
                        _with11.DrawString("ü", new Font("Wingdings", 18), new SolidBrush(BorderColor), new Rectangle(5, 7, H - 9, H - 9), ForeverLibrary.CenterSF);
                    }

                    //-- If Enabled
                    if (Enabled == false)
                    {
                        _with11.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), Base);
                        _with11.DrawString(Text, Font, new SolidBrush(Color.FromArgb(48, 119, 91)), new Rectangle(20, 2, W, H), ForeverLibrary.NearSF);
                    }

                    //-- Text
                    _with11.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(20, 2, W, H), ForeverLibrary.NearSF);
                    break;
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        private void UpdateColors()
        {
            ForeverColors Colors = ForeverLibrary.GetColors(this);

            BorderColor = Colors.Forever;
        }
    }

    #endregion
}