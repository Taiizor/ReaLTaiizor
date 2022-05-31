#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RadioButton

    [DefaultEvent("CheckedChanged")]
    public class RadioButton : Control
    {
        #region Variables

        private int X;
        private bool _Checked;
        private Color _CheckedColor = Color.FromArgb(32, 34, 37);
        private Color _CircleColor = Color.FromArgb(66, 76, 85);
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;

        #endregion

        #region Properties

        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                InvalidateControls();
                CheckedChangedEvent?.Invoke(this);
                Invalidate();
            }
        }

        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color CircleColor
        {
            get => _CircleColor;
            set
            {
                _CircleColor = value;
                Invalidate();
            }
        }

        public Color CheckedColor
        {
            get => _CheckedColor;
            set
            {
                _CheckedColor = value;
                Invalidate();
            }
        }

        #endregion

        #region EventArgs

        public delegate void CheckedChangedEventHandler(object sender);
        private CheckedChangedEventHandler CheckedChangedEvent;

        public event CheckedChangedEventHandler CheckedChanged
        {
            add => CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Combine(CheckedChangedEvent, value);
            remove => CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Remove(CheckedChangedEvent, value);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_Checked)
            {
                @Checked = true;
            }
            else
            {
                @Checked = false;
            }

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
            //Width = 20 + (int)CreateGraphics().MeasureString(Text, Font).Width;
            //Width = 20 + (int)TextRenderer.MeasureText(Text, Font).Width;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 17;
        }

        #endregion

        public RadioButton()
        {
            Size = new(120, 17);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            ForeColor = Color.FromArgb(116, 125, 132);
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
            {
                return;
            }

            foreach (Control _Control in Parent.Controls)
            {
                if (_Control != this && _Control is RadioButton button)
                {
                    button.Checked = false;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingType;

            G.FillEllipse(new SolidBrush(CircleColor), new Rectangle(0, 0, 16, 16));

            if (_Checked)
            {
                G.DrawString("a", new Font("Marlett", 15), new SolidBrush(CheckedColor), new Point(-3, -2));
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(20, -3));
        }
    }

    #endregion
}