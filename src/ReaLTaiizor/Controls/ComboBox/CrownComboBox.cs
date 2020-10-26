﻿#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownComboBox

    public class CrownComboBox : ComboBox
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBoxStyle DropDownStyle { get; set; }

        private Bitmap _buffer;

        public CrownComboBox() : base()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );

            DrawMode = DrawMode.OwnerDrawVariable;

            base.FlatStyle = FlatStyle.Flat;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _buffer = null;
            }

            base.Dispose(disposing);
        }

        protected override void OnTabStopChanged(EventArgs e)
        {
            base.OnTabStopChanged(e);
            Invalidate();
        }

        protected override void OnTabIndexChanged(EventArgs e)
        {
            base.OnTabIndexChanged(e);
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnTextUpdate(EventArgs e)
        {
            base.OnTextUpdate(e);
            Invalidate();
        }

        protected override void OnSelectedValueChanged(EventArgs e)
        {
            base.OnSelectedValueChanged(e);
            Invalidate();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            Invalidate();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            PaintCombobox();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _buffer = null;
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _buffer = null;
            Invalidate();
        }

        private void PaintCombobox()
        {
            if (_buffer == null)
            {
                _buffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            }

            using (Graphics g = Graphics.FromImage(_buffer))
            {
                Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

                Color textColor = Enabled ? ThemeProvider.Theme.Colors.LightText : ThemeProvider.Theme.Colors.DisabledText;

                Color borderColor = ThemeProvider.Theme.Colors.GreySelection;
                Color fillColor = ThemeProvider.Theme.Colors.LightBackground;

                if (Focused && TabStop)
                {
                    borderColor = ThemeProvider.Theme.Colors.BlueHighlight;
                }

                using (SolidBrush b = new SolidBrush(fillColor))
                {
                    g.FillRectangle(b, rect);
                }

                using (Pen p = new Pen(borderColor, 1))
                {
                    Rectangle modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
                    g.DrawRectangle(p, modRect);
                }

                Bitmap icon = Properties.Resources.scrollbar_arrow_hot;
                g.DrawImageUnscaled(icon, rect.Right - icon.Width - (ThemeProvider.Theme.Sizes.Padding / 2), (rect.Height / 2) - (icon.Height / 2));

                string text = SelectedItem != null ? SelectedItem.ToString() : Text;

                using (SolidBrush b = new SolidBrush(textColor))
                {
                    int padding = 2;

                    Rectangle modRect = new Rectangle(rect.Left + padding, rect.Top + padding, rect.Width - icon.Width - (ThemeProvider.Theme.Sizes.Padding / 2) - (padding * 2), rect.Height - (padding * 2));

                    StringFormat stringFormat = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near,
                        FormatFlags = StringFormatFlags.NoWrap,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    g.DrawString(text, Font, b, modRect, stringFormat);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_buffer == null)
            {
                PaintCombobox();
            }

            Graphics g = e.Graphics;
            g.DrawImageUnscaled(_buffer, Point.Empty);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;

            Color textColor = ThemeProvider.Theme.Colors.LightText;
            Color fillColor = ThemeProvider.Theme.Colors.LightBackground;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected || (e.State & DrawItemState.Focus) == DrawItemState.Focus || (e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect)
            {
                fillColor = ThemeProvider.Theme.Colors.BlueSelection;
            }

            using (SolidBrush b = new SolidBrush(fillColor))
            {
                g.FillRectangle(b, rect);
            }

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                string text = Items[e.Index].ToString();

                using (SolidBrush b = new SolidBrush(textColor))
                {
                    int padding = 2;

                    Rectangle modRect = new Rectangle(rect.Left + padding, rect.Top + padding, rect.Width - (padding * 2), rect.Height - (padding * 2));

                    StringFormat stringFormat = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near,
                        FormatFlags = StringFormatFlags.NoWrap,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    g.DrawString(text, Font, b, modRect, stringFormat);
                }
            }
        }
    }

    #endregion
}