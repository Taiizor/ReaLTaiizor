﻿#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonComboBox

    public class DungeonComboBox : ComboBox
    {
        #region Variables

        private int _StartIndex = 0;
        private Color _HoverSelectionColor; // VBConversions Note: Initial value cannot be assigned here since it is non-static. Assignment has been moved to the class constructors.

        private Color _ColorA = Color.FromArgb(246, 132, 85);
        private Color _ColorB = Color.FromArgb(231, 108, 57);
        private Color _ColorC = Color.FromArgb(242, 241, 240);
        private Color _ColorD = Color.FromArgb(253, 252, 252);
        private Color _ColorE = Color.FromArgb(239, 237, 236);
        private Color _ColorF = Color.FromArgb(180, 180, 180);
        private Color _ColorG = Color.FromArgb(119, 119, 118);
        private Color _ColorH = Color.FromArgb(224, 222, 220);
        private Color _ColorI = Color.FromArgb(250, 249, 249);

        #endregion

        #region Custom Properties

        public int StartIndex
        {
            get => _StartIndex;
            set
            {
                _StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch
                {
                }
                Invalidate();
            }
        }

        public Color HoverSelectionColor
        {
            get => _HoverSelectionColor;
            set
            {
                _HoverSelectionColor = value;
                Invalidate();
            }
        }

        public Color ColorA
        {
            get => _ColorA;
            set => _ColorA = value;
        }

        public Color ColorB
        {
            get => _ColorB;
            set => _ColorB = value;
        }

        public Color ColorC
        {
            get => _ColorC;
            set => _ColorC = value;
        }

        public Color ColorD
        {
            get => _ColorD;
            set => _ColorD = value;
        }

        public Color ColorE
        {
            get => _ColorE;
            set => _ColorE = value;
        }

        public Color ColorF
        {
            get => _ColorF;
            set => _ColorF = value;
        }

        public Color ColorG
        {
            get => _ColorG;
            set => _ColorG = value;
        }

        public Color ColorH
        {
            get => _ColorH;
            set => _ColorH = value;
        }

        public Color ColorI
        {
            get => _ColorI;
            set => _ColorI = value;
        }

        #endregion

        #region EventArgs

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            LinearGradientBrush LGB = new(e.Bounds, _ColorA, _ColorB, 90.0F);

            if (Convert.ToInt32((e.State & DrawItemState.Selected)) == (int)DrawItemState.Selected)
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(LGB, e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.WhiteSmoke, e.Bounds);
                }
            }
            else
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(new SolidBrush(_ColorC), e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.DimGray, e.Bounds);
                }
            }
            LGB.Dispose();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            SuspendLayout();
            Update();
            ResumeLayout();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!Focused)
            {
                SelectionLength = 0;
            }
        }

        #endregion

        public DungeonComboBox()
        {
            SetStyle((ControlStyles)(139286), true);
            SetStyle(ControlStyles.Selectable, false);

            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;

            BackColor = Color.FromArgb(246, 246, 246);
            ForeColor = Color.FromArgb(76, 76, 97);
            Size = new(135, 26);
            ItemHeight = 20;
            DropDownHeight = 100;
            Font = new("Segoe UI", 10, FontStyle.Regular);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            LinearGradientBrush LGB = default;
            GraphicsPath GP = default;

            e.Graphics.Clear(Parent.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create a curvy border
            GP = RoundRectangle.RoundRect(0, 0, Width - 1, Height - 1, 5);
            // Fills the body of the rectangle with a gradient
            LGB = new(ClientRectangle, _ColorD, _ColorE, 90.0F);

            e.Graphics.SetClip(GP);
            e.Graphics.FillRectangle(LGB, ClientRectangle);
            e.Graphics.ResetClip();

            // Draw rectangle border
            e.Graphics.DrawPath(new(_ColorF), GP);
            // Draw string
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(3, 0, Width - 20, Height), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            });
            e.Graphics.DrawString("6", new Font("Marlett", 13, FontStyle.Regular), new SolidBrush(_ColorG), new Rectangle(3, 0, Width - 4, Height), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Far
            });
            e.Graphics.DrawLine(new(_ColorH), Width - 24, 4, Width - 24, Height - 5);
            e.Graphics.DrawLine(new(_ColorI), Width - 25, 4, Width - 25, Height - 5);

            GP.Dispose();
            LGB.Dispose();
        }
    }

    #endregion
}