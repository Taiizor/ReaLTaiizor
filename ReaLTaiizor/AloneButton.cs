#region Imports

using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

#endregion

namespace ReaLTaiizor
{
    #region AloneButton

    public class AloneButton : Control
    {
        public enum MouseState : byte
        {
            None,
            Over,
            Down
        }

        public delegate void ClickEventHandler(object sender, EventArgs e);

        private Graphics G;

        private AloneButton.MouseState State;

        private bool _EnabledCalc;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private AloneButton.ClickEventHandler ClickEvent;

        public new event AloneButton.ClickEventHandler Click
        {
            [CompilerGenerated]
            add
            {
                AloneButton.ClickEventHandler clickEventHandler = this.ClickEvent;
                AloneButton.ClickEventHandler clickEventHandler2;
                do
                {
                    clickEventHandler2 = clickEventHandler;
                    AloneButton.ClickEventHandler value2 = (AloneButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
                    clickEventHandler = Interlocked.CompareExchange<AloneButton.ClickEventHandler>(ref this.ClickEvent, value2, clickEventHandler2);
                }
                while (clickEventHandler != clickEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                AloneButton.ClickEventHandler clickEventHandler = this.ClickEvent;
                AloneButton.ClickEventHandler clickEventHandler2;
                do
                {
                    clickEventHandler2 = clickEventHandler;
                    AloneButton.ClickEventHandler value2 = (AloneButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
                    clickEventHandler = Interlocked.CompareExchange<AloneButton.ClickEventHandler>(ref this.ClickEvent, value2, clickEventHandler2);
                }
                while (clickEventHandler != clickEventHandler2);
            }
        }

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
                this.Enabled = value;
                base.Invalidate();
            }
        }

        public AloneButton()
        {
            this.DoubleBuffered = true;
            this.Enabled = true;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(120, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.G = e.Graphics;
            this.G.SmoothingMode = SmoothingMode.HighQuality;
            this.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            bool enabled = this.Enabled;
            if (enabled)
            {
                AloneButton.MouseState state = this.State;
                if (state != AloneButton.MouseState.Over)
                {
                    if (state != AloneButton.MouseState.Down)
                    {
                        using (SolidBrush solidBrush = new SolidBrush(AloneLibrary.ColorFromHex("#F6F6F6")))
                        {
                            this.G.FillPath(solidBrush, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                        }
                    }
                    else
                    {
                        using (SolidBrush solidBrush2 = new SolidBrush(AloneLibrary.ColorFromHex("#F0F0F0")))
                        {
                            this.G.FillPath(solidBrush2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                        }
                    }
                }
                else
                {
                    using (SolidBrush solidBrush3 = new SolidBrush(AloneLibrary.ColorFromHex("#FDFDFD")))
                    {
                        this.G.FillPath(solidBrush3, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    }
                }
                using (Font font = new Font("Segoe UI", 9f))
                {
                    using (Pen pen = new Pen(AloneLibrary.ColorFromHex("#C3C3C3")))
                    {
                        this.G.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                        AloneLibrary.CenterString(this.G, this.Text, font, AloneLibrary.ColorFromHex("#7C858E"), AloneLibrary.FullRectangle(base.Size, false));
                    }
                }
                Cursor = Cursors.Hand;
            }
            else
            {
                using (SolidBrush solidBrush4 = new SolidBrush(AloneLibrary.ColorFromHex("#F3F4F7")))
                {
                    using (Pen pen2 = new Pen(AloneLibrary.ColorFromHex("#DCDCDC")))
                    {
                        using (Font font2 = new Font("Segoe UI", 9f))
                        {
                            this.G.FillPath(solidBrush4, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                            this.G.DrawPath(pen2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                            AloneLibrary.CenterString(this.G, this.Text, font2, AloneLibrary.ColorFromHex("#D0D3D7"), AloneLibrary.FullRectangle(base.Size, false));
                        }
                    }
                }
                Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.State = AloneButton.MouseState.Over;
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.State = AloneButton.MouseState.None;
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bool enabled = this.Enabled;
            if (enabled)
            {
                this.ClickEvent?.Invoke(this, e);
            }
            this.State = AloneButton.MouseState.Over;
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.State = AloneButton.MouseState.Down;
            base.Invalidate();
        }
    }

    #endregion
}