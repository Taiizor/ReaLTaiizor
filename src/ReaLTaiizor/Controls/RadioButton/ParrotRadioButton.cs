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
    #region ParrotRadio

    public class ParrotRadioButton : Control
    {
        public ParrotRadioButton()
        {
            base.Size = new Size(100, 16);
            Text = base.Name;
            ForeColor = Color.White;
            currentColor = radioColor;
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
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Radio color")]
        public Color RadioColor
        {
            get => radioColor;
            set
            {
                radioColor = value;
                currentColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Radio color when hovering")]
        public Color RadioHoverColor
        {
            get => radioHoverColor;
            set
            {
                radioHoverColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The radio style")]
        public Style RadioStyle
        {
            get => radioStyle;
            set
            {
                radioStyle = value;
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

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            currentColor = radioHoverColor;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            currentColor = radioColor;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            foreach (object obj in base.Parent.Controls)
            {
                Control control = (Control)obj;
                if (control is System.Windows.Forms.RadioButton)
                {
                    ((RadioButton)control).Checked = false;
                }
                if (control is ParrotRadioButton button)
                {
                    button.Checked = false;
                }
            }
            isChecked = true;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingType;
            if (radioStyle == Style.Material)
            {
                e.Graphics.DrawEllipse(new Pen(currentColor, 2f), 2, 2, base.Height - 4, base.Height - 4);
                if (isChecked)
                {
                    e.Graphics.FillPie(new SolidBrush(currentColor), new Rectangle(5, 5, base.Height - 2 - 8, base.Height - 2 - 8), 0f, 360f);
                }
                e.Graphics.FillPie(new SolidBrush(currentColor), new Rectangle(1, 1, base.Height - 2, base.Height - 2), 0f, 360f);
                if (isChecked)
                {
                    e.Graphics.FillPie(new SolidBrush(Color.White), new Rectangle(4, 4, base.Height - 2 - 6, base.Height - 2 - 6), 0f, 360f);
                }
            }
            if (radioStyle == Style.iOS)
            {
                e.Graphics.DrawEllipse(new Pen(Color.FromArgb(30, 150, 240), 2f), 2, 2, base.Height - 4, base.Height - 4);
                if (isChecked)
                {
                    e.Graphics.FillPie(new SolidBrush(Color.FromArgb(30, 150, 240)), new Rectangle(5, 5, base.Height - 2 - 8, base.Height - 2 - 8), 0f, 360f);
                }
            }
            if (radioStyle == Style.Android)
            {
                e.Graphics.DrawEllipse(new Pen(Color.FromArgb(0, 150, 135), 2f), 2, 2, base.Height - 4, base.Height - 4);
                if (isChecked)
                {
                    e.Graphics.FillPie(new SolidBrush(Color.FromArgb(0, 150, 135)), new Rectangle(5, 5, base.Height - 2 - 8, base.Height - 2 - 8), 0f, 360f);
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

        private Color radioColor = Color.FromArgb(0, 162, 250);

        private Color radioHoverColor = Color.FromArgb(249, 55, 98);

        private Style radioStyle = Style.Material;

        private Color currentColor;

        public enum Style
        {
            iOS,
            Android,
            Material
        }
    }

    #endregion
}