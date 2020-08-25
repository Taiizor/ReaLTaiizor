#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonRadioButton

    [DefaultEvent("CheckedChanged")]
    public class DungeonRadioButton : Control
    {

        #region Enums

        public enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        #endregion
        #region Variables

        private bool _Checked;
        private Color _BorderColor = Color.FromArgb(182, 88, 55);
        private Color _CheckedBackColorA = Color.FromArgb(213, 85, 32);
        private Color _CheckedBackColorB = Color.FromArgb(224, 123, 82);
        private Color _CheckedColor = Color.FromArgb(255, 255, 255);
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        #endregion
        #region Properties

        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        public Color CheckedBackColorA
        {
            get { return _CheckedBackColorA; }
            set { _CheckedBackColorA = value; }
        }

        public Color CheckedBackColorB
        {
            get { return _CheckedBackColorB; }
            set { _CheckedBackColorB = value; }
        }

        public Color CheckedColor
        {
            get { return _CheckedColor; }
            set { _CheckedColor = value; }
        }

        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                InvalidateControls();
                CheckedChanged?.Invoke(this);
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 15;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_Checked)
                Checked = true;
            base.OnMouseDown(e);
            Focus();
        }

        #endregion

        public DungeonRadioButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 12);
            Width = 180;
            ForeColor = Color.FromArgb(76, 76, 95);
            Cursor = Cursors.Hand;
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
                return;

            foreach (Control _Control in Parent.Controls)
            {
                if (!object.ReferenceEquals(_Control, this) && _Control is DungeonRadioButton)
                    ((DungeonRadioButton)_Control).Checked = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var MyDrawer = e.Graphics;

            MyDrawer.Clear(BackColor);
            MyDrawer.SmoothingMode = SmoothingMode.AntiAlias;

            // Fill the body of the ellipse with a gradient
            LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(14, 14)), _CheckedBackColorA, _CheckedBackColorB, 90);
            MyDrawer.FillEllipse(LGB, new Rectangle(new Point(0, 0), new Size(14, 14)));

            GraphicsPath GP = new GraphicsPath();
            GP.AddEllipse(new Rectangle(0, 0, 14, 14));
            MyDrawer.SetClip(GP);
            MyDrawer.ResetClip();

            // Draw ellipse border
            MyDrawer.DrawEllipse(new Pen(_BorderColor), new Rectangle(new Point(0, 0), new Size(14, 14)));

            // Draw an ellipse inside the body
            if (_Checked)
            {
                SolidBrush EllipseColor = new SolidBrush(_CheckedColor);
                MyDrawer.FillEllipse(EllipseColor, new Rectangle(new Point(4, 4), new Size(6, 6)));
            }
            MyDrawer.DrawString(Text, Font, new SolidBrush(ForeColor), 16, 7, new StringFormat { LineAlignment = StringAlignment.Center });
            e.Dispose();
        }
    }

    #endregion
}