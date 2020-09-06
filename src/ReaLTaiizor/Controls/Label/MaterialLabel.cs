#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;
using static ReaLTaiizor.Helpers.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialLabel

    public class MaterialLabel : System.Windows.Forms.Label, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialManager SkinManager => MaterialManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        private ContentAlignment _TextAlign = ContentAlignment.TopLeft;

        [DefaultValue(typeof(ContentAlignment), "TopLeft")]
        public override ContentAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
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

        private MaterialManager.fontType _fontType = MaterialManager.fontType.Body1;

        [Category("Material"),
        DefaultValue(typeof(MaterialManager.fontType), "Body1")]
        public MaterialManager.fontType FontType
        {
            get
            {
                return _fontType;
            }
            set
            {
                _fontType = value;
                Font = SkinManager.getFontByType(_fontType);
                Refresh();
            }
        }

        public MaterialLabel()
        {
            FontType = MaterialManager.fontType.Body1;
            TextAlign = ContentAlignment.TopLeft;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (AutoSize)
            {
                Size strSize;
                using (MaterialNativeTextRenderer NativeText = new MaterialNativeTextRenderer(CreateGraphics()))
                {
                    strSize = NativeText.MeasureLogString(Text, SkinManager.getLogFontByType(_fontType));
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
            switch (_TextAlign)
            {
                case ContentAlignment.TopLeft:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Left;
                    break;
                case ContentAlignment.TopCenter:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Center;
                    break;
                case ContentAlignment.TopRight:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Right;
                    break;
                case ContentAlignment.MiddleLeft:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Middle | MaterialNativeTextRenderer.TextAlignFlags.Left;
                    break;
                case ContentAlignment.MiddleCenter:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Middle | MaterialNativeTextRenderer.TextAlignFlags.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Middle | MaterialNativeTextRenderer.TextAlignFlags.Right;
                    break;
                case ContentAlignment.BottomLeft:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Bottom | MaterialNativeTextRenderer.TextAlignFlags.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Bottom | MaterialNativeTextRenderer.TextAlignFlags.Center;
                    break;
                case ContentAlignment.BottomRight:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Bottom | MaterialNativeTextRenderer.TextAlignFlags.Right;
                    break;
                default:
                    Alignment = MaterialNativeTextRenderer.TextAlignFlags.Top | MaterialNativeTextRenderer.TextAlignFlags.Left;
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor);

            // Draw Text
            using (MaterialNativeTextRenderer NativeText = new MaterialNativeTextRenderer(g))
            {
                NativeText.DrawMultilineTransparentText(
                    Text,
                    SkinManager.getLogFontByType(_fontType),
                    Enabled ? HighEmphasis ? UseAccent ?
                    SkinManager.ColorScheme.AccentColor : // High emphasis, accent
                    SkinManager.ColorScheme.PrimaryColor : // High emphasis, primary
                    SkinManager.TextHighEmphasisColor : // Normal
                    SkinManager.TextDisabledOrHintColor, // Disabled
                    ClientRectangle.Location,
                    ClientRectangle.Size,
                    Alignment);
            }
        }

        protected override void InitLayout()
        {
            Font = SkinManager.getFontByType(_fontType);
            BackColorChanged += (sender, args) => Refresh();
        }
    }

    #endregion
}