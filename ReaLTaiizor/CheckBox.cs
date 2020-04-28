#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region CheckBox

    [DefaultEvent("CheckedChanged")]
    public class CheckBox : Control
    {

        #region Variables

        private int X;
        private bool _Checked = false;
        private GraphicsPath Shape;

        #endregion
        #region Properties

        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        public delegate void CheckedChangedEventHandler(object sender);
        private CheckedChangedEventHandler CheckedChangedEvent;

        public event CheckedChangedEventHandler CheckedChanged
        {
            add
            {
                CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Combine(CheckedChangedEvent, value);
            }
            remove
            {
                CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Remove(CheckedChangedEvent, value);
            }
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            X = e.Location.X;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _Checked = !_Checked;
            Focus();
            CheckedChangedEvent?.Invoke(this);
            base.OnMouseDown(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.Height = 16;

            Shape = new GraphicsPath();
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
            Width = 148;
            Height = 16;
            Font = new Font("Microsoft Sans Serif", 9);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.Clear(Parent.BackColor);

            if (_Checked)
            {
                G.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(0, 0, 16, 16));
                G.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(1, 1, 16 - 2, 16 - 2));
            }
            else
            {
                G.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(0, 0, 16, 16));
                G.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(1, 1, 16 - 2, 16 - 2));
            }

            if (Enabled == true)
            {
                if (_Checked)
                    G.DrawString("a", new Font("Marlett", 16), new SolidBrush(Color.FromArgb(32, 34, 37)), new Point(-5, -3));
            }
            else
            {
                if (_Checked)
                    G.DrawString("a", new Font("Marlett", 16), new SolidBrush(Color.Gray), new Point(-5, -3));
            }

            G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(116, 125, 132)), new Point(20, 0));
        }
    }
    #endregion
}