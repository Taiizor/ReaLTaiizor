#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ToggleEdit

    [DefaultEvent("ToggledChanged")]
    public class ToggleEdit : Control
    {

        #region Designer

        public class PillStyle
        {
            public bool Left;
            public bool Right;
        }

        public static GraphicsPath Pill(Rectangle Rectangle, PillStyle PillStyle)
        {
            GraphicsPath functionReturnValue = new();

            if (PillStyle.Left)
            {
                functionReturnValue.AddArc(new Rectangle(Rectangle.X, Rectangle.Y, Rectangle.Height, Rectangle.Height), -270, 180);
            }
            else
            {
                functionReturnValue.AddLine(Rectangle.X, Rectangle.Y + Rectangle.Height, Rectangle.X, Rectangle.Y);
            }

            if (PillStyle.Right)
            {
                functionReturnValue.AddArc(new Rectangle(Rectangle.X + Rectangle.Width - Rectangle.Height, Rectangle.Y, Rectangle.Height, Rectangle.Height), -90, 180);
            }
            else
            {
                functionReturnValue.AddLine(Rectangle.X + Rectangle.Width, Rectangle.Y, Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);
            }

            functionReturnValue.CloseAllFigures();
            return functionReturnValue;
        }

        public object Pill(int X, int Y, int Width, int Height, PillStyle PillStyle)
        {
            return Pill(new Rectangle(X, Y, Width, Height), PillStyle);
        }

        #endregion
        #region Enums

        public enum _Type
        {
            YesNo,
            OnOff,
            IO
        }

        #endregion
        #region Variables

        private readonly Timer AnimationTimer = new() { Interval = 1 };
        private int ToggleLocation = 0;
        public event ToggledChangedEventHandler ToggledChanged;
        public delegate void ToggledChangedEventHandler();
        private bool _Toggled;
        private _Type ToggleType;
        private Rectangle Bar;
        private Size cHandle = new(15, 20);

        #endregion
        #region Properties

        public bool Toggled
        {
            get => _Toggled;
            set
            {
                _Toggled = value;
                Invalidate();

                ToggledChanged?.Invoke();
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
            Width = 41;
            Height = 23;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Toggled = !Toggled;
        }

        #endregion

        public ToggleEdit()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            AnimationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            Cursor = Cursors.Hand;
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            //  Create a slide animation when toggled on/off
            if (_Toggled == true)
            {
                if (ToggleLocation < 100)
                {
                    ToggleLocation += 10;
                    Invalidate(false);
                }
            }
            else if (ToggleLocation > 0)
            {
                ToggleLocation -= 10;
                Invalidate(false);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.Clear(Parent.BackColor);
            checked
            {
                Point point = new(0, (int)Math.Round(unchecked((Height / 2.0) - (cHandle.Height / 2.0))));
                Point arg_A8_0 = point;
                Point point2 = new(0, (int)Math.Round(unchecked((Height / 2.0) + (cHandle.Height / 2.0))));
                LinearGradientBrush Gradient = new(arg_A8_0, point2, Color.FromArgb(250, 250, 250), Color.FromArgb(240, 240, 240));
                Bar = new(8, 10, Width - 21, Height - 21);

                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.FillPath(Gradient, (GraphicsPath)Pill(0, (int)Math.Round(unchecked((Height / 2.0) - (cHandle.Height / 2.0))), Width - 1, cHandle.Height - 5, new ToggleEdit.PillStyle
                {
                    Left = true,
                    Right = true
                }));
                G.DrawPath(new(Color.FromArgb(177, 177, 176)), (GraphicsPath)Pill(0, (int)Math.Round(unchecked((Height / 2.0) - (cHandle.Height / 2.0))), Width - 1, cHandle.Height - 5, new ToggleEdit.PillStyle
                {
                    Left = true,
                    Right = true
                }));
                Gradient.Dispose();
                switch (ToggleType)
                {
                    case ToggleEdit._Type.YesNo:
                        {
                            bool toggled = Toggled;
                            if (toggled)
                            {
                                G.DrawString("Yes", new Font("Segoe UI", 7f, FontStyle.Regular), Brushes.Gray, Bar.X + 7, Bar.Y, new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
                            }
                            else
                            {
                                G.DrawString("No", new Font("Segoe UI", 7f, FontStyle.Regular), Brushes.Gray, Bar.X + 18, Bar.Y, new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
                            }
                            break;
                        }
                    case ToggleEdit._Type.OnOff:
                        {
                            bool toggled = Toggled;
                            if (toggled)
                            {
                                G.DrawString("On", new Font("Segoe UI", 7f, FontStyle.Regular), Brushes.Gray, Bar.X + 7, Bar.Y, new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
                            }
                            else
                            {
                                G.DrawString("Off", new Font("Segoe UI", 7f, FontStyle.Regular), Brushes.Gray, Bar.X + 18, Bar.Y, new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
                            }
                            break;
                        }
                    case ToggleEdit._Type.IO:
                        {
                            bool toggled = Toggled;
                            if (toggled)
                            {
                                G.DrawString("I", new Font("Segoe UI", 7f, FontStyle.Regular), Brushes.Gray, Bar.X + 7, Bar.Y, new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
                            }
                            else
                            {
                                G.DrawString("O", new Font("Segoe UI", 7f, FontStyle.Regular), Brushes.Gray, Bar.X + 18, Bar.Y, new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                });
                            }
                            break;
                        }
                }
                G.FillEllipse(new SolidBrush(Color.FromArgb(249, 249, 249)), Bar.X + (int)Math.Round(unchecked(Bar.Width * (ToggleLocation / 80.0))) - (int)Math.Round(cHandle.Width / 2.0), Bar.Y + (int)Math.Round(Bar.Height / 2.0) - (int)Math.Round(unchecked((cHandle.Height / 2.0) - 1.0)), cHandle.Width, cHandle.Height - 5);
                G.DrawEllipse(new(Color.FromArgb(177, 177, 176)), Bar.X + (int)Math.Round(unchecked((Bar.Width * (ToggleLocation / 80.0)) - checked((int)Math.Round(cHandle.Width / 2.0)))), Bar.Y + (int)Math.Round(Bar.Height / 2.0) - (int)Math.Round(unchecked((cHandle.Height / 2.0) - 1.0)), cHandle.Width, cHandle.Height - 5);
            }
        }
    }
    #endregion
}