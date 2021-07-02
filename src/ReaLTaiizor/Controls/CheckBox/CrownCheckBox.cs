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
    #region CrownCheckBox

    public class CrownCheckBox : System.Windows.Forms.CheckBox
    {
        #region Field Region

        private ControlState _controlState = ControlState.Normal;

        private bool _spacePressed;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Appearance Appearance => base.Appearance;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AutoEllipsis => base.AutoEllipsis;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image BackgroundImage => base.BackgroundImage;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageLayout BackgroundImageLayout => base.BackgroundImageLayout;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool FlatAppearance => false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle => base.FlatStyle;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image Image => base.Image;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Drawing.ContentAlignment ImageAlign => base.ImageAlign;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int ImageIndex => base.ImageIndex;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string ImageKey => base.ImageKey;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageList ImageList => base.ImageList;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Drawing.ContentAlignment TextAlign => base.TextAlign;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new TextImageRelation TextImageRelation => base.TextImageRelation;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ThreeState => base.ThreeState;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseCompatibleTextRendering => false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool UseVisualStyleBackColor => false;

        #endregion

        #region Constructor Region

        public CrownCheckBox()
        {
            SetStyle
            (
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );
        }

        #endregion

        #region Method Region

        private void SetControlState(ControlState controlState)
        {
            if (_controlState != controlState)
            {
                _controlState = controlState;
                Invalidate();
            }
        }

        #endregion

        #region Event Handler Region

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
                    SetControlState(ControlState.Pressed);
                }
                else
                {
                    SetControlState(ControlState.Hover);
                }
            }
            else
            {
                SetControlState(ControlState.Hover);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!ClientRectangle.Contains(e.Location))
            {
                return;
            }

            SetControlState(ControlState.Pressed);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_spacePressed)
            {
                return;
            }

            SetControlState(ControlState.Normal);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_spacePressed)
            {
                return;
            }

            SetControlState(ControlState.Normal);
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
                SetControlState(ControlState.Normal);
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
                SetControlState(ControlState.Normal);
            }
            else
            {
                SetControlState(ControlState.Hover);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                _spacePressed = true;
                SetControlState(ControlState.Pressed);
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
                    SetControlState(ControlState.Normal);
                }
                else
                {
                    SetControlState(ControlState.Hover);
                }
            }
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new(0, 0, ClientSize.Width, ClientSize.Height);

            int size = ThemeProvider.Theme.Sizes.CheckBoxSize;

            Color textColor = ThemeProvider.Theme.Colors.LightText;
            Color borderColor = ThemeProvider.Theme.Colors.LightText;
            Color fillColor = ThemeProvider.Theme.Colors.LightestBackground;

            if (Enabled)
            {
                if (Focused)
                {
                    borderColor = ThemeProvider.Theme.Colors.BlueHighlight;
                    fillColor = ThemeProvider.Theme.Colors.BlueSelection;
                }

                if (_controlState == ControlState.Hover)
                {
                    borderColor = ThemeProvider.Theme.Colors.BlueHighlight;
                    fillColor = ThemeProvider.Theme.Colors.BlueSelection;
                }
                else if (_controlState == ControlState.Pressed)
                {
                    borderColor = ThemeProvider.Theme.Colors.GreyHighlight;
                    fillColor = ThemeProvider.Theme.Colors.GreySelection;
                }
            }
            else
            {
                textColor = ThemeProvider.Theme.Colors.DisabledText;
                borderColor = ThemeProvider.Theme.Colors.GreyHighlight;
                fillColor = ThemeProvider.Theme.Colors.GreySelection;
            }

            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, rect);
            }

            using (Pen p = new(borderColor))
            {
                Rectangle boxRect = new(0, (rect.Height / 2) - (size / 2), size, size);
                g.DrawRectangle(p, boxRect);
            }

            if (Checked)
            {
                using SolidBrush b = new(fillColor);
                Rectangle boxRect = new(2, (rect.Height / 2) - ((size - 4) / 2), size - 3, size - 3);
                g.FillRectangle(b, boxRect);
            }

            using (SolidBrush b = new(textColor))
            {
                StringFormat stringFormat = new()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                };

                Rectangle modRect = new(size + 4, 0, rect.Width - size, rect.Height);
                g.DrawString(Text, Font, b, modRect, stringFormat);
            }
        }

        #endregion
    }

    #endregion
}