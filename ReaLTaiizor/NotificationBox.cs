#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region  NotificationBox

    public class NotificationBox : Control
    {
        #region  Variables

        private Point CloseCoordinates;
        private bool IsOverClose;
        private int _BorderCurve = 8;
        private GraphicsPath CreateRoundPath;
        private string NotificationText = null;
        private Type _NotificationType;
        private bool _RoundedCorners;
        private bool _ShowCloseButton;
        private Image _Image;
        private Size _ImageSize;

        #endregion
        #region  Enums

        // Create a list of Notification Types
        public enum Type
        {
            @Notice,
            @Success,
            @Warning,
            @Error
        }

        #endregion
        #region  Custom Properties

        // Create a NotificationType property and add the Type enum to it
        public Type NotificationType
        {
            get
            {
                return _NotificationType;
            }
            set
            {
                _NotificationType = value;
                Invalidate();
            }
        }
        // Boolean value to determine whether the control should use border radius
        public bool RoundCorners
        {
            get
            {
                return _RoundedCorners;
            }
            set
            {
                _RoundedCorners = value;
                Invalidate();
            }
        }
        // Boolean value to determine whether the control should draw the close button
        public bool ShowCloseButton
        {
            get
            {
                return _ShowCloseButton;
            }
            set
            {
                _ShowCloseButton = value;
                Invalidate();
            }
        }
        // Integer value to determine the curve level of the borders
        public int BorderCurve
        {
            get
            {
                return _BorderCurve;
            }
            set
            {
                _BorderCurve = value;
                Invalidate();
            }
        }
        // Image value to determine whether the control should draw an image before the header
        public Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                if (value == null)
                    _ImageSize = Size.Empty;
                else
                    _ImageSize = value.Size;

                _Image = value;
                Invalidate();
            }
        }
        // Size value - returns the image size
        protected Size ImageSize
        {
            get
            {
                return _ImageSize;
            }
        }

        #endregion
        #region  EventArgs

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Decides the location of the drawn ellipse. If mouse is over the correct coordinates, "IsOverClose" boolean will be triggered to draw the ellipse
            if (e.X >= Width - 19 && e.X <= Width - 10 && e.Y > CloseCoordinates.Y && e.Y < CloseCoordinates.Y + 12)
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
            if (_ShowCloseButton == true)
            {
                if (IsOverClose)
                    Dispose();
            }
        }

        #endregion

        internal GraphicsPath CreateRoundRect(Rectangle r, int curve)
        {
            // Draw a border radius
            try
            {
                CreateRoundPath = new GraphicsPath(FillMode.Winding);
                CreateRoundPath.AddArc(r.X, r.Y, curve, curve, 180.0F, 90.0F);
                CreateRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270.0F, 90.0F);
                CreateRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0.0F, 90.0F);
                CreateRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90.0F, 90.0F);
                CreateRoundPath.CloseFigure();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Value must be either \'1\' or higher", "Invalid Integer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Return to the default border curve if the parameter is less than "1"
                _BorderCurve = 8;
                BorderCurve = 8;
            }
            return CreateRoundPath;
        }

        public NotificationBox()
        {
            SetStyle((ControlStyles)(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw), true);
            Font = new Font("Tahoma", 9);
            MinimumSize = new Size(100, 40);
            RoundCorners = false;
            ShowCloseButton = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Declare Graphics to draw the control
            Graphics GFX = e.Graphics;
            // Declare Color to paint the control's Text, Background and Border
            Color ForeColor = new Color();
            Color BackgroundColor = new Color();
            Color BorderColor = new Color();
            // Determine the header Notification Type font
            Font TypeFont = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
            // Decalre a new rectangle to draw the control inside it
            Rectangle MainRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            // Declare a GraphicsPath to create a border radius
            GraphicsPath CrvBorderPath = CreateRoundRect(MainRectangle, _BorderCurve);

            GFX.SmoothingMode = SmoothingMode.HighQuality;
            GFX.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            GFX.Clear(Parent.BackColor);

            switch (_NotificationType)
            {
                case Type.Notice:
                    BackgroundColor = Color.Gray;
                    BorderColor = Color.Gray;
                    ForeColor = Color.White;
                    break;
                case Type.Success:
                    BackgroundColor = Color.SeaGreen;
                    BorderColor = Color.SeaGreen;
                    ForeColor = Color.White;
                    break;
                case Type.Warning:
                    BackgroundColor = Color.FromArgb(255, 128, 0);
                    BorderColor = Color.FromArgb(255, 128, 0);
                    ForeColor = Color.White;
                    break;
                case Type.Error:
                    BackgroundColor = Color.Crimson;
                    BorderColor = Color.Crimson;
                    ForeColor = Color.White;
                    break;
            }

            if (_RoundedCorners == true)
            {
                GFX.FillPath(new SolidBrush(BackgroundColor), CrvBorderPath);
                GFX.DrawPath(new Pen(BorderColor), CrvBorderPath);
            }
            else
            {
                GFX.FillRectangle(new SolidBrush(BackgroundColor), MainRectangle);
                GFX.DrawRectangle(new Pen(BorderColor), MainRectangle);
            }

            switch (_NotificationType)
            {
                case Type.Notice:
                    NotificationText = "NOTICE";
                    break;
                case Type.Success:
                    NotificationText = "SUCCESS";
                    break;
                case Type.Warning:
                    NotificationText = "WARNING";
                    break;
                case Type.Error:
                    NotificationText = "ERROR";
                    break;
            }

            if (Image == null)
            {
                GFX.DrawString(NotificationText, TypeFont, new SolidBrush(ForeColor), new Point(10, 5));
                GFX.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(10, 21, Width - 17, Height - 5));
            }
            else
            {
                GFX.DrawImage(_Image, 12, 4, 16, 16);
                GFX.DrawString(NotificationText, TypeFont, new SolidBrush(ForeColor), new Point(30, 5));
                GFX.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(10, 21, Width - 17, Height - 5));
            }

            CloseCoordinates = new Point(Width - 26, 4);

            if (_ShowCloseButton == true)
            {
                // Draw the close button
                GFX.DrawString("r", new Font("Marlett", 7, FontStyle.Regular), new SolidBrush(Color.Black), new Rectangle(Width - 20, 10, Width, Height), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
            }

            CrvBorderPath.Dispose();
        }
    }

    #endregion
}