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
            get => navBarStyle;
            set
            {
                if (navBarStyle != value)
                {
                    navBarStyle = value;

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
            get => itemColor;
            set
            {
                itemColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the title")]
        public Color TitleColor
        {
            get => titleColor;
            set
            {
                titleColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the title")]
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The left navigation item")]
        public NavigationItem LeftItem
        {
            get => leftItem;
            set
            {
                leftItem = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The right navigation item")]
        public NavigationItem RightItem
        {
            get => rightItem;
            set
            {
                rightItem = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The navigation bar title")]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text of the left item if set to CustomText")]
        public string LeftCustomText
        {
            get => leftCustomText;
            set
            {
                leftCustomText = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text of the right item if set to CustomText")]
        public string RightCustomText
        {
            get => rightCustomText;
            set
            {
                rightCustomText = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The image of the left item if set to CustomImage")]
        public Image LeftCustomImage
        {
            get => leftCustomImage;
            set
            {
                leftCustomImage = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The image of the right item if set to CustomImage")]
        public Image RightCustomImage
        {
            get => rightCustomImage;
            set
            {
                rightCustomImage = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The navigation bar interaction")]
        public bool Interaction
        {
            get => interaction;
            set
            {
                interaction = value;
                Invalidate();
            }
        }

        private InterpolationMode _InterpolationType = InterpolationMode.HighQualityBilinear;
        [Category("Parrot")]
        [Browsable(true)]
        public InterpolationMode InterpolationType
        {
            get => _InterpolationType;
            set
            {
                _InterpolationType = value;
                Invalidate();
            }
        }

        private CompositingQuality _CompositingQualityType = CompositingQuality.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get => _CompositingQualityType;
            set
            {
                _CompositingQualityType = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingType = TextRenderingHint.ClearTypeGridFit;
        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (interaction && cursor == null)
            {
                cursor = Cursor;
            }

            e.Graphics.InterpolationMode = InterpolationType;
            e.Graphics.CompositingQuality = CompositingQualityType;
            e.Graphics.TextRenderingHint = TextRenderingType;

            FontStyle style = FontStyle.Bold;

            if (navBarStyle == Style.iOS)
            {
                style = FontStyle.Regular;
            }

            e.Graphics.FillRectangle(new SolidBrush(backgroundColor), 0, 0, base.Width, base.Height);
            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };

            if (leftItem == NavigationItem.Back)
            {
                e.Graphics.DrawString("Back", Font, new SolidBrush(itemColor), base.ClientRectangle, stringFormat);
            }
            else if (leftItem == NavigationItem.Next)
            {
                e.Graphics.DrawString("Next", Font, new SolidBrush(itemColor), base.ClientRectangle, stringFormat);
            }
            else if (leftItem == NavigationItem.CustomText)
            {
                e.Graphics.DrawString(leftCustomText, Font, new SolidBrush(itemColor), base.ClientRectangle, stringFormat);
            }
            else if (leftItem == NavigationItem.Menu)
            {
                e.Graphics.DrawLine(new Pen(itemColor, 2f), base.Height / 5, base.Height / 4, base.Height / 5 * 4, base.Height / 4);
                e.Graphics.DrawLine(new Pen(itemColor, 2f), base.Height / 5, base.Height / 4 * 2, base.Height / 5 * 4, base.Height / 4 * 2);
                e.Graphics.DrawLine(new Pen(itemColor, 2f), base.Height / 5, base.Height / 4 * 3, base.Height / 5 * 4, base.Height / 4 * 3);
            }
            else if (leftItem == NavigationItem.CustomImage && leftCustomImage != null)
            {
                e.Graphics.DrawImage(new Bitmap(leftCustomImage, base.Height, base.Height), 0, 0);
            }

            stringFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(title, new Font(Font.FontFamily, Font.Size, style), new SolidBrush(titleColor), base.ClientRectangle, stringFormat);
            stringFormat.Alignment = StringAlignment.Far;

            if (rightItem == NavigationItem.Back)
            {
                e.Graphics.DrawString("Back", Font, new SolidBrush(itemColor), base.ClientRectangle, stringFormat);
                return;
            }

            if (rightItem == NavigationItem.Next)
            {
                e.Graphics.DrawString("Next", Font, new SolidBrush(itemColor), base.ClientRectangle, stringFormat);
                return;
            }

            if (rightItem == NavigationItem.CustomText)
            {
                e.Graphics.DrawString(rightCustomText, Font, new SolidBrush(itemColor), base.ClientRectangle, stringFormat);
                return;
            }

            if (rightItem == NavigationItem.Menu)
            {
                e.Graphics.DrawLine(new Pen(itemColor, 2f), base.Width - base.Height + (base.Height / 5), base.Height / 4, base.Width - base.Height + (base.Height / 5 * 4), base.Height / 4);
                e.Graphics.DrawLine(new Pen(itemColor, 2f), base.Width - base.Height + (base.Height / 5), base.Height / 4 * 2, base.Width - base.Height + (base.Height / 5 * 4), base.Height / 4 * 2);
                e.Graphics.DrawLine(new Pen(itemColor, 2f), base.Width - base.Height + (base.Height / 5), base.Height / 4 * 3, base.Width - base.Height + (base.Height / 5 * 4), base.Height / 4 * 3);
                return;
            }

            if (rightItem == NavigationItem.CustomImage && rightCustomImage != null)
            {
                e.Graphics.DrawImage(new Bitmap(rightCustomImage, base.Height, base.Height), base.Width - base.Height, 0);
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
            if (interaction)
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
            if (interaction)
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

        private Style navBarStyle;

        private Color itemColor = Color.White;

        private Color titleColor = Color.White;

        private Color backgroundColor = Color.White;

        private NavigationItem leftItem = NavigationItem.Back;

        private NavigationItem rightItem = NavigationItem.Next;

        private string title = "Navigation Bar";

        private string leftCustomText = "⫷⩶";

        private string rightCustomText = "⩶⫸";

        private Image leftCustomImage;

        private Image rightCustomImage;

        private Cursor cursor;

        private bool interaction = true;

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