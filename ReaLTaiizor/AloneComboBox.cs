#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

#endregion

namespace ReaLTaiizor
{
    #region AloneComboBox

    public class AloneCombobox : ComboBox
    {
        private Graphics G;

        private Rectangle Rect;

        private bool _EnabledCalc;

        public new bool Enabled
        {
            get
            {
                return this.EnabledCalc;
            }
            set
            {
                this._EnabledCalc = value;
                base.Invalidate();
            }
        }

        [DisplayName("Enabled")]
        public bool EnabledCalc
        {
            get
            {
                return this._EnabledCalc;
            }
            set
            {
                base.Enabled = value;
                this.Enabled = value;
                base.Invalidate();
            }
        }

        public AloneCombobox()
        {
            this.DoubleBuffered = true;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Cursor = Cursors.Hand;
            this.Enabled = true;
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.ItemHeight = 20;
            this.Size = new Size(64, 26);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.G = e.Graphics;
            this.G.SmoothingMode = SmoothingMode.HighQuality;
            this.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            this.G.Clear(Color.White);
            bool enabled = this.Enabled;
            checked
            {
                if (enabled)
                {
                    using (Pen pen = new Pen(AloneLibrary.ColorFromHex("#D0D5D9")))
                    {
                        using (SolidBrush solidBrush = new SolidBrush(AloneLibrary.ColorFromHex("#7C858E")))
                        {
                            using (Font font = new Font("Marlett", 13f))
                            {
                                this.G.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 6, AloneLibrary.RoundingStyle.All));
                                this.G.DrawString("6", font, solidBrush, new Point(base.Width - 22, 3));
                            }
                        }
                    }
                }
                else
                {
                    using (Pen pen2 = new Pen(AloneLibrary.ColorFromHex("#E1E1E2")))
                    {
                        using (SolidBrush solidBrush2 = new SolidBrush(AloneLibrary.ColorFromHex("#D0D3D7")))
                        {
                            using (Font font2 = new Font("Marlett", 13f))
                            {
                                this.G.DrawPath(pen2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 6, AloneLibrary.RoundingStyle.All));
                                this.G.DrawString("6", font2, solidBrush2, new Point(base.Width - 22, 3));
                            }
                        }
                    }
                }
                bool flag = !Information.IsNothing(base.Items);
                if (flag)
                {
                    using (Font font3 = new Font("Segoe UI", 9f))
                    {
                        using (SolidBrush solidBrush3 = new SolidBrush(AloneLibrary.ColorFromHex("#7C858E")))
                        {
                            bool enabled2 = this.Enabled;
                            if (enabled2)
                            {
                                bool flag2 = this.SelectedIndex != -1;
                                if (flag2)
                                {
                                    this.G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[this.SelectedIndex])), font3, solidBrush3, new Point(7, 4));
                                }
                                else
                                {
                                    try
                                    {
                                        this.G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[0])), font3, solidBrush3, new Point(7, 4));
                                    }
                                    catch (Exception arg_272_0)
                                    {
                                        ProjectData.SetProjectError(arg_272_0);
                                        ProjectData.ClearProjectError();
                                    }
                                }
                            }
                            else
                            {
                                using (SolidBrush solidBrush4 = new SolidBrush(AloneLibrary.ColorFromHex("#D0D3D7")))
                                {
                                    bool flag3 = this.SelectedIndex != -1;
                                    if (flag3)
                                    {
                                        this.G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[this.SelectedIndex])), font3, solidBrush4, new Point(7, 4));
                                    }
                                    else
                                    {
                                        this.G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[0])), font3, solidBrush4, new Point(7, 4));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            this.G = e.Graphics;
            this.G.SmoothingMode = SmoothingMode.HighQuality;
            this.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            bool enabled = this.Enabled;
            checked
            {
                if (enabled)
                {
                    e.DrawBackground();
                    this.Rect = e.Bounds;
                    try
                    {
                        using (new Font("Segoe UI", 9f))
                        {
                            using (new Pen(AloneLibrary.ColorFromHex("#D0D5D9")))
                            {
                                bool flag = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                                if (flag)
                                {
                                    using (new SolidBrush(Color.White))
                                    {
                                        using (SolidBrush solidBrush2 = new SolidBrush(AloneLibrary.ColorFromHex("#78B7E6")))
                                        {
                                            this.G.FillRectangle(solidBrush2, this.Rect);
                                            this.G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[e.Index])), new Font("Segoe UI", 9f), Brushes.White, new Point(this.Rect.X + 5, this.Rect.Y + 1));
                                        }
                                    }
                                }
                                else
                                {
                                    using (SolidBrush solidBrush3 = new SolidBrush(AloneLibrary.ColorFromHex("#7C858E")))
                                    {
                                        this.G.FillRectangle(Brushes.White, this.Rect);
                                        this.G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[e.Index])), new Font("Segoe UI", 9f), solidBrush3, new Point(this.Rect.X + 5, this.Rect.Y + 1));
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception arg_1F1_0)
                    {
                        ProjectData.SetProjectError(arg_1F1_0);
                        ProjectData.ClearProjectError();
                    }
                }
            }
        }

        protected override void OnSelectedItemChanged(EventArgs e)
        {
            base.OnSelectedItemChanged(e);
            base.Invalidate();
        }
    }

    #endregion
}