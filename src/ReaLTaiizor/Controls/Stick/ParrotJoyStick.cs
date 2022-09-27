#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotJoyStick

    public class ParrotJoyStick : Control
    {
        public ParrotJoyStick()
        {
            base.Size = new Size(122, 122);
            BackColor = Color.White;
            BackgroundImage = new Bitmap(base.Width, base.Height);
            ng = base.CreateGraphics();
        }

        [Category("Parrot")]
        [Browsable(true)]
        public Direction JoystickDirection { get; private set; } = Direction.MiddleCenter;

        [Category("Parrot")]
        [Browsable(true)]
        public Control MovableObject
        {
            get => movableObject;
            set => movableObject = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        public bool KeepOnScreen { get; set; } = true;

        [Category("Parrot")]
        [Browsable(true)]
        public int Sensitivity
        {
            get => sensitivity;
            set
            {
                sensitivity = value;

                if (sensitivity > 10)
                {
                    sensitivity = 10;
                }

                if (sensitivity < 1)
                {
                    sensitivity = 1;
                }
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        public override Image BackgroundImage
        {
            get => backgroundImage;
            set
            {
                backgroundImage = value;
                backgroundImage = new Bitmap(BackgroundImage, new Size(base.Width, base.Height));
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        public Color JoyStickColor
        {
            get => joyStickColor;
            set
            {
                joyStickColor = value;
                base.Invalidate();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            moveStick = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (moveStick)
            {
                int num;

                if (e.X - base.Width / 6 * 2 / 2 < 0)
                {
                    num = 0;
                }
                else if (e.X - base.Width / 6 * 2 / 2 + base.Width / 6 * 2 > base.Width)
                {
                    num = base.Width - base.Width / 6 * 2;
                }
                else
                {
                    num = e.X - base.Width / 6 * 2 / 2;
                }

                int num2;

                if (e.Y - base.Width / 6 * 2 / 2 < 0)
                {
                    num2 = 0;
                }
                else if (e.Y - base.Width / 6 * 2 / 2 + base.Height / 6 * 2 > base.Height)
                {
                    num2 = base.Height - base.Height / 6 * 2;
                }
                else
                {
                    num2 = e.Y - base.Height / 6 * 2 / 2;
                }

                int num3 = num + base.Width / 6;
                int num4 = num2 + base.Height / 6;

                if (num4 < base.Height / 3 * 3)
                {
                    if (num3 < base.Width / 3 * 3)
                    {
                        JoystickDirection = Direction.LowerRight;
                    }

                    if (num3 < base.Width / 3 * 2)
                    {
                        JoystickDirection = Direction.LowerCenter;
                    }

                    if (num3 < base.Width / 3)
                    {
                        JoystickDirection = Direction.LowerLeft;
                    }
                }

                if (num4 < base.Height / 3 * 2)
                {
                    if (num3 < base.Width / 3 * 3)
                    {
                        JoystickDirection = Direction.MiddleRight;
                    }

                    if (num3 < base.Width / 3 * 2)
                    {
                        JoystickDirection = Direction.MiddleCenter;
                    }

                    if (num3 < base.Width / 3)
                    {
                        JoystickDirection = Direction.MiddleLeft;
                    }
                }
                if (num4 < base.Height / 3)
                {
                    if (num3 < base.Width / 3 * 3)
                    {
                        JoystickDirection = Direction.UpperRight;
                    }

                    if (num3 < base.Width / 3 * 2)
                    {
                        JoystickDirection = Direction.UpperCenter;
                    }

                    if (num3 < base.Width / 3)
                    {
                        JoystickDirection = Direction.UpperLeft;
                    }
                }

                OnDirectionChanged();

                Image image = new Bitmap(base.Width, base.Height);
                Graphics graphics = Graphics.FromImage(image);

                graphics.SmoothingMode = SmoothingType;

                graphics.FillRectangle(new SolidBrush(BackColor), -1, -1, base.Width + 1, base.Height + 1);
                graphics.DrawImage(new Bitmap(BackgroundImage, new Size(base.Width, base.Height)), 0, 0);
                graphics.FillPie(new SolidBrush(joyStickColor), new Rectangle(num, num2, base.Width / 6 * 2, base.Height / 6 * 2), 0f, 360f);

                ng.DrawImage(image, 0, 0);

                MoveObject();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            moveStick = false;

            JoystickDirection = Direction.MiddleCenter;

            OnDirectionChanged();

            Image image = new Bitmap(base.Width, base.Height);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillRectangle(new SolidBrush(BackColor), -1, -1, base.Width + 1, base.Height + 1);
            graphics.DrawImage(new Bitmap(BackgroundImage, new Size(base.Width, base.Height)), 0, 0);
            graphics.FillPie(new SolidBrush(joyStickColor), new Rectangle(base.Width / 6 * 2, base.Height / 6 * 2, base.Width / 6 * 2, base.Height / 6 * 2), 0f, 360f);

            ng.DrawImage(image, 0, 0);

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Image image = new Bitmap(base.Width, base.Height);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillRectangle(new SolidBrush(BackColor), -1, -1, base.Width + 1, base.Height + 1);
            graphics.DrawImage(new Bitmap(BackgroundImage, new Size(base.Width, base.Height)), 0, 0);
            graphics.FillPie(new SolidBrush(joyStickColor), new Rectangle(base.Width / 6 * 2, base.Height / 6 * 2, base.Width / 6 * 2, base.Height / 6 * 2), 0f, 360f);

            graphics.DrawImage(image, 0, 0);

            e.Graphics.DrawImage(image, 0, 0);

            base.OnPaint(e);
        }

        public event EventHandler DirectionChanged;

        protected virtual void OnDirectionChanged()
        {
            DirectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void MoveObject()
        {
            if (movableObject != null)
            {
                if (KeepOnScreen)
                {
                    if (JoystickDirection == Direction.UpperLeft)
                    {
                        if (movableObject.Location.X - sensitivity > -1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X - sensitivity, movableObject.Location.Y);
                        }
                        else
                        {
                            movableObject.Location = new Point(0, movableObject.Location.Y);
                        }

                        if (movableObject.Location.Y - sensitivity > -1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y - sensitivity);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Location.X, 0);
                        }
                    }

                    if (JoystickDirection == Direction.UpperCenter)
                    {
                        if (movableObject.Location.Y - sensitivity > -1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y - sensitivity);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Location.X, 0);
                        }
                    }

                    if (JoystickDirection == Direction.UpperRight)
                    {
                        if (movableObject.Location.X + movableObject.Width + sensitivity < movableObject.Parent.Width - 1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X + sensitivity, movableObject.Location.Y);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Parent.Width - movableObject.Width, movableObject.Location.Y);
                        }

                        if (movableObject.Location.Y - sensitivity > -1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y - sensitivity);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Location.X, 0);
                        }
                    }

                    if (JoystickDirection == Direction.MiddleLeft)
                    {
                        if (movableObject.Location.X - sensitivity > -1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X - sensitivity, movableObject.Location.Y);
                        }
                        else
                        {
                            movableObject.Location = new Point(0, movableObject.Location.Y);
                        }
                    }

                    if (JoystickDirection == Direction.MiddleRight)
                    {
                        if (movableObject.Location.X + movableObject.Width + sensitivity < movableObject.Parent.Width - 1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X + sensitivity, movableObject.Location.Y);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Parent.Width - movableObject.Width, movableObject.Location.Y);
                        }
                    }

                    if (JoystickDirection == Direction.LowerLeft)
                    {
                        if (movableObject.Location.X - sensitivity > -1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X - sensitivity, movableObject.Location.Y);
                        }
                        else
                        {
                            movableObject.Location = new Point(0, movableObject.Location.Y);
                        }

                        if (movableObject.Location.Y + movableObject.Height + sensitivity < movableObject.Parent.Height - 1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y + sensitivity);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Parent.Height - movableObject.Height);
                        }
                    }

                    if (JoystickDirection == Direction.LowerCenter)
                    {
                        if (movableObject.Location.Y + movableObject.Height + sensitivity < movableObject.Parent.Height - 1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y + sensitivity);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Parent.Height - movableObject.Height);
                        }
                    }

                    if (JoystickDirection == Direction.LowerRight)
                    {
                        if (movableObject.Location.X + movableObject.Width + sensitivity < movableObject.Parent.Width - 1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X + sensitivity, movableObject.Location.Y);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Parent.Width - movableObject.Width, movableObject.Location.Y);
                        }

                        if (movableObject.Location.Y + movableObject.Height + sensitivity < movableObject.Parent.Height - 1)
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y + sensitivity);
                        }
                        else
                        {
                            movableObject.Location = new Point(movableObject.Location.X, movableObject.Parent.Height - movableObject.Height);
                        }
                    }
                }
                else
                {
                    if (JoystickDirection == Direction.UpperCenter)
                    {
                        movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y - sensitivity);
                    }

                    if (JoystickDirection == Direction.UpperLeft)
                    {
                        movableObject.Location = new Point(movableObject.Location.X - sensitivity, movableObject.Location.Y - sensitivity);
                    }

                    if (JoystickDirection == Direction.UpperRight)
                    {
                        movableObject.Location = new Point(movableObject.Location.X + sensitivity, movableObject.Location.Y - sensitivity);
                    }

                    if (JoystickDirection == Direction.MiddleLeft)
                    {
                        movableObject.Location = new Point(movableObject.Location.X - sensitivity, movableObject.Location.Y);
                    }

                    if (JoystickDirection == Direction.MiddleRight)
                    {
                        movableObject.Location = new Point(movableObject.Location.X + sensitivity, movableObject.Location.Y);
                    }

                    if (JoystickDirection == Direction.LowerLeft)
                    {
                        movableObject.Location = new Point(movableObject.Location.X - sensitivity, movableObject.Location.Y + sensitivity);
                    }

                    if (JoystickDirection == Direction.LowerCenter)
                    {
                        movableObject.Location = new Point(movableObject.Location.X, movableObject.Location.Y + sensitivity);
                    }

                    if (JoystickDirection == Direction.LowerRight)
                    {
                        movableObject.Location = new Point(movableObject.Location.X + sensitivity, movableObject.Location.Y + sensitivity);
                    }
                }

                movableObject.Refresh();
            }
        }

        private readonly Graphics ng;
        private Control movableObject;
        private int sensitivity = 3;

        private Image backgroundImage;

        private Color joyStickColor = Color.DodgerBlue;

        private bool moveStick;

        public enum Direction
        {
            UpperLeft,
            MiddleLeft,
            LowerLeft,
            UpperCenter,
            MiddleCenter,
            LowerCenter,
            UpperRight,
            MiddleRight,
            LowerRight
        }
    }

    #endregion
}