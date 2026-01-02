#region Imports

using ReaLTaiizor.Helper;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialLabel

    public class MaterialLabel : Label, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        private ContentAlignment _TextAlign = ContentAlignment.TopLeft;

        [DefaultValue(typeof(ContentAlignment), "TopLeft")]
        public override ContentAlignment TextAlign
        {
            get => _TextAlign;
            set
            {
                _TextAlign = value;
                updateAligment();
                Invalidate();
            }
        }

        [Category("Material"),
        DefaultValue(false)]
        public bool HighEmphasis { get; set; }

        [Category("Material"),
        DefaultValue(false)]
        public bool UseAccent { get; set; }

        [Category("Material"),
        DefaultValue(typeof(MaterialSkinManager.FontType), "Body1")]
        public MaterialSkinManager.FontType FontType
        {
            get;
            set
            {
                field = value;
                Font = SkinManager.GetFontByType(field);
                Refresh();
            }
        } = MaterialSkinManager.FontType.Body1;

        public MaterialLabel()
        {
            FontType = MaterialSkinManager.FontType.Body1;
            TextAlign = ContentAlignment.TopLeft;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (AutoSize)
            {
                Size strSize;
                using (MaterialNativeTextRenderer NativeText = new(CreateGraphics()))
                {
                    strSize = NativeText.MeasureLogString(Text, SkinManager.GetLogFontByType(FontType));
                    strSize.Width += 1; // necessary to avoid a bug when autosize = true
                }
                return strSize;
            }
            else
            {
                return proposedSize;
            }
        }

        private MaterialNativeTextRenderer.TextAlignFlags Alignment;

        private void updateAligment()
        {
            Alignment = _TextAlign switch
            {
                ContentAlignment.TopLeft => MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Left,
                ContentAlignment.TopCenter => MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Center,
                ContentAlignment.TopRight => MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Right,
                ContentAlignment.MiddleLeft => MaterialNativeTextRenderer.TextAlignFlags.Middle | MaterialNativeTextRenderer.TextAlignFlags.Left,
                ContentAlignment.MiddleCenter => MaterialNativeTextRenderer.TextAlignFlags.Middle | MaterialNativeTextRenderer.TextAlignFlags.Center,
                ContentAlignment.MiddleRight => MaterialNativeTextRenderer.TextAlignFlags.Middle | MaterialNativeTextRenderer.TextAlignFlags.Right,
                ContentAlignment.BottomLeft => MaterialNativeTextRenderer.TextAlignFlags.Bottom | MaterialNativeTextRenderer.TextAlignFlags.Left,
                ContentAlignment.BottomCenter => MaterialNativeTextRenderer.TextAlignFlags.Bottom | MaterialNativeTextRenderer.TextAlignFlags.Center,
                ContentAlignment.BottomRight => MaterialNativeTextRenderer.TextAlignFlags.Bottom | MaterialNativeTextRenderer.TextAlignFlags.Right,
                _ => MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Left,
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor);

            // Draw Text
            using MaterialNativeTextRenderer NativeText = new(g);
            NativeText.DrawMultilineTransparentText(
                Text,
                SkinManager.GetLogFontByType(FontType),
                Enabled ? HighEmphasis ? UseAccent ?
                SkinManager.ColorScheme.AccentColor : // High emphasis, accent
                (SkinManager.Theme == MaterialSkinManager.Themes.LIGHT) ?
                SkinManager.ColorScheme.PrimaryColor : // High emphasis, primary Light theme
                SkinManager.ColorScheme.PrimaryColor.Lighten(0.25f) : // High emphasis, primary Dark theme
                SkinManager.TextHighEmphasisColor : // Normal
                SkinManager.TextDisabledOrHintColor, // Disabled
                ClientRectangle.Location,
                ClientRectangle.Size,
                Alignment);
        }

        protected override void InitLayout()
        {
            Font = SkinManager.GetFontByType(FontType);
        }
    }

    #endregion
}