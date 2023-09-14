#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverTabPage

    public class ForeverTabPage : TabControl
    {
        private int W;
        private int H;

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Alignment = TabAlignment.Top;
        }

        [Category("Colors")]
        public Color BGColor { get; set; } = Color.FromArgb(60, 70, 73);

        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(45, 47, 49);

        [Category("Colors")]
        public Color ActiveColor { get; set; } = ForeverLibrary.ForeverColor;

        [Category("Colors")]
        public Color ActiveFontColor { get; set; } = Color.White;

        [Category("Colors")]
        public Color DeactiveFontColor { get; set; } = Color.White;

        public ForeverTabPage()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.FromArgb(60, 70, 73);

            Font = new("Segoe UI", 10);
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new(120, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width - 1;
            H = Height - 1;

            Graphics _with13 = G;
            _with13.SmoothingMode = SmoothingMode.HighQuality;
            _with13.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with13.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with13.Clear(BaseColor);

            try
            {
                SelectedTab.BackColor = BGColor;
            }
            catch
            {
            }

            for (int i = 0; i <= TabCount - 1; i++)
            {
                Rectangle Base = new(new Point(GetTabRect(i).Location.X + 2, GetTabRect(i).Location.Y), new Size(GetTabRect(i).Width, GetTabRect(i).Height));
                Rectangle BaseSize = new(Base.Location, new Size(Base.Width, Base.Height));

                if (i == SelectedIndex)
                {
                    //-- Base
                    _with13.FillRectangle(new SolidBrush(BaseColor), BaseSize);

                    //-- Gradiant
                    //.fill
                    _with13.FillRectangle(new SolidBrush(ActiveColor), BaseSize);

                    //-- ImageList
                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {
                                //-- Image
                                _with13.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(BaseSize.Location.X + 8, BaseSize.Location.Y + 6));
                                //-- Text
                                _with13.DrawString("      " + TabPages[i].Text, Font, new SolidBrush(ActiveFontColor), BaseSize, ForeverLibrary.CenterSF);
                            }
                            else
                            {
                                //-- Text
                                _with13.DrawString(TabPages[i].Text, Font, new SolidBrush(ActiveFontColor), BaseSize, ForeverLibrary.CenterSF);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        //-- Text
                        _with13.DrawString(TabPages[i].Text, Font, new SolidBrush(ActiveFontColor), BaseSize, ForeverLibrary.CenterSF);
                    }
                }
                else
                {
                    //-- Base
                    _with13.FillRectangle(new SolidBrush(BaseColor), BaseSize);

                    //-- ImageList
                    if (ImageList != null)
                    {
                        try
                        {
                            if (ImageList.Images[TabPages[i].ImageIndex] != null)
                            {
                                //-- Image
                                _with13.DrawImage(ImageList.Images[TabPages[i].ImageIndex], new Point(BaseSize.Location.X + 8, BaseSize.Location.Y + 6));
                                //-- Text
                                _with13.DrawString("      " + TabPages[i].Text, Font, new SolidBrush(DeactiveFontColor), BaseSize, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Center
                                });
                            }
                            else
                            {
                                //-- Text
                                _with13.DrawString(TabPages[i].Text, Font, new SolidBrush(DeactiveFontColor), BaseSize, new StringFormat
                                {
                                    LineAlignment = StringAlignment.Center,
                                    Alignment = StringAlignment.Center
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        //-- Text
                        _with13.DrawString(TabPages[i].Text, Font, new SolidBrush(DeactiveFontColor), BaseSize, new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        });
                    }
                }
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        private void UpdateColors()
        {
            ForeverColors Colors = ForeverLibrary.GetColors(this);

            ActiveColor = Colors.Forever;
        }
    }

    #endregion
}