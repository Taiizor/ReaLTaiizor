#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotCheckBox

    public class ParrotCheckBox : Control
    {
        public ParrotCheckBox()
        {
            base.Size = new Size(100, 20);
            Text = base.Name;
            ForeColor = Color.White;
            currentColor = checkboxColor;
            Cursor = Cursors.Hand;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checked or unchecked")]
        public bool Checked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnCheckedStateChanged();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Thickness of the tick when checked")]
        public int TickThickness
        {
            get => tickThickness;
            set
            {
                tickThickness = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox color")]
        public Color CheckboxColor
        {
            get => checkboxColor;
            set
            {
                checkboxColor = value;
                currentColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox color")]
        public Color CheckboxCheckColor
        {
            get => checkboxCheckColor;
            set
            {
                checkboxCheckColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox ios border color")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox ios badge color")]
        public Color BadgeColor
        {
            get => badgeColor;
            set
            {
                badgeColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox color when hovering")]
        public Color CheckboxHoverColor
        {
            get => checkboxHoverColor;
            set
            {
                checkboxHoverColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The Checkbox style")]
        public Style CheckboxStyle
        {
            get => checkboxStyle;
            set
            {
                checkboxStyle = value;
                Invalidate();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public PixelOffsetMode PixelOffsetType
        {
            get => _PixelOffsetType;
            set
            {
                _PixelOffsetType = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingType = TextRenderingHint.ClearTypeGridFit;
        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        public event EventHandler CheckedStateChanged;

        protected virtual void OnCheckedStateChanged()
        {
            CheckedStateChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            currentColor = checkboxHoverColor;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            currentColor = checkboxColor;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!Checked)
            {
                Checked = true;
                return;
            }
            Checked = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingType;
            if (checkboxStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(currentColor), 1, 1, base.Height - 2, base.Height - 2);
                if (isChecked)
                {
                    e.Graphics.DrawLine(new Pen(checkboxCheckColor, tickThickness), 2, base.Height / 3 * 2, base.Height / 2, base.Height - 2);
                    e.Graphics.DrawLine(new Pen(checkboxCheckColor, tickThickness), base.Height / 2, base.Height - 2, base.Height - 2, 1);
                }
            }
            if (checkboxStyle == Style.iOS)
            {
                if (!isChecked)
                {
                    e.Graphics.DrawEllipse(new Pen(BorderColor), 2, 2, base.Height - 4, base.Height - 4);
                }
                if (isChecked)
                {
                    e.Graphics.FillEllipse(new SolidBrush(BadgeColor), 1, 1, base.Height - 2, base.Height - 2);
                    e.Graphics.DrawLine(new Pen(checkboxCheckColor, tickThickness), base.Height / 5, base.Height / 2, base.Height / 2, base.Height / 4 * 3);
                    e.Graphics.DrawLine(new Pen(checkboxCheckColor, tickThickness), base.Height / 2, base.Height / 4 * 3, base.Height / 5 * 4, base.Height / 4);
                }
            }
            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };
            SolidBrush brush = new(ForeColor);
            RectangleF layoutRectangle = new(base.Height + 3, 0f, base.Width - base.Height - 2, Height);
            e.Graphics.PixelOffsetMode = PixelOffsetType;
            e.Graphics.TextRenderingHint = TextRenderingType;
            e.Graphics.DrawString(Text, Font, brush, layoutRectangle, stringFormat);
            base.OnPaint(e);
        }

        private bool isChecked;

        private int tickThickness = 2;

        private Color checkboxColor = Color.FromArgb(0, 162, 250);

        private Color checkboxCheckColor = Color.White;

        private Color checkboxHoverColor = Color.FromArgb(249, 55, 98);

        private Style checkboxStyle = Style.Material;

        private Color currentColor;

        private Color borderColor = Color.FromArgb(200, 200, 200);

        private Color badgeColor = Color.FromArgb(0, 120, 255);

        public enum Style
        {
            iOS,
            Material
        }
    }

    #endregion
}