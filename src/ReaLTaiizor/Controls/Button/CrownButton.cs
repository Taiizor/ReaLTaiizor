#region Imports

using ReaLTaiizor.Enum.Crown;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownButton

    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    [DefaultEvent("Click")]
    public class CrownButton : System.Windows.Forms.Button
    {
        #region Field Region

        private ButtonStyle _style = ButtonStyle.Normal;
        private bool _isDefault;
        private bool _spacePressed;

        private readonly int _padding = ThemeProvider.Theme.Sizes.Padding / 2;
        private int _imagePadding = 5; // ThemeProvider.Theme.Sizes.Padding / 2

        #endregion

        #region Designer Property Region

        public new string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the style of the button.")]
        [DefaultValue(ButtonStyle.Normal)]
        public ButtonStyle ButtonStyle
        {
            get => _style;
            set
            {
                _style = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Determines the amount of padding between the image and text.")]
        [DefaultValue(5)]
        public int ImagePadding
        {
            get => _imagePadding;
            set
            {
                _imagePadding = value;
                Invalidate();
            }
        }

        #endregion

        #region Code Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AutoEllipsis => false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ControlState ButtonState { get; private set; } = ControlState.Normal;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Drawing.ContentAlignment ImageAlign => base.ImageAlign;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool FlatAppearance => false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle => base.FlatStyle;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Drawing.ContentAlignment TextAlign => base.TextAlign;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseCompatibleTextRendering => false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseVisualStyleBackColor => false;

        #endregion

        #region Constructor Region

        public CrownButton()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true
            );

            base.UseVisualStyleBackColor = false;
            base.UseCompatibleTextRendering = false;

            SetButtonState(ControlState.Normal);
            Padding = new Padding(_padding);
        }

        #endregion

        #region Method Region

        private void SetButtonState(ControlState buttonState)
        {
            if (ButtonState != buttonState)
            {
                ButtonState = buttonState;
                Invalidate();
            }
        }

        #endregion

        #region Event Handler Region

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            Form form = FindForm();
            if (form != null)
            {
                if (form.AcceptButton == this)
                {
                    _isDefault = true;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_spacePressed)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(e.Location))
                {
                    SetButtonState(ControlState.Pressed);
                }
                else
                {
                    SetButtonState(ControlState.Hover);
                }
            }
            else
            {
                SetButtonState(ControlState.Hover);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!ClientRectangle.Contains(e.Location))
            {
                return;
            }

            SetButtonState(ControlState.Pressed);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_spacePressed)
            {
                return;
            }

            SetButtonState(ControlState.Normal);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_spacePressed)
            {
                return;
            }

            SetButtonState(ControlState.Normal);
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            if (_spacePressed)
            {
                return;
            }

            Point location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
            {
                SetButtonState(ControlState.Normal);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            _spacePressed = false;

            Point location = Cursor.Position;

            if (!ClientRectangle.Contains(location))
            {
                SetButtonState(ControlState.Normal);
            }
            else
            {
                SetButtonState(ControlState.Hover);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                _spacePressed = true;
                SetButtonState(ControlState.Pressed);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyCode == Keys.Space)
            {
                _spacePressed = false;

                Point location = Cursor.Position;

                if (!ClientRectangle.Contains(location))
                {
                    SetButtonState(ControlState.Normal);
                }
                else
                {
                    SetButtonState(ControlState.Hover);
                }
            }
        }

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(value);

            if (!DesignMode)
            {
                return;
            }

            _isDefault = value;
            Invalidate();
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new(0, 0, ClientSize.Width, ClientSize.Height);

            Color textColor = ThemeProvider.Theme.Colors.LightText;
            Color borderColor = ThemeProvider.Theme.Colors.GreySelection;
            Color fillColor = _isDefault ? ThemeProvider.Theme.Colors.DarkBlueBackground : ThemeProvider.Theme.Colors.LightBackground;

            if (Enabled)
            {
                if (ButtonStyle == ButtonStyle.Normal)
                {
                    if (Focused && TabStop)
                    {
                        borderColor = ThemeProvider.Theme.Colors.BlueHighlight;
                    }

                    switch (ButtonState)
                    {
                        case ControlState.Hover:
                            fillColor = _isDefault ? ThemeProvider.Theme.Colors.BlueBackground : ThemeProvider.Theme.Colors.LighterBackground;
                            break;
                        case ControlState.Pressed:
                            fillColor = _isDefault ? ThemeProvider.Theme.Colors.DarkBackground : ThemeProvider.Theme.Colors.DarkBackground;
                            break;
                    }
                }
                else if (ButtonStyle == ButtonStyle.Flat)
                {
                    switch (ButtonState)
                    {
                        case ControlState.Normal:
                            fillColor = ThemeProvider.Theme.Colors.GreyBackground;
                            break;
                        case ControlState.Hover:
                            fillColor = ThemeProvider.Theme.Colors.MediumBackground;
                            break;
                        case ControlState.Pressed:
                            fillColor = ThemeProvider.Theme.Colors.DarkBackground;
                            break;
                    }
                }
            }
            else
            {
                textColor = ThemeProvider.Theme.Colors.DisabledText;
                fillColor = ThemeProvider.Theme.Colors.DarkGreySelection;
            }

            using (SolidBrush b = new(fillColor))
            {
                g.FillRectangle(b, rect);
            }

            if (ButtonStyle == ButtonStyle.Normal)
            {
                using Pen p = new(borderColor, 1);
                Rectangle modRect = new(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

                g.DrawRectangle(p, modRect);
            }

            int textOffsetX = 0;
            int textOffsetY = 0;

            if (Image != null)
            {
                SizeF stringSize = g.MeasureString(Text, Font, rect.Size);

                int x = (ClientSize.Width / 2) - (Image.Size.Width / 2);
                int y = (ClientSize.Height / 2) - (Image.Size.Height / 2);

                switch (TextImageRelation)
                {
                    case TextImageRelation.ImageAboveText:
                        textOffsetY = (Image.Size.Height / 2) + (ImagePadding / 2);
                        y -= (int)(stringSize.Height / 2) + (ImagePadding / 2);
                        break;
                    case TextImageRelation.TextAboveImage:
                        textOffsetY = ((Image.Size.Height / 2) + (ImagePadding / 2)) * -1;
                        y += (int)(stringSize.Height / 2) + (ImagePadding / 2);
                        break;
                    case TextImageRelation.ImageBeforeText:
                        textOffsetX = Image.Size.Width + (ImagePadding * 2);
                        x = ImagePadding;
                        break;
                    case TextImageRelation.TextBeforeImage:
                        x += (int)stringSize.Width;
                        break;
                }

                g.DrawImageUnscaled(Image, x, y);
            }

            using (SolidBrush b = new(textColor))
            {
                Rectangle modRect = new(rect.Left + textOffsetX + Padding.Left, rect.Top + textOffsetY + Padding.Top, rect.Width - Padding.Horizontal, rect.Height - Padding.Vertical);

                StringFormat stringFormat = new()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(Text, Font, b, modRect, stringFormat);
            }
        }

        #endregion
    }

    #endregion
}