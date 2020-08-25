#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeNotify

    public class HopeNotify : Control
    {
        #region Variables

        private bool _Close = true;

        private Color _DefaultBackColor = HopeColors.PrimaryColor;
        private Color _DefaultTextColor = HopeColors.PrimaryColor;

        private Color _SuccessBackColor = Color.FromArgb(25, HopeColors.Success);
        private Color _SuccessTextColor = HopeColors.Success;
        private Color _WarningBackColor = Color.FromArgb(25, HopeColors.Warning);
        private Color _WarningTextColor = HopeColors.Warning;
        private Color _InfoBackColor = Color.FromArgb(25, HopeColors.Info);
        private Color _InfoTextColor = HopeColors.Info;
        private Color _ErrorBackColor = Color.FromArgb(25, HopeColors.Danger);
        private Color _ErrorTextColor = HopeColors.Danger;

        private Color _CloseColor = RoundRectangle.DarkBackColor;

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

        [RefreshProperties(RefreshProperties.Repaint)]
        public bool Close
        {
            get { return _Close; }
            set { _Close = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessBackColor
        {
            get { return _SuccessBackColor; }
            set { _SuccessBackColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessTextColor
        {
            get { return _SuccessTextColor; }
            set { _SuccessTextColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningBackColor
        {
            get { return _WarningBackColor; }
            set { _WarningBackColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningTextColor
        {
            get { return _WarningTextColor; }
            set { _WarningTextColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoBackColor
        {
            get { return _InfoBackColor; }
            set { _InfoBackColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoTextColor
        {
            get { return _InfoTextColor; }
            set { _InfoTextColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ErrorBackColor
        {
            get { return _ErrorBackColor; }
            set { _ErrorBackColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ErrorTextColor
        {
            get { return _ErrorTextColor; }
            set { _ErrorTextColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color CloseColor
        {
            get { return _CloseColor; }
            set { _CloseColor = value; }
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
            if (_Close)
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

            var backBrush = new SolidBrush(_DefaultBackColor);
            var textBrush = new SolidBrush(_DefaultTextColor);
            switch (Type)
            {
                case AlertType.Success:
                    backBrush = new SolidBrush(_SuccessBackColor);
                    textBrush = new SolidBrush(_SuccessTextColor);
                    break;
                case AlertType.Warning:
                    backBrush = new SolidBrush(_WarningBackColor);
                    textBrush = new SolidBrush(_WarningTextColor);
                    break;
                case AlertType.Info:
                    backBrush = new SolidBrush(_InfoBackColor);
                    textBrush = new SolidBrush(_InfoTextColor);
                    break;
                case AlertType.Error:
                    backBrush = new SolidBrush(_ErrorBackColor);
                    textBrush = new SolidBrush(_ErrorTextColor);
                    break;
                default:
                    break;
            }

            var back = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
            graphics.FillPath(new SolidBrush(Color.White), back);
            graphics.FillPath(backBrush, back);
            graphics.DrawPath(new Pen(textBrush, 1f), back);
            if (_Close)
            {
                graphics.DrawString(Text, Font, textBrush, new RectangleF(20, 0, Width - 40, Height), HopeStringAlign.Left);
                graphics.DrawString("r", new Font("Marlett", 10), new SolidBrush(_CloseColor), new Rectangle(Width - 34, 1, 34, 34), HopeStringAlign.Center);
            }
            else
                graphics.DrawString(Text, Font, textBrush, new RectangleF(20, 0, Width - 35, Height), HopeStringAlign.Left);
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
            if (_Close)
                Visible = false;
            _Timer.Enabled = false;
            _Timer.Dispose();
        }

        /// <summary>
        /// How to use: HopeNotify.ShowAlertBox(Type, String, Interval)
        /// </summary>

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