#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region NightForm

    public class NightForm : ContainerControl
    {
        #region Fields

        private readonly int draggableHeight;
        private bool isBeingDragged;
        private Point mouseLocation;

        private Rectangle titleBarRect;
        private int titleBarStringLeft;

        #endregion

        #region Enum

        public enum Alignment
        {
            Left,
            Center
        }

        #endregion

        #region Custom Properties

        private bool _ControlMode;
        protected bool ControlMode
        {
            get => _ControlMode;
            set
            {
                _ControlMode = value;
                Invalidate();
            }
        }

        private Alignment _TextAlignment = Alignment.Left;
        [Browsable(true)]
        [Description("Indicates how the window title should be aligned.")]
        public Alignment TextAlignment
        {
            get => _TextAlignment;
            set
            {
                _TextAlignment = value;
                Invalidate();
            }
        }

        private bool _DrawIcon;
        [Browsable(true)]
        [Description("Determines whether the icon specified in the parent form should be drawn.")]
        public bool DrawIcon
        {
            get => _DrawIcon;
            set
            {
                _DrawIcon = value;
                Invalidate();
            }
        }

        private Color _TitleBarTextColor = Color.Gainsboro;
        [Browsable(true)]
        [Description("Sets the title bar title color.")]
        public Color TitleBarTextColor
        {
            get => _TitleBarTextColor;
            set
            {
                _TitleBarTextColor = value;
                Invalidate();
            }
        }

        private Color _HeadColor = ColorTranslator.FromHtml("#323A3D");
        [Browsable(true)]
        [Description("Sets the title bar color.")]
        public Color HeadColor
        {
            get => _HeadColor;
            set
            {
                _HeadColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Hidden Properties

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageLayout BackgroundImageLayout { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image BackgroundImage { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Returns true if the mouse is over the title bar icon.
        /// </summary>
        private static bool IsOverTitleBarIcon(MouseEventArgs e)
        {
            bool point = e.X > 8 && e.X < 26 && e.Y > 6 && e.Y < 22;
            return point;
        }

        #endregion

        #region EventArgs

        protected override void OnSizeChanged(EventArgs e)
        {
            if (!ControlMode)
            {
                titleBarRect = new(9, 2, Width, draggableHeight);
            }

            base.OnSizeChanged(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            if (titleBarRect.Contains(e.Location))
            {
                isBeingDragged = true;
                mouseLocation = e.Location;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // Close when double-clicking on the title bar icon
            if (_DrawIcon && IsOverTitleBarIcon(e))
            {
                Application.Exit();
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isBeingDragged)
            {
                Parent.Location = Point.Subtract(MousePosition, (Size)mouseLocation);
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (isBeingDragged)
            {
                isBeingDragged = false;
            }

            base.OnMouseUp(e);
        }

        #endregion

        public NightForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);

            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            Padding = new Padding(0, 31, 0, 0);
            MinimumSize = new(100, 42);

            BackColor = Color.FromArgb(40, 48, 51);

            Font = new("Segoe UI", 9);

            draggableHeight = 28;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            ParentForm.FormBorderStyle = FormBorderStyle.None;
            ParentForm.TransparencyKey = Color.Fuchsia; // IMPORTANT!
            ParentForm.BackColor = SystemColors.ControlDarkDark;
            ParentForm.MaximumSize = Screen.FromRectangle(ParentForm.Bounds).WorkingArea.Size;

            ParentForm.StartPosition = FormStartPosition.CenterScreen;
        }

        private void DrawTitleBar(Graphics g)
        {
            using SolidBrush brush = new(HeadColor);
            g.FillRectangle(brush, new Rectangle(0, 0, Width, 31));

            // ========== FOR TESTING PURPOSES ONLY! ==========
            // PLACEMENT BACKGROUNDS FOR THE CONTROLBOX BUTTONS
            // ================================================
            //          using (var brush = new(ColorTranslator.FromHtml("#FF0000")))
            //              g.FillRectangle(brush, new Rectangle(Width - 46, 0, 46, 31));
            //          
            //          using (var brush = new(ColorTranslator.FromHtml("#00FF00")))
            //              g.FillRectangle(brush, new Rectangle(Width - 92, 0, 46, 31));
            //          
            //          using (var brush = new(ColorTranslator.FromHtml("#0000FF")))
            //              g.FillRectangle(brush, new Rectangle(Width - 138, 0, 46, 31));
        }

        private void DrawTitleBarIcon(Graphics g)
        {
            if (_DrawIcon)
            {
                titleBarStringLeft = _TextAlignment == Alignment.Left ? 33 : 5;
                Rectangle iconRect = new(10, 7, 16, 16);

                g.DrawIcon(FindForm().Icon, iconRect);
            }
            else
            {
                titleBarStringLeft = 5;
            }
        }

        private void DrawTitleBarText(Graphics g)
        {
            Rectangle stringRect = new(titleBarStringLeft, 7, Width - 13, Height);

            switch (_TextAlignment)
            {
                case Alignment.Left:
                    using (SolidBrush stringColor = new(_TitleBarTextColor))
                    using (StringFormat sf = new()
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Near
                    })
                    {
                        g.DrawString(Text, Font, stringColor, stringRect, sf);
                    }
                    break;
                case Alignment.Center:
                    using (SolidBrush stringColor = new(_TitleBarTextColor))
                    using (StringFormat sf = new()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Near
                    })
                    {
                        g.DrawString(Text, Font, stringColor, stringRect, sf);
                    }
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackColor);

            DrawTitleBar(g);
            DrawTitleBarIcon(g);
            DrawTitleBarText(g);

            base.OnPaint(e);
        }
    }

    #endregion
}