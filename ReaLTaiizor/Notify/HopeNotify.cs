#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeNotify

    public class HopeNotify : Control
    {
        #region Variables
        public enum AlertType
        {
            Success = 0,
            Warning = 1,
            Info = 2,
            Error = 3
        };
        #endregion

        #region Settings

        [RefreshProperties(RefreshProperties.Repaint)]
        public AlertType Type { get; set; } = AlertType.Success;

        private Timer _timer;
        private Timer _Timer
        {
            get { return _timer; }
            set
            {
                if (_timer != null)
                    _timer.Tick -= Timer_Tick;
                _timer = value;
                if (_timer != null)
                    _timer.Tick += Timer_Tick;
            }
        }

        #endregion

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 34;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Visible = false;
        }
        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            var backBrush = new SolidBrush(HopeColors.PrimaryColor);
            var textBrush = new SolidBrush(HopeColors.PrimaryColor);
            switch (Type)
            {
                case AlertType.Success:
                    backBrush = new SolidBrush(Color.FromArgb(25, HopeColors.Success));
                    textBrush = new SolidBrush(HopeColors.Success);
                    break;
                case AlertType.Warning:
                    backBrush = new SolidBrush(Color.FromArgb(25, HopeColors.Warning));
                    textBrush = new SolidBrush(HopeColors.Warning);
                    break;
                case AlertType.Info:
                    backBrush = new SolidBrush(Color.FromArgb(25, HopeColors.Info));
                    textBrush = new SolidBrush(HopeColors.Info);
                    break;
                case AlertType.Error:
                    backBrush = new SolidBrush(Color.FromArgb(25, HopeColors.Danger));
                    textBrush = new SolidBrush(HopeColors.Danger);
                    break;
                default:
                    break;
            }

            var back = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
            graphics.FillPath(new SolidBrush(Color.White), back);
            graphics.FillPath(backBrush, back);
            graphics.DrawPath(new Pen(textBrush, 1f), back);
            graphics.DrawString(Text, Font, textBrush, new RectangleF(20, 0, Width - 40, Height), HopeStringAlign.Left);
            graphics.DrawString("r", new Font("Marlett", 10), new SolidBrush(RoundRectangle.DarkBackColor), new Rectangle(Width - 34, 1, 34, 34), HopeStringAlign.Center);
        }

        #endregion

        public HopeNotify()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);
            Width = 150;
            Cursor = Cursors.Hand;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Visible = false;
            _Timer.Enabled = false;
            _Timer.Dispose();
        }

        public void ShowAlertBox(AlertType type, string text, int Interval)
        {
            Type = type;
            Text = text;
            Visible = true;
            _Timer = new Timer();
            _Timer.Interval = Interval;
            _Timer.Enabled = true;
        }
    }

    #endregion
}