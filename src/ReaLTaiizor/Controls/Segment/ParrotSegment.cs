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
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "Contacts, Recents, Messages, Dialer";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The selected index")]
        public int SelectedIndex
        {
            get;
            set
            {
                field = value;
                OnIndexChanged();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The segment style")]
        public Style SegmentStyle
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
        [Description("The segment selected base color")]
        public Color SegmentColor
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
        [Description("The segment back color")]
        public Color SegmentBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(0, 150, 135);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The active segment text color")]
        public Color SegmentActiveTextColor
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
        [Description("The active segment android font color")]
        public Color SegmentActiveFontColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(65, 130, 205);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Theinactive segment android font color")]
        public Color SegmentInactiveFontColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(153, 153, 153);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The active segment ios back color")]
        public Color SegmentActiveBackColor
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
        [Description("Theinactive segment text color")]
        public Color SegmentInactiveTextColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(150, 210, 210);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Theinactive segment ios border color")]
        public Color SegmentInactiveBorderColor
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
        [Description("Theinactive segment android normal color")]
        public Color SegmentNormalBackColor
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
        [Description("The active segment android line color")]
        public Color SegmentActiveLineColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(65, 130, 205);

        [Category("Parrot")]
        [Browsable(true)]
        public InterpolationMode InterpolationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = InterpolationMode.HighQualityBilinear;

        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = CompositingQuality.HighQuality;

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
            foreach (string text in Items.Split(new char[]
            {
                ','
            }))
            {
                num++;
            }
            int num2 = base.Width / num;
            int num3 = 0;
            int num4 = 0;
            if (SegmentStyle == Style.iOS)
            {
                foreach (string s in Items.Split(new char[]
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
                        if (SelectedIndex == num3)
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
            if (SegmentStyle == Style.Android)
            {
                e.Graphics.FillRectangle(new SolidBrush(SegmentNormalBackColor), 0, 0, base.Width, base.Height);
                foreach (string s2 in Items.Split(new char[]
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
                        if (SelectedIndex == num3)
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
            if (SegmentStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(SegmentBackColor), 0, 0, base.Width, base.Height);
                foreach (string s3 in Items.Split(new char[]
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
                        if (SelectedIndex == num3)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(SegmentColor), num4, base.Height - 3, num2, 3);
                            e.Graphics.DrawString(s3, Font, new SolidBrush(SegmentActiveTextColor), r3, stringFormat3);
                        }
                        else
                        {
                            e.Graphics.DrawString(s3, Font, new SolidBrush(SegmentInactiveTextColor), r3, stringFormat3);
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
            foreach (string text in Items.Split(new char[]
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
            if (num != SelectedIndex)
            {
                SelectedIndex = num;
            }
        }

        public enum Style
        {
            iOS,
            Android,
            Material
        }
    }

    #endregion
}