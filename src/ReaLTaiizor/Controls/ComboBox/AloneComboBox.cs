#region Imports

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AloneComboBox

    public class AloneComboBox : ComboBox
    {
        private Graphics G;

        private Rectangle Rect;

        private bool _EnabledCalc;

        public new bool Enabled
        {
            get => EnabledCalc;
            set
            {
                _EnabledCalc = value;
                Invalidate();
            }
        }

        [DisplayName("Enabled")]
        public bool EnabledCalc
        {
            get => _EnabledCalc;
            set
            {
                base.Enabled = value;
                Enabled = value;
                Invalidate();
            }
        }

        public AloneComboBox()
        {
            DoubleBuffered = true;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            Cursor = Cursors.Hand;
            Enabled = true;
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.ItemHeight = 20;
            Size = new(64, 26);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            G.Clear(Color.White);
            bool enabled = Enabled;
            checked
            {
                if (enabled)
                {
                    using Pen pen = new(AloneLibrary.ColorFromHex("#D0D5D9"));
                    using SolidBrush solidBrush = new(AloneLibrary.ColorFromHex("#7C858E"));
                    using Font font = new("Marlett", 13f);
                    G.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 6, AloneLibrary.RoundingStyle.All));
                    G.DrawString("6", font, solidBrush, new Point(base.Width - 22, 3));
                }
                else
                {
                    using Pen pen2 = new(AloneLibrary.ColorFromHex("#E1E1E2"));
                    using SolidBrush solidBrush2 = new(AloneLibrary.ColorFromHex("#D0D3D7"));
                    using Font font2 = new("Marlett", 13f);
                    G.DrawPath(pen2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 6, AloneLibrary.RoundingStyle.All));
                    G.DrawString("6", font2, solidBrush2, new Point(base.Width - 22, 3));
                }
                bool flag = !Information.IsNothing(base.Items);
                if (flag)
                {
                    using Font font3 = new("Segoe UI", 9f);
                    using SolidBrush solidBrush3 = new(AloneLibrary.ColorFromHex("#7C858E"));
                    bool enabled2 = Enabled;
                    if (enabled2)
                    {
                        bool flag2 = SelectedIndex != -1;
                        if (flag2)
                        {
                            G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[SelectedIndex])), font3, solidBrush3, new Point(7, 4));
                        }
                        else
                        {
                            try
                            {
                                G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[0])), font3, solidBrush3, new Point(7, 4));
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
                        using SolidBrush solidBrush4 = new(AloneLibrary.ColorFromHex("#D0D3D7"));
                        bool flag3 = SelectedIndex != -1;
                        if (flag3)
                        {
                            G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[SelectedIndex])), font3, solidBrush4, new Point(7, 4));
                        }
                        else
                        {
                            G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[0])), font3, solidBrush4, new Point(7, 4));
                        }
                    }
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            bool enabled = Enabled;
            checked
            {
                if (enabled)
                {
                    e.DrawBackground();
                    Rect = e.Bounds;
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
                                        using SolidBrush solidBrush2 = new(AloneLibrary.ColorFromHex("#78B7E6"));
                                        G.FillRectangle(solidBrush2, Rect);
                                        G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[e.Index])), new Font("Segoe UI", 9f), Brushes.White, new Point(Rect.X + 5, Rect.Y + 1));
                                    }
                                }
                                else
                                {
                                    using SolidBrush solidBrush3 = new(AloneLibrary.ColorFromHex("#7C858E"));
                                    G.FillRectangle(Brushes.White, Rect);
                                    G.DrawString(base.GetItemText(RuntimeHelpers.GetObjectValue(base.Items[e.Index])), new Font("Segoe UI", 9f), solidBrush3, new Point(Rect.X + 5, Rect.Y + 1));
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
            Invalidate();
        }
    }

    #endregion
}