#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ExtendedPanel

    public class ExtendedPanel : System.Windows.Forms.Panel
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private readonly Timer Most = new()
        {
            Interval = 100
        };

        public enum Drawer
        {
            Default,
            Image,
            Debug
        }

        private Drawer _DrawMode = Drawer.Default;
        public Drawer DrawMode
        {
            get => _DrawMode;
            set
            {
                if (value == Drawer.Image)
                {
                    SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                    SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                    SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                }
                else
                {
                    SetStyle(ControlStyles.AllPaintingInWmPaint, false);
                    SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                    SetStyle(ControlStyles.SupportsTransparentBackColor, false);
                }

                _DrawMode = value;
                Invalidate();
            }
        }

        private bool _TopMost = true;
        public bool TopMost
        {
            get => _TopMost;
            set
            {
                _TopMost = value;
                Invalidate();
            }
        }

        private int _Opacity = 50;
        public int Opacity
        {
            get => _Opacity;
            set
            {
                if (value is < 0 or > 100)
                {
                    value = 0;
                }

                _Opacity = value;
                Invalidate();
            }
        }

        public int MostInterval
        {
            get => Most.Interval;
            set
            {
                Most.Interval = value;
                Invalidate();
            }
        }

        public ExtendedPanel()
        {
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams CP = base.CreateParams;
                CP.ExStyle |= WS_EX_TRANSPARENT;
                CP.Style &= ~0x04000000; //WS_CLIPSIBLINGS
                CP.Style &= ~0x02000000; //WS_CLIPCHILDREN
                return CP;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0xF)
            {
                foreach (Control C in Controls) { C.Invalidate(); C.Update(); BringToFront(); }
            }

            FindForm().Controls.SetChildIndex(this, 0);
            UpdateZOrder();

            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (DrawMode)
            {
                case Drawer.Default:
                    using (SolidBrush Brush = new(Color.FromArgb(Opacity * 255 / 100, BackColor)))
                    {
                        e.Graphics.FillRectangle(Brush, ClientRectangle);
                    }

                    break;
                case Drawer.Image:
                    e.Graphics.Clear(BackColor);
                    e.Graphics.CopyFromScreen(PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(Width, Height));
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(Opacity * 255 / 100, BackColor)), new Rectangle(new Point(0, 0), Size));
                    break;
                default:
                    break;
            }

            if (TopMost && !Most.Enabled)
            {
                Most.Tick += new EventHandler(Most_Tick);
                Most.Start();
            }
            else if (!TopMost && Most.Enabled)
            {
                Most.Stop();
            }

            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Invalidate();
            base.OnPaintBackground(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Invalidate(Bounds, true);
            }

            base.OnBackColorChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnParentBackColorChanged(e);
        }

        private void Most_Tick(object sender, EventArgs e)
        {
            switch (DrawMode)
            {
                case Drawer.Default:
                    Invalidate();
                    //PaintHelperA.Resume(this);
                    //PaintHelperB.Suspend(this);
                    break;
                case Drawer.Image:
                    //BackColor = Color.FromArgb(Opacity * 255 / 100, BackColor);
                    Graphics Graph = CreateGraphics();
                    Graph.CopyFromScreen(PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(Width, Height));
                    Graph.FillRectangle(new SolidBrush(Color.FromArgb(Opacity * 255 / 100, BackColor)), new Rectangle(new Point(0, 0), Size));
                    break;
                case Drawer.Debug:
                    foreach (Control CTRL in FindForm().Controls)
                    {
                        try
                        {
                            ExtendedPanel EP = CTRL as ExtendedPanel;
                            EP.BringToFront();
                            FindForm().Controls.SetChildIndex(CTRL, 0);
                            EP.UpdateZOrder();
                            EP.Invalidate();
                        }
                        catch
                        {
                            //
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    #endregion
}