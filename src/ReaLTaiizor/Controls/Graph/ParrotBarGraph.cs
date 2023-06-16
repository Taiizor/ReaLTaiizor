#region Imports

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotBarGraph

    public class ParrotBarGraph : Control
    {
        public ParrotBarGraph()
        {
            items.Clear();
            items.Add(50);
            items.Add(75);
            items.Add(10);
            items.Add(30);
            items.Add(90);
            items.Add(60);
            items.Add(80);
            items.Add(45);
            items.Add(70);
            items.Add(5);
            items.Add(25);
            items.Add(85);
            items.Add(55);
            items.Add(75);
            base.Size = new Size(294, 200);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("a collection of input numbers, will base the percentage of all numbers by the highest number")]
        public List<int> Items
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
        [Description("The filled color")]
        public Color FilledColor
        {
            get => filledColor;
            set
            {
                filledColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled color")]
        public Color UnfilledColor
        {
            get => unfilledColor;
            set
            {
                unfilledColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The splitter color")]
        public Color SplitterColor
        {
            get => splitterColor;
            set
            {
                splitterColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text color")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The item sorting style")]
        public SortStyle Sorting
        {
            get => sorting;
            set
            {
                sorting = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text aligning")]
        public Aligning TextAlignment
        {
            get => textAlignment;
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The orientation of the graph")]
        public Orientation GraphOrientation
        {
            get => graphOrientation;
            set
            {
                graphOrientation = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The style of the graph")]
        public Style GraphStyle
        {
            get => graphStyle;
            set
            {
                graphStyle = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the item grid")]
        public bool ShowGrid
        {
            get => showGrid;
            set
            {
                showGrid = value;
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

        public void ClearItems()
        {
            items = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (items != null)
            {
                if (graphStyle == Style.Flat)
                {
                    e.Graphics.FillRectangle(new SolidBrush(unfilledColor), 0, 0, base.Width, base.Height);

                    List<int> list = new();

                    if (sorting == SortStyle.Normal)
                    {
                        list = items;
                    }

                    if (sorting == SortStyle.Descending)
                    {
                        list = items;
                        list = (from p in list
                                orderby p descending
                                select p).ToList();
                    }

                    if (sorting == SortStyle.Ascending)
                    {
                        list = items;
                        list.Sort();
                    }

                    int num = 0;

                    if (graphOrientation == Orientation.Horizontal)
                    {
                        int num2 = base.Height / items.Count;
                        decimal d = base.Width / Items.Max();

                        foreach (int value in list)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(filledColor), new RectangleF(0f, num, (int)(value * d), num2));

                            StringFormat stringFormat = new()
                            {
                                LineAlignment = StringAlignment.Center
                            };

                            if (textAlignment == Aligning.Near)
                            {
                                stringFormat.Alignment = StringAlignment.Near;
                            }

                            if (textAlignment == Aligning.Center)
                            {
                                stringFormat.Alignment = StringAlignment.Center;
                            }

                            if (textAlignment == Aligning.Far)
                            {
                                stringFormat.Alignment = StringAlignment.Far;
                            }

                            SolidBrush brush = new(textColor);
                            RectangleF layoutRectangle = new(5f, num, base.Width - 5, num2);

                            e.Graphics.PixelOffsetMode = PixelOffsetType;
                            e.Graphics.TextRenderingHint = TextRenderingType;

                            e.Graphics.DrawString(value.ToString(), Font, brush, layoutRectangle, stringFormat);

                            num += num2;
                        }

                        if (showGrid)
                        {
                            num = 0;

                            foreach (int num3 in list)
                            {
                                e.Graphics.DrawRectangle(new Pen(splitterColor, 1f), new Rectangle(0, num, base.Width, num + num2));
                                num += num2;
                            }

                            e.Graphics.DrawRectangle(new Pen(splitterColor, 1f), 1, 1, base.Width, base.Height);
                        }
                    }
                    else
                    {
                        int num4 = base.Width / items.Count;
                        decimal d2 = base.Height / Items.Max();

                        foreach (int value2 in list)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(filledColor), new RectangleF(num, base.Height - (int)(value2 * d2), num4, Height));

                            StringFormat stringFormat2 = new()
                            {
                                Alignment = StringAlignment.Center
                            };

                            if (textAlignment == Aligning.Near)
                            {
                                stringFormat2.LineAlignment = StringAlignment.Near;
                            }

                            if (textAlignment == Aligning.Center)
                            {
                                stringFormat2.LineAlignment = StringAlignment.Center;
                            }

                            if (textAlignment == Aligning.Far)
                            {
                                stringFormat2.LineAlignment = StringAlignment.Far;
                            }

                            SolidBrush brush2 = new(textColor);
                            RectangleF layoutRectangle2 = new(num, 5f, num4, base.Height - 5);

                            e.Graphics.PixelOffsetMode = PixelOffsetType;
                            e.Graphics.TextRenderingHint = TextRenderingType;

                            e.Graphics.DrawString(value2.ToString(), Font, brush2, layoutRectangle2, stringFormat2);

                            num += num4;
                        }

                        if (showGrid)
                        {
                            num = 0;

                            foreach (int num5 in list)
                            {
                                e.Graphics.DrawRectangle(new Pen(splitterColor, 1f), new Rectangle(num, 0, num + num4, base.Height));
                                num += num4;
                            }

                            e.Graphics.DrawRectangle(new Pen(splitterColor, 1f), 1, 1, base.Width, base.Height);
                        }
                    }
                }

                if (graphStyle == Style.Material)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(40, 40, 40)), 0, 0, base.Width, base.Height);

                    List<int> list2 = new();

                    if (sorting == SortStyle.Normal)
                    {
                        list2 = items;
                    }

                    if (sorting == SortStyle.Descending)
                    {
                        list2 = items;
                        list2 = (from p in list2
                                 orderby p descending
                                 select p).ToList();
                    }

                    if (sorting == SortStyle.Ascending)
                    {
                        list2 = items;
                        list2.Sort();
                    }

                    int num6 = 0;

                    List<Color> list3 = new()
                    {
                        Color.FromArgb(249, 55, 98),
                        Color.FromArgb(219, 55, 128),
                        Color.FromArgb(193, 58, 151),
                        Color.FromArgb(166, 58, 182),
                        Color.FromArgb(147, 61, 180),
                        Color.FromArgb(126, 66, 186),
                        Color.FromArgb(107, 70, 188),
                        Color.FromArgb(77, 94, 210),
                        Color.FromArgb(48, 119, 227),
                        Color.FromArgb(23, 144, 249),
                        Color.FromArgb(10, 148, 249),
                        Color.FromArgb(0, 152, 250),
                        Color.FromArgb(0, 162, 250),
                        Color.FromArgb(0, 150, 212)
                    };

                    int num7 = 0;

                    if (graphOrientation == Orientation.Vertical)
                    {
                        int num8 = base.Width / items.Count;
                        int num9 = base.Height / Items.Max();

                        foreach (int num10 in list2)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(list3[num7]), new RectangleF(num6, base.Height - (num10 * num9), num8, Height));
                            StringFormat stringFormat3 = new()
                            {
                                Alignment = StringAlignment.Center
                            };

                            SolidBrush brush3 = new(list3[num7]);
                            RectangleF layoutRectangle3 = default;

                            layoutRectangle3 = new RectangleF(num6, base.Height - (num10 * num9) - (Font.Size / 2f * 3f), num8, Font.Size * 2f);

                            e.Graphics.PixelOffsetMode = PixelOffsetType;
                            e.Graphics.TextRenderingHint = TextRenderingType;

                            e.Graphics.DrawString(num10.ToString(), Font, brush3, layoutRectangle3, stringFormat3);

                            num6 += num8;
                            num7++;

                            if (num7 == 14)
                            {
                                list3.Reverse();
                                num7 = 0;
                            }
                        }
                    }

                    if (graphOrientation == Orientation.Horizontal)
                    {
                        int num11 = base.Height / items.Count;
                        int num12 = base.Width / Items.Max();

                        foreach (int num13 in list2)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(list3[num7]), new RectangleF(0f, num6, num13 * num12, num11));

                            StringFormat stringFormat4 = new()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Near
                            };

                            SolidBrush brush4 = new(list3[num7]);
                            RectangleF layoutRectangle4 = default;

                            layoutRectangle4 = new RectangleF(num13 * num12, num6, base.Width - (num13 * num12), num11);

                            e.Graphics.PixelOffsetMode = PixelOffsetType;
                            e.Graphics.TextRenderingHint = TextRenderingType;

                            e.Graphics.DrawString(num13.ToString(), Font, brush4, layoutRectangle4, stringFormat4);

                            num6 += num11;
                            num7++;

                            if (num7 == 14)
                            {
                                list3.Reverse();
                                num7 = 0;
                            }
                        }
                    }
                }

                if (graphStyle == Style.Bootstrap)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(35, 40, 50)), 0, 0, base.Width, base.Height);

                    List<int> list4 = new();

                    if (sorting == SortStyle.Normal)
                    {
                        list4 = items;
                    }

                    if (sorting == SortStyle.Descending)
                    {
                        list4 = items;
                        list4 = (from p in list4
                                 orderby p descending
                                 select p).ToList();
                    }

                    if (sorting == SortStyle.Ascending)
                    {
                        list4 = items;
                        list4.Sort();
                    }

                    int num14 = 0;

                    if (graphOrientation == Orientation.Horizontal)
                    {
                        int num15 = base.Height / items.Count;
                        decimal d3 = base.Width / Items.Max();

                        foreach (int value3 in list4)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 35, 40)), new RectangleF(0f, num14, (int)(value3 * d3), num15));

                            StringFormat stringFormat5 = new()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Far
                            };

                            SolidBrush brush5 = new(Color.FromArgb(115, 120, 125));
                            RectangleF layoutRectangle5 = new(5f, num14, base.Width - 5, num15);

                            e.Graphics.PixelOffsetMode = PixelOffsetType;
                            e.Graphics.TextRenderingHint = TextRenderingType;

                            e.Graphics.DrawString(value3.ToString(), Font, brush5, layoutRectangle5, stringFormat5);

                            num14 += num15;
                        }

                        num14 = 0;

                        foreach (int num16 in list4)
                        {
                            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(60, 65, 70), 1f), new Rectangle(0, num14, base.Width, num14 + num15));
                            num14 += num15;
                        }

                        e.Graphics.DrawRectangle(new Pen(Color.FromArgb(60, 65, 70), 1f), 1, 1, base.Width, base.Height);
                    }
                    else
                    {
                        int num17 = base.Width / items.Count;
                        decimal d4 = base.Height / Items.Max();
                        foreach (int value4 in list4)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 35, 40)), new RectangleF(num14, base.Height - (int)(value4 * d4), num17, Height));

                            StringFormat stringFormat6 = new()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Near
                            };

                            SolidBrush brush6 = new(Color.FromArgb(115, 120, 125));
                            RectangleF layoutRectangle6 = new(num14, 5f, num17, base.Height - 5);

                            e.Graphics.PixelOffsetMode = PixelOffsetType;
                            e.Graphics.TextRenderingHint = TextRenderingType;

                            e.Graphics.DrawString(value4.ToString(), Font, brush6, layoutRectangle6, stringFormat6);

                            num14 += num17;
                        }

                        num14 = 0;

                        foreach (int num18 in list4)
                        {
                            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(60, 65, 70), 1f), new Rectangle(num14, 0, num14 + num17, base.Height));
                            num14 += num17;
                        }

                        e.Graphics.DrawRectangle(new Pen(Color.FromArgb(60, 65, 70), 1f), 1, 1, base.Width, base.Height);
                    }
                }
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(unfilledColor), 0, 0, base.Width, base.Height);
            }
            base.OnPaint(e);
        }

        private List<int> items = new();

        private Color filledColor = Color.FromArgb(30, 33, 38);

        private Color unfilledColor = Color.FromArgb(37, 40, 49);

        private Color splitterColor = Color.FromArgb(59, 62, 71);

        private Color textColor = Color.FromArgb(120, 120, 120);

        private SortStyle sorting = SortStyle.Normal;

        private Aligning textAlignment = Aligning.Far;

        private Orientation graphOrientation = Orientation.Vertical;

        private Style graphStyle = Style.Material;

        private bool showGrid;

        public enum SortStyle
        {
            Ascending,
            Descending,
            Normal
        }

        public enum Aligning
        {
            Near,
            Center,
            Far
        }

        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        public enum Style
        {
            Flat,
            Material,
            Bootstrap
        }
    }

    #endregion
}