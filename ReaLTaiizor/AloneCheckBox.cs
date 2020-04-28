#region Imports

using System;
using System.IO;
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
    #region AloneCheckBox

    [DefaultEvent("CheckedChanged")]
    public class AloneCheckBox : Control
    {
        public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private AloneCheckBox.CheckedChangedEventHandler CheckedChangedEvent;

        private bool _Checked;

        private bool _EnabledCalc;

        private Graphics G;

        private string B64Enabled;

        private string B64Disabled;

        public event AloneCheckBox.CheckedChangedEventHandler CheckedChanged
        {
            [CompilerGenerated]
            add
            {
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler = this.CheckedChangedEvent;
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneCheckBox.CheckedChangedEventHandler value2 = (AloneCheckBox.CheckedChangedEventHandler)Delegate.Combine(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneCheckBox.CheckedChangedEventHandler>(ref this.CheckedChangedEvent, value2, checkedChangedEventHandler2);
                }
                while (checkedChangedEventHandler != checkedChangedEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler = this.CheckedChangedEvent;
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneCheckBox.CheckedChangedEventHandler value2 = (AloneCheckBox.CheckedChangedEventHandler)Delegate.Remove(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneCheckBox.CheckedChangedEventHandler>(ref this.CheckedChangedEvent, value2, checkedChangedEventHandler2);
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
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;
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

        public AloneCheckBox()
        {
            this.B64Enabled = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA00lEQVQ4T6WTwQ2CMBSG30/07Ci6gY7gxZoIiYADuAIrsIDpQQ/cHMERZBOuXHimDSWALYL01EO/L//724JmLszk6S+BCOIExFsmL50sEH4kAZxVciYuJgnacD16Plpgg8tFtYMILntQdSXiZ3aXqa1UF/yUsoDw4wKglQaZZPa4RW3JEKzO4RjEbyJaN1BL8gvWgsMp3ADeq0lRJ2FimLZNYWpmFbudUJdolXTLyG2wTmDODUiccEfgSDIIfwmMxAMStS+XHPZn7l/z6Ifk+nSzBR8zi2d9JmVXSgAAAABJRU5ErkJggg==";
            this.B64Disabled = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA1UlEQVQ4T6WTzQ2CQBCF56EnLpaiXvUAJBRgB2oFtkALdEAJnoVEMIGzdEIFjNkFN4DLn+xpD/N9efMWQAsPFvL0lyBMUg8MiwzyZwuiJAuI6CyTMxezBC24EuSTBTp4xaaN6JWdqKQbge6udfB1pfbBjrMvEMZZAdCm3ilw7eO1KRmCxRyiOH0TsFUQs5KMwVLweKY7ALFKUZUTECD6qdquCxM7i9jNhLJEraQ5xZzrYJngO9crGYBbAm2SEfhHoCQGeeK+Ls1Ld+fuM0/+kPp+usWCD10idEOGa4QuAAAAAElFTkSuQmCC";
            this.DoubleBuffered = true;
            this.Enabled = true;
            this.Size = new Size(118, 18);
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
                                this.G.FillPath(solidBrush, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                                this.G.DrawPath(pen, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                                this.G.DrawString(this.Text, font, solidBrush2, new Point(25, 0));
                            }
                        }
                    }
                }
                bool @checked = this.Checked;
                if (@checked)
                {
                    using (Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(this.B64Enabled))))
                    {
                        this.G.DrawImage(image, new Rectangle(3, 3, 11, 11));
                    }
                }
            }
            else
            {
                using (SolidBrush solidBrush3 = new SolidBrush(AloneLibrary.ColorFromHex("#F5F5F8")))
                {
                    using (Pen pen2 = new Pen(AloneLibrary.ColorFromHex("#E1E1E2")))
                    {
                        using (SolidBrush solidBrush4 = new SolidBrush(AloneLibrary.ColorFromHex("#D0D3D7")))
                        {
                            using (Font font2 = new Font("Segoe UI", 9f))
                            {
                                this.G.FillPath(solidBrush3, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                                this.G.DrawPath(pen2, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                                this.G.DrawString(this.Text, font2, solidBrush4, new Point(25, 0));
                            }
                        }
                    }
                }
                bool checked2 = this.Checked;
                if (checked2)
                {
                    using (Image image2 = Image.FromStream(new MemoryStream(Convert.FromBase64String(this.B64Disabled))))
                    {
                        this.G.DrawImage(image2, new Rectangle(3, 3, 11, 11));
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
                this.Checked = !this.Checked;
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEvent = this.CheckedChangedEvent;
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