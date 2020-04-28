#region Imports

using System;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

#endregion

namespace ReaLTaiizor
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
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler = this.CheckedChangedEvent;
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneRadioButton.CheckedChangedEventHandler value2 = (AloneRadioButton.CheckedChangedEventHandler)Delegate.Combine(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneRadioButton.CheckedChangedEventHandler>(ref this.CheckedChangedEvent, value2, checkedChangedEventHandler2);
                }
                while (checkedChangedEventHandler != checkedChangedEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler = this.CheckedChangedEvent;
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneRadioButton.CheckedChangedEventHandler value2 = (AloneRadioButton.CheckedChangedEventHandler)Delegate.Remove(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneRadioButton.CheckedChangedEventHandler>(ref this.CheckedChangedEvent, value2, checkedChangedEventHandler2);
                }
                while (checkedChangedEventHandler != checkedChangedEventHandler2);
            }
        }

        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
                base.Invalidate();
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
                bool enabled = this.Enabled;
                if (enabled)
                {
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
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

        public AloneRadioButton()
        {
            this.DoubleBuffered = true;
            this.Enabled = true;
            this.Size = new Size(138, 18);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.G = e.Graphics;
            this.G.SmoothingMode = SmoothingMode.HighQuality;
            this.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            this.G.Clear(Color.White);
            bool enabled = this.Enabled;
            if (enabled)
            {
                using (SolidBrush solidBrush = new SolidBrush(AloneLibrary.ColorFromHex("#F3F4F7")))
                {
                    using (Pen pen = new Pen(AloneLibrary.ColorFromHex("#D0D5D9")))
                    {
                        using (SolidBrush solidBrush2 = new SolidBrush(AloneLibrary.ColorFromHex("#7C858E")))
                        {
                            using (Font font = new Font("Segoe UI", 9f))
                            {
                                this.G.FillEllipse(solidBrush, new Rectangle(0, 0, 16, 16));
                                this.G.DrawEllipse(pen, new Rectangle(0, 0, 16, 16));
                                this.G.DrawString(this.Text, font, solidBrush2, new Point(25, 0));
                            }
                        }
                    }
                }
                bool @checked = this.Checked;
                if (@checked)
                {
                    using (SolidBrush solidBrush3 = new SolidBrush(AloneLibrary.ColorFromHex("#575C62")))
                    {
                        this.G.FillEllipse(solidBrush3, new Rectangle(4, 4, 8, 8));
                    }
                }
            }
            else
            {
                using (SolidBrush solidBrush4 = new SolidBrush(AloneLibrary.ColorFromHex("#F5F5F8")))
                {
                    using (Pen pen2 = new Pen(AloneLibrary.ColorFromHex("#E1E1E2")))
                    {
                        using (SolidBrush solidBrush5 = new SolidBrush(AloneLibrary.ColorFromHex("#D0D3D7")))
                        {
                            using (Font font2 = new Font("Segoe UI", 9f))
                            {
                                this.G.FillEllipse(solidBrush4, new Rectangle(0, 0, 16, 16));
                                this.G.DrawEllipse(pen2, new Rectangle(0, 0, 16, 16));
                                this.G.DrawString(this.Text, font2, solidBrush5, new Point(25, 0));
                            }
                        }
                    }
                }
                bool checked2 = this.Checked;
                if (checked2)
                {
                    using (SolidBrush solidBrush6 = new SolidBrush(AloneLibrary.ColorFromHex("#BCC1C6")))
                    {
                        this.G.FillEllipse(solidBrush6, new Rectangle(4, 4, 8, 8));
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bool enabled = this.Enabled;
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
                this.Checked = !this.Checked;
                AloneRadioButton.CheckedChangedEventHandler checkedChangedEvent = this.CheckedChangedEvent;
                if (checkedChangedEvent != null)
                {
                    checkedChangedEvent(this, e);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Size = new Size(base.Width, 18);
        }
    }

    #endregion
}