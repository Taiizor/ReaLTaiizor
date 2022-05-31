#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region Toggle Button

    [DefaultEvent("ToggledChanged")]
    public class ToggleButton : Control
    {
        #region Enums

        public enum _Type
        {
            CheckMark,
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
        private int _Width;
        private int _Height;

        #endregion
        #region Properties

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
            Size = new(76, 33);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Toggled = !Toggled;
            Focus();
        }

        #endregion

        public ToggleButton()
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
            _Width = Width - 1;
            _Height = Height - 1;

            GraphicsPath GP = default;
            GraphicsPath GP2 = new();
            Rectangle BaseRect = new(0, 0, _Width, _Height);
            Rectangle ThumbRect = new(_Width / 2, 0, 38, _Height);

            G.SmoothingMode = (SmoothingMode)2;
            G.PixelOffsetMode = (PixelOffsetMode)2;
            G.TextRenderingHint = (TextRenderingHint)5;
            G.Clear(BackColor);

            GP = RoundRectangle.RoundRect(BaseRect, 4);
            ThumbRect = new(4, 4, 36, _Height - 8);
            GP2 = RoundRectangle.RoundRect(ThumbRect, 4);
            G.FillPath(new SolidBrush(Color.FromArgb(66, 76, 85)), GP);
            G.FillPath(new SolidBrush(Color.FromArgb(32, 41, 50)), GP2);

            if (_Toggled)
            {
                GP = RoundRectangle.RoundRect(BaseRect, 4);
                ThumbRect = new((_Width / 2) - 2, 4, 36, _Height - 8);
                GP2 = RoundRectangle.RoundRect(ThumbRect, 4);
                G.FillPath(new SolidBrush(Color.FromArgb(32, 34, 37)), GP);
                G.FillPath(new SolidBrush(Color.FromArgb(32, 41, 50)), GP2);
            }

            // Draw string
            switch (ToggleType)
            {
                case _Type.CheckMark:
                    if (Toggled)
                    {
                        G.DrawString("ü", new Font("Wingdings", 18, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 19, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("r", new Font("Marlett", 14, FontStyle.Regular), Brushes.DimGray, Bar.X + 59, Bar.Y + 18, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    break;
                case _Type.OnOff:
                    if (Toggled)
                    {
                        G.DrawString("ON", new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 16, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("OFF", new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.DimGray, Bar.X + 57, Bar.Y + 16, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    break;
                case _Type.YesNo:
                    if (Toggled)
                    {
                        G.DrawString("YES", new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 19, Bar.Y + 16, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("NO", new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.DimGray, Bar.X + 56, Bar.Y + 16, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    break;
                case _Type.IO:
                    if (Toggled)
                    {
                        G.DrawString("I", new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 16, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("O", new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.DimGray, Bar.X + 57, Bar.Y + 16, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }

                    break;
            }
        }
    }

    #endregion
}