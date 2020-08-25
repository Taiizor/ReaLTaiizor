#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonToggleButton

    [DefaultEvent("ToggledChanged")]
    public class DungeonToggleButton : Control
    {

        #region Enums

        public enum _Type
        {
            OnOff,
            YesNo,
            IO
        }

        #endregion
        #region Variables

        public delegate void ToggledChangedEventHandler();
        private ToggledChangedEventHandler ToggledChangedEvent;

        public event ToggledChangedEventHandler ToggledChanged
        {
            add
            {
                ToggledChangedEvent = (ToggledChangedEventHandler)Delegate.Combine(ToggledChangedEvent, value);
            }
            remove
            {
                ToggledChangedEvent = (ToggledChangedEventHandler)Delegate.Remove(ToggledChangedEvent, value);
            }
        }

        private bool _Toggled;
        private _Type ToggleType;
        private Rectangle Bar;
        private Size cHandle = new Size(15, 20);

        private Color _ToggledBackColorA = Color.FromArgb(253, 253, 253);
        private Color _ToggledBackColorB = Color.FromArgb(240, 238, 237);
        private Color _ToggledColorA = Color.FromArgb(231, 108, 58);
        private Color _ToggledBorderColorA = Color.FromArgb(185, 89, 55);
        private Color _ToggledColorB = Color.FromArgb(236, 113, 63);
        private Color _ToggledBorderColorB = Color.FromArgb(185, 89, 55);
        private Color _ToggledColorC = Color.FromArgb(208, 208, 208);
        private Color _ToggledBorderColorC = Color.FromArgb(181, 181, 181);
        private Color _ToggledColorD = Color.FromArgb(226, 226, 226);
        private Color _ToggledBorderColorD = Color.FromArgb(181, 181, 181);

        private Color _ToggledOnOffColorA = Color.WhiteSmoke;
        private Color _ToggledOnOffColorB = Color.DimGray;
        private Color _ToggledYesNoColorA = Color.WhiteSmoke;
        private Color _ToggledYesNoColorB = Color.DimGray;
        private Color _ToggledIOColorA = Color.WhiteSmoke;
        private Color _ToggledIOColorB = Color.DimGray;

        #endregion
        #region Properties

        public Color ToggledBackColorA
        {
            get { return _ToggledBackColorA; }
            set { _ToggledBackColorA = value; }
        }

        public Color ToggledBackColorB
        {
            get { return _ToggledBackColorB; }
            set { _ToggledBackColorB = value; }
        }

        public Color ToggledColorA
        {
            get { return _ToggledColorA; }
            set { _ToggledColorA = value; }
        }

        public Color ToggledBorderColorA
        {
            get { return _ToggledBorderColorA; }
            set { _ToggledBorderColorA = value; }
        }

        public Color ToggledColorB
        {
            get { return _ToggledColorB; }
            set { _ToggledColorB = value; }
        }

        public Color ToggledBorderColorB
        {
            get { return _ToggledBorderColorB; }
            set { _ToggledBorderColorB = value; }
        }

        public Color ToggledColorC
        {
            get { return _ToggledColorC; }
            set { _ToggledColorC = value; }
        }

        public Color ToggledBorderColorC
        {
            get { return _ToggledBorderColorC; }
            set { _ToggledBorderColorC = value; }
        }

        public Color ToggledColorD
        {
            get { return _ToggledColorD; }
            set { _ToggledColorD = value; }
        }

        public Color ToggledBorderColorD
        {
            get { return _ToggledBorderColorD; }
            set { _ToggledBorderColorD = value; }
        }

        public Color ToggledOnOffColorA
        {
            get { return _ToggledOnOffColorA; }
            set { _ToggledOnOffColorA = value; }
        }

        public Color ToggledOnOffColorB
        {
            get { return _ToggledOnOffColorB; }
            set { _ToggledOnOffColorB = value; }
        }

        public Color ToggledYesNoColorA
        {
            get { return _ToggledYesNoColorA; }
            set { _ToggledYesNoColorA = value; }
        }

        public Color ToggledYesNoColorB
        {
            get { return _ToggledYesNoColorB; }
            set { _ToggledYesNoColorB = value; }
        }

        public Color ToggledIOColorA
        {
            get { return _ToggledIOColorA; }
            set { _ToggledIOColorA = value; }
        }

        public Color ToggledIOColorB
        {
            get { return _ToggledIOColorB; }
            set { _ToggledIOColorB = value; }
        }

        public bool Toggled
        {
            get
            {
                return _Toggled;
            }
            set
            {
                _Toggled = value;
                Invalidate();
                ToggledChangedEvent?.Invoke();
            }
        }

        public _Type Type
        {
            get
            {
                return ToggleType;
            }
            set
            {
                ToggleType = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Width = 79;
            Height = 27;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Toggled = !Toggled;
            Focus();
        }

        #endregion

        public DungeonToggleButton()
        {
            SetStyle((ControlStyles)(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint), true);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.Clear(Parent.BackColor);

            int SwitchXLoc = 3;
            Rectangle ControlRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath ControlPath = RoundRectangle.RoundRect(ControlRectangle, 4);

            LinearGradientBrush BackgroundLGB = default(LinearGradientBrush);
            if (_Toggled)
            {
                SwitchXLoc = 37;
                BackgroundLGB = new LinearGradientBrush(ControlRectangle, _ToggledColorA, _ToggledColorB, 90.0F);
            }
            else
            {
                SwitchXLoc = 0;
                BackgroundLGB = new LinearGradientBrush(ControlRectangle, _ToggledColorC, _ToggledColorD, 90.0F);
            }

            // Fill inside background gradient
            G.FillPath(BackgroundLGB, ControlPath);

            // Draw string
            switch (ToggleType)
            {
                case _Type.OnOff:
                    if (Toggled)
                        G.DrawString("ON", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(_ToggledOnOffColorA), Bar.X + 18, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    else
                        G.DrawString("OFF", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(_ToggledOnOffColorB), Bar.X + 59, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    break;
                case _Type.YesNo:
                    if (Toggled)
                        G.DrawString("YES", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(_ToggledYesNoColorA), Bar.X + 18, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    else
                        G.DrawString("NO", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(_ToggledYesNoColorB), Bar.X + 59, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    break;
                case _Type.IO:
                    if (Toggled)
                        G.DrawString("I", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(_ToggledIOColorA), Bar.X + 18, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    else
                        G.DrawString("O", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(_ToggledIOColorB), Bar.X + 59, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    break;
            }

            Rectangle SwitchRectangle = new Rectangle(SwitchXLoc, 0, Width - 38, Height);
            GraphicsPath SwitchPath = RoundRectangle.RoundRect(SwitchRectangle, 4);
            LinearGradientBrush SwitchButtonLGB = new LinearGradientBrush(SwitchRectangle, _ToggledBackColorA, _ToggledBackColorB, LinearGradientMode.Vertical);

            // Fill switch background gradient
            G.FillPath(SwitchButtonLGB, SwitchPath);

            // Draw borders
            if (_Toggled == true)
            {
                G.DrawPath(new Pen(_ToggledBorderColorA), SwitchPath);
                G.DrawPath(new Pen(_ToggledBorderColorB), ControlPath);
            }
            else
            {
                G.DrawPath(new Pen(_ToggledBorderColorC), SwitchPath);
                G.DrawPath(new Pen(_ToggledBorderColorD), ControlPath);
            }
        }
    }

    #endregion
}