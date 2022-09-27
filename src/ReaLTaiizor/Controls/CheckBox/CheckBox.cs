#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CheckBox

    [DefaultEvent("CheckedChanged")]
    public class CheckBox : Control
    {
        #region Variables

        private int X;
        private bool _Enable = true;
        private bool _Checked = false;
        private GraphicsPath Shape;

        #endregion

        #region Properties

        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                CheckedChangedEvent?.Invoke(this);
                Invalidate();
            }
        }

        public bool Enable
        {
            get => _Enable;
            set
            {
                _Enable = value;
                Invalidate();
            }
        }

        public Color CheckedEnabledColor { get; set; } = Color.FromArgb(32, 34, 37);

        public Color CheckedDisabledColor { get; set; } = Color.Gray;

        public Color CheckedBorderColor { get; set; } = Color.FromArgb(66, 76, 85);

        public Color CheckedBackColor { get; set; } = Color.FromArgb(66, 76, 85);

        #endregion

        #region EventArgs

        public delegate void CheckedChangedEventHandler(object sender);
        private CheckedChangedEventHandler CheckedChangedEvent;

        public event CheckedChangedEventHandler CheckedChanged
        {
            add => CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Combine(CheckedChangedEvent, value);
            remove => CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Remove(CheckedChangedEvent, value);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            X = e.Location.X;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_Enable)
            {
                Checked = !Checked;
                Focus();
                base.OnMouseDown(e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Height = 16;

            Shape = new();
            Shape.AddArc(0, 0, 10, 10, 180, 90);
            Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            Shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            Shape.CloseAllFigures();
            Invalidate();
        }

        #endregion

        public CheckBox()
        {
            Width = 85;
            Height = 16;
            Font = new("Microsoft Sans Serif", 9);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            ForeColor = Color.FromArgb(116, 125, 132);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.Clear(BackColor);

            if (_Checked)
            {
                G.FillRectangle(new SolidBrush(CheckedBorderColor), new Rectangle(0, 0, 16, 16));
                G.FillRectangle(new SolidBrush(CheckedBackColor), new Rectangle(1, 1, 16 - 2, 16 - 2));
            }
            else
            {
                G.FillRectangle(new SolidBrush(CheckedBorderColor), new Rectangle(0, 0, 16, 16));
                G.FillRectangle(new SolidBrush(CheckedBackColor), new Rectangle(1, 1, 16 - 2, 16 - 2));
            }

            if (_Enable)
            {
                if (_Checked)
                {
                    G.DrawString("a", new Font("Marlett", 16), new SolidBrush(CheckedEnabledColor), new Point(-5, -3));
                }

                Cursor = Cursors.Hand;
            }
            else
            {
                if (_Checked)
                {
                    G.DrawString("a", new Font("Marlett", 16), new SolidBrush(CheckedDisabledColor), new Point(-5, -3));
                }

                Cursor = Cursors.Default;
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(20, 0));
        }
    }
    #endregion
}