﻿#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverGroupBox

    public class ForeverGroupBox : ContainerControl
    {
        private int W;
        private int H;
        private bool _ShowText = true;
        private bool _ShowArrow = true;

        [Category("Colors")]
        public Color BaseColor
        {
            get => _BaseColor;
            set => _BaseColor = value;
        }

        [Category("Colors")]
        public Color TextColor
        {
            get => _TextColor;
            set => _TextColor = value;
        }

        [Category("Colors")]
        public Color ArrowColorH
        {
            get => _ArrowColorH;
            set => _ArrowColorH = value;
        }

        [Category("Colors")]
        public Color ArrowColorF
        {
            get => _ArrowColorF;
            set => _ArrowColorF = value;
        }

        [Category("Options")]
        public bool ShowText
        {
            get => _ShowText;
            set => _ShowText = value;
        }

        [Category("Options")]
        public bool ShowArrow
        {
            get => _ShowArrow;
            set => _ShowArrow = value;
        }

        private Color _BaseColor = Color.FromArgb(60, 70, 73);
        private Color _ArrowColorH = Color.FromArgb(60, 70, 73);
        private Color _ArrowColorF = Color.FromArgb(60, 70, 73);
        private Color _TextColor = ForeverLibrary.ForeverColor;

        public ForeverGroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Size = new(240, 180);
            Font = new("Segoe UI", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width - 1;
            H = Height - 1;

            GraphicsPath GP = new();
            GraphicsPath GP2 = new();
            GraphicsPath GP3 = new();
            Rectangle Base = new(8, 8, W - 16, H - 16);

            Graphics _with7 = G;
            _with7.SmoothingMode = SmoothingMode.HighQuality;
            _with7.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with7.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with7.Clear(BackColor);

            //-- Base
            GP = ForeverLibrary.RoundRec(Base, 8);
            _with7.FillPath(new SolidBrush(_BaseColor), GP);

            //-- Arrows
            if (_ShowArrow)
            {
                GP2 = ForeverLibrary.DrawArrow(28, 2, false);
                _with7.FillPath(new SolidBrush(_ArrowColorH), GP2);
                GP3 = ForeverLibrary.DrawArrow(28, 8, true);
                _with7.FillPath(new SolidBrush(_ArrowColorF), GP3);
            }

            //-- if ShowText
            if (ShowText)
            {
                _with7.DrawString(Text, Font, new SolidBrush(_TextColor), new Rectangle(16, 16, W, H), ForeverLibrary.NearSF);
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

            _TextColor = Colors.Forever;
        }
    }

    #endregion
}