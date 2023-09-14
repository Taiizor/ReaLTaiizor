#region Imports

using ReaLTaiizor.Design.Poison;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonDropDownButton

    [Designer(typeof(PoisonDropDownButtonDesigner))]
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    [DefaultEvent("Click")]
    public class PoisonDropDownButton : PoisonButton
    {
        #region Constants
        private const int SplitSectionWidth = 18;
        #endregion

        #region Variables

        private PushButtonState _state;
        private static readonly int BorderSize = SystemInformation.Border3DSize.Width * 2;
        private bool skipNextOpen;
        private Rectangle dropDownRectangle;
        private bool showSplit;
        private bool isSplitMenuVisible;
        private ContextMenuStrip m_SplitMenuStrip;
#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
        private ContextMenu m_SplitMenu;
#endif
        private readonly TextFormatFlags textFormatFlags = TextFormatFlags.Default;
        #endregion

        #region Constructor
        public PoisonDropDownButton()
        {
            AutoSize = true;
        }
        #endregion

        #region Properties

        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get => SplitMenuStrip;
            set => SplitMenuStrip = value;
        }

#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
        [DefaultValue(null)]
        public ContextMenu SplitMenu
        {
            get => m_SplitMenu;
            set
            {
                //remove the event handlers for the old SplitMenu
                if (m_SplitMenu != null)
                {
                    m_SplitMenu.Popup -= SplitMenu_Popup;
                }

                //add the event handlers for the new SplitMenu
                if (value != null)
                {
                    ShowSplit = true;
                    value.Popup += SplitMenu_Popup;
                }
                else
                {
                    ShowSplit = false;
                }

                m_SplitMenu = value;
            }
        }
#endif

        [DefaultValue(null)]
        public ContextMenuStrip SplitMenuStrip
        {
            get => m_SplitMenuStrip;
            set
            {
                //remove the event handlers for the old SplitMenuStrip
                if (m_SplitMenuStrip != null)
                {
                    m_SplitMenuStrip.Closing -= SplitMenuStrip_Closing;
                    m_SplitMenuStrip.Opening -= SplitMenuStrip_Opening;
                }

                //add the event handlers for the new SplitMenuStrip
                if (value != null)
                {
                    ShowSplit = true;
                    value.Closing += SplitMenuStrip_Closing;
                    value.Opening += SplitMenuStrip_Opening;
                }
                else
                {
                    ShowSplit = false;
                }

                m_SplitMenuStrip = value;
            }
        }

        [DefaultValue(false)]
        public bool ShowSplit
        {
            set
            {
                if (value != showSplit)
                {
                    showSplit = value;
                    Invalidate();

                    if (Parent != null)
                    {
                        Parent.PerformLayout();
                    }
                }
            }
        }

        private PushButtonState State
        {
            get => _state;
            set
            {
                if (!_state.Equals(value))
                {
                    _state = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region SplitMenuStrip
        private void SplitMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            isSplitMenuVisible = true;
        }

        private void SplitMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            isSplitMenuVisible = false;

            SetButtonDrawState();

            if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
            {
                skipNextOpen = dropDownRectangle.Contains(PointToClient(Cursor.Position)) && MouseButtons == MouseButtons.Left;
            }
        }

        private void SplitMenu_Popup(object sender, EventArgs e)
        {
            isSplitMenuVisible = true;
        }
        #endregion

        #region Overrrides

        protected override void WndProc(ref Message m)
        {
            //0x0212 == WM_EXITMENULOOP
            if (m.Msg == 0x0212)
            {
                //this message is only sent when a ContextMenu is closed (not a ContextMenuStrip)
                isSplitMenuVisible = false;
                SetButtonDrawState();
            }

            base.WndProc(ref m);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData.Equals(Keys.Down) && showSplit)
            {
                return true;
            }

            return base.IsInputKey(keyData);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnGotFocus(e);
                return;
            }

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                State = PushButtonState.Default;
            }
        }

        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            if (showSplit)
            {
                if (kevent.KeyCode.Equals(Keys.Down) && !isSplitMenuVisible)
                {
                    ShowContextMenuStrip();
                }
                else if (kevent.KeyCode.Equals(Keys.Space) && kevent.Modifiers == Keys.None)
                {
                    State = PushButtonState.Pressed;
                }
            }

            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (kevent.KeyCode.Equals(Keys.Space))
            {
                if (MouseButtons == MouseButtons.None)
                {
                    State = PushButtonState.Normal;
                }
            }
            else if (kevent.KeyCode.Equals(Keys.Apps))
            {
                if (MouseButtons == MouseButtons.None && !isSplitMenuVisible)
                {
                    ShowContextMenuStrip();
                }
            }

            base.OnKeyUp(kevent);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            State = Enabled ? PushButtonState.Normal : PushButtonState.Disabled;

            base.OnEnabledChanged(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnLostFocus(e);
                return;
            }

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                State = PushButtonState.Normal;
            }
        }

        private bool isMouseEntered;

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnMouseEnter(e);
                return;
            }

            isMouseEntered = true;

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                State = PushButtonState.Hot;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!showSplit)
            {
                base.OnMouseLeave(e);
                return;
            }

            isMouseEntered = false;

            if (!State.Equals(PushButtonState.Pressed) && !State.Equals(PushButtonState.Disabled))
            {
                State = Focused ? PushButtonState.Default : PushButtonState.Normal;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!showSplit)
            {
                base.OnMouseDown(e);
                return;
            }

#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
            //handle ContextMenu re-clicking the drop-down region to close the menu
            if (m_SplitMenu != null && e.Button == MouseButtons.Left && !isMouseEntered)
            {
                skipNextOpen = true;
            }
#endif

            if (dropDownRectangle.Contains(e.Location) && !isSplitMenuVisible && e.Button == MouseButtons.Left)
            {
                ShowContextMenuStrip();
            }
            else
            {
                State = PushButtonState.Pressed;
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (!showSplit)
            {
                base.OnMouseUp(mevent);
                return;
            }

#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
            // if the right button was released inside the button
            if (mevent.Button == MouseButtons.Right && ClientRectangle.Contains(mevent.Location) && !isSplitMenuVisible)
            {
                ShowContextMenuStrip();
            }
            else if ((m_SplitMenuStrip == null && m_SplitMenu == null) || !isSplitMenuVisible)
            {
                SetButtonDrawState();

                if (ClientRectangle.Contains(mevent.Location) && !dropDownRectangle.Contains(mevent.Location))
                {
                    OnClick(new EventArgs());
                }
            }
#endif

#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0
            // if the right button was released inside the button
            if (mevent.Button == MouseButtons.Right && ClientRectangle.Contains(mevent.Location) && !isSplitMenuVisible)
            {
                ShowContextMenuStrip();
            }
            else if (m_SplitMenuStrip == null || !isSplitMenuVisible)
            {
                SetButtonDrawState();

                if (ClientRectangle.Contains(mevent.Location) && !dropDownRectangle.Contains(mevent.Location))
                {
                    OnClick(new EventArgs());
                }
            }
#endif
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            if (!showSplit)
            {
                return;
            }

            Graphics g = pevent.Graphics;
            Rectangle bounds = ClientRectangle;

            // calculate the current dropdown rectangle.
            dropDownRectangle = new(bounds.Right - SplitSectionWidth, 0, SplitSectionWidth, bounds.Height);

            int internalBorder = BorderSize;
            Rectangle focusRect = new(internalBorder - 1, internalBorder - 1, bounds.Width - dropDownRectangle.Width - internalBorder, bounds.Height - (internalBorder * 2) + 2);

            bool drawSplitLine = State == PushButtonState.Hot || State == PushButtonState.Pressed || !Application.RenderWithVisualStyles;

            if (RightToLeft == RightToLeft.Yes)
            {
                dropDownRectangle.X = bounds.Left + 1;
                focusRect.X = dropDownRectangle.Right;

                if (drawSplitLine)
                {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Left + SplitSectionWidth, BorderSize, bounds.Left + SplitSectionWidth, bounds.Bottom - BorderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Left + SplitSectionWidth + 1, BorderSize, bounds.Left + SplitSectionWidth + 1, bounds.Bottom - BorderSize);
                }
            }
            else
            {
                if (drawSplitLine)
                {
                    // draw two lines at the edge of the dropdown button
                    g.DrawLine(SystemPens.ButtonShadow, bounds.Right - SplitSectionWidth, BorderSize, bounds.Right - SplitSectionWidth, bounds.Bottom - BorderSize);
                    g.DrawLine(SystemPens.ButtonFace, bounds.Right - SplitSectionWidth - 1, BorderSize, bounds.Right - SplitSectionWidth - 1, bounds.Bottom - BorderSize);
                }
            }

            // Draw an arrow in the correct location
            PaintArrow(g, dropDownRectangle);

            //paint the image and text in the "button" part of the splitButton
            PaintTextandImage(g, new Rectangle(0, 0, ClientRectangle.Width - SplitSectionWidth, ClientRectangle.Height));

            // draw the focus rectangle.
            if (State != PushButtonState.Pressed && Focused && ShowFocusCues)
            {
                ControlPaint.DrawFocusRectangle(g, focusRect);
            }
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize = base.GetPreferredSize(proposedSize);

            //autosize correctly for splitbuttons
            if (showSplit)
            {
                if (AutoSize)
                {
                    return CalculateButtonAutoSize();
                }

                if (!string.IsNullOrEmpty(Text) && TextRenderer.MeasureText(Text, Font).Width + SplitSectionWidth > preferredSize.Width)
                {
                    return preferredSize + new Size(SplitSectionWidth + (BorderSize * 2), 0);
                }
            }

            return preferredSize;
        }

        #endregion

        #region Paint Functions
        private void PaintTextandImage(Graphics g, Rectangle bounds)
        {
            // Figure out where our text and image should go

            CalculateButtonTextAndImageLayout(ref bounds, out Rectangle text_rectangle, out Rectangle image_rectangle);

            //draw the image
            if (Image != null)
            {
                if (Enabled)
                {
                    g.DrawImage(Image, image_rectangle.X, image_rectangle.Y, Image.Width, Image.Height);
                }
                else
                {
                    ControlPaint.DrawImageDisabled(g, Image, image_rectangle.X, image_rectangle.Y, BackColor);
                }
            }
        }

        private void PaintArrow(Graphics g, Rectangle dropDownRect)
        {
            Point middle = new(Convert.ToInt32(dropDownRect.Left + (dropDownRect.Width / 2)), Convert.ToInt32(dropDownRect.Top + (dropDownRect.Height / 2)));

            //if the width is odd - favor pushing it over one pixel right.
            middle.X += dropDownRect.Width % 2;

            Point[] arrow = new[] { new Point(middle.X - 2, middle.Y - 1), new Point(middle.X + 3, middle.Y - 1), new Point(middle.X, middle.Y + 2) };

            if (Enabled)
            {
                g.FillPolygon(SystemBrushes.ControlText, arrow);
            }
            else
            {
                g.FillPolygon(SystemBrushes.ButtonShadow, arrow);
            }
        }
        #endregion

        #region Button Layout Calculations

        private void CalculateButtonTextAndImageLayout(ref Rectangle content_rect, out Rectangle textRectangle, out Rectangle imageRectangle)
        {
            Size text_size = TextRenderer.MeasureText(Text, Font, content_rect.Size, textFormatFlags);
            Size image_size = Image == null ? Size.Empty : Image.Size;

            textRectangle = Rectangle.Empty;
            imageRectangle = Rectangle.Empty;

            switch (TextImageRelation)
            {
                case TextImageRelation.Overlay:
                    // Overlay is easy, text always goes here
                    textRectangle = OverlayObjectRect(ref content_rect, ref text_size, TextAlign); // Rectangle.Inflate(content_rect, -4, -4);

                    //Offset on Windows 98 style when button is pressed
                    if (_state == PushButtonState.Pressed && !Application.RenderWithVisualStyles)
                    {
                        textRectangle.Offset(1, 1);
                    }

                    // Image is dependent on ImageAlign
                    if (Image != null)
                    {
                        imageRectangle = OverlayObjectRect(ref content_rect, ref image_size, ImageAlign);
                    }

                    break;
                case TextImageRelation.ImageAboveText:
                    content_rect.Inflate(-4, -4);
                    LayoutTextAboveOrBelowImage(content_rect, false, text_size, image_size, out textRectangle, out imageRectangle);
                    break;
                case TextImageRelation.TextAboveImage:
                    content_rect.Inflate(-4, -4);
                    LayoutTextAboveOrBelowImage(content_rect, true, text_size, image_size, out textRectangle, out imageRectangle);
                    break;
                case TextImageRelation.ImageBeforeText:
                    content_rect.Inflate(-4, -4);
                    LayoutTextBeforeOrAfterImage(content_rect, false, text_size, image_size, out textRectangle, out imageRectangle);
                    break;
                case TextImageRelation.TextBeforeImage:
                    content_rect.Inflate(-4, -4);
                    LayoutTextBeforeOrAfterImage(content_rect, true, text_size, image_size, out textRectangle, out imageRectangle);
                    break;
            }
        }

        private static Rectangle OverlayObjectRect(ref Rectangle container, ref Size sizeOfObject, System.Drawing.ContentAlignment alignment)
        {
            int x, y;

            switch (alignment)
            {
                case System.Drawing.ContentAlignment.TopLeft:
                    x = 4;
                    y = 4;
                    break;
                case System.Drawing.ContentAlignment.TopCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = 4;
                    break;
                case System.Drawing.ContentAlignment.TopRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = 4;
                    break;
                case System.Drawing.ContentAlignment.MiddleLeft:
                    x = 4;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case System.Drawing.ContentAlignment.MiddleCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case System.Drawing.ContentAlignment.MiddleRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = (container.Height - sizeOfObject.Height) / 2;
                    break;
                case System.Drawing.ContentAlignment.BottomLeft:
                    x = 4;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                case System.Drawing.ContentAlignment.BottomCenter:
                    x = (container.Width - sizeOfObject.Width) / 2;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                case System.Drawing.ContentAlignment.BottomRight:
                    x = container.Width - sizeOfObject.Width - 4;
                    y = container.Height - sizeOfObject.Height - 4;
                    break;
                default:
                    x = 4;
                    y = 4;
                    break;
            }

            return new Rectangle(x, y, sizeOfObject.Width, sizeOfObject.Height);
        }

        private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, out Rectangle textRect, out Rectangle imageRect)
        {
            int element_spacing = 0;	// Spacing between the Text and the Image
            int total_width = textSize.Width + element_spacing + imageSize.Width;

            if (!textFirst)
            {
                element_spacing += 2;
            }

            // If the text is too big, chop it down to the size we have available to it
            if (total_width > totalArea.Width)
            {
                textSize.Width = totalArea.Width - element_spacing - imageSize.Width;
                total_width = totalArea.Width;
            }

            int excess_width = totalArea.Width - total_width;
            int offset = 0;

            Rectangle final_text_rect;
            Rectangle final_image_rect;

            HorizontalAlignment h_text = GetHorizontalAlignment(TextAlign);
            HorizontalAlignment h_image = GetHorizontalAlignment(ImageAlign);

            if (h_image == HorizontalAlignment.Left)
            {
                offset = 0;
            }
            else if (h_image == HorizontalAlignment.Right && h_text == HorizontalAlignment.Right)
            {
                offset = excess_width;
            }
            else if (h_image == HorizontalAlignment.Center && (h_text == HorizontalAlignment.Left || h_text == HorizontalAlignment.Center))
            {
                offset += excess_width / 3;
            }
            else
            {
                offset += 2 * (excess_width / 3);
            }

            if (textFirst)
            {
                final_text_rect = new(totalArea.Left + offset, AlignInRectangle(totalArea, textSize, TextAlign).Top, textSize.Width, textSize.Height);
                final_image_rect = new(final_text_rect.Right + element_spacing, AlignInRectangle(totalArea, imageSize, ImageAlign).Top, imageSize.Width, imageSize.Height);
            }
            else
            {
                final_image_rect = new(totalArea.Left + offset, AlignInRectangle(totalArea, imageSize, ImageAlign).Top, imageSize.Width, imageSize.Height);
                final_text_rect = new(final_image_rect.Right + element_spacing, AlignInRectangle(totalArea, textSize, TextAlign).Top, textSize.Width, textSize.Height);
            }

            textRect = final_text_rect;
            imageRect = final_image_rect;
        }

        private void LayoutTextAboveOrBelowImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, out Rectangle textRect, out Rectangle imageRect)
        {
            int element_spacing = 0;	// Spacing between the Text and the Image
            int total_height = textSize.Height + element_spacing + imageSize.Height;

            if (textFirst)
            {
                element_spacing += 2;
            }

            if (textSize.Width > totalArea.Width)
            {
                textSize.Width = totalArea.Width;
            }

            // If the there isn't enough room and we're text first, cut out the image
            if (total_height > totalArea.Height && textFirst)
            {
                imageSize = Size.Empty;
                total_height = totalArea.Height;
            }

            int excess_height = totalArea.Height - total_height;
            int offset = 0;

            Rectangle final_text_rect;
            Rectangle final_image_rect;

            VerticalAlignment v_text = GetVerticalAlignment(TextAlign);
            VerticalAlignment v_image = GetVerticalAlignment(ImageAlign);

            if (v_image == VerticalAlignment.Top)
            {
                offset = 0;
            }
            else if (v_image == VerticalAlignment.Bottom && v_text == VerticalAlignment.Bottom)
            {
                offset = excess_height;
            }
            else if (v_image == VerticalAlignment.Center && (v_text == VerticalAlignment.Top || v_text == VerticalAlignment.Center))
            {
                offset += excess_height / 3;
            }
            else
            {
                offset += 2 * (excess_height / 3);
            }

            if (textFirst)
            {
                final_text_rect = new(AlignInRectangle(totalArea, textSize, TextAlign).Left, totalArea.Top + offset, textSize.Width, textSize.Height);
                final_image_rect = new(AlignInRectangle(totalArea, imageSize, ImageAlign).Left, final_text_rect.Bottom + element_spacing, imageSize.Width, imageSize.Height);
            }
            else
            {
                final_image_rect = new(AlignInRectangle(totalArea, imageSize, ImageAlign).Left, totalArea.Top + offset, imageSize.Width, imageSize.Height);
                final_text_rect = new(AlignInRectangle(totalArea, textSize, TextAlign).Left, final_image_rect.Bottom + element_spacing, textSize.Width, textSize.Height);

                if (final_text_rect.Bottom > totalArea.Bottom)
                {
                    final_text_rect.Y = totalArea.Top;
                }
            }

            textRect = final_text_rect;
            imageRect = final_image_rect;
        }

        private static HorizontalAlignment GetHorizontalAlignment(System.Drawing.ContentAlignment align)
        {
            return align switch
            {
                System.Drawing.ContentAlignment.BottomLeft or System.Drawing.ContentAlignment.MiddleLeft or System.Drawing.ContentAlignment.TopLeft => HorizontalAlignment.Left,
                System.Drawing.ContentAlignment.BottomCenter or System.Drawing.ContentAlignment.MiddleCenter or System.Drawing.ContentAlignment.TopCenter => HorizontalAlignment.Center,
                System.Drawing.ContentAlignment.BottomRight or System.Drawing.ContentAlignment.MiddleRight or System.Drawing.ContentAlignment.TopRight => HorizontalAlignment.Right,
                _ => HorizontalAlignment.Left,
            };
        }

        private static VerticalAlignment GetVerticalAlignment(System.Drawing.ContentAlignment align)
        {
            return align switch
            {
                System.Drawing.ContentAlignment.TopLeft or System.Drawing.ContentAlignment.TopCenter or System.Drawing.ContentAlignment.TopRight => VerticalAlignment.Top,
                System.Drawing.ContentAlignment.MiddleLeft or System.Drawing.ContentAlignment.MiddleCenter or System.Drawing.ContentAlignment.MiddleRight => VerticalAlignment.Center,
                System.Drawing.ContentAlignment.BottomLeft or System.Drawing.ContentAlignment.BottomCenter or System.Drawing.ContentAlignment.BottomRight => VerticalAlignment.Bottom,
                _ => VerticalAlignment.Top,
            };
        }

        internal static Rectangle AlignInRectangle(Rectangle outer, Size inner, System.Drawing.ContentAlignment align)
        {
            int x = 0;
            int y = 0;

            if (align is System.Drawing.ContentAlignment.BottomLeft or System.Drawing.ContentAlignment.MiddleLeft or System.Drawing.ContentAlignment.TopLeft)
            {
                x = outer.X;
            }
            else if (align is System.Drawing.ContentAlignment.BottomCenter or System.Drawing.ContentAlignment.MiddleCenter or System.Drawing.ContentAlignment.TopCenter)
            {
                x = Math.Max(outer.X + ((outer.Width - inner.Width) / 2), outer.Left);
            }
            else if (align is System.Drawing.ContentAlignment.BottomRight or System.Drawing.ContentAlignment.MiddleRight or System.Drawing.ContentAlignment.TopRight)
            {
                x = outer.Right - inner.Width;
            }

            if (align is System.Drawing.ContentAlignment.TopCenter or System.Drawing.ContentAlignment.TopLeft or System.Drawing.ContentAlignment.TopRight)
            {
                y = outer.Y;
            }
            else if (align is System.Drawing.ContentAlignment.MiddleCenter or System.Drawing.ContentAlignment.MiddleLeft or System.Drawing.ContentAlignment.MiddleRight)
            {
                y = outer.Y + ((outer.Height - inner.Height) / 2);
            }
            else if (align is System.Drawing.ContentAlignment.BottomCenter or System.Drawing.ContentAlignment.BottomRight or System.Drawing.ContentAlignment.BottomLeft)
            {
                y = outer.Bottom - inner.Height;
            }

            return new Rectangle(x, y, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
        }

        #endregion

        #region Helper Functions       

        private void ShowContextMenuStrip()
        {
            if (skipNextOpen)
            {
                // we were called because we're closing the context menu strip
                // when clicking the dropdown button.
                skipNextOpen = false;
                return;
            }

            State = PushButtonState.Pressed;

#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
            if (m_SplitMenu != null)
            {
                m_SplitMenu.Show(this, new Point(0, Height));
            }
            else if (m_SplitMenuStrip != null)
            {
                m_SplitMenuStrip.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            }
#endif

#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0
            if (m_SplitMenuStrip != null)
            {
                m_SplitMenuStrip.Show(this, new Point(0, Height), ToolStripDropDownDirection.BelowRight);
            }
#endif
        }

        private void SetButtonDrawState()
        {
            if (Bounds.Contains(Parent.PointToClient(Cursor.Position)))
            {
                State = PushButtonState.Hot;
            }
            else if (Focused)
            {
                State = PushButtonState.Default;
            }
            else if (!Enabled)
            {
                State = PushButtonState.Disabled;
            }
            else
            {
                State = PushButtonState.Normal;
            }
        }

        private Size CalculateButtonAutoSize()
        {
            Size ret_size = Size.Empty;
            Size text_size = TextRenderer.MeasureText(Text, Font);
            Size image_size = Image == null ? Size.Empty : Image.Size;

            // Pad the text size
            if (Text.Length != 0)
            {
                text_size.Height += 4;
                text_size.Width += 4;
            }

            switch (TextImageRelation)
            {
                case TextImageRelation.Overlay:
                    ret_size.Height = Math.Max(Text.Length == 0 ? 0 : text_size.Height, image_size.Height);
                    ret_size.Width = Math.Max(text_size.Width, image_size.Width);
                    break;
                case TextImageRelation.ImageAboveText:
                case TextImageRelation.TextAboveImage:
                    ret_size.Height = text_size.Height + image_size.Height;
                    ret_size.Width = Math.Max(text_size.Width, image_size.Width);
                    break;
                case TextImageRelation.ImageBeforeText:
                case TextImageRelation.TextBeforeImage:
                    ret_size.Height = Math.Max(text_size.Height, image_size.Height);
                    ret_size.Width = text_size.Width + image_size.Width;
                    break;
            }

            // Pad the result
            ret_size.Height += Padding.Vertical + 6;
            ret_size.Width += Padding.Horizontal + 6;

            //pad the splitButton arrow region
            if (showSplit)
            {
                ret_size.Width += SplitSectionWidth;
            }

            return ret_size;
        }

        #endregion
    }

    #endregion
}