#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightTextBox

    [DefaultEvent("TextChanged")]
    public class NightTextBox : Control
    {
        #region Fields

        private readonly TextBox tbCtrl = new();
        private Color BorderColor;
        private Panel watermarkContainer;

        #endregion

        #region Properties

        [Browsable(true)]
        [Description("Decides whether the top and bottom border lines are recolored on Enter event.")]
        public bool ColorBordersOnEnter { get; set; } = true;

        private Image _Image;
        [Browsable(true)]
        [Description("The image displayed in the TextBox.")]
        public Image Image
        {
            get => _Image;
            set
            {
                _Image = value;
                ImageSize = value == null ? Size.Empty : value.Size;
                tbCtrl.Location = new(24, 14);

                Invalidate();
            }
        }

        protected Size ImageSize { get; private set; }

        private int _MaxLength = 32767;
        [Browsable(true)]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        public int MaxLength
        {
            get => _MaxLength;
            set
            {
                _MaxLength = value;
                tbCtrl.MaxLength = MaxLength;
                Invalidate();
            }
        }

        private bool _Multiline;
        [Browsable(true)]
        [Description("Controls whether the text of the edit control can span more than one line.")]
        public bool Multiline
        {
            get => _Multiline;
            set
            {
                _Multiline = value;
                if (tbCtrl != null)
                {
                    tbCtrl.Multiline = value;
                    if (value)
                    {
                        tbCtrl.Height = Height - 10;
                    }
                    else
                    {
                        Height = tbCtrl.Height + 10;
                    }
                }
            }
        }

        private bool _ReadOnly;
        [Browsable(true)]
        [Description("Controls whether the text in the edit control can be changed or not.")]
        public bool ReadOnly
        {
            get => _ReadOnly;
            set
            {
                _ReadOnly = value;
                if (tbCtrl != null)
                {
                    tbCtrl.ReadOnly = value;
                }
            }
        }

        private bool _ShortcutsEnabled = true;
        [Browsable(true)]
        [Description("Indicates whether shortcuts defined for the control are enabled.")]
        public bool ShortcutsEnabled
        {
            get => _ShortcutsEnabled;
            set
            {
                _ShortcutsEnabled = value;
                tbCtrl.ShortcutsEnabled = value;
            }
        }

        private bool _ShowBottomBorder = true;
        [Browsable(true)]
        [Description("Decides whether the bottom border line should be drawn.")]
        public bool ShowBottomBorder
        {
            get => _ShowBottomBorder;
            set
            {
                _ShowBottomBorder = value;
                Invalidate();
            }
        }

        private bool _ShowTopBorder = true;
        [Browsable(true)]
        [Description("Decides whether the top border line should be drawn.")]
        public bool ShowTopBorder
        {
            get => _ShowTopBorder;
            set
            {
                _ShowTopBorder = value;
                Invalidate();
            }
        }

        private HorizontalAlignment _TextAlignment;
        [Browsable(true)]
        [Description("Indicates how the text should be aligned for edit controls.")]
        public HorizontalAlignment TextAlignment
        {
            get => _TextAlignment;
            set
            {
                _TextAlignment = value;
                tbCtrl.TextAlign = _TextAlignment;
                Invalidate();
            }
        }

        private bool _UseSystemPasswordChar = false;
        [Browsable(true)]
        [Description("Indicates if the text in the edit control should appear as the default password character.")]
        public bool UseSystemPasswordChar
        {
            get => _UseSystemPasswordChar;
            set
            {
                _UseSystemPasswordChar = value;
                tbCtrl.UseSystemPasswordChar = UseSystemPasswordChar;
                Invalidate();
            }
        }

        private string _Watermark = string.Empty;
        [Browsable(true)]
        [Description("Allows adding a watermark to the TextBox field when it is empty.")]
        public string Watermark
        {
            get => _Watermark;
            set
            {
                _Watermark = value;
                Invalidate();
            }
        }

        private Color _WatermarkColor;
        [Browsable(true)]
        [Description("Allows adding a watermark to the TextBox field when it is empty.")]
        public Color WatermarkColor
        {
            get => _WatermarkColor;
            set
            {
                _WatermarkColor = value;
                Invalidate();
            }
        }

        private Color _ActiveBorderColor = ColorTranslator.FromHtml("#F25D59");
        [Browsable(true)]
        [Description("Actived TextBox border line color.")]
        public Color ActiveBorderColor
        {
            get => _ActiveBorderColor;
            set
            {
                _ActiveBorderColor = value;
                Invalidate();
            }
        }

        private Color _DisableBorderColor = ColorTranslator.FromHtml("#3C3F50");
        [Browsable(true)]
        [Description("Disabled TextBox border line color.")]
        public Color DisableBorderColor
        {
            get => _DisableBorderColor;
            set
            {
                _DisableBorderColor = value;
                BorderColor = value;
                Invalidate();
            }
        }

        private Color _BaseBackColor = ColorTranslator.FromHtml("#2B3043");
        [Browsable(true)]
        [Description("TextBox is change BackColor.")]
        public Color BaseBackColor
        {
            get => _BaseBackColor;
            set
            {
                _BaseBackColor = value;
                Invalidate();
            }
        }

        #endregion

        #region EventArgs

        private void TextBox_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (ColorBordersOnEnter)
            {
                BorderColor = _ActiveBorderColor;
            }

            if (tbCtrl.TextLength <= 0)
            {
                RemoveWatermark();
                DrawWatermark();
            }

            Invalidate();
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (ColorBordersOnEnter)
            {
                BorderColor = _DisableBorderColor;
            }

            if (tbCtrl.TextLength <= 0)
            {
                RemoveWatermark();
            }
            else
            {
                Invalidate();
            }

            Invalidate();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                tbCtrl.SelectAll();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                tbCtrl.Copy();
                e.SuppressKeyPress = true;
            }

            OnKeyDown(e);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        public void TextBox_TextChanged(object sender, EventArgs e)
        {
            Text = tbCtrl.Text;

            if (tbCtrl.TextLength > 0)
            {
                RemoveWatermark();
            }
            else
            {
                DrawWatermark();
            }
        }

        private void WatermarkContainer_Click(object sender, EventArgs e)
        {
            tbCtrl.Focus();
        }

        private void WatermarkContainer_Paint(object sender, PaintEventArgs e)
        {
            // X has to be >=1, otherwise the cursor won't show
            watermarkContainer.Location = new(1, -1);
            watermarkContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            watermarkContainer.Width = tbCtrl.Width - 25;
            watermarkContainer.Height = tbCtrl.Height;

            using SolidBrush watermark = new(_WatermarkColor);
            e.Graphics.DrawString(_Watermark, Font, watermark, new PointF(-3.0f, 1.0f));
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            tbCtrl.Font = Font;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);

            tbCtrl.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            tbCtrl.Focus();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_Multiline)
            {
                tbCtrl.Height = Height - 30;
            }
            else
            {
                Height = tbCtrl.Height + 32;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            tbCtrl.Text = Text;
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            if (watermarkContainer != null)
            {
                watermarkContainer.Invalidate();
            }
        }

        #endregion

        public NightTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            DoubleBuffered = true;

            _WatermarkColor = ColorTranslator.FromHtml("#747881");
            watermarkContainer = null;

            AddTextBox();
            DrawWatermark();

            BorderColor = ColorTranslator.FromHtml("#3C3F50");
            BackColor = ColorTranslator.FromHtml("#2B3043");

            Text = string.Empty;
            Font = new("Segoe UI", 10);
            Size = new(145, 49);
        }

        private void AddTextBox()
        {
            tbCtrl.Size = new(Width - 10, 49);
            tbCtrl.Location = new(24, 14);
            tbCtrl.BorderStyle = BorderStyle.None;
            tbCtrl.TextAlign = HorizontalAlignment.Left;
            tbCtrl.Font = new("Segoe UI", 10);
            tbCtrl.UseSystemPasswordChar = _UseSystemPasswordChar;
            tbCtrl.ShortcutsEnabled = _ShortcutsEnabled;
            tbCtrl.Multiline = false;
            tbCtrl.BackColor = BaseBackColor;

            ForeColor = ColorTranslator.FromHtml("#7F838C");

            tbCtrl.TextChanged += TextBox_TextChanged;
            tbCtrl.KeyDown += TextBox_KeyDown;
            tbCtrl.KeyPress += TextBox_KeyPress;
            tbCtrl.KeyUp += TextBox_KeyUp;
            tbCtrl.Click += TextBox_Click;
            tbCtrl.Enter += TextBox_Enter;
            tbCtrl.Leave += TextBox_Leave;

            Controls.Add(tbCtrl);
        }

        private void DrawWatermark()
        {
            if (watermarkContainer != null || tbCtrl.TextLength > 0)
            {
                return;
            }

            watermarkContainer = new Panel();
            watermarkContainer.Paint += WatermarkContainer_Paint;
            watermarkContainer.Click += WatermarkContainer_Click;

            tbCtrl.Controls.Add(watermarkContainer);
        }

        private void RemoveWatermark()
        {
            if (watermarkContainer == null)
            {
                return;
            }

            tbCtrl.Controls.Remove(watermarkContainer);
            watermarkContainer = null;
        }

        private void DrawBorder(Graphics g)
        {
            using Pen border = new(BorderColor);
            // Top border
            if (_ShowTopBorder)
            {
                g.DrawLine(border, 0, 0, Width - 1, 0);
                g.DrawLine(border, 0, 1, Width - 1, 1);
            }

            // Bottom border
            if (_ShowBottomBorder)
            {
                g.DrawLine(border, 0, Height - 2, Width - 1, Height - 2);
                g.DrawLine(border, 0, Height - 1, Width - 1, Height - 1);
            }
        }

        private void DrawImage(Graphics g)
        {
            if (Image == null)
            {
                tbCtrl.Width = Width - 35;
            }
            else
            {
                tbCtrl.Location = new(48, tbCtrl.Location.Y);
                tbCtrl.Width = Width - 59;

                g.DrawImage(_Image, 23, 14, 16, 16);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            DrawWatermark();
            DrawBorder(g);
            DrawImage(g);

            tbCtrl.BackColor = BaseBackColor;

            base.OnPaint(e);
        }
    }

    #endregion
}