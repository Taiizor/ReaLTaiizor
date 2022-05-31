#region Imports

using ReaLTaiizor.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotColorPicker

    public class ParrotColorPicker : Control
    {
        public ParrotColorPicker()
        {
            base.Size = new Size(133, 133);
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color picker image")]
        public Image PickerImage
        {
            get => image;
            set
            {
                image = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The selected color")]
        public Color SelectedColor
        {
            get => selectedColor;
            set => selectedColor = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Returns the selected color hex value")]
        public string SelectedColorHex => ColorTranslator.ToHtml(selectedColor);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Returns the selected color rgb value")]
        public string SelectedColorRgb => $"{ColorTranslator.FromHtml(SelectedColorHex).R}, {ColorTranslator.FromHtml(SelectedColorHex).G}, {ColorTranslator.FromHtml(SelectedColorHex).B}";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the selected color preview")]
        public bool ShowColorPreview
        {
            get => showColorPreview;
            set => showColorPreview = value;
        }

        private void GetColor(int x, int y)
        {
            if (x > 1 && y > 1 && x < base.Width - 2 && y < base.Height - 2)
            {
                Bitmap bitmap = (Bitmap)new Bitmap(image, base.Width - 3, base.Height - 3).Clone();
                if (bitmap.GetPixel(x - 1, y - 1).A > 0)
                {
                    try
                    {
                        selectedColor = bitmap.GetPixel(x, y);
                    }
                    catch
                    {
                    }
                }
                bitmap.Dispose();
                x1 = x;
                y1 = y;
                OnSelectedColorChanged();
                base.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isSelectingColor = true;
            GetColor(e.X, e.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isSelectingColor = false;
            base.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            base.CreateGraphics();
            if (isSelectingColor)
            {
                GetColor(e.X, e.Y);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BufferedGraphicsContext bufferedGraphicsContext = BufferedGraphicsManager.Current;
            bufferedGraphicsContext.MaximumBuffer = new Size(base.Width + 1, base.Height + 1);
            bufferedGraphics = bufferedGraphicsContext.Allocate(base.CreateGraphics(), base.ClientRectangle);
            bufferedGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            bufferedGraphics.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            bufferedGraphics.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            bufferedGraphics.Graphics.Clear(BackColor);
            bufferedGraphics.Graphics.DrawImage(new Bitmap(image, base.Width - 3, base.Height - 3), 1, 1);
            if (isSelectingColor && showColorPreview)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(selectedColor), new RectangleF(x1 - 10, y1 - 10, 20f, 20f));
            }
            bufferedGraphics.Render(e.Graphics);
        }

        public event EventHandler SelectedColorChanged;

        protected virtual void OnSelectedColorChanged()
        {
            SelectedColorChanged?.Invoke(this, new EventArgs());
        }

        private BufferedGraphics bufferedGraphics;

        public Image image = Resources.color_picker;

        private Color selectedColor;

        private bool showColorPreview = true;

        private int x1;

        private int y1;

        private bool isSelectingColor;
    }

    #endregion
}