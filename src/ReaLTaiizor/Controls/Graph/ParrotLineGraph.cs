#region Imports

using ReaLTaiizor.Util;
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
    #region ParrotLineGraph

    public class ParrotLineGraph : Control
    {
        public ParrotLineGraph()
        {
            DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.Size = new Size(200, 100);
            Items.Add(50);
            Items.Add(20);
            Items.Add(100);
            Items.Add(60);
            Items.Add(1);
            Items.Add(20);
            Items.Add(80);
            Items.Add(12);
            Items.Add(72);
            Items.Add(58);
            Items.Add(19);
            Items.Add(600);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public List<int> Items
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = [];

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public bool ShowVerticalLines
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BackGroundColor
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = Color.FromArgb(102, 217, 174);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BackColor
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = Color.FromArgb(40, 40, 40);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BelowLineColor
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = Color.FromArgb(24, 202, 142);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color LineColor
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BorderColor
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color VerticalLineColor
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = Color.DimGray;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the graph title")]
        public Color GraphTitleColor
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = Color.Gray;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The of the graph")]
        public string GraphTitle
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = "Parrot Line Graph";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Draw the title on the control")]
        public bool ShowTitle
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Draw the border on the control")]
        public bool ShowBorder
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Draw the points on each value")]
        public bool ShowPoints
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = true;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The point size")]
        public int PointSize
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = 7;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The title alignment")]
        public StringAlignment TitleAlignment
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The style of the graph")]
        public Style GraphStyle
        {
            get;
            set
            {
                field = value;
                Refresh();
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
        } = SmoothingMode.HighQuality;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingType;

            Pen pen = new(LineColor, 1f);
            Pen pen2 = new(VerticalLineColor, 1f);

            if (GraphStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, base.Width, base.Height));
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(BackGroundColor), new Rectangle(0, 0, base.Width, base.Height));
            }

            int total = Items.ToArray().Max();
            int num = base.Width / Items.Count;
            int num2 = 0;
            int num3 = base.Height;
            int num4 = num;
            int num5 = 0;

            List<PointF> list =
            [
                new Point(1, base.Height)
            ];

            foreach (int num6 in Items)
            {
                if (num6 > 0)
                {
                    int num7 = Percentage.IntToPercent(num6, total);

                    if (num7 > 97)
                    {
                        num5 = base.Height - Percentage.PercentToInt(97, base.Height);
                    }
                    else if (num7 < 3)
                    {
                        num5 = base.Height - Percentage.PercentToInt(3, base.Height);
                    }
                    else
                    {
                        num5 = base.Height - (num7 * base.Height / 100);
                    }

                    list.Add(new Point(num4 - 1, num5 - 1));

                    num2 = num4;
                    num3 = num5;
                    num4 += num;
                }
            }

            list.Add(new Point(base.Width, num5 - 1));

            if (GraphStyle != Style.Curved)
            {
                list.Add(new Point(base.Width, base.Height));

                if (GraphStyle == Style.Flat)
                {
                    SolidBrush brush = new(BelowLineColor);
                    e.Graphics.FillPolygon(brush, list.ToArray());
                }
                else
                {
                    LinearGradientBrush brush2 = new(new Rectangle(0, 0, base.Width, base.Height), Color.FromArgb(249, 55, 98), Color.FromArgb(0, 162, 250), 1f);
                    e.Graphics.FillPolygon(brush2, list.ToArray());
                }

                num2 = 1;
                num3 = base.Height;
                num4 = num;
                num5 = 0;

                int num8 = 0;

                foreach (int number in Items)
                {
                    int num9 = Percentage.IntToPercent(number, total);

                    if (num9 > 97)
                    {
                        num5 = base.Height - Percentage.PercentToInt(97, base.Height);
                    }
                    else if (num9 < 3)
                    {
                        num5 = base.Height - Percentage.PercentToInt(3, base.Height);
                    }
                    else
                    {
                        num5 = base.Height - (num9 * base.Height / 100);
                    }

                    if (GraphStyle == Style.Flat && ShowVerticalLines)
                    {
                        num8++;

                        if (num8 != Items.ToArray().Length && num4 != 0 && num4 != base.Width)
                        {
                            e.Graphics.DrawLine(pen2, num4, base.Height, num4, 0);
                        }
                    }

                    e.Graphics.DrawLine(pen, num2 - 1, num3 - 1, num4 - 1, num5 - 1);

                    if (ShowPoints)
                    {
                        if (num5 - (PointSize / 2) - 1 < 0)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(LineColor), new RectangleF(num4 - (PointSize / 2) - 1, -1f, PointSize, PointSize));
                        }
                        else if (num5 - (PointSize / 2) - 1 + PointSize > base.Height)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(LineColor), new RectangleF(num4 - (PointSize / 2) - 1, base.Height - PointSize + 1, PointSize, PointSize));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(LineColor), new RectangleF(num4 - (PointSize / 2) - 1, num5 - (PointSize / 2) - 1, PointSize, PointSize));
                        }
                    }

                    num2 = num4;
                    num3 = num5;
                    num4 += num;
                }

                e.Graphics.DrawLine(pen, num2, num3, base.Width, num3);
            }
            else
            {
                if (ShowPoints)
                {
                    foreach (PointF pointF in list)
                    {
                        if (pointF.Y - (PointSize / 2) - 1f < 0f)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(LineColor), new RectangleF(pointF.X - (PointSize / 2) - 1f, -1f, PointSize, PointSize));
                        }
                        else if (pointF.Y - (PointSize / 2) - 1f + PointSize > Height)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(LineColor), new RectangleF(pointF.X - (PointSize / 2) - 1f, base.Height - PointSize + 1, PointSize, PointSize));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(LineColor), new RectangleF(pointF.X - (PointSize / 2) - 1f, pointF.Y - (PointSize / 2) - 1f, PointSize, PointSize));
                        }
                    }
                }

                e.Graphics.DrawCurve(pen, list.ToArray());
            }

            if (GraphStyle != Style.Material && ShowBorder)
            {
                e.Graphics.DrawRectangle(new Pen(BorderColor, 2f), new Rectangle(0, 0, base.Width - 1, base.Height - 1));
            }

            if (ShowTitle)
            {
                StringFormat stringFormat = new()
                {
                    LineAlignment = StringAlignment.Near,
                    Alignment = TitleAlignment
                };

                Font font = new("Arial", 14f);
                SolidBrush brush3 = new(GraphTitleColor);
                RectangleF layoutRectangle = new(0f, 0f, Width, Height);

                e.Graphics.PixelOffsetMode = PixelOffsetType;
                e.Graphics.TextRenderingHint = TextRenderingType;

                e.Graphics.DrawString(GraphTitle, font, brush3, layoutRectangle, stringFormat);
            }
            base.OnPaint(e);
        }

        public enum Style
        {
            Flat,
            Material,
            Curved
        }
    }

    #endregion
}