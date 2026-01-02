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
            currentColor = CheckboxColor;
            Cursor = Cursors.Hand;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checked or unchecked")]
        public bool Checked
        {
            get;
            set
            {
                field = value;
                OnCheckedStateChanged();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Thickness of the tick when checked")]
        public int TickThickness
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 2;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox color")]
        public Color CheckboxColor
        {
            get;
            set
            {
                field = value;
                currentColor = value;
                Invalidate();
            }
        } = Color.FromArgb(0, 162, 250);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox color")]
        public Color CheckboxCheckColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox ios border color")]
        public Color BorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(200, 200, 200);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox ios badge color")]
        public Color BadgeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(0, 120, 255);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Checkbox color when hovering")]
        public Color CheckboxHoverColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(249, 55, 98);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The Checkbox style")]
        public Style CheckboxStyle
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Style.Material;

        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.AntiAlias;

        [Category("Parrot")]
        [Browsable(true)]
        public PixelOffsetMode PixelOffsetType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = PixelOffsetMode.HighQuality;

        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

        public event EventHandler CheckedStateChanged;

        protected virtual void OnCheckedStateChanged()
        {
            CheckedStateChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            currentColor = CheckboxHoverColor;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            currentColor = CheckboxColor;
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
            if (CheckboxStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(currentColor), 1, 1, base.Height - 2, base.Height - 2);
                if (Checked)
                {
                    e.Graphics.DrawLine(new Pen(CheckboxCheckColor, TickThickness), 2, base.Height / 3 * 2, base.Height / 2, base.Height - 2);
                    e.Graphics.DrawLine(new Pen(CheckboxCheckColor, TickThickness), base.Height / 2, base.Height - 2, base.Height - 2, 1);
                }
            }
            if (CheckboxStyle == Style.iOS)
            {
                if (!Checked)
                {
                    e.Graphics.DrawEllipse(new Pen(BorderColor), 2, 2, base.Height - 4, base.Height - 4);
                }
                if (Checked)
                {
                    e.Graphics.FillEllipse(new SolidBrush(BadgeColor), 1, 1, base.Height - 2, base.Height - 2);
                    e.Graphics.DrawLine(new Pen(CheckboxCheckColor, TickThickness), base.Height / 5, base.Height / 2, base.Height / 2, base.Height / 4 * 3);
                    e.Graphics.DrawLine(new Pen(CheckboxCheckColor, TickThickness), base.Height / 2, base.Height / 4 * 3, base.Height / 5 * 4, base.Height / 4);
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

        private Color currentColor;

        public enum Style
        {
            iOS,
            Material
        }
    }

    #endregion
}