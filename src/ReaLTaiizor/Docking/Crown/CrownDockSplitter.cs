#region Imports

using ReaLTaiizor.Enum.Crown;
using ReaLTaiizor.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region CrownDockSplitterDocking

    public class CrownDockSplitter
    {
        #region Field Region

        private readonly Control _parentControl;
        private readonly Control _control;

        private readonly SplitterType _splitterType;

        private int _minimum;
        private int _maximum;
        private CrownTranslucentForm _overlayForm;

        #endregion

        #region Property Region

        public Rectangle Bounds { get; set; }

        public Cursor ResizeCursor { get; private set; }

        #endregion

        #region Constructor Region

        public CrownDockSplitter(Control parentControl, Control control, SplitterType splitterType)
        {
            _parentControl = parentControl;
            _control = control;
            _splitterType = splitterType;

            switch (_splitterType)
            {
                case SplitterType.Left:
                case SplitterType.Right:
                    ResizeCursor = Cursors.SizeWE;
                    break;
                case SplitterType.Top:
                case SplitterType.Bottom:
                    ResizeCursor = Cursors.SizeNS;
                    break;
            }
        }

        #endregion

        #region Method Region

        public void ShowOverlay()
        {
            _overlayForm = new CrownTranslucentForm(Color.Black)
            {
                Visible = true
            };

            UpdateOverlay(new Point(0, 0));
        }

        public void HideOverlay()
        {
            _overlayForm.Visible = false;
        }

        public void UpdateOverlay(Point difference)
        {
            Rectangle bounds = new(Bounds.Location, Bounds.Size);

            switch (_splitterType)
            {
                case SplitterType.Left:
                    int leftX = Math.Max(bounds.Location.X - difference.X, _minimum);

                    if (_maximum != 0 && leftX > _maximum)
                    {
                        leftX = _maximum;
                    }

                    bounds.Location = new(leftX, bounds.Location.Y);
                    break;
                case SplitterType.Right:
                    int rightX = Math.Max(bounds.Location.X - difference.X, _minimum);

                    if (_maximum != 0 && rightX > _maximum)
                    {
                        rightX = _maximum;
                    }

                    bounds.Location = new(rightX, bounds.Location.Y);
                    break;
                case SplitterType.Top:
                    int topY = Math.Max(bounds.Location.Y - difference.Y, _minimum);

                    if (_maximum != 0 && topY > _maximum)
                    {
                        topY = _maximum;
                    }

                    bounds.Location = new(bounds.Location.X, topY);
                    break;
                case SplitterType.Bottom:
                    int bottomY = Math.Max(bounds.Location.Y - difference.Y, _minimum);

                    if (_maximum != 0 && bottomY > _maximum)
                    {
                        topY = _maximum;
                    }

                    bounds.Location = new(bounds.Location.X, bottomY);
                    break;
            }

            _overlayForm.Bounds = bounds;
        }

        public void Move(Point difference)
        {
            switch (_splitterType)
            {
                case SplitterType.Left:
                    _control.Width += difference.X;
                    break;
                case SplitterType.Right:
                    _control.Width -= difference.X;
                    break;
                case SplitterType.Top:
                    _control.Height += difference.Y;
                    break;
                case SplitterType.Bottom:
                    _control.Height -= difference.Y;
                    break;
            }

            UpdateBounds();
        }

        public void UpdateBounds()
        {
            Rectangle bounds = _parentControl.RectangleToScreen(_control.Bounds);

            switch (_splitterType)
            {
                case SplitterType.Left:
                    Bounds = new(bounds.Left - 2, bounds.Top, 5, bounds.Height);
                    _maximum = bounds.Right - 2 - _control.MinimumSize.Width;
                    break;
                case SplitterType.Right:
                    Bounds = new(bounds.Right - 2, bounds.Top, 5, bounds.Height);
                    _minimum = bounds.Left - 2 + _control.MinimumSize.Width;
                    break;
                case SplitterType.Top:
                    Bounds = new(bounds.Left, bounds.Top - 2, bounds.Width, 5);
                    _maximum = bounds.Bottom - 2 - _control.MinimumSize.Height;
                    break;
                case SplitterType.Bottom:
                    Bounds = new(bounds.Left, bounds.Bottom - 2, bounds.Width, 5);
                    _minimum = bounds.Top - 2 + _control.MinimumSize.Height;
                    break;
            }
        }

        #endregion
    }

    #endregion
}