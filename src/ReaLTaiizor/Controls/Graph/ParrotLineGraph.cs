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
            items.Add(50);
            items.Add(20);
            items.Add(100);
            items.Add(60);
            items.Add(1);
            items.Add(20);
            items.Add(80);
            items.Add(12);
            items.Add(72);
            items.Add(58);
            items.Add(19);
            items.Add(600);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public List<int> Items
        {
            get => items;
            set
            {
                items = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public bool ShowVerticalLines
        {
            get => showVerticalLines;
            set
            {
                showVerticalLines = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BackGroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BackColor
        {
            get => backColor;
            set
            {
                backColor = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BelowLineColor
        {
            get => belowLineColor;
            set
            {
                belowLineColor = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color LineColor
        {
            get => lineColor;
            set
            {
                lineColor = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        public Color VerticalLineColor
        {
            get => verticalLineColor;
            set
            {
                verticalLineColor = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the graph title")]
        public Color GraphTitleColor
        {
            get => graphTitleColor;
            set
            {
                graphTitleColor = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The of the graph")]
        public string GraphTitle
        {
            get => graphTitle;
            set
            {
                graphTitle = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Draw the title on the control")]
        public bool ShowTitle
        {
            get => showTitle;
            set
            {
                showTitle = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Draw the border on the control")]
        public bool ShowBorder
        {
            get => showBorder;
            set
            {
                showBorder = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Draw the points on each value")]
        public bool ShowPoints
        {
            get => showPoints;
            set
            {
                showPoints = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The point size")]
        public int PointSize
        {
            get => pointSize;
            set
            {
                pointSize = value;
                Refresh();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The title alignment")]
        public StringAlignment TitleAlignment
        {
            get => titleAlignment;
            set
            {
                titleAlignment = value;
                Refresh();
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
                Refresh();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingType;

            Pen pen = new(lineColor, 1f);
            Pen pen2 = new(verticalLineColor, 1f);

            if (graphStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(backColor), new Rectangle(0, 0, base.Width, base.Height));
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(backgroundColor), new Rectangle(0, 0, base.Width, base.Height));
            }

            int total = items.ToArray().Max();
            int num = base.Width / items.Count;
            int num2 = 0;
            int num3 = base.Height;
            int num4 = num;
            int num5 = 0;

            List<PointF> list = new()
            {
                new Point(1, base.Height)
            };

            foreach (int num6 in items)
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

            if (graphStyle != Style.Curved)
            {
                list.Add(new Point(base.Width, base.Height));

                if (graphStyle == Style.Flat)
                {
                    SolidBrush brush = new(belowLineColor);
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

                foreach (int number in items)
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

                    if (graphStyle == Style.Flat && showVerticalLines)
                    {
                        num8++;

                        if (num8 != items.ToArray().Length && num4 != 0 && num4 != base.Width)
                        {
                            e.Graphics.DrawLine(pen2, num4, base.Height, num4, 0);
                        }
                    }

                    e.Graphics.DrawLine(pen, num2 - 1, num3 - 1, num4 - 1, num5 - 1);

                    if (showPoints)
                    {
                        if (num5 - (pointSize / 2) - 1 < 0)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(lineColor), new RectangleF(num4 - (pointSize / 2) - 1, -1f, pointSize, pointSize));
                        }
                        else if (num5 - (pointSize / 2) - 1 + pointSize > base.Height)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(lineColor), new RectangleF(num4 - (pointSize / 2) - 1, base.Height - pointSize + 1, pointSize, pointSize));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(lineColor), new RectangleF(num4 - (pointSize / 2) - 1, num5 - (pointSize / 2) - 1, pointSize, pointSize));
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
                if (showPoints)
                {
                    foreach (PointF pointF in list)
                    {
                        if (pointF.Y - (pointSize / 2) - 1f < 0f)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(lineColor), new RectangleF(pointF.X - (pointSize / 2) - 1f, -1f, pointSize, pointSize));
                        }
                        else if (pointF.Y - (pointSize / 2) - 1f + pointSize > Height)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(lineColor), new RectangleF(pointF.X - (pointSize / 2) - 1f, base.Height - pointSize + 1, pointSize, pointSize));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(lineColor), new RectangleF(pointF.X - (pointSize / 2) - 1f, pointF.Y - (pointSize / 2) - 1f, pointSize, pointSize));
                        }
                    }
                }

                e.Graphics.DrawCurve(pen, list.ToArray());
            }

            if (graphStyle != Style.Material && showBorder)
            {
                e.Graphics.DrawRectangle(new Pen(borderColor, 2f), new Rectangle(0, 0, base.Width - 1, base.Height - 1));
            }

            if (showTitle)
            {
                StringFormat stringFormat = new()
                {
                    LineAlignment = StringAlignment.Near,
                    Alignment = titleAlignment
                };

                Font font = new("Arial", 14f);
                SolidBrush brush3 = new(graphTitleColor);
                RectangleF layoutRectangle = new(0f, 0f, Width, Height);

                e.Graphics.PixelOffsetMode = PixelOffsetType;
                e.Graphics.TextRenderingHint = TextRenderingType;

                e.Graphics.DrawString(graphTitle, font, brush3, layoutRectangle, stringFormat);
            }
            base.OnPaint(e);
        }

        private List<int> items = new();

        private bool showVerticalLines;

        private bool showBorder;

        private bool showTitle;

        private bool showPoints = true;

        private StringAlignment titleAlignment;

        private int pointSize = 7;

        private Color backgroundColor = Color.FromArgb(102, 217, 174);

        private Color backColor = Color.FromArgb(40, 40, 40);

        private Color belowLineColor = Color.FromArgb(24, 202, 142);

        private Color borderColor = Color.White;

        private Color lineColor = Color.White;

        private Color verticalLineColor = Color.DimGray;

        private Color graphTitleColor = Color.Gray;

        private string graphTitle = "Parrot Line Graph";

        private Style graphStyle = Style.Material;

        public enum Style
        {
            Flat,
            Material,
            Curved
        }
    }

    #endregion
}