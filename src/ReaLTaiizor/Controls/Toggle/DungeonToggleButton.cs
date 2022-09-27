#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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
            add => ToggledChangedEvent = (ToggledChangedEventHandler)Delegate.Combine(ToggledChangedEvent, value);
            remove => ToggledChangedEvent = (ToggledChangedEventHandler)Delegate.Remove(ToggledChangedEvent, value);
        }

        private bool _Toggled;
        private _Type ToggleType;
        private Rectangle Bar;

        #endregion
        #region Properties

        public Color ToggledBackColorA { get; set; } = Color.FromArgb(253, 253, 253);

        public Color ToggledBackColorB { get; set; } = Color.FromArgb(240, 238, 237);

        public Color ToggledColorA { get; set; } = Color.FromArgb(231, 108, 58);

        public Color ToggledBorderColorA { get; set; } = Color.FromArgb(185, 89, 55);

        public Color ToggledColorB { get; set; } = Color.FromArgb(236, 113, 63);

        public Color ToggledBorderColorB { get; set; } = Color.FromArgb(185, 89, 55);

        public Color ToggledColorC { get; set; } = Color.FromArgb(208, 208, 208);

        public Color ToggledBorderColorC { get; set; } = Color.FromArgb(181, 181, 181);

        public Color ToggledColorD { get; set; } = Color.FromArgb(226, 226, 226);

        public Color ToggledBorderColorD { get; set; } = Color.FromArgb(181, 181, 181);

        public Color ToggledOnOffColorA { get; set; } = Color.WhiteSmoke;

        public Color ToggledOnOffColorB { get; set; } = Color.DimGray;

        public Color ToggledYesNoColorA { get; set; } = Color.WhiteSmoke;

        public Color ToggledYesNoColorB { get; set; } = Color.DimGray;

        public Color ToggledIOColorA { get; set; } = Color.WhiteSmoke;

        public Color ToggledIOColorB { get; set; } = Color.DimGray;

        public bool Toggled
        {
            get => _Toggled;
            set
            {
                _Toggled = value;
                Invalidate();
                ToggledChangedEvent?.Invoke();
            }
        }

        public _Type Type
        {
            get => ToggleType;
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
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.Clear(Parent.BackColor);

            int SwitchXLoc = 3;
            Rectangle ControlRectangle = new(0, 0, Width - 1, Height - 1);
            GraphicsPath ControlPath = RoundRectangle.RoundRect(ControlRectangle, 4);

            LinearGradientBrush BackgroundLGB;
            if (_Toggled)
            {
                SwitchXLoc = 37;
                BackgroundLGB = new(ControlRectangle, ToggledColorA, ToggledColorB, 90.0F);
            }
            else
            {
                SwitchXLoc = 0;
                BackgroundLGB = new(ControlRectangle, ToggledColorC, ToggledColorD, 90.0F);
            }

            // Fill inside background gradient
            G.FillPath(BackgroundLGB, ControlPath);

            // Draw string
            switch (ToggleType)
            {
                case _Type.OnOff:
                    if (Toggled)
                    {
                        G.DrawString("ON", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(ToggledOnOffColorA), Bar.X + 18, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("OFF", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(ToggledOnOffColorB), Bar.X + 59, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    break;
                case _Type.YesNo:
                    if (Toggled)
                    {
                        G.DrawString("YES", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(ToggledYesNoColorA), Bar.X + 18, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("NO", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(ToggledYesNoColorB), Bar.X + 59, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    break;
                case _Type.IO:
                    if (Toggled)
                    {
                        G.DrawString("I", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(ToggledIOColorA), Bar.X + 18, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("O", new Font("Segoe UI", 12, FontStyle.Regular), new SolidBrush(ToggledIOColorB), Bar.X + 59, (float)(Bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    break;
            }

            Rectangle SwitchRectangle = new(SwitchXLoc, 0, Width - 38, Height);
            GraphicsPath SwitchPath = RoundRectangle.RoundRect(SwitchRectangle, 4);
            LinearGradientBrush SwitchButtonLGB = new(SwitchRectangle, ToggledBackColorA, ToggledBackColorB, LinearGradientMode.Vertical);

            // Fill switch background gradient
            G.FillPath(SwitchButtonLGB, SwitchPath);

            // Draw borders
            if (_Toggled == true)
            {
                G.DrawPath(new(ToggledBorderColorA), SwitchPath);
                G.DrawPath(new(ToggledBorderColorB), ControlPath);
            }
            else
            {
                G.DrawPath(new(ToggledBorderColorC), SwitchPath);
                G.DrawPath(new(ToggledBorderColorD), ControlPath);
            }
        }
    }

    #endregion
}