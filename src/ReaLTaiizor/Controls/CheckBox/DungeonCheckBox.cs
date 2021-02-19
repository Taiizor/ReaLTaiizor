﻿#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonCheckBox

    [DefaultEvent("CheckedChanged")]
    public class DungeonCheckBox : Control
    {

        #region Variables

        private GraphicsPath Shape;
        private LinearGradientBrush GB;
        private Rectangle R1;
        private Rectangle R2;
        private bool _Checked;
        private Color _CheckedColor = Color.FromArgb(255, 255, 255);
        private Color _CheckedBackColorA = Color.FromArgb(213, 85, 32);
        private Color _CheckedBackColorB = Color.FromArgb(224, 123, 82);
        private Color _CheckedBorderColor = Color.FromArgb(182, 88, 55);
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        #endregion
        #region Properties

        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                CheckedChanged?.Invoke(this);
                Invalidate();
            }
        }

        public Color CheckedColor
        {
            get => _CheckedColor;
            set => _CheckedColor = value;
        }

        public Color CheckedBackColorA
        {
            get => _CheckedBackColorA;
            set => _CheckedBackColorA = value;
        }

        public Color CheckedBackColorB
        {
            get => _CheckedBackColorB;
            set => _CheckedBackColorB = value;
        }

        public Color CheckedBorderColor
        {
            get => _CheckedBorderColor;
            set => _CheckedBorderColor = value;
        }

        #endregion

        public DungeonCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            // Reduce control flicker
            Font = new("Segoe UI", 12);
            Size = new(160, 26);
            ForeColor = Color.FromArgb(76, 76, 95);
            Cursor = Cursors.Hand;
        }

        protected override void OnClick(EventArgs e)
        {
            _Checked = !_Checked;
            CheckedChanged?.Invoke(this);
            Focus();
            Invalidate();
            base.OnClick(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (Width > 0 && Height > 0)
            {
                Shape = new();

                R1 = new(17, 0, Width, Height + 1);
                R2 = new(0, 0, Width, Height);
                GB = new(new Rectangle(0, 0, 25, 25), _CheckedBackColorA, _CheckedBackColorB, 90);

                GraphicsPath MyDrawer = Shape;
                MyDrawer.AddArc(0, 0, 7, 7, 180, 90);
                MyDrawer.AddArc(7, 0, 7, 7, -90, 90);
                MyDrawer.AddArc(7, 7, 7, 7, 0, 90);
                MyDrawer.AddArc(0, 7, 7, 7, 90, 90);
                MyDrawer.CloseAllFigures();
                Height = 15;
            }

            Invalidate();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics MyDrawer = e.Graphics;
            MyDrawer.Clear(BackColor);
            MyDrawer.SmoothingMode = SmoothingMode.AntiAlias;

            MyDrawer.FillPath(GB, Shape);
            // Fill the body of the CheckBox
            MyDrawer.DrawPath(new(_CheckedBorderColor), Shape);
            // Draw the border

            MyDrawer.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(17, 0, Width, Height - 1), new StringFormat { LineAlignment = StringAlignment.Center });

            if (Checked)
            {
                MyDrawer.DrawString("ü", new Font("Wingdings", 12), new SolidBrush(_CheckedColor), new Rectangle(-2, 1, Width, Height + 2), new StringFormat { LineAlignment = StringAlignment.Center });
            }

            e.Dispose();
        }
    }

    #endregion
}