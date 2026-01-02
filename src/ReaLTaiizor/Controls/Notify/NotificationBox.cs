#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NotificationBox

    public class NotificationBox : Control
    {
        #region Variables

        private Point CloseCoordinates;
        private bool IsOverClose;
        private int _BorderCurve = 8;
        private GraphicsPath CreateRoundPath;
        private string NotificationText = null;
        private Size _ImageSize;

        #endregion

        #region Enums

        // Create a list of Notification Types
        public enum Type
        {
            @Notice,
            @Success,
            @Warning,
            @Error
        }

        #endregion

        #region Custom Properties

        // Create a NotificationType property and add the Type enum to it
        public Type NotificationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        // Boolean value to determine whether the control should use border radius
        public bool RoundCorners
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        // Boolean value to determine whether the control should draw the close button
        public bool ShowCloseButton
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        // Integer value to determine the curve level of the borders
        public int BorderCurve
        {
            get => _BorderCurve;
            set
            {
                _BorderCurve = value;
                Invalidate();
            }
        }

        // Image value to determine whether the control should draw an image before the header
        public Image Image
        {
            get;
            set
            {
                if (value == null)
                {
                    _ImageSize = Size.Empty;
                }
                else
                {
                    _ImageSize = value.Size;
                }

                field = value;
                Invalidate();
            }
        }
        // Size value - returns the image size
        protected Size ImageSize => _ImageSize;

        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighQuality;

        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

        public Color CloseForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Black;

        public Color ErrorBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Crimson;

        public Color ErrorForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        public Color ErrorBorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Crimson;

        public Color SuccessBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.SeaGreen;

        public Color SuccessForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        public Color SuccessBorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.SeaGreen;

        public Color WarningBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(255, 128, 0);

        public Color WarningForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        public Color WarningBorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(255, 128, 0);

        public Color NoticeBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Gray;

        public Color NoticeForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        public Color NoticeBorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Gray;

        public string ErrorTitleText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "ERROR";

        public string SuccessTitleText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "SUCCESS";

        public string WarningTitleText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "WARNING";

        public string NoticeTitleText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "NOTICE";

        #endregion

        #region EventArgs

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Decides the location of the drawn ellipse. If mouse is over the correct coordinates, "IsOverClose" boolean will be triggered to draw the ellipse
            if (e.X >= Width - 19 && e.X <= Width - 10 && e.Y > CloseCoordinates.Y && e.Y < CloseCoordinates.Y + 12 && ShowCloseButton)
            {
                IsOverClose = true;
                Cursor = Cursors.Hand;
            }
            else
            {
                IsOverClose = false;
                Cursor = Cursors.Default;
            }
            // Updates the control
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Disposes the control when the close button is clicked
            if (ShowCloseButton == true)
            {
                if (IsOverClose)
                {
                    Dispose();
                }
            }
        }

        #endregion

        internal GraphicsPath CreateRoundRect(Rectangle r, int curve)
        {
            // Draw a border radius
            try
            {
                CreateRoundPath = new(FillMode.Winding);
                CreateRoundPath.AddArc(r.X, r.Y, curve, curve, 180.0F, 90.0F);
                CreateRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270.0F, 90.0F);
                CreateRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0.0F, 90.0F);
                CreateRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90.0F, 90.0F);
                CreateRoundPath.CloseFigure();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Value must be either \'1\' or higher", "Invalid Integer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Return to the default border curve if the parameter is less than "1"
                _BorderCurve = 8;
                BorderCurve = 8;
            }
            return CreateRoundPath;
        }

        public NotificationBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Font = new("Tahoma", 9);
            MinimumSize = new(100, 40);
            RoundCorners = false;
            Size = new(130, 40);
            ShowCloseButton = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Declare Graphics to draw the control
            Graphics GFX = e.Graphics;
            // Declare Color to paint the control's Text, Background and Border
            Color ForeColor = new();
            Color BackgroundColor = new();
            Color BorderColor = new();
            // Determine the header Notification Type font
            Font TypeFont = new(Font.FontFamily, Font.Size, FontStyle.Bold);
            // Decalre a new rectangle to draw the control inside it
            Rectangle MainRectangle = new(0, 0, Width - 1, Height - 1);
            // Declare a GraphicsPath to create a border radius
            GraphicsPath CrvBorderPath = CreateRoundRect(MainRectangle, _BorderCurve);

            GFX.SmoothingMode = SmoothingType;
            GFX.TextRenderingHint = TextRenderingType;
            GFX.Clear(Parent.BackColor);

            switch (NotificationType)
            {
                case Type.Notice:
                    NotificationText = NoticeTitleText;
                    BackgroundColor = NoticeBackColor;
                    BorderColor = NoticeBorderColor;
                    ForeColor = NoticeForeColor;
                    break;
                case Type.Success:
                    NotificationText = SuccessTitleText;
                    BackgroundColor = SuccessBackColor;
                    BorderColor = SuccessBorderColor;
                    ForeColor = SuccessForeColor;
                    break;
                case Type.Warning:
                    NotificationText = WarningTitleText;
                    BackgroundColor = WarningBackColor;
                    BorderColor = WarningBorderColor;
                    ForeColor = WarningForeColor;
                    break;
                case Type.Error:
                    NotificationText = ErrorTitleText;
                    BackgroundColor = ErrorBackColor;
                    BorderColor = ErrorBorderColor;
                    ForeColor = ErrorForeColor;
                    break;
            }

            if (RoundCorners)
            {
                GFX.FillPath(new SolidBrush(BackgroundColor), CrvBorderPath);
                GFX.DrawPath(new(BorderColor), CrvBorderPath);
            }
            else
            {
                GFX.FillRectangle(new SolidBrush(BackgroundColor), MainRectangle);
                GFX.DrawRectangle(new(BorderColor), MainRectangle);
            }

            if (Image == null)
            {
                GFX.DrawString(NotificationText, TypeFont, new SolidBrush(ForeColor), new Point(10, 5));
                GFX.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(10, 21, Width - 17, Height - 5));
            }
            else
            {
                GFX.DrawImage(Image, 12, 4, 16, 16);
                GFX.DrawString(NotificationText, TypeFont, new SolidBrush(ForeColor), new Point(30, 5));
                GFX.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(10, 21, Width - 17, Height - 5));
            }

            CloseCoordinates = new(Width - 26, 4);

            if (ShowCloseButton)
            {
                GFX.DrawString("r", new Font("Marlett", 7, FontStyle.Regular), new SolidBrush(CloseForeColor), new Rectangle(Width - 20, 10, Width, Height), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near }); // Draw the close button
            }

            CrvBorderPath.Dispose();
        }
    }

    #endregion
}