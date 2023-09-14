#region Imports

using ReaLTaiizor.Util;
using System;
using System.Collections;
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
    #region AloneRadioButton

    [DefaultEvent("CheckedChanged")]
    public class AloneRadioButton : Control
    {
        public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private AloneRadioButton.CheckedChangedEventHandler CheckedChangedEvent;

        private bool _Checked;

        private bool _EnabledCalc;

        private Graphics G;

        public event AloneRadioButton.CheckedChangedEventHandler CheckedChanged
        {
            [CompilerGenerated]
            add
            {
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneRadioButton.CheckedChangedEventHandler value2 = (AloneRadioButton.CheckedChangedEventHandler)Delegate.Combine(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneRadioButton.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
                }
                while (checkedChangedEventHandler != checkedChangedEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneRadioButton.CheckedChangedEventHandler value2 = (AloneRadioButton.CheckedChangedEventHandler)Delegate.Remove(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneRadioButton.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
                }
                while (checkedChangedEventHandler != checkedChangedEventHandler2);
            }
        }

        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                Invalidate();
            }
        }

        public new bool Enabled
        {
            get => EnabledCalc;
            set
            {
                _EnabledCalc = value;
                bool enabled = Enabled;
                if (enabled)
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

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

        public AloneRadioButton()
        {
            DoubleBuffered = true;
            Enabled = true;
            Size = new(138, 18);
            ForeColor = AloneLibrary.ColorFromHex("#7C858E");
            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            G.Clear(BackColor);
            bool enabled = Enabled;
            if (enabled)
            {
                using (SolidBrush solidBrush = new(AloneLibrary.ColorFromHex("#F3F4F7")))
                {
                    using Pen pen = new(AloneLibrary.ColorFromHex("#D0D5D9"));
                    using SolidBrush solidBrush2 = new(ForeColor);
                    using Font font = new("Segoe UI", 9f);
                    G.FillEllipse(solidBrush, new Rectangle(0, 0, 16, 16));
                    G.DrawEllipse(pen, new Rectangle(0, 0, 16, 16));
                    G.DrawString(Text, font, solidBrush2, new Point(25, 0));
                }
                bool @checked = Checked;
                if (@checked)
                {
                    using SolidBrush solidBrush3 = new(AloneLibrary.ColorFromHex("#575C62"));
                    G.FillEllipse(solidBrush3, new Rectangle(4, 4, 8, 8));
                }
            }
            else
            {
                using (SolidBrush solidBrush4 = new(AloneLibrary.ColorFromHex("#F5F5F8")))
                {
                    using Pen pen2 = new(AloneLibrary.ColorFromHex("#E1E1E2"));
                    using SolidBrush solidBrush5 = new(AloneLibrary.ColorFromHex("#D0D3D7"));
                    using Font font2 = new("Segoe UI", 9f);
                    G.FillEllipse(solidBrush4, new Rectangle(0, 0, 16, 16));
                    G.DrawEllipse(pen2, new Rectangle(0, 0, 16, 16));
                    G.DrawString(Text, font2, solidBrush5, new Point(25, 0));
                }
                bool checked2 = Checked;
                if (checked2)
                {
                    using SolidBrush solidBrush6 = new(AloneLibrary.ColorFromHex("#BCC1C6"));
                    G.FillEllipse(solidBrush6, new Rectangle(4, 4, 8, 8));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bool enabled = Enabled;
            if (enabled)
            {
                try
                {
                    IEnumerator enumerator = base.Parent.Controls.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Control control = (Control)enumerator.Current;
                        bool flag = control is AloneRadioButton;
                        if (flag)
                        {
                            ((AloneRadioButton)control).Checked = false;
                        }
                    }
                }
                finally
                {

                }
                Checked = !Checked;
                CheckedChangedEvent?.Invoke(this, e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Size = new(base.Width, 18);
        }
    }

    #endregion
}