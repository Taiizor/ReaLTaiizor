#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

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

        private MouseState State;

        private bool _EnabledCalc;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private ClickEventHandler ClickEvent;

        public new event ClickEventHandler Click
        {
            [CompilerGenerated]
            add
            {
                ClickEventHandler clickEventHandler = ClickEvent;
                ClickEventHandler clickEventHandler2;
                do
                {
                    clickEventHandler2 = clickEventHandler;
                    ClickEventHandler value2 = (ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
                    clickEventHandler = Interlocked.CompareExchange(ref ClickEvent, value2, clickEventHandler2);
                }
                while (clickEventHandler != clickEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                ClickEventHandler clickEventHandler = ClickEvent;
                ClickEventHandler clickEventHandler2;
                do
                {
                    clickEventHandler2 = clickEventHandler;
                    ClickEventHandler value2 = (ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
                    clickEventHandler = Interlocked.CompareExchange(ref ClickEvent, value2, clickEventHandler2);
                }
                while (clickEventHandler != clickEventHandler2);
            }
        }

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
                Enabled = value;
                Invalidate();
            }
        }

        public AloneButton()
        {
            DoubleBuffered = true;
            Enabled = true;
            Cursor = Cursors.Hand;
            Size = new(120, 40);
            Font = new("Segoe UI", 9f);
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
                MouseState state = State;
                if (state != MouseState.Over)
                {
                    if (state != MouseState.Down)
                    {
                        using SolidBrush solidBrush = new(AloneLibrary.ColorFromHex("#F6F6F6"));
                        G.FillPath(solidBrush, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    }
                    else
                    {
                        using SolidBrush solidBrush2 = new(AloneLibrary.ColorFromHex("#F0F0F0"));
                        G.FillPath(solidBrush2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    }
                }
                else
                {
                    using SolidBrush solidBrush3 = new(AloneLibrary.ColorFromHex("#FDFDFD"));
                    G.FillPath(solidBrush3, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                }
                using (Pen pen = new(AloneLibrary.ColorFromHex("#C3C3C3")))
                {
                    G.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    AloneLibrary.CenterString(G, Text, Font, ForeColor, AloneLibrary.FullRectangle(base.Size, false));
                }
                Cursor = Cursors.Hand;
            }
            else
            {
                using (SolidBrush solidBrush4 = new(AloneLibrary.ColorFromHex("#F3F4F7")))
                {
                    using Pen pen2 = new(AloneLibrary.ColorFromHex("#DCDCDC"));
                    G.FillPath(solidBrush4, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    G.DrawPath(pen2, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 3, AloneLibrary.RoundingStyle.All));
                    AloneLibrary.CenterString(G, Text, Font, AloneLibrary.ColorFromHex("#D0D3D7"), AloneLibrary.FullRectangle(base.Size, false));
                }
                Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bool enabled = Enabled;
            if (enabled)
            {
                ClickEvent?.Invoke(this, e);
            }

            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
        }
    }

    #endregion
}