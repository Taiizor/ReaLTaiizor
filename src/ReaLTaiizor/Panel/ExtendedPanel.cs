#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region ExtendedPanel

    public class ExtendedPanel : System.Windows.Forms.Panel
    {
        private const int WS_EX_TRANSPARENT = 0x20;

        readonly Timer Timer = new Timer()
        {
            Interval = 1
        };

        public enum Drawer
        {
            Default,
            Image
        }

        private Drawer _DrawMode = Drawer.Default;
        public Drawer DrawMode
        {
            get { return _DrawMode; }
            set
            {
                _DrawMode = value;
                Invalidate();
            }
        }

        private bool _TopMost = true;
        public bool TopMost
        {
            get { return _TopMost; }
            set
            {
                _TopMost = value;
                Invalidate();
            }
        }

        private int _Opacity = 50;
        public int Opacity
        {
            get
            {
                return _Opacity;
            }
            set
            {
                if (value < 0 || value > 100)
                    value = 0;
                _Opacity = value;
                Invalidate();
            }
        }

        public ExtendedPanel()
        {
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
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
                foreach (Control C in Controls) { C.Invalidate(); C.Update(); BringToFront(); }

            FindForm().Controls.SetChildIndex(this, 0);
            UpdateZOrder();

            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (DrawMode)
            {
                case Drawer.Default:
                    using (var Brush = new SolidBrush(Color.FromArgb(Opacity * 255 / 100, BackColor)))
                        e.Graphics.FillRectangle(Brush, ClientRectangle);
                    break;
                case Drawer.Image:
                    e.Graphics.Clear(BackColor);
                    e.Graphics.CopyFromScreen(PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(Width, Height));
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(Opacity * 255 / 100, BackColor)), new Rectangle(new Point(0, 0), Size));
                    break;
                default:
                    break;
            }

            if (TopMost && !Timer.Enabled)
            {
                Timer.Tick += new EventHandler(Timer_Tick);
                Timer.Start();
            }
            else if (!TopMost && Timer.Enabled)
                Timer.Stop();

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
                Parent.Invalidate(Bounds, true);
            base.OnBackColorChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnParentBackColorChanged(e);
        }

        private void Timer_Tick(object sender, EventArgs e)
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
                    Graphics Graph = null;
                    Graph = CreateGraphics();
                    Graph.CopyFromScreen(PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(Width, Height));
                    Graph.FillRectangle(new SolidBrush(Color.FromArgb(Opacity * 255 / 100, BackColor)), new Rectangle(new Point(0, 0), Size));
                    break;
                default:
                    break;
            }
        }
    }

    #endregion
}