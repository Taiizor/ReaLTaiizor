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
    #region ParrotSegment

    public class ParrotSegment : Control
    {
        public ParrotSegment()
        {
            base.Size = new Size(240, 30);
            Cursor = Cursors.Hand;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The items, split by ','.")]
        public string Items
        {
            get => items;
            set
            {
                items = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The selected index")]
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnIndexChanged();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The segment style")]
        public Style SegmentStyle
        {
            get => segmentStyle;
            set
            {
                segmentStyle = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The segment selected base color")]
        public Color SegmentColor
        {
            get => segmentColor;
            set
            {
                segmentColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The segment back color")]
        public Color SegmentBackColor
        {
            get => segmentBackColor;
            set
            {
                segmentBackColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The active segment text color")]
        public Color SegmentActiveTextColor
        {
            get => segmentActiveTextColor;
            set
            {
                segmentActiveTextColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The active segment android font color")]
        public Color SegmentActiveFontColor
        {
            get => segmentActiveFontColor;
            set
            {
                segmentActiveFontColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Theinactive segment android font color")]
        public Color SegmentInactiveFontColor
        {
            get => segmentInactiveFontColor;
            set
            {
                segmentInactiveFontColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The active segment ios back color")]
        public Color SegmentActiveBackColor
        {
            get => segmentActiveBackColor;
            set
            {
                segmentActiveBackColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Theinactive segment text color")]
        public Color SegmentInactiveTextColor
        {
            get => segmentInactiveTextColor;
            set
            {
                segmentInactiveTextColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Theinactive segment ios border color")]
        public Color SegmentInactiveBorderColor
        {
            get => segmentInactiveBorderColor;
            set
            {
                segmentInactiveBorderColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Theinactive segment android normal color")]
        public Color SegmentNormalBackColor
        {
            get => segmentNormalBackColor;
            set
            {
                segmentNormalBackColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The active segment android line color")]
        public Color SegmentActiveLineColor
        {
            get => segmentActiveLineColor;
            set
            {
                segmentActiveLineColor = value;
                Invalidate();
            }
        }

        private InterpolationMode _InterpolationType = InterpolationMode.HighQualityBilinear;
        [Category("Parrot")]
        [Browsable(true)]
        public InterpolationMode InterpolationType
        {
            get => _InterpolationType;
            set
            {
                _InterpolationType = value;
                Invalidate();
            }
        }

        private CompositingQuality _CompositingQualityType = CompositingQuality.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get => _CompositingQualityType;
            set
            {
                _CompositingQualityType = value;
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

        public event EventHandler IndexChanged;

        protected virtual void OnIndexChanged()
        {
            IndexChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.InterpolationMode = InterpolationType;
            e.Graphics.CompositingQuality = CompositingQualityType;
            e.Graphics.TextRenderingHint = TextRenderingType;
            int num = 0;
            foreach (string text in items.Split(new char[]
            {
                ','
            }))
            {
                num++;
            }
            int num2 = base.Width / num;
            int num3 = 0;
            int num4 = 0;
            if (segmentStyle == Style.iOS)
            {
                foreach (string s in items.Split(new char[]
                {
                    ','
                }))
                {
                    if (num3 <= num)
                    {
                        Rectangle r = new(num4, 0, num2, base.Height);
                        StringFormat stringFormat = new()
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        };
                        e.Graphics.DrawRectangle(new Pen(SegmentInactiveBorderColor, 1f), 0, 0, base.Width - 1, base.Height - 1);
                        if (selectedIndex == num3)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(SegmentActiveBackColor), num4, 0, num2, base.Height);
                            e.Graphics.DrawString(s, Font, new SolidBrush(SegmentActiveTextColor), r, stringFormat);
                        }
                        else
                        {
                            e.Graphics.DrawRectangle(new Pen(SegmentInactiveBorderColor, 1f), num4, 0, num4 + num2, base.Height - 1);
                            e.Graphics.DrawString(s, Font, new SolidBrush(SegmentInactiveBorderColor), r, stringFormat);
                        }
                    }
                    num4 += num2;
                    num3++;
                }
            }
            if (segmentStyle == Style.Android)
            {
                e.Graphics.FillRectangle(new SolidBrush(SegmentNormalBackColor), 0, 0, base.Width, base.Height);
                foreach (string s2 in items.Split(new char[]
                {
                    ','
                }))
                {
                    if (num3 <= num)
                    {
                        Rectangle r2 = new(num4, 0, num2, base.Height - 5);
                        StringFormat stringFormat2 = new()
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        };
                        if (selectedIndex == num3)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(SegmentActiveLineColor), num4, base.Height - 3, num2, 3);
                            e.Graphics.DrawString(s2, Font, new SolidBrush(SegmentActiveFontColor), r2, stringFormat2);
                        }
                        else
                        {
                            e.Graphics.DrawString(s2, Font, new SolidBrush(SegmentInactiveFontColor), r2, stringFormat2);
                        }
                    }
                    num4 += num2;
                    num3++;
                }
            }
            if (segmentStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(segmentBackColor), 0, 0, base.Width, base.Height);
                foreach (string s3 in items.Split(new char[]
                {
                    ','
                }))
                {
                    if (num3 <= num)
                    {
                        Rectangle r3 = new(num4, 0, num2, base.Height - 5);
                        StringFormat stringFormat3 = new()
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        };
                        if (selectedIndex == num3)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(segmentColor), num4, base.Height - 3, num2, 3);
                            e.Graphics.DrawString(s3, Font, new SolidBrush(segmentActiveTextColor), r3, stringFormat3);
                        }
                        else
                        {
                            e.Graphics.DrawString(s3, Font, new SolidBrush(segmentInactiveTextColor), r3, stringFormat3);
                        }
                    }
                    num4 += num2;
                    num3++;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int num = 0;
            int num2 = 0;
            foreach (string text in items.Split(new char[]
            {
                ','
            }))
            {
                num2++;
            }
            int num3 = base.Width / num2;
            if (e.X > 0)
            {
                num = 0;
            }
            if (e.X > num3)
            {
                num = 1;
            }
            if (e.X > num3 * 2)
            {
                num = 2;
            }
            if (e.X > num3 * 3)
            {
                num = 3;
            }
            if (e.X > num3 * 4)
            {
                num = 4;
            }
            if (e.X > num3 * 5)
            {
                num = 5;
            }
            if (e.X > num3 * 6)
            {
                num = 6;
            }
            if (e.X > num3 * 7)
            {
                num = 7;
            }
            if (e.X > num3 * 8)
            {
                num = 8;
            }
            if (e.X > num3 * 9)
            {
                num = 9;
            }
            if (e.X > num3 * 10)
            {
                num = 10;
            }
            if (num != selectedIndex)
            {
                SelectedIndex = num;
            }
        }

        private string items = "Contacts, Recents, Messages, Dialer";

        private int selectedIndex;

        private Style segmentStyle = Style.Material;

        private Color segmentColor = Color.White;

        private Color segmentBackColor = Color.FromArgb(0, 150, 135);

        private Color segmentActiveTextColor = Color.White;

        private Color segmentActiveFontColor = Color.FromArgb(65, 130, 205);

        private Color segmentActiveBackColor = Color.FromArgb(0, 120, 255);

        private Color segmentActiveLineColor = Color.FromArgb(65, 130, 205);

        private Color segmentInactiveTextColor = Color.FromArgb(150, 210, 210);

        private Color segmentInactiveFontColor = Color.FromArgb(153, 153, 153);

        private Color segmentInactiveBorderColor = Color.FromArgb(0, 120, 255);

        private Color segmentNormalBackColor = Color.White;

        public enum Style
        {
            iOS,
            Android,
            Material
        }
    }

    #endregion
}