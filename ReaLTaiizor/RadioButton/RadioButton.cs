#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region Radio Button

    [DefaultEvent("CheckedChanged")]
    public class RadioButton : Control
    {

        #region Variables

        private int X;
        private bool _Checked;

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
                InvalidateControls();
                CheckedChangedEvent?.Invoke(this);
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
                CheckedChangedEvent = (CheckedChangedEventHandler)System.Delegate.Combine(CheckedChangedEvent, value);
            }
            remove
            {
                CheckedChangedEvent = (CheckedChangedEventHandler)System.Delegate.Remove(CheckedChangedEvent, value);
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_Checked)
                @Checked = true;
            Focus();
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            X = e.X;
            Invalidate();
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            int textSize = 0;
            textSize = (int)(CreateGraphics().MeasureString(Text, Font).Width);
            Width = 28 + textSize;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 17;
        }

        #endregion

        public RadioButton()
        {
            Width = 159;
            Height = 17;
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            ForeColor = Color.FromArgb(116, 125, 132);
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
                return;

            foreach (Control _Control in Parent.Controls)
            {
                if (_Control != this && _Control is RadioButton)
                    ((RadioButton)_Control).Checked = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingMode.HighQuality;

            G.FillEllipse(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(0, 0, 16, 16));

            if (_Checked)
                G.DrawString("a", new Font("Marlett", 15), new SolidBrush(Color.FromArgb(32, 34, 37)), new Point(-3, -2));

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(20, 0));
        }
    }

    #endregion
}