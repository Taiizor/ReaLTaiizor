#region Imports

using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region TabPage

    public class TabPage : TabControl
    {
        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighQuality;

        public CompositingQuality CompositingQualityType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = CompositingQuality.HighQuality;

        public CompositingMode CompositingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = CompositingMode.SourceOver;

        public InterpolationMode InterpolationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = InterpolationMode.HighQualityBicubic;

        public PixelOffsetMode PixelOffsetType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = PixelOffsetMode.HighQuality;

        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

        public StringAlignment StringType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = StringAlignment.Near;

        public Color FrameColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(41, 50, 63);

        public Color PageColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(50, 63, 74);

        public Color ActiveForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(254, 255, 255);

        public Color NormalForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(159, 162, 167);

        public Color ControlBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(54, 57, 64);

        public Color LineColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(25, 26, 28);

        public Color ActiveTabColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(35, 36, 38);

        public Color TabColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(54, 57, 64);

        public Color ActiveLineTabColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(89, 169, 222);

        public Color LineTabColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(54, 57, 64);

        public TabPage()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            DoubleBuffered = true;
            ItemSize = new(44, 135);
            SizeMode = TabSizeMode.Fixed;
            DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            base.DoubleBuffered = true;
            SizeMode = TabSizeMode.Fixed;
            Alignment = TabAlignment.Left;
            Appearance = TabAppearance.Normal;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is System.Windows.Forms.TabPage)
            {
                IEnumerator Enumerator;
                try
                {
                    Enumerator = Controls.GetEnumerator();

                    while (Enumerator.MoveNext())
                    {
                        System.Windows.Forms.TabPage Current = (System.Windows.Forms.TabPage)Enumerator.Current;
                        Current = new System.Windows.Forms.TabPage();
                    }
                }
                finally
                {
                    e.Control.BackColor = FrameColor;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Graphics _Graphics = G;

            _Graphics.Clear(FrameColor);
            _Graphics.SmoothingMode = SmoothingType;
            _Graphics.CompositingMode = CompositingType;
            _Graphics.PixelOffsetMode = PixelOffsetType;
            _Graphics.TextRenderingHint = TextRenderingType;
            _Graphics.CompositingQuality = CompositingQualityType;

            // Draw tab selector background
            _Graphics.FillRectangle(new SolidBrush(ControlBackColor), new Rectangle(-5, 0, ItemSize.Height + 4, Height));
            // Draw vertical line at the end of the tab selector rectangle
            _Graphics.DrawLine(new(LineColor), ItemSize.Height - 1, 0, ItemSize.Height - 1, Height);

            for (int TabIndex = 0; TabIndex <= TabCount - 1; TabIndex++)
            {
                if (TabPages[TabIndex].BackColor != PageColor)
                {
                    TabPages[TabIndex].BackColor = PageColor;
                }

                if (TabIndex == SelectedIndex)
                {
                    Rectangle TabRect = new(new Point(GetTabRect(TabIndex).Location.X - 2, GetTabRect(TabIndex).Location.Y - 2), new Size(GetTabRect(TabIndex).Width + 3, GetTabRect(TabIndex).Height - 8));

                    // Draw background of the selected tab
                    _Graphics.FillRectangle(new SolidBrush(ActiveTabColor), TabRect.X, TabRect.Y, TabRect.Width - 4, TabRect.Height + 3);
                    // Draw a tab highlighter on the background of the selected tab
                    Rectangle TabHighlighter = new(new Point(GetTabRect(TabIndex).X - 2, GetTabRect(TabIndex).Location.Y - (TabIndex == 0 ? 1 : 1)), new Size(4, GetTabRect(TabIndex).Height - 7));
                    _Graphics.FillRectangle(new SolidBrush(ActiveLineTabColor), TabHighlighter);
                    // Draw tab text
                    _Graphics.DrawString(TabPages[TabIndex].Text, new Font(Font.FontFamily, Font.Size, Font.Style), new SolidBrush(ActiveForeColor), new Rectangle(TabRect.Left + 40, TabRect.Top + 8, TabRect.Width - 40, TabRect.Height), new StringFormat { Alignment = StringType });

                    if (ImageList != null)
                    {
                        int Index = TabPages[TabIndex].ImageIndex;

                        if (!(Index == -1))
                        {
                            _Graphics.DrawImage(ImageList.Images[TabPages[TabIndex].ImageIndex], TabRect.X + 9, TabRect.Y + 6, 24, 24);
                        }
                    }
                }
                else
                {
                    Rectangle TabRect = new(new Point(GetTabRect(TabIndex).Location.X - 2, GetTabRect(TabIndex).Location.Y - 2), new Size(GetTabRect(TabIndex).Width + 3, GetTabRect(TabIndex).Height - 8));

                    // Draw background of the tab
                    _Graphics.FillRectangle(new SolidBrush(TabColor), TabRect.X, TabRect.Y, TabRect.Width - 4, TabRect.Height + 3);
                    // Draw a tab highlighter on the background of the tab
                    Rectangle TabHighlighter = new(new Point(GetTabRect(TabIndex).X - 2, GetTabRect(TabIndex).Location.Y - (TabIndex == 0 ? 1 : 1)), new Size(4, GetTabRect(TabIndex).Height - 7));
                    _Graphics.FillRectangle(new SolidBrush(LineTabColor), TabHighlighter);

                    _Graphics.DrawString(TabPages[TabIndex].Text, new Font(Font.FontFamily, Font.Size, Font.Style), new SolidBrush(NormalForeColor), new Rectangle(TabRect.Left + 40, TabRect.Top + 8, TabRect.Width - 40, TabRect.Height), new StringFormat { Alignment = StringType });

                    if (ImageList != null)
                    {
                        int Index = TabPages[TabIndex].ImageIndex;

                        if (!(Index == -1))
                        {
                            _Graphics.DrawImage(ImageList.Images[TabPages[TabIndex].ImageIndex], TabRect.X + 9, TabRect.Y + 6, 24, 24);
                        }
                    }

                }
            }

            e.Graphics.SmoothingMode = SmoothingType;
            e.Graphics.CompositingMode = CompositingType;
            e.Graphics.PixelOffsetMode = PixelOffsetType;
            e.Graphics.TextRenderingHint = TextRenderingType;
            e.Graphics.InterpolationMode = InterpolationType;
            e.Graphics.CompositingQuality = CompositingQualityType;

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}