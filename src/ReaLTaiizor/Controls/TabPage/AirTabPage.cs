#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AirTabPage

    public class AirTabPage : TabControl
    {
        public AirTabPage()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            ItemSize = new(30, 115);
            SizeMode = TabSizeMode.Fixed;
            MouseMove += AirTabPage_MouseMove;
        }

        private void AirTabPage_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < TabCount; i++)
            {
                // get their rectangle area and check if it contains the mouse cursor
                Rectangle r = GetTabRect(i);
                if (r.Contains(e.Location))
                {
                    // show the context menu here
                    System.Diagnostics.Debug.WriteLine("TabPressed: " + i);
                }
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Alignment = TabAlignment.Left;
        }

        private Color C1 = Color.FromArgb(78, 87, 100);
        public Color SquareColor
        {
            get => C1;
            set
            {
                C1 = value;
                Invalidate();
            }
        }

        private Color _BaseColor = Color.White;
        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        private Color _NormalTextColor = Color.DimGray;
        public Color NormalTextColor
        {
            get => _NormalTextColor;
            set
            {
                _NormalTextColor = value;
                Invalidate();
            }
        }

        private Color _SelectedTextColor = Color.Black;
        public Color SelectedTextColor
        {
            get => _SelectedTextColor;
            set
            {
                _SelectedTextColor = value;
                Invalidate();
            }
        }

        private Color _SelectedTabBackColor = Color.White;
        public Color SelectedTabBackColor
        {
            get => _SelectedTabBackColor;
            set
            {
                _SelectedTabBackColor = value;
                Invalidate();
            }
        }

        private Cursor _TabCursor = Cursors.Hand;
        public Cursor TabCursor
        {
            get => _TabCursor;
            set
            {
                _TabCursor = value;
                Invalidate();
            }
        }

        private bool OB = false;
        public bool ShowOuterBorders
        {
            get => OB;
            set
            {
                OB = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            try
            {
                SelectedTab.BackColor = SelectedTabBackColor;
                SelectedTab.Cursor = Cursors.Default;
                Cursor = TabCursor;
            }
            catch
            {
            }

            G.Clear(BaseColor);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            for (int i = 0; i <= TabCount - 1; i++)
            {
                Rectangle x2 = new(new Point(GetTabRect(i).Location.X - 2, GetTabRect(i).Location.Y - 2), new Size(GetTabRect(i).Width + 3, GetTabRect(i).Height - 1));
                Rectangle textrectangle = new(x2.Location.X + 20, x2.Location.Y, x2.Width - 20, x2.Height);

                if (i == SelectedIndex)
                {
                    G.FillRectangle(new SolidBrush(C1), new Rectangle(x2.Location, new Size(9, x2.Height)));

                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {
                                G.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(textrectangle.Location.X + 8, textrectangle.Location.Y + 6));
                                G.DrawString("      " + TabPages[i].Text, Font, new SolidBrush(SelectedTextColor), textrectangle, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Near
                                });
                            }
                            else
                            {
                                G.DrawString(TabPages[i].Text, Font, new SolidBrush(SelectedTextColor), textrectangle, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Near
                                });
                            }
                        }
                        catch
                        {
                            G.DrawString(TabPages[i].Text, Font, new SolidBrush(SelectedTextColor), textrectangle, new StringFormat
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Near
                            });
                        }
                    }
                    else
                    {
                        G.DrawString(TabPages[i].Text, Font, new SolidBrush(SelectedTextColor), textrectangle, new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Near
                        });
                    }

                }
                else
                {
                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {
                                G.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(textrectangle.Location.X + 8, textrectangle.Location.Y + 6));
                                G.DrawString("      " + TabPages[i].Text, Font, new SolidBrush(NormalTextColor), textrectangle, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Near
                                });
                            }
                            else
                            {
                                G.DrawString(TabPages[i].Text, Font, new SolidBrush(NormalTextColor), textrectangle, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Near
                                });
                            }
                        }
                        catch
                        {
                            G.DrawString(TabPages[i].Text, Font, new SolidBrush(NormalTextColor), textrectangle, new StringFormat
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Near
                            });
                        }
                    }
                    else
                    {
                        G.DrawString(TabPages[i].Text, Font, new SolidBrush(NormalTextColor), textrectangle, new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Near
                        });
                    }
                }
            }

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}