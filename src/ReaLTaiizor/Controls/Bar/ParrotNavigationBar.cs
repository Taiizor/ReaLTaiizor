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
    #region ParrotNavigationBar

    public class ParrotNavigationBar : Control
    {
        public ParrotNavigationBar()
        {
            Size = new Size(300, 40);
            NavBarStyle = Style.Android;
            Font = new Font("Arial", 12f);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The navigation bar style")]
        public Style NavBarStyle
        {
            get;
            set
            {
                if (field != value)
                {
                    field = value;

                    if (value == Style.iOS)
                    {
                        ItemColor = Color.FromArgb(0, 120, 255);
                        TitleColor = Color.Black;
                        BackgroundColor = Color.White;
                    }
                    else if (value == Style.Android)
                    {
                        ItemColor = Color.White;
                        TitleColor = Color.White;
                        BackgroundColor = Color.FromArgb(0, 150, 135);
                    }
                    else
                    {
                        ItemColor = Color.White;
                        TitleColor = Color.White;
                        BackgroundColor = Color.FromArgb(1, 119, 215);
                    }

                    Invalidate();
                }
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the items")]
        public Color ItemColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the title")]
        public Color TitleColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the title")]
        public Color BackgroundColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The left navigation item")]
        public NavigationItem LeftItem
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = NavigationItem.Back;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The right navigation item")]
        public NavigationItem RightItem
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = NavigationItem.Next;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The navigation bar title")]
        public string Title
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "Navigation Bar";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text of the left item if set to CustomText")]
        public string LeftCustomText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "⫷⩶";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text of the right item if set to CustomText")]
        public string RightCustomText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "⩶⫸";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The image of the left item if set to CustomImage")]
        public Image LeftCustomImage
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The image of the right item if set to CustomImage")]
        public Image RightCustomImage
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The navigation bar interaction")]
        public bool Interaction
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        [Category("Parrot")]
        [Browsable(true)]
        public InterpolationMode InterpolationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = InterpolationMode.HighQualityBilinear;

        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = CompositingQuality.HighQuality;

        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Interaction && cursor == null)
            {
                cursor = Cursor;
            }

            e.Graphics.InterpolationMode = InterpolationType;
            e.Graphics.CompositingQuality = CompositingQualityType;
            e.Graphics.TextRenderingHint = TextRenderingType;

            FontStyle style = FontStyle.Bold;

            if (NavBarStyle == Style.iOS)
            {
                style = FontStyle.Regular;
            }

            e.Graphics.FillRectangle(new SolidBrush(BackgroundColor), 0, 0, base.Width, base.Height);
            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };

            if (LeftItem == NavigationItem.Back)
            {
                e.Graphics.DrawString("Back", Font, new SolidBrush(ItemColor), base.ClientRectangle, stringFormat);
            }
            else if (LeftItem == NavigationItem.Next)
            {
                e.Graphics.DrawString("Next", Font, new SolidBrush(ItemColor), base.ClientRectangle, stringFormat);
            }
            else if (LeftItem == NavigationItem.CustomText)
            {
                e.Graphics.DrawString(LeftCustomText, Font, new SolidBrush(ItemColor), base.ClientRectangle, stringFormat);
            }
            else if (LeftItem == NavigationItem.Menu)
            {
                e.Graphics.DrawLine(new Pen(ItemColor, 2f), base.Height / 5, base.Height / 4, base.Height / 5 * 4, base.Height / 4);
                e.Graphics.DrawLine(new Pen(ItemColor, 2f), base.Height / 5, base.Height / 4 * 2, base.Height / 5 * 4, base.Height / 4 * 2);
                e.Graphics.DrawLine(new Pen(ItemColor, 2f), base.Height / 5, base.Height / 4 * 3, base.Height / 5 * 4, base.Height / 4 * 3);
            }
            else if (LeftItem == NavigationItem.CustomImage && LeftCustomImage != null)
            {
                e.Graphics.DrawImage(new Bitmap(LeftCustomImage, base.Height, base.Height), 0, 0);
            }

            stringFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(Title, new Font(Font.FontFamily, Font.Size, style), new SolidBrush(TitleColor), base.ClientRectangle, stringFormat);
            stringFormat.Alignment = StringAlignment.Far;

            if (RightItem == NavigationItem.Back)
            {
                e.Graphics.DrawString("Back", Font, new SolidBrush(ItemColor), base.ClientRectangle, stringFormat);
                return;
            }

            if (RightItem == NavigationItem.Next)
            {
                e.Graphics.DrawString("Next", Font, new SolidBrush(ItemColor), base.ClientRectangle, stringFormat);
                return;
            }

            if (RightItem == NavigationItem.CustomText)
            {
                e.Graphics.DrawString(RightCustomText, Font, new SolidBrush(ItemColor), base.ClientRectangle, stringFormat);
                return;
            }

            if (RightItem == NavigationItem.Menu)
            {
                e.Graphics.DrawLine(new Pen(ItemColor, 2f), base.Width - base.Height + (base.Height / 5), base.Height / 4, base.Width - base.Height + (base.Height / 5 * 4), base.Height / 4);
                e.Graphics.DrawLine(new Pen(ItemColor, 2f), base.Width - base.Height + (base.Height / 5), base.Height / 4 * 2, base.Width - base.Height + (base.Height / 5 * 4), base.Height / 4 * 2);
                e.Graphics.DrawLine(new Pen(ItemColor, 2f), base.Width - base.Height + (base.Height / 5), base.Height / 4 * 3, base.Width - base.Height + (base.Height / 5 * 4), base.Height / 4 * 3);
                return;
            }

            if (RightItem == NavigationItem.CustomImage && RightCustomImage != null)
            {
                e.Graphics.DrawImage(new Bitmap(RightCustomImage, base.Height, base.Height), base.Width - base.Height, 0);
            }
        }

        public event EventHandler LeftItemClick;

        protected virtual void OnLeftItemClick()
        {
            LeftItemClick?.Invoke(this, new EventArgs());
        }

        public event EventHandler RightItemClick;

        protected virtual void OnRightItemClick()
        {
            RightItemClick?.Invoke(this, new EventArgs());
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Interaction)
            {
                if (e.X < base.Width / 3)
                {
                    OnLeftItemClick();
                }
                if (e.X > base.Width / 3 * 2)
                {
                    OnRightItemClick();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Interaction)
            {
                if (e.X < base.Width / 3 || e.X > base.Width / 3 * 2)
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = cursor;
                }
            }
        }

        private Cursor cursor;

        public enum NavigationItem
        {
            Menu,
            None,
            Back,
            Next,
            CustomText,
            CustomImage
        }

        public enum Style
        {
            iOS,
            Android,
            Material
        }
    }

    #endregion
}