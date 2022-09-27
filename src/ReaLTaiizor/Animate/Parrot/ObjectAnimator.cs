#region Imports

using ReaLTaiizor.Enum.Parrot;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Animate.Parrot
{
    #region ObjectAnimator

    public class ObjectAnimator : Component
    {
        private void WaitAnimation(int milliseconds)
        {
            DateTime t = DateTime.Now.AddMilliseconds(milliseconds);

            while (DateTime.Now < t)
            {
                Application.DoEvents();
            }
        }

        public void FormAnimate(Form animationForm, FormAnimation animation, int animationSpeed)
        {
            if (animationSpeed < 1)
            {
                animationSpeed = 1;
            }

            if (animationSpeed > 10)
            {
                animationSpeed = 10;
            }

            if (animation == FormAnimation.FadeIn)
            {
                animationForm.Opacity = 0.0;

                while (animationForm.Opacity < 100.0)
                {
                    animationForm.Opacity = (0.01 * animationSpeed) + animationForm.Opacity;
                    WaitAnimation(50);
                }
            }

            if (animation == FormAnimation.FadeOut)
            {
                animationForm.Opacity = 1.0;

                while (animationForm.Opacity > 0.1)
                {
                    animationForm.Opacity -= 0.01 * animationSpeed;
                    WaitAnimation(50);
                }
            }
        }

        public void StandardAnimate(object animationObject, StandardAnimation animation, int animationSpeed)
        {
            Control control = animationObject as Control;

            if (animationSpeed < 1)
            {
                animationSpeed = 1;
            }

            if (animationSpeed > 10)
            {
                animationSpeed = 10;
            }

            if (animation == StandardAnimation.SlideRight)
            {
                int x = control.Location.X;

                control.Location = new Point(0 - control.Width, control.Location.Y);
                control.Refresh();

                while (control.Location.X < x / 2)
                {
                    control.Location = new Point(control.Location.X + (10 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.X < x / 4)
                {
                    control.Location = new Point(control.Location.X + (7 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.X < x / 8)
                {
                    control.Location = new Point(control.Location.X + (5 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.X < x)
                {
                    control.Location = new Point(control.Location.X + (2 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                control.Location = new Point(x, control.Location.Y);
            }

            if (animation == StandardAnimation.SlideLeft)
            {
                int x2 = control.Location.X;

                control.Location = new Point(control.Parent.Width + control.Width, control.Location.Y);
                control.Refresh();

                while (control.Location.X > x2 + (control.Width / 2))
                {
                    control.Location = new Point(control.Location.X - (10 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.X > x2 + (control.Width / 4))
                {
                    control.Location = new Point(control.Location.X - (7 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.X > x2 + (control.Width / 8))
                {
                    control.Location = new Point(control.Location.X - (5 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.X > x2)
                {
                    control.Location = new Point(control.Location.X - (2 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    WaitAnimation(40);
                }

                control.Location = new Point(x2, control.Location.Y);
            }

            if (animation == StandardAnimation.SlideDown)
            {
                int y = control.Location.Y;
                control.Location = new Point(control.Location.X, 0 - control.Height);
                control.Refresh();

                while (control.Location.Y < y / 2)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + (10 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.Y < y / 4)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + (7 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.Y < y / 8)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + (5 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.Y < y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + (2 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                control.Location = new Point(control.Location.X, y);
            }

            if (animation == StandardAnimation.SlideUp)
            {
                int y2 = control.Location.Y;

                control.Location = new Point(control.Location.X, control.Parent.Height + control.Height);
                control.Refresh();

                while (control.Location.Y > y2 + (control.Height / 2))
                {
                    control.Location = new Point(control.Location.X, control.Location.Y - (10 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.Y > y2 + (control.Height / 4))
                {
                    control.Location = new Point(control.Location.X, control.Location.Y - (7 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.Y > y2 + (control.Height / 8))
                {
                    control.Location = new Point(control.Location.X, control.Location.Y - (5 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                while (control.Location.Y > y2)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y - (2 * animationSpeed));
                    control.Refresh();
                    WaitAnimation(40);
                }

                control.Location = new Point(control.Location.X, y2);
            }

            if (animation == StandardAnimation.SlugRight)
            {
                int x3 = control.Location.X;
                int width = control.Width;

                control.Location = new Point(0 - control.Width, control.Location.Y);
                control.Refresh();

                while (control.Location.X < x3)
                {
                    control.Location = new Point(control.Location.X + (8 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    control.Refresh();

                    WaitAnimation(100 / animationSpeed);

                    while (control.Width > width / 2)
                    {
                        control.Width -= 3 * animationSpeed;
                        control.Refresh();
                        WaitAnimation(50 / animationSpeed);
                    }

                    while (control.Width < width)
                    {
                        control.Width += 3 * animationSpeed;
                        control.Refresh();
                        WaitAnimation(50 / animationSpeed);
                    }
                }

                control.Location = new Point(x3, control.Location.Y);
                control.Width = width;
                control.Refresh();
            }

            if (animation == StandardAnimation.SlugLeft)
            {
                int x4 = control.Location.X;
                int width2 = control.Width;

                control.Location = new Point(control.Parent.Width + control.Width, control.Location.Y);
                control.Refresh();

                while (control.Location.X > x4)
                {
                    control.Location = new Point(control.Location.X - (8 * animationSpeed), control.Location.Y);
                    control.Refresh();
                    control.Refresh();

                    WaitAnimation(100 / animationSpeed);

                    while (control.Width > width2 / 2)
                    {
                        control.Width -= 3 * animationSpeed;
                        control.Refresh();
                        WaitAnimation(50 / animationSpeed);
                    }

                    while (control.Width < width2)
                    {
                        control.Width += 3 * animationSpeed;
                        control.Refresh();
                        WaitAnimation(50 / animationSpeed);
                    }
                }

                control.Location = new Point(x4, control.Location.Y);
                control.Width = width2;
                control.Refresh();
            }

            if (animation == StandardAnimation.Hop)
            {
                int y3 = control.Location.Y;

                while (control.Location.Y > y3 - 20)
                {
                    while (control.Location.Y > y3 - 10)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y - 5);
                        control.Refresh();
                        WaitAnimation(100 / animationSpeed);
                    }

                    while (control.Location.Y > y3 - 18)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y - 4);
                        control.Refresh();
                        WaitAnimation(100 / animationSpeed);
                    }

                    while (control.Location.Y > y3 - 20)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y - 2);
                        control.Refresh();
                        WaitAnimation(100 / animationSpeed);
                    }
                }

                while (control.Location.Y < y3)
                {
                    while (control.Location.Y < y3 - 18)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y + 2);
                        control.Refresh();
                        WaitAnimation(100 / animationSpeed);
                    }

                    while (control.Location.Y < y3 - 20)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y + 6);
                        control.Refresh();
                        WaitAnimation(100 / animationSpeed);
                    }
                    control.Location = new Point(control.Location.X, y3);
                }
            }

            if (animation == StandardAnimation.ShootRight)
            {
                int x5 = control.Location.X;
                int y4 = control.Location.Y;
                Size size = control.Size;
                control.Size = new Size(control.Width, 6);
                control.Refresh();
                control.Location = new Point(0 - control.Width, control.Location.Y + (size.Height / 2) - 3);
                control.Refresh();

                while (control.Width - size.Width < x5 + size.Width)
                {
                    control.Size = new Size(control.Width + 50, control.Height);
                    control.Refresh();
                    WaitAnimation(50 / animationSpeed);
                }

                control.Size = new Size(x5 + (size.Width * 2), control.Height);
                control.Refresh();

                while (control.Location.X < x5)
                {
                    control.Location = new Point(control.Location.X + 25, control.Location.Y);
                    control.Size = new Size(control.Width - 25, control.Height);
                    control.Refresh();
                    WaitAnimation(50 / animationSpeed);
                }

                control.Size = size;
                control.Location = new Point(x5, y4);
                control.Refresh();
            }

            if (animation == StandardAnimation.ShootLeft)
            {
                int x6 = control.Location.X;
                int y5 = control.Location.Y;
                Size size2 = control.Size;
                control.Size = new Size(control.Width, 6);
                control.Refresh();
                control.Location = new Point(control.Parent.Width + control.Width, control.Location.Y + (size2.Height / 2) - 3);
                control.Refresh();

                while (control.Location.X > x6)
                {
                    control.Size = new Size(control.Width + 50, control.Height);
                    control.Location = new Point(control.Location.X - 50, control.Location.Y);
                    control.Refresh();
                    WaitAnimation(50 / animationSpeed);
                }

                control.Size = new Size(x6 + (size2.Width * 2), control.Height);
                control.Refresh();

                while (control.Location.X + control.Width > size2.Width)
                {
                    control.Size = new Size(control.Width - 25, control.Height);
                    control.Refresh();
                    WaitAnimation(50 / animationSpeed);
                }

                control.Size = size2;
                control.Location = new Point(x6, y5);
                control.Refresh();
            }
        }

        public void ColorAnimate(object animationObject, Color color, ColorAnimation animation, bool keepColor, int animationSpeed)
        {
            Control control = animationObject as Control;

            if (animationSpeed < 1)
            {
                animationSpeed = 1;
            }

            if (animationSpeed > 10)
            {
                animationSpeed = 10;
            }

            Graphics graphics = control.CreateGraphics();

            if (animationSpeed < 1)
            {
                animationSpeed = 1;
            }

            if (animationSpeed > 10)
            {
                animationSpeed = 10;
            }

            if (animation == ColorAnimation.FillEllipse)
            {
                int i = 1;
                int num = control.Width;
                if (control.Height > control.Width)
                {
                    num = control.Height;
                }

                num = num + 200 + (10 * animationSpeed);

                while (i < num)
                {
                    graphics.FillEllipse(new SolidBrush(color), (control.Width / 2) - (i / 2), (control.Height / 2) - (i / 2), i, i);
                    WaitAnimation(10);
                    i += 10 * animationSpeed;
                }
            }

            if (animation == ColorAnimation.FillSquare)
            {
                int j = 1;
                int num2 = control.Width;

                if (control.Height > control.Width)
                {
                    num2 = control.Height;
                }

                num2 += 200;

                while (j < num2)
                {
                    graphics.FillRectangle(new SolidBrush(color), (control.Width / 2) - (j / 2), (control.Height / 2) - (j / 2), j, j);
                    WaitAnimation(10);
                    j += 10 * animationSpeed;
                }
            }

            if (animation == ColorAnimation.SlideFill)
            {
                for (int k = 10; k < control.Width + (10 * animationSpeed); k += 10 * animationSpeed)
                {
                    graphics.FillRectangle(new SolidBrush(color), 0, 0, k, control.Height);
                    WaitAnimation(10);
                }
            }

            if (animation == ColorAnimation.StripeFill)
            {
                int l = 10;
                int num3 = (control.Height / 10) + 5;

                while (l < control.Width + (10 * animationSpeed))
                {
                    graphics.FillRectangle(new SolidBrush(color), 0, 0, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), control.Width - l, num3, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), 0, num3 * 2, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), control.Width - l, num3 * 3, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), 0, num3 * 4, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), control.Width - l, num3 * 5, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), 0, num3 * 6, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), control.Width - l, num3 * 7, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), 0, num3 * 8, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), control.Width - l, num3 * 9, l, num3);
                    graphics.FillRectangle(new SolidBrush(color), 0, num3 * 10, l, num3);

                    WaitAnimation(10);

                    l += 10 * animationSpeed;
                }
            }

            if (animation == ColorAnimation.SplitFill)
            {
                int m = 10;
                int num4 = control.Width + (10 * animationSpeed);

                while (m < num4)
                {
                    graphics.FillRectangle(new SolidBrush(color), 0, (control.Height / 2) - (m / 2), control.Width, m);
                    WaitAnimation(10);
                    m += 10 * animationSpeed;
                }
            }

            WaitAnimation(200);
            graphics.Dispose();

            if (keepColor)
            {
                control.BackColor = color;
            }

            control.Refresh();
        }
    }

    #endregion
}