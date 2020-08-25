#region Imports

using System;
using System.Drawing;
using System.Threading;
using ReaLTaiizor.Utils;
using System.Diagnostics;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

#endregion

namespace ReaLTaiizor.Controls
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
                AloneButton.ClickEventHandler clickEventHandler = ClickEvent;
                AloneButton.ClickEventHandler clickEventHandler2;
                do
                {
                    clickEventHandler2 = clickEventHandler;
                    AloneButton.ClickEventHandler value2 = (AloneButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
                    clickEventHandler = Interlocked.CompareExchange<AloneButton.ClickEventHandler>(ref ClickEvent, value2, clickEventHandler2);
                }
                while (clickEventHandler != clickEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                AloneButton.ClickEventHandler clickEventHandler = ClickEvent;
                AloneButton.ClickEventHandler clickEventHandler2;
                do
                {
                    clickEventHandler2 = clickEventHandler;
                    AloneButton.ClickEventHandler value2 = (AloneButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
                    clickEventHandler = Interlocked.CompareExchange<AloneButton.ClickEventHandler>(ref ClickEvent, value2, clickEventHandler2);
                }
                while (clickEventHandler != clickEventHandler2);
            }
        }

        public new bool Enabled
        {
            get
            {
                return EnabledCalc;
            }
            set
            {
                _EnabledCalc = value;
                base.Invalidate();
            }
        }

        [DisplayName("Enabled")]
        public bool EnabledCalc
        {
            get
            {
                return _EnabledCalc;
            }
            set
            {
                Enabled = value;
                base.Invalidate();
            }
        }

        public AloneButton()
        {
            DoubleBuffered = true;
            Enabled = true;
            Cursor = Cursors.Hand;
            Size = new Size(120, 40);
            Font = new Font("Segoe UI", 9f);
            ForeColor = AloneLibrary.ColorFromHex("#7C858E");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            if (Enabled)
            {
                AloneButton.MouseState state = State;
                if (state != AloneButton.MouseState.Over)
                {
                    if (state != AloneButton.MouseState.Down)
                    {
                        using (SolidBrush solidBrush = new SolidBrush(AloneLibrary.ColorFromHex("#F6F6F6")))
                            G.FillPath(solidBrush, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    }
                    else
                    {
                        using (SolidBrush solidBrush2 = new SolidBrush(AloneLibrary.ColorFromHex("#F0F0F0")))
                            G.FillPath(solidBrush2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    }
                }
                else
                {
                    using (SolidBrush solidBrush3 = new SolidBrush(AloneLibrary.ColorFromHex("#FDFDFD")))
                        G.FillPath(solidBrush3, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                }
                using (Pen pen = new Pen(AloneLibrary.ColorFromHex("#C3C3C3")))
                {
                    G.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    AloneLibrary.CenterString(G, Text, Font, ForeColor, AloneLibrary.FullRectangle(base.Size, false));
                }
                Cursor = Cursors.Hand;
            }
            else
            {
                using (SolidBrush solidBrush4 = new SolidBrush(AloneLibrary.ColorFromHex("#F3F4F7")))
                {
                    using (Pen pen2 = new Pen(AloneLibrary.ColorFromHex("#DCDCDC")))
                    {
                        G.FillPath(solidBrush4, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                        G.DrawPath(pen2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                        AloneLibrary.CenterString(G, Text, Font, AloneLibrary.ColorFromHex("#D0D3D7"), AloneLibrary.FullRectangle(base.Size, false));
                    }
                }
                Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = AloneButton.MouseState.Over;
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = AloneButton.MouseState.None;
            base.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bool enabled = Enabled;
            if (enabled)
                ClickEvent?.Invoke(this, e);
            State = AloneButton.MouseState.Over;
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = AloneButton.MouseState.Down;
            base.Invalidate();
        }
    }

    #endregion
}