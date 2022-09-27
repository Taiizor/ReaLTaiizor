#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region FoxUtil

    public static class FoxLibrary
    {
        public enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2
        }

        public enum RoundingStyle : byte
        {
            All = 0,
            Top = 1,
            Bottom = 2,
            Left = 3,
            Right = 4,
            TopRight = 5,
            BottomRight = 6
        }

        public static void CenterString(Graphics G, string T, Font F, Color C, Rectangle R)
        {
            SizeF TS = G.MeasureString(T, F);

            using SolidBrush B = new(C);
            G.DrawString(T, F, B, new Point((int)(R.X + (R.Width / 2) - (TS.Width / 2)), (int)(R.Y + (R.Height / 2) - (TS.Height / 2))));
        }

        public static Color ColorFromHex(string Hex)
        {
            return Color.FromArgb((int)long.Parse(string.Format("FFFFFFFFFF{0}", Hex.Substring(1)), System.Globalization.NumberStyles.HexNumber));
        }

        public static Rectangle FullRectangle(Size S, bool Subtract)
        {
            if (Subtract)
            {
                return new Rectangle(0, 0, S.Width - 1, S.Height - 1);
            }
            else
            {
                return new Rectangle(0, 0, S.Width, S.Height);
            }
        }

        public static GraphicsPath RoundRect(Rectangle Rect, int Rounding, RoundingStyle Style = RoundingStyle.All)
        {

            GraphicsPath GP = new();
            int AW = Rounding * 2;

            GP.StartFigure();

            if (Rounding == 0)
            {
                GP.AddRectangle(Rect);
                GP.CloseAllFigures();
                return GP;
            }

            switch (Style)
            {
                case RoundingStyle.All:
                    GP.AddArc(new Rectangle(Rect.X, Rect.Y, AW, AW), -180, 90);
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                    GP.AddArc(new Rectangle(Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 90, 90);
                    break;
                case RoundingStyle.Top:
                    GP.AddArc(new Rectangle(Rect.X, Rect.Y, AW, AW), -180, 90);
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                    GP.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                    break;
                case RoundingStyle.Bottom:
                    GP.AddLine(new Point(Rect.X, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                    GP.AddArc(new Rectangle(Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 90, 90);
                    break;
                case RoundingStyle.Left:
                    GP.AddArc(new Rectangle(Rect.X, Rect.Y, AW, AW), -180, 90);
                    GP.AddLine(new Point(Rect.X + Rect.Width, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                    GP.AddArc(new Rectangle(Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 90, 90);
                    break;
                case RoundingStyle.Right:
                    GP.AddLine(new Point(Rect.X, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y));
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                    break;
                case RoundingStyle.TopRight:
                    GP.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                    GP.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height - 1), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                    GP.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                    break;
                case RoundingStyle.BottomRight:
                    GP.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                    GP.AddLine(new Point(Rect.X + Rect.Width - 1, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                    GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                    GP.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                    break;
            }

            GP.CloseAllFigures();

            return GP;
        }
    }

    namespace FoxBase
    {
        public abstract class CheckControlBox : Control
        {
            public event CheckedChangedEventHandler CheckedChanged;
            public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

            public FoxLibrary.MouseState State;
            private bool IsEnabled;

            private bool IsChecked;
            public new bool Enabled
            {
                get => EnabledCalc;
                set
                {
                    IsEnabled = value;

                    if (Enabled)
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
                get => IsEnabled;
                set
                {
                    Enabled = value;
                    Invalidate();
                }
            }

            public bool Checked
            {
                get => IsChecked;
                set
                {
                    IsChecked = value;
                    CheckedChanged?.Invoke(this, null);
                    Invalidate();
                }
            }

            public CheckControlBox()
            {
                Enabled = true;
                DoubleBuffered = true;
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                State = FoxLibrary.MouseState.None;
                Invalidate();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();

                if (Enabled)
                {
                    Checked = !Checked;
                    //CheckedChanged?.Invoke(this, e);
                }

            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                State = FoxLibrary.MouseState.Down;
                Invalidate();
            }

        }

        public abstract class CheckControlEdit : Control
        {

            public event CheckedChangedEventHandler CheckedChanged;
            public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

            public FoxLibrary.MouseState State;
            private bool IsEnabled;

            private bool IsChecked;
            public new bool Enabled
            {
                get => EnabledCalc;
                set
                {
                    IsEnabled = value;

                    if (Enabled)
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
                get => IsEnabled;
                set
                {
                    Enabled = value;
                    Invalidate();
                }
            }

            public bool Checked
            {
                get => IsChecked;
                set
                {
                    IsChecked = value;
                    CheckedChanged?.Invoke(this, null);
                    Invalidate();
                }
            }

            public CheckControlEdit()
            {
                Enabled = true;
                DoubleBuffered = true;
                Size = new(115, 23);
                ForeColor = FoxLibrary.ColorFromHex("#424E5A");
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                State = FoxLibrary.MouseState.None;
                Invalidate();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();

                if (Enabled)
                {
                    Checked = !Checked;
                    //CheckedChanged?.Invoke(this, e);
                }

            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                State = FoxLibrary.MouseState.Down;
                Invalidate();
            }

        }

        public abstract class FoxBaseRadioButton : Control
        {

            public event CheckedChangedEventHandler CheckedChanged;
            public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

            public FoxLibrary.MouseState State;
            private bool IsEnabled;

            private bool IsChecked;
            public new bool Enabled
            {
                get => EnabledCalc;
                set
                {
                    IsEnabled = value;

                    if (Enabled)
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
                get => IsEnabled;
                set
                {
                    Enabled = value;
                    Invalidate();
                }
            }

            public bool Checked
            {
                get => IsChecked;
                set
                {
                    IsChecked = value;
                    Invalidate();
                }
            }

            public FoxBaseRadioButton()
            {
                Enabled = true;
                DoubleBuffered = true;
                Size = new(130, 23);
                ForeColor = FoxLibrary.ColorFromHex("#424E5A");
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                State = FoxLibrary.MouseState.None;
                Invalidate();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();


                if (Enabled)
                {

                    if (!Checked)
                    {
                        foreach (Control C in Parent.Controls)
                        {
                            if (C is FoxBaseRadioButton button)
                            {
                                button.Checked = false;
                            }
                        }

                    }

                    Checked = true;
                    CheckedChanged?.Invoke(this, e);
                }

            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                State = FoxLibrary.MouseState.Down;
                Invalidate();
            }

        }

        public abstract class ButtonFoxBase : Control
        {
            public new event ClickEventHandler Click;
            public new delegate void ClickEventHandler(object sender, EventArgs e);

            public FoxLibrary.MouseState State;

            private bool IsEnabled;
            public new bool Enabled
            {
                get => EnabledCalc;
                set
                {
                    IsEnabled = value;

                    if (Enabled)
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
                get => IsEnabled;
                set
                {
                    Enabled = value;
                    Invalidate();
                }
            }

            public ButtonFoxBase()
            {
                DoubleBuffered = true;
                Enabled = true;
                Size = new(120, 40);
                ForeColor = FoxLibrary.ColorFromHex("#424E5A");
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                State = FoxLibrary.MouseState.None;
                Invalidate();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();

                if (Enabled)
                {
                    Click?.Invoke(this, e);
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                State = FoxLibrary.MouseState.Down;
                Invalidate();
            }

        }

        public abstract class NotifyFoxBase : Control
        {
            public new event ClickEventHandler Click;
            public new delegate void ClickEventHandler(object sender, EventArgs e);

            public FoxLibrary.MouseState State;

            private bool IsEnabled;
            public new bool Enabled
            {
                get => EnabledCalc;
                set
                {
                    IsEnabled = value;

                    if (!Enabled)
                    {
                        Cursor = Cursors.Default;
                    }

                    Invalidate();
                }
            }

            [DisplayName("Enabled")]
            public bool EnabledCalc
            {
                get => IsEnabled;
                set
                {
                    Enabled = value;
                    Invalidate();
                }
            }

            public NotifyFoxBase()
            {
                DoubleBuffered = true;
                Enabled = true;
                Cursor = Cursors.Default;
                Size = new(120, 40);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                State = FoxLibrary.MouseState.None;
                Invalidate();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();

                if (Enabled)
                {
                    Click?.Invoke(this, e);
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                State = FoxLibrary.MouseState.Down;
                Invalidate();
            }

        }

        public abstract class LinkFoxBase : Control
        {
            public new event ClickEventHandler Click;
            public new delegate void ClickEventHandler(object sender, EventArgs e);

            public FoxLibrary.MouseState State;

            private bool IsEnabled;
            public new bool Enabled
            {
                get => EnabledCalc;
                set
                {
                    IsEnabled = value;

                    if (Enabled)
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
                get => IsEnabled;
                set
                {
                    Enabled = value;
                    Invalidate();
                }
            }

            public LinkFoxBase()
            {
                DoubleBuffered = true;
                Enabled = true;
                Size = new(85, 19);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                State = FoxLibrary.MouseState.None;
                Invalidate();
            }

            protected override void OnMouseUp(MouseEventArgs e)
            {
                base.OnMouseUp(e);
                State = FoxLibrary.MouseState.Over;
                Invalidate();

                if (Enabled)
                {
                    Click?.Invoke(this, e);
                }
            }

            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                State = FoxLibrary.MouseState.Down;
                Invalidate();
            }

        }
    }

    #endregion
}