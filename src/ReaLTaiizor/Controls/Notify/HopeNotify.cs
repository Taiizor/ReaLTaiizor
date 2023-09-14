#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeNotify

    public class HopeNotify : Control
    {
        #region Variables


        private readonly Color _DefaultBackColor = HopeColors.PrimaryColor;
        private readonly Color _DefaultTextColor = HopeColors.PrimaryColor;

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
            get => _timer;
            set
            {
                if (_timer != null)
                {
                    _timer.Tick -= Timer_Tick;
                }

                _timer = value;
                if (_timer != null)
                {
                    _timer.Tick += Timer_Tick;
                }
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public bool Close { get; set; } = true;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessBackColor { get; set; } = Color.FromArgb(25, HopeColors.Success);

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessTextColor { get; set; } = HopeColors.Success;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningBackColor { get; set; } = Color.FromArgb(25, HopeColors.Warning);

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningTextColor { get; set; } = HopeColors.Warning;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoBackColor { get; set; } = Color.FromArgb(25, HopeColors.Info);

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoTextColor { get; set; } = HopeColors.Info;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ErrorBackColor { get; set; } = Color.FromArgb(25, HopeColors.Danger);

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ErrorTextColor { get; set; } = HopeColors.Danger;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color CloseColor { get; set; } = RoundRectangle.DarkBackColor;

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
            if (Close)
            {
                Visible = false;
            }
        }
        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            SolidBrush backBrush = new(_DefaultBackColor);
            SolidBrush textBrush = new(_DefaultTextColor);
            switch (Type)
            {
                case AlertType.Success:
                    backBrush = new(SuccessBackColor);
                    textBrush = new(SuccessTextColor);
                    break;
                case AlertType.Warning:
                    backBrush = new(WarningBackColor);
                    textBrush = new(WarningTextColor);
                    break;
                case AlertType.Info:
                    backBrush = new(InfoBackColor);
                    textBrush = new(InfoTextColor);
                    break;
                case AlertType.Error:
                    backBrush = new(ErrorBackColor);
                    textBrush = new(ErrorTextColor);
                    break;
                default:
                    break;
            }

            GraphicsPath back = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
            graphics.FillPath(new SolidBrush(Color.White), back);
            graphics.FillPath(backBrush, back);
            graphics.DrawPath(new(textBrush, 1f), back);
            if (Close)
            {
                graphics.DrawString(Text, Font, textBrush, new RectangleF(20, 0, Width - 40, Height), HopeStringAlign.Left);
                graphics.DrawString("r", new Font("Marlett", 10), new SolidBrush(CloseColor), new Rectangle(Width - 34, 1, 34, 34), HopeStringAlign.Center);
            }
            else
            {
                graphics.DrawString(Text, Font, textBrush, new RectangleF(20, 0, Width - 35, Height), HopeStringAlign.Left);
            }
        }

        #endregion

        public HopeNotify()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            Width = 150;
            Cursor = Cursors.Hand;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Close)
            {
                Visible = false;
            }

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
            _Timer = new Timer
            {
                Interval = Interval,
                Enabled = true
            };
        }
    }

    #endregion
}