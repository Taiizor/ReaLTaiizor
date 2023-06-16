#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
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

        private readonly string B64Enabled;

        private readonly string B64Disabled;

        public event AloneCheckBox.CheckedChangedEventHandler CheckedChanged
        {
            [CompilerGenerated]
            add
            {
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneCheckBox.CheckedChangedEventHandler value2 = (AloneCheckBox.CheckedChangedEventHandler)Delegate.Combine(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneCheckBox.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
                }
                while (checkedChangedEventHandler != checkedChangedEventHandler2);
            }
            [CompilerGenerated]
            remove
            {
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
                AloneCheckBox.CheckedChangedEventHandler checkedChangedEventHandler2;
                do
                {
                    checkedChangedEventHandler2 = checkedChangedEventHandler;
                    AloneCheckBox.CheckedChangedEventHandler value2 = (AloneCheckBox.CheckedChangedEventHandler)Delegate.Remove(checkedChangedEventHandler2, value);
                    checkedChangedEventHandler = Interlocked.CompareExchange<AloneCheckBox.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
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
                CheckedChangedEvent?.Invoke(this, null);
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

        public AloneCheckBox()
        {
            B64Enabled = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA00lEQVQ4T6WTwQ2CMBSG30/07Ci6gY7gxZoIiYADuAIrsIDpQQ/cHMERZBOuXHimDSWALYL01EO/L//724JmLszk6S+BCOIExFsmL50sEH4kAZxVciYuJgnacD16Plpgg8tFtYMILntQdSXiZ3aXqa1UF/yUsoDw4wKglQaZZPa4RW3JEKzO4RjEbyJaN1BL8gvWgsMp3ADeq0lRJ2FimLZNYWpmFbudUJdolXTLyG2wTmDODUiccEfgSDIIfwmMxAMStS+XHPZn7l/z6Ifk+nSzBR8zi2d9JmVXSgAAAABJRU5ErkJggg==";
            B64Disabled = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA1UlEQVQ4T6WTzQ2CQBCF56EnLpaiXvUAJBRgB2oFtkALdEAJnoVEMIGzdEIFjNkFN4DLn+xpD/N9efMWQAsPFvL0lyBMUg8MiwzyZwuiJAuI6CyTMxezBC24EuSTBTp4xaaN6JWdqKQbge6udfB1pfbBjrMvEMZZAdCm3ilw7eO1KRmCxRyiOH0TsFUQs5KMwVLweKY7ALFKUZUTECD6qdquCxM7i9jNhLJEraQ5xZzrYJngO9crGYBbAm2SEfhHoCQGeeK+Ls1Ld+fuM0/+kPp+usWCD10idEOGa4QuAAAAAElFTkSuQmCC";
            DoubleBuffered = true;
            Enabled = true;
            ForeColor = AloneLibrary.ColorFromHex("#7C858E");
            BackColor = Color.White;
            Size = new(118, 18);
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
                    G.FillPath(solidBrush, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                    G.DrawPath(pen, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                    G.DrawString(Text, font, solidBrush2, new Point(25, 0));
                }
                bool @checked = Checked;
                if (@checked)
                {
                    using Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64Enabled)));
                    G.DrawImage(image, new Rectangle(3, 3, 11, 11));
                }
            }
            else
            {
                using (SolidBrush solidBrush3 = new(AloneLibrary.ColorFromHex("#F5F5F8")))
                {
                    using Pen pen2 = new(AloneLibrary.ColorFromHex("#E1E1E2"));
                    using SolidBrush solidBrush4 = new(AloneLibrary.ColorFromHex("#D0D3D7"));
                    using Font font2 = new("Segoe UI", 9f);
                    G.FillPath(solidBrush3, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                    G.DrawPath(pen2, AloneLibrary.RoundRect(new Rectangle(0, 0, 16, 16), 3, AloneLibrary.RoundingStyle.All));
                    G.DrawString(Text, font2, solidBrush4, new Point(25, 0));
                }
                bool checked2 = Checked;
                if (checked2)
                {
                    using Image image2 = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64Disabled)));
                    G.DrawImage(image2, new Rectangle(3, 3, 11, 11));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bool enabled = Enabled;
            if (enabled)
            {
                Checked = !Checked;
                //CheckedChangedEvent?.Invoke(this, e);
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