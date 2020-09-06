#region Imports

using System;
using System.Linq;
using System.Drawing;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using ReaLTaiizor.Controls;
using System.Windows.Forms;
using ReaLTaiizor.Extensions;
using System.Threading.Tasks;
using ReaLTaiizor.Properties;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Utils
{
    #region MaterialLibrary

    public enum MaterialTextShade
    {
        WHITE = 0xFFFFFF,
        BLACK = 0x212121
    }

    public class MaterialManager
    {
        private static MaterialManager _instance;

        private readonly List<Forms.MaterialForm> _formsToManage = new List<Forms.MaterialForm>();

        public delegate void SkinManagerEventHandler(object sender);

        public event SkinManagerEventHandler ColorSchemeChanged;

        public event SkinManagerEventHandler ThemeChanged;

        public bool EnforceBackcolorOnAllComponents = true;

        public static MaterialManager Instance => _instance ?? (_instance = new MaterialManager());

        public int FORM_PADDING = 14;

        // Constructor
        private MaterialManager()
        {
            Theme = Themes.LIGHT;
            ColorScheme = new MaterialColorScheme(MaterialPrimary.Indigo500, MaterialPrimary.Indigo700, MaterialPrimary.Indigo100, MaterialAccent.Pink200, MaterialTextShade.WHITE);

            // Add font to system table in memory and save the font family
            addFont(Resources.Roboto_Thin);
            addFont(Resources.Roboto_Light);
            addFont(Resources.Roboto_Regular);
            addFont(Resources.Roboto_Medium);
            addFont(Resources.Roboto_Bold);
            addFont(Resources.Roboto_Black);

            RobotoFontFamilies = new Dictionary<string, FontFamily>();
            foreach (FontFamily ff in privateFontCollection.Families.ToArray())
                RobotoFontFamilies.Add(ff.Name.Replace(' ', '_'), ff);

            // create and save font handles for GDI
            logicalFonts = new Dictionary<string, IntPtr>(18);
            logicalFonts.Add("H1", createLogicalFont("Roboto Light", 96, MaterialNativeTextRenderer.logFontWeight.FW_LIGHT));
            logicalFonts.Add("H2", createLogicalFont("Roboto Light", 60, MaterialNativeTextRenderer.logFontWeight.FW_LIGHT));
            logicalFonts.Add("H3", createLogicalFont("Roboto", 48, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("H4", createLogicalFont("Roboto", 34, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("H5", createLogicalFont("Roboto", 24, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("H6", createLogicalFont("Roboto Medium", 20, MaterialNativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("Subtitle1", createLogicalFont("Roboto", 16, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Subtitle2", createLogicalFont("Roboto Medium", 14, MaterialNativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("Body1", createLogicalFont("Roboto", 16, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Body2", createLogicalFont("Roboto", 14, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Button", createLogicalFont("Roboto Medium", 14, MaterialNativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("Caption", createLogicalFont("Roboto", 12, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("Overline", createLogicalFont("Roboto", 10, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            // Logical fonts for textbox animation
            logicalFonts.Add("textBox16", createLogicalFont("Roboto", 16, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("textBox15", createLogicalFont("Roboto", 15, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("textBox14", createLogicalFont("Roboto", 14, MaterialNativeTextRenderer.logFontWeight.FW_REGULAR));
            logicalFonts.Add("textBox13", createLogicalFont("Roboto Medium", 13, MaterialNativeTextRenderer.logFontWeight.FW_MEDIUM));
            logicalFonts.Add("textBox12", createLogicalFont("Roboto Medium", 12, MaterialNativeTextRenderer.logFontWeight.FW_MEDIUM));
        }

        // Destructor
        ~MaterialManager()
        {
            // RemoveFontMemResourceEx
            foreach (IntPtr handle in logicalFonts.Values)
                MaterialNativeTextRenderer.DeleteObject(handle);
        }

        private Themes _theme;
        public Themes Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                UpdateBackgrounds();
                ThemeChanged?.Invoke(this);
            }
        }

        private MaterialColorScheme _colorScheme;
        public MaterialColorScheme ColorScheme
        {
            get { return _colorScheme; }
            set
            {
                _colorScheme = value;
                UpdateBackgrounds();
                ColorSchemeChanged?.Invoke(this);
            }
        }

        public enum Themes : byte
        {
            LIGHT,
            DARK
        }

        // Text
        private static readonly Color TEXT_HIGH_EMPHASIS_LIGHT = Color.FromArgb(222, 255, 255, 255); // Alpha 87%

        private static readonly Brush TEXT_HIGH_EMPHASIS_LIGHT_BRUSH = new SolidBrush(TEXT_HIGH_EMPHASIS_LIGHT);
        private static readonly Color TEXT_HIGH_EMPHASIS_DARK = Color.FromArgb(222, 0, 0, 0); // Alpha 87%
        private static readonly Brush TEXT_HIGH_EMPHASIS_DARK_BRUSH = new SolidBrush(TEXT_HIGH_EMPHASIS_DARK);

        private static readonly Color TEXT_MEDIUM_EMPHASIS_LIGHT = Color.FromArgb(153, 255, 255, 255); // Alpha 60%
        private static readonly Brush TEXT_MEDIUM_EMPHASIS_LIGHT_BRUSH = new SolidBrush(TEXT_MEDIUM_EMPHASIS_LIGHT);
        private static readonly Color TEXT_MEDIUM_EMPHASIS_DARK = Color.FromArgb(153, 0, 0, 0); // Alpha 60%
        private static readonly Brush TEXT_MEDIUM_EMPHASIS_DARK_BRUSH = new SolidBrush(TEXT_MEDIUM_EMPHASIS_DARK);

        private static readonly Color TEXT_DISABLED_OR_HINT_LIGHT = Color.FromArgb(97, 255, 255, 255); // Alpha 38%
        private static readonly Brush TEXT_DISABLED_OR_HINT_LIGHT_BRUSH = new SolidBrush(TEXT_DISABLED_OR_HINT_LIGHT);
        private static readonly Color TEXT_DISABLED_OR_HINT_DARK = Color.FromArgb(97, 0, 0, 0); // Alpha 38%
        private static readonly Brush TEXT_DISABLED_OR_HINT_DARK_BRUSH = new SolidBrush(TEXT_DISABLED_OR_HINT_DARK);

        // Dividers and thin lines
        private static readonly Color DIVIDERS_LIGHT = Color.FromArgb(30, 255, 255, 255); // Alpha 30%

        private static readonly Brush DIVIDERS_LIGHT_BRUSH = new SolidBrush(DIVIDERS_LIGHT);
        private static readonly Color DIVIDERS_DARK = Color.FromArgb(30, 0, 0, 0); // Alpha 30%
        private static readonly Brush DIVIDERS_DARK_BRUSH = new SolidBrush(DIVIDERS_DARK);
        private static readonly Color DIVIDERS_ALTERNATIVE_LIGHT = Color.FromArgb(153, 255, 255, 255); // Alpha 60%
        private static readonly Brush DIVIDERS_ALTERNATIVE_LIGHT_BRUSH = new SolidBrush(DIVIDERS_ALTERNATIVE_LIGHT);
        private static readonly Color DIVIDERS_ALTERNATIVE_DARK = Color.FromArgb(153, 0, 0, 0); // Alpha 60%
        private static readonly Brush DIVIDERS_ALTERNATIVE_DARK_BRUSH = new SolidBrush(DIVIDERS_ALTERNATIVE_DARK);

        // Checkbox / Radio / Switches
        private static readonly Color CHECKBOX_OFF_LIGHT = Color.FromArgb(138, 0, 0, 0);

        private static readonly Brush CHECKBOX_OFF_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_LIGHT);
        private static readonly Color CHECKBOX_OFF_DARK = Color.FromArgb(179, 255, 255, 255);
        private static readonly Brush CHECKBOX_OFF_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DARK);
        private static readonly Color CHECKBOX_OFF_DISABLED_LIGHT = Color.FromArgb(66, 0, 0, 0);
        private static readonly Brush CHECKBOX_OFF_DISABLED_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_LIGHT);
        private static readonly Color CHECKBOX_OFF_DISABLED_DARK = Color.FromArgb(77, 255, 255, 255);
        private static readonly Brush CHECKBOX_OFF_DISABLED_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_DARK);

        // Switch specific
        private static readonly Color SWITCH_OFF_THUMB_LIGHT = Color.FromArgb(255, 255, 255, 255);

        private static readonly Color SWITCH_OFF_THUMB_DARK = Color.FromArgb(255, 190, 190, 190);
        private static readonly Color SWITCH_OFF_TRACK_LIGHT = Color.FromArgb(100, 0, 0, 0);
        private static readonly Color SWITCH_OFF_TRACK_DARK = Color.FromArgb(100, 255, 255, 255);
        private static readonly Color SWITCH_OFF_DISABLED_THUMB_LIGHT = Color.FromArgb(255, 230, 230, 230);
        private static readonly Color SWITCH_OFF_DISABLED_THUMB_DARK = Color.FromArgb(255, 150, 150, 150);

        // Generic back colors - for user controls
        private static readonly Color BACKGROUND_LIGHT = Color.FromArgb(255, 255, 255, 255);

        private static readonly Brush BACKGROUND_LIGHT_BRUSH = new SolidBrush(BACKGROUND_LIGHT);
        private static readonly Color BACKGROUND_DARK = Color.FromArgb(255, 80, 80, 80);
        private static readonly Brush BACKGROUND_DARK_BRUSH = new SolidBrush(BACKGROUND_DARK);
        private static readonly Color BACKGROUND_ALTERNATIVE_LIGHT = Color.FromArgb(10, 0, 0, 0);
        private static readonly Brush BACKGROUND_ALTERNATIVE_LIGHT_BRUSH = new SolidBrush(BACKGROUND_ALTERNATIVE_LIGHT);
        private static readonly Color BACKGROUND_ALTERNATIVE_DARK = Color.FromArgb(10, 255, 255, 255);
        private static readonly Brush BACKGROUND_ALTERNATIVE_DARK_BRUSH = new SolidBrush(BACKGROUND_ALTERNATIVE_DARK);
        private static readonly Color BACKGROUND_HOVER_LIGHT = Color.FromArgb(20, 0, 0, 0);
        private static readonly Brush BACKGROUND_HOVER_LIGHT_BRUSH = new SolidBrush(BACKGROUND_HOVER_LIGHT);
        private static readonly Color BACKGROUND_HOVER_DARK = Color.FromArgb(20, 255, 255, 255);
        private static readonly Brush BACKGROUND_HOVER_DARK_BRUSH = new SolidBrush(BACKGROUND_HOVER_DARK);
        private static readonly Color BACKGROUND_FOCUS_LIGHT = Color.FromArgb(30, 0, 0, 0);
        private static readonly Brush BACKGROUND_FOCUS_LIGHT_BRUSH = new SolidBrush(BACKGROUND_FOCUS_LIGHT);
        private static readonly Color BACKGROUND_FOCUS_DARK = Color.FromArgb(30, 255, 255, 255);
        private static readonly Brush BACKGROUND_FOCUS_DARK_BRUSH = new SolidBrush(BACKGROUND_FOCUS_DARK);
        private static readonly Color BACKGROUND_DISABLED_LIGHT = Color.FromArgb(25, 0, 0, 0);
        private static readonly Brush BACKGROUND_DISABLED_LIGHT_BRUSH = new SolidBrush(BACKGROUND_DISABLED_LIGHT);
        private static readonly Color BACKGROUND_DISABLED_DARK = Color.FromArgb(25, 255, 255, 255);
        private static readonly Brush BACKGROUND_DISABLED_DARK_BRUSH = new SolidBrush(BACKGROUND_DISABLED_DARK);

        // Backdrop colors - for containers, like forms or panels
        private static readonly Color BACKDROP_LIGHT = Color.FromArgb(255, 242, 242, 242);

        private static readonly Brush BACKDROP_LIGHT_BRUSH = new SolidBrush(BACKGROUND_LIGHT);
        private static readonly Color BACKDROP_DARK = Color.FromArgb(255, 50, 50, 50);
        private static readonly Brush BACKDROP_DARK_BRUSH = new SolidBrush(BACKGROUND_DARK);

        // Getters - Using these makes handling the dark theme switching easier
        // Text
        public Color TextHighEmphasisColor => Theme == Themes.LIGHT ? TEXT_HIGH_EMPHASIS_DARK : TEXT_HIGH_EMPHASIS_LIGHT;

        public Brush TextHighEmphasisBrush => Theme == Themes.LIGHT ? TEXT_HIGH_EMPHASIS_DARK_BRUSH : TEXT_HIGH_EMPHASIS_LIGHT_BRUSH;
        public Color TextMediumEmphasisColor => Theme == Themes.LIGHT ? TEXT_MEDIUM_EMPHASIS_DARK : TEXT_MEDIUM_EMPHASIS_LIGHT;
        public Brush TextMediumEmphasisBrush => Theme == Themes.LIGHT ? TEXT_MEDIUM_EMPHASIS_DARK_BRUSH : TEXT_MEDIUM_EMPHASIS_LIGHT_BRUSH;
        public Color TextDisabledOrHintColor => Theme == Themes.LIGHT ? TEXT_DISABLED_OR_HINT_DARK : TEXT_DISABLED_OR_HINT_LIGHT;
        public Brush TextDisabledOrHintBrush => Theme == Themes.LIGHT ? TEXT_DISABLED_OR_HINT_DARK_BRUSH : TEXT_DISABLED_OR_HINT_LIGHT_BRUSH;

        // Divider
        public Color DividersColor => Theme == Themes.LIGHT ? DIVIDERS_DARK : DIVIDERS_LIGHT;

        public Brush DividersBrush => Theme == Themes.LIGHT ? DIVIDERS_DARK_BRUSH : DIVIDERS_LIGHT_BRUSH;
        public Color DividersAlternativeColor => Theme == Themes.LIGHT ? DIVIDERS_ALTERNATIVE_DARK : DIVIDERS_ALTERNATIVE_LIGHT;
        public Brush DividersAlternativeBrush => Theme == Themes.LIGHT ? DIVIDERS_ALTERNATIVE_DARK_BRUSH : DIVIDERS_ALTERNATIVE_LIGHT_BRUSH;

        // Checkbox / Radio / Switch
        public Color CheckboxOffColor => Theme == Themes.LIGHT ? CHECKBOX_OFF_LIGHT : CHECKBOX_OFF_DARK;

        public Brush CheckboxOffBrush => Theme == Themes.LIGHT ? CHECKBOX_OFF_LIGHT_BRUSH : CHECKBOX_OFF_DARK_BRUSH;
        public Color CheckBoxOffDisabledColor => Theme == Themes.LIGHT ? CHECKBOX_OFF_DISABLED_LIGHT : CHECKBOX_OFF_DISABLED_DARK;
        public Brush CheckBoxOffDisabledBrush => Theme == Themes.LIGHT ? CHECKBOX_OFF_DISABLED_LIGHT_BRUSH : CHECKBOX_OFF_DISABLED_DARK_BRUSH;

        // Switch
        public Color SwitchOffColor => Theme == Themes.LIGHT ? CHECKBOX_OFF_DARK : CHECKBOX_OFF_LIGHT;

        public Color SwitchOffThumbColor => Theme == Themes.LIGHT ? SWITCH_OFF_THUMB_LIGHT : SWITCH_OFF_THUMB_DARK;
        public Color SwitchOffTrackColor => Theme == Themes.LIGHT ? SWITCH_OFF_TRACK_LIGHT : SWITCH_OFF_TRACK_DARK;
        public Color SwitchOffDisabledThumbColor => Theme == Themes.LIGHT ? SWITCH_OFF_DISABLED_THUMB_LIGHT : SWITCH_OFF_DISABLED_THUMB_DARK;

        // Control Back colors
        public Color BackgroundColor => Theme == Themes.LIGHT ? BACKGROUND_LIGHT : BACKGROUND_DARK;

        public Brush BackgroundBrush => Theme == Themes.LIGHT ? BACKGROUND_LIGHT_BRUSH : BACKGROUND_DARK_BRUSH;
        public Color BackgroundAlternativeColor => Theme == Themes.LIGHT ? BACKGROUND_ALTERNATIVE_LIGHT : BACKGROUND_ALTERNATIVE_DARK;
        public Brush BackgroundAlternativeBrush => Theme == Themes.LIGHT ? BACKGROUND_ALTERNATIVE_LIGHT_BRUSH : BACKGROUND_ALTERNATIVE_DARK_BRUSH;
        public Color BackgroundDisabledColor => Theme == Themes.LIGHT ? BACKGROUND_DISABLED_LIGHT : BACKGROUND_DISABLED_DARK;
        public Brush BackgroundDisabledBrush => Theme == Themes.LIGHT ? BACKGROUND_DISABLED_LIGHT_BRUSH : BACKGROUND_DISABLED_DARK_BRUSH;
        public Color BackgroundHoverColor => Theme == Themes.LIGHT ? BACKGROUND_HOVER_LIGHT : BACKGROUND_HOVER_DARK;
        public Brush BackgroundHoverBrush => Theme == Themes.LIGHT ? BACKGROUND_HOVER_LIGHT_BRUSH : BACKGROUND_HOVER_DARK_BRUSH;
        public Color BackgroundFocusColor => Theme == Themes.LIGHT ? BACKGROUND_FOCUS_LIGHT : BACKGROUND_FOCUS_DARK;
        public Brush BackgroundFocusBrush => Theme == Themes.LIGHT ? BACKGROUND_FOCUS_LIGHT_BRUSH : BACKGROUND_FOCUS_DARK_BRUSH;

        // Backdrop color
        public Color BackdropColor => Theme == Themes.LIGHT ? BACKDROP_LIGHT : BACKDROP_DARK;

        public Brush BackdropBrush => Theme == Themes.LIGHT ? BACKDROP_LIGHT_BRUSH : BACKDROP_DARK_BRUSH;

        // Font Handling
        public enum fontType
        {
            H1,
            H2,
            H3,
            H4,
            H5,
            H6,
            Subtitle1,
            Subtitle2,
            Body1,
            Body2,
            Button,
            Caption,
            Overline
        }

        public Font getFontByType(fontType type)
        {
            switch (type)
            {
                case fontType.H1:
                    return new Font(RobotoFontFamilies["Roboto_Light"], 96f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.H2:
                    return new Font(RobotoFontFamilies["Roboto_Light"], 60f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.H3:
                    return new Font(RobotoFontFamilies["Roboto"], 48f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.H4:
                    return new Font(RobotoFontFamilies["Roboto"], 34f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.H5:
                    return new Font(RobotoFontFamilies["Roboto"], 24f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.H6:
                    return new Font(RobotoFontFamilies["Roboto_Medium"], 20f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.Subtitle1:
                    return new Font(RobotoFontFamilies["Roboto"], 16f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Subtitle2:
                    return new Font(RobotoFontFamilies["Roboto_Medium"], 14f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.Body1:
                    return new Font(RobotoFontFamilies["Roboto"], 14f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Body2:
                    return new Font(RobotoFontFamilies["Roboto"], 12f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Button:
                    return new Font(RobotoFontFamilies["Roboto"], 14f, FontStyle.Bold, GraphicsUnit.Pixel);

                case fontType.Caption:
                    return new Font(RobotoFontFamilies["Roboto"], 12f, FontStyle.Regular, GraphicsUnit.Pixel);

                case fontType.Overline:
                    return new Font(RobotoFontFamilies["Roboto"], 10f, FontStyle.Regular, GraphicsUnit.Pixel);
            }
            return new Font(RobotoFontFamilies["Roboto"], 14f, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public IntPtr getTextBoxFontBySize(int size)
        {
            string name = "textBox" + Math.Min(16, Math.Max(12, size)).ToString();
            return logicalFonts[name];
        }

        public IntPtr getLogFontByType(fontType type)
        {
            return logicalFonts[Enum.GetName(typeof(fontType), type)];
        }

        // Font stuff
        private Dictionary<string, IntPtr> logicalFonts;

        private Dictionary<string, FontFamily> RobotoFontFamilies;

        private PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        private void addFont(byte[] fontdata)
        {
            // Add font to system table in memory
            int dataLength = fontdata.Length;

            IntPtr ptrFont = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontdata, 0, ptrFont, dataLength);

            // GDI Font
            uint cFonts = 0;
            MaterialNativeTextRenderer.AddFontMemResourceEx(fontdata, dataLength, IntPtr.Zero, out _);

            // GDI+ Font
            privateFontCollection.AddMemoryFont(ptrFont, dataLength);
        }

        private IntPtr createLogicalFont(string fontName, int size, MaterialNativeTextRenderer.logFontWeight weight)
        {
            // Logical font:
            MaterialNativeTextRenderer.LogFont lfont = new MaterialNativeTextRenderer.LogFont();
            lfont.lfFaceName = fontName;
            lfont.lfHeight = -size;
            lfont.lfWeight = (int)weight;
            return MaterialNativeTextRenderer.CreateFontIndirect(lfont);
        }

        // Dyanmic Themes
        public void AddFormToManage(Forms.MaterialForm materialForm)
        {
            _formsToManage.Add(materialForm);
            UpdateBackgrounds();
        }

        public void RemoveFormToManage(Forms.MaterialForm materialForm)
        {
            _formsToManage.Remove(materialForm);
        }

        private void UpdateBackgrounds()
        {
            var newBackColor = BackdropColor;
            foreach (var materialForm in _formsToManage)
            {
                materialForm.BackColor = newBackColor;
                UpdateControlBackColor(materialForm, newBackColor);
            }
        }

        private void UpdateControlBackColor(Control controlToUpdate, Color newBackColor)
        {
            // No control
            if (controlToUpdate == null) return;

            // Control's Context menu
            if (controlToUpdate.ContextMenuStrip != null) UpdateToolStrip(controlToUpdate.ContextMenuStrip, newBackColor);

            // Material Tabcontrol pages
            if (controlToUpdate is System.Windows.Forms.TabPage)
                ((System.Windows.Forms.TabPage)controlToUpdate).BackColor = newBackColor;

            // Material Divider
            else if (controlToUpdate is MaterialDivider)
                controlToUpdate.BackColor = DividersColor;

            // Other Material control
            else if (controlToUpdate.IsMaterialControl())
            {
                controlToUpdate.BackColor = newBackColor;
                controlToUpdate.ForeColor = TextHighEmphasisColor;
            }

            // Other Generic control not part of Material
            else if (EnforceBackcolorOnAllComponents && controlToUpdate.HasProperty("BackColor") && !controlToUpdate.IsMaterialControl() && controlToUpdate.Parent != null)
            {
                controlToUpdate.BackColor = controlToUpdate.Parent.BackColor;
                controlToUpdate.ForeColor = TextHighEmphasisColor;
                controlToUpdate.Font = getFontByType(fontType.Body1);
            }

            // Recursive call to control's children
            foreach (Control control in controlToUpdate.Controls)
                UpdateControlBackColor(control, newBackColor);
        }

        private void UpdateToolStrip(ToolStrip toolStrip, Color newBackColor)
        {
            if (toolStrip == null)
                return;

            toolStrip.BackColor = newBackColor;
            foreach (ToolStripItem control in toolStrip.Items)
            {
                control.BackColor = newBackColor;
                if (control is MaterialToolStripMenuItem && (control as MaterialToolStripMenuItem).HasDropDown)
                    UpdateToolStrip((control as MaterialToolStripMenuItem).DropDown, newBackColor);
            }
        }
    }

    public sealed class MaterialNativeTextRenderer : IDisposable
    {
        #region Fields and Consts

        private static readonly int[] _charFit = new int[1];

        private static readonly int[] _charFitWidth = new int[1000];

        private static readonly Dictionary<string, Dictionary<float, Dictionary<FontStyle, IntPtr>>> _fontsCache = new Dictionary<string, Dictionary<float, Dictionary<FontStyle, IntPtr>>>(StringComparer.InvariantCultureIgnoreCase);

        private readonly Graphics _g;

        private IntPtr _hdc;

        #endregion Fields and Consts

        public MaterialNativeTextRenderer(Graphics g)
        {
            _g = g;

            var clip = _g.Clip.GetHrgn(_g);

            _hdc = _g.GetHdc();
            SetBkMode(_hdc, 1);

            SelectClipRgn(_hdc, clip);

            DeleteObject(clip);
        }

        public Size MeasureString(string str, Font font)
        {
            SetFont(font);

            var size = new Size();
            GetTextExtentPoint32(_hdc, str, str.Length, ref size);
            return size;
        }

        public Size MeasureLogString(string str, IntPtr LogFont)
        {
            SelectObject(_hdc, LogFont);

            var size = new Size();
            GetTextExtentPoint32(_hdc, str, str.Length, ref size);
            return size;
        }

        public Size MeasureString(string str, Font font, float maxWidth, out int charFit, out int charFitWidth)
        {
            SetFont(font);

            var size = new Size();
            GetTextExtentExPoint(_hdc, str, str.Length, (int)Math.Round(maxWidth), _charFit, _charFitWidth, ref size);
            charFit = _charFit[0];
            charFitWidth = charFit > 0 ? _charFitWidth[charFit - 1] : 0;
            return size;
        }

        public void DrawString(String str, Font font, Color color, Point point)
        {
            SetFont(font);
            SetTextColor(color);

            TextOut(_hdc, point.X, point.Y, str, str.Length);
        }

        public void DrawString(String str, Font font, Color color, Rectangle rect, TextFormatFlags flags)
        {
            SetFont(font);
            SetTextColor(color);

            var rect2 = new Rect(rect);
            DrawText(_hdc, str, str.Length, ref rect2, (uint)flags);
        }

        public void DrawTransparentText(string str, Font font, Color color, Point point, Size size, TextAlignFlags flags)
        {
            DrawTransparentText(GetCachedHFont(font), str, color, point, size, flags, false);
        }

        public void DrawTransparentText(string str, IntPtr LogFont, Color color, Point point, Size size, TextAlignFlags flags)
        {
            DrawTransparentText(LogFont, str, color, point, size, flags, false);
        }

        public void DrawMultilineTransparentText(string str, Font font, Color color, Point point, Size size, TextAlignFlags flags)
        {
            DrawTransparentText(GetCachedHFont(font), str, color, point, size, flags, true);
        }

        public void DrawMultilineTransparentText(string str, IntPtr LogFont, Color color, Point point, Size size, TextAlignFlags flags)
        {
            DrawTransparentText(LogFont, str, color, point, size, flags, true);
        }

        private void DrawTransparentText(IntPtr fontHandle, string str, Color color, Point point, Size size, TextAlignFlags flags, bool multilineSupport)
        {
            // Create a memory DC so we can work off-screen
            IntPtr memoryHdc = CreateCompatibleDC(_hdc);
            SetBkMode(memoryHdc, 1);

            // Create a device-independent bitmap and select it into our DC
            var info = new BitMapInfo();
            info.biSize = Marshal.SizeOf(info);
            info.biWidth = size.Width;
            info.biHeight = -size.Height;
            info.biPlanes = 1;
            info.biBitCount = 32;
            info.biCompression = 0; // BI_RGB
            IntPtr ppvBits;
            IntPtr dib = CreateDIBSection(_hdc, ref info, 0, out ppvBits, IntPtr.Zero, 0);
            SelectObject(memoryHdc, dib);

            try
            {
                // copy target background to memory HDC so when copied back it will have the proper background
                BitBlt(memoryHdc, 0, 0, size.Width, size.Height, _hdc, point.X, point.Y, 0x00CC0020);

                // Create and select font
                SelectObject(memoryHdc, fontHandle);
                SetTextColor(memoryHdc, (color.B & 0xFF) << 16 | (color.G & 0xFF) << 8 | color.R);

                Size strSize = new Size();
                Point pos = new Point();

                if (multilineSupport)
                {
                    TextFormatFlags fmtFlags = TextFormatFlags.WordBreak;
                    // Aligment
                    if (flags.HasFlag(TextAlignFlags.Center))
                        fmtFlags |= TextFormatFlags.Center;
                    if (flags.HasFlag(TextAlignFlags.Right))
                        fmtFlags |= TextFormatFlags.Right;

                    // Calculate the string size
                    Rect strRect = new Rect(new Rectangle(point, size));
                    DrawText(memoryHdc, str, str.Length, ref strRect, TextFormatFlags.CalcRect | fmtFlags);

                    if (flags.HasFlag(TextAlignFlags.Middle))
                        pos.Y = ((size.Height) >> 1) - (strRect.Height >> 1);
                    if (flags.HasFlag(TextAlignFlags.Bottom))
                        pos.Y = (size.Height) - (strRect.Height);

                    // Draw Text for multiline format
                    Rect region = new Rect(new Rectangle(pos, size));
                    DrawText(memoryHdc, str, str.Length, ref region, fmtFlags);
                }
                else
                {
                    // Calculate the string size
                    GetTextExtentPoint32(memoryHdc, str, str.Length, ref strSize);
                    // Aligment
                    if (flags.HasFlag(TextAlignFlags.Center))
                        pos.X = ((size.Width) >> 1) - (strSize.Width >> 1);
                    if (flags.HasFlag(TextAlignFlags.Right))
                        pos.X = (size.Width) - (strSize.Width);

                    if (flags.HasFlag(TextAlignFlags.Middle))
                        pos.Y = ((size.Height) >> 1) - (strSize.Height >> 1);
                    if (flags.HasFlag(TextAlignFlags.Bottom))
                        pos.Y = (size.Height) - (strSize.Height);

                    // Draw text to memory HDC
                    TextOut(memoryHdc, pos.X, pos.Y, str, str.Length);
                }

                // copy from memory HDC to normal HDC with alpha blend so achieve the transparent text
                AlphaBlend(_hdc, point.X, point.Y, size.Width, size.Height, memoryHdc, 0, 0, size.Width, size.Height, new BlendFunction(color.A));
            }
            finally
            {
                DeleteObject(dib);
                DeleteDC(memoryHdc);
            }
        }

        public void Dispose()
        {
            if (_hdc != IntPtr.Zero)
            {
                SelectClipRgn(_hdc, IntPtr.Zero);
                _g.ReleaseHdc(_hdc);
                _hdc = IntPtr.Zero;
            }
        }

        #region Private Methods

        private void SetFont(Font font)
        {
            SelectObject(_hdc, GetCachedHFont(font));
        }

        private static IntPtr GetCachedHFont(Font font)
        {
            IntPtr hfont = IntPtr.Zero;
            Dictionary<float, Dictionary<FontStyle, IntPtr>> dic1;
            if (_fontsCache.TryGetValue(font.Name, out dic1))
            {
                Dictionary<FontStyle, IntPtr> dic2;
                if (dic1.TryGetValue(font.Size, out dic2))
                    dic2.TryGetValue(font.Style, out hfont);
                else
                    dic1[font.Size] = new Dictionary<FontStyle, IntPtr>();
            }
            else
            {
                _fontsCache[font.Name] = new Dictionary<float, Dictionary<FontStyle, IntPtr>>();
                _fontsCache[font.Name][font.Size] = new Dictionary<FontStyle, IntPtr>();
            }

            if (hfont == IntPtr.Zero)
                _fontsCache[font.Name][font.Size][font.Style] = hfont = font.ToHfont();

            return hfont;
        }

        private void SetTextColor(Color color)
        {
            int rgb = (color.B & 0xFF) << 16 | (color.G & 0xFF) << 8 | color.R;
            SetTextColor(_hdc, rgb);
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int DrawText(IntPtr hdc, string lpchText, int cchText, ref Rect lprc, TextFormatFlags dwDTFormat);

        [DllImport("gdi32.dll")]
        private static extern int SetBkMode(IntPtr hdc, int mode);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiObj);

        [DllImport("gdi32.dll")]
        private static extern int SetTextColor(IntPtr hdc, int color);

        [DllImport("gdi32.dll", EntryPoint = "GetTextExtentPoint32W")]
        private static extern int GetTextExtentPoint32(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string str, int len, ref Size size);

        [DllImport("gdi32.dll", EntryPoint = "GetTextExtentExPointW")]
        private static extern bool GetTextExtentExPoint(IntPtr hDc, [MarshalAs(UnmanagedType.LPWStr)] string str, int nLength, int nMaxExtent, int[] lpnFit, int[] alpDx, ref Size size);

        [DllImport("gdi32.dll", EntryPoint = "TextOutW")]
        private static extern bool TextOut(IntPtr hdc, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string str, int len);

        [DllImport("gdi32.dll")]
        public static extern int SetTextAlign(IntPtr hdc, uint fMode);

        [DllImport("user32.dll", EntryPoint = "DrawTextW")]
        private static extern int DrawText(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string str, int len, ref Rect rect, uint uFormat);

        [DllImport("gdi32.dll")]
        private static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFontIndirect([In, MarshalAs(UnmanagedType.LPStruct)] LogFont lplf);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        private static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, BlendFunction blendFunction);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BitMapInfo pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LogFont
        {
            public int lfHeight = 0;
            public int lfWidth = 0;
            public int lfEscapement = 0;
            public int lfOrientation = 0;
            public int lfWeight = 0;
            public byte lfItalic = 0;
            public byte lfUnderline = 0;
            public byte lfStrikeOut = 0;
            public byte lfCharSet = 0;
            public byte lfOutPrecision = 0;
            public byte lfClipPrecision = 0;
            public byte lfQuality = 0;
            public byte lfPitchAndFamily = 0;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName = string.Empty;
        }

        private struct Rect
        {
            private int _left;
            private int _top;
            private int _right;
            private int _bottom;

            public Rect(Rectangle r)
            {
                _left = r.Left;
                _top = r.Top;
                _bottom = r.Bottom;
                _right = r.Right;
            }

            public int Height
            {
                get
                {
                    return _bottom - _top;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BlendFunction
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public BlendFunction(byte alpha)
            {
                BlendOp = 0;
                BlendFlags = 0;
                AlphaFormat = 0;
                SourceConstantAlpha = alpha;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BitMapInfo
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        [Flags]
        public enum TextFormatFlags : uint
        {
            Default = 0x00000000,
            Center = 0x00000001,
            Right = 0x00000002,
            VCenter = 0x00000004,
            Bottom = 0x00000008,
            WordBreak = 0x00000010,
            SingleLine = 0x00000020,
            ExpandTabs = 0x00000040,
            TabStop = 0x00000080,
            NoClip = 0x00000100,
            ExternalLeading = 0x00000200,
            CalcRect = 0x00000400,
            NoPrefix = 0x00000800,
            Internal = 0x00001000,
            EditControl = 0x00002000,
            PathEllipsis = 0x00004000,
            EndEllipsis = 0x00008000,
            ModifyString = 0x00010000,
            RtlReading = 0x00020000,
            WordEllipsis = 0x00040000,
            NoFullWidthCharBreak = 0x00080000,
            HidePrefix = 0x00100000,
            ProfixOnly = 0x00200000,
        }

        private const int DT_TOP = 0x00000000;

        private const int DT_LEFT = 0x00000000;

        private const int DT_CENTER = 0x00000001;

        private const int DT_RIGHT = 0x00000002;

        private const int DT_VCENTER = 0x00000004;

        private const int DT_BOTTOM = 0x00000008;

        private const int DT_WORDBREAK = 0x00000010;

        private const int DT_SINGLELINE = 0x00000020;

        private const int DT_EXPANDTABS = 0x00000040;

        private const int DT_TABSTOP = 0x00000080;

        private const int DT_NOCLIP = 0x00000100;

        private const int DT_EXTERNALLEADING = 0x00000200;

        private const int DT_CALCRECT = 0x00000400;

        private const int DT_NOPREFIX = 0x00000800;

        private const int DT_INTERNAL = 0x00001000;

        private const int DT_EDITCONTROL = 0x00002000;

        private const int DT_PATH_ELLIPSIS = 0x00004000;

        private const int DT_END_ELLIPSIS = 0x00008000;

        private const int DT_MODIFYSTRING = 0x00010000;

        private const int DT_RTLREADING = 0x00020000;

        private const int DT_WORD_ELLIPSIS = 0x00040000;

        private const int DT_NOFULLWIDTHCHARBREAK = 0x00080000;

        private const int DT_HIDEPREFIX = 0x00100000;

        private const int DT_PREFIXONLY = 0x00200000;

        [Flags]
        public enum TextAlignFlags : uint
        {
            Left = 1 << 0,
            Center = 1 << 1,
            Right = 1 << 2,
            Top = 1 << 3,
            Middle = 1 << 4,
            Bottom = 1 << 5
        }

        public enum logFontWeight : int
        {
            FW_DONTCARE = 0,
            FW_THIN = 100,
            FW_EXTRALIGHT = 200,
            FW_ULTRALIGHT = 200,
            FW_LIGHT = 300,
            FW_NORMAL = 400,
            FW_REGULAR = 400,
            FW_MEDIUM = 500,
            FW_SEMIBOLD = 600,
            FW_DEMIBOLD = 600,
            FW_BOLD = 700,
            FW_EXTRABOLD = 800,
            FW_ULTRABOLD = 800,
            FW_HEAVY = 900,
            FW_BLACK = 900,
        }

        #endregion Private methods
    }

    public class MaterialAnimations
    {
        public enum AnimationRunType
        {
            Normal,
            Fast
        }

        internal enum AnimationType
        {
            Linear,
            EaseOut,
            EaseInOut,
            CustomQuadratic
        }

        internal enum AnimationDirection
        {
            In,
            Out,
            InOutIn,
            InOutOut,
            InOutRepeatingIn,
            InOutRepeatingOut
        }

        internal static class AnimationLinear
        {
            public static double CalculateProgress(double progress)
            {
                return progress;
            }
        }

        internal static class AnimationEaseInOut
        {
            public static double PI = Math.PI;

            public static double PI_HALF = Math.PI / 2;

            public static double CalculateProgress(double progress)
            {
                return EaseInOut(progress);
            }

            private static double EaseInOut(double s)
            {
                return s - Math.Sin(s * 2 * PI) / (2 * PI);
            }
        }

        public static class AnimationEaseOut
        {
            public static double CalculateProgress(double progress)
            {
                return -1 * progress * (progress - 2);
            }
        }

        private static AnimationRunType _AnimationRun = AnimationRunType.Normal;
        public static AnimationRunType AnimationRun
        {
            get { return _AnimationRun; }
            set { _AnimationRun = value; }
        }

        public static class AnimationCustomQuadratic
        {
            public static double CalculateProgress(double progress)
            {
                var kickoff = 0.6;
                return 1 - Math.Cos((Math.Max(progress, kickoff) - kickoff) * Math.PI / (2 - (2 * kickoff)));
            }
        }

        internal class AnimationManager
        {
            public bool InterruptAnimation { get; set; }

            public double Increment { get; set; }

            public double SecondaryIncrement { get; set; }

            public AnimationType AnimationType { get; set; }

            public bool Singular { get; set; }

            public delegate void AnimationFinished(object sender);

            public event AnimationFinished OnAnimationFinished;

            public delegate void AnimationProgress(object sender);

            public event AnimationProgress OnAnimationProgress;

            private readonly List<double> _animationProgresses;

            private readonly List<Point> _animationSources;

            private readonly List<AnimationDirection> _animationDirections;

            private readonly List<object[]> _animationDatas;

            private const double MIN_VALUE = 0.00;

            private const double MAX_VALUE = 1.00;

            private readonly Timer _animationTimer = new Timer
            {
                Interval = 5,
                Enabled = false
            };

            public AnimationManager(bool singular = true)
            {
                try
                {
                    _animationProgresses = new List<double>();
                    _animationSources = new List<Point>();
                    _animationDirections = new List<AnimationDirection>();
                    _animationDatas = new List<object[]>();

                    Increment = 0.03;
                    SecondaryIncrement = 0.03;
                    AnimationType = AnimationType.Linear;
                    InterruptAnimation = true;
                    Singular = singular;

                    if (Singular)
                    {
                        _animationProgresses.Add(0);
                        _animationSources.Add(new Point(0, 0));
                        _animationDirections.Add(AnimationDirection.In);
                    }

                    /*
                        _animationTimer.Tick += delegate (object _s, EventArgs _e)
                        {
                            AnimationTimerOnTick(_s, _e);
                        };
                    */
                    _animationTimer.Tick += AnimationTimerOnTick;
                }
                catch
                {
                    //
                }
            }

            private void AnimationTimerOnTick(object sender, EventArgs eventArgs)
            {
                try
                {
                    if (AnimationRun == AnimationRunType.Fast)
                    {
                        Parallel.For(0, _animationProgresses.Count, i =>
                        {
                            UpdateProgress(i);

                            if (!Singular)
                            {
                                if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MIN_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                                else if ((_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] == MAX_VALUE) || (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] == MIN_VALUE) || (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] == MIN_VALUE))
                                {
                                    _animationProgresses.RemoveAt(i);
                                    _animationSources.RemoveAt(i);
                                    _animationDirections.RemoveAt(i);
                                    _animationDatas.RemoveAt(i);
                                }
                            }
                            else
                            {
                                if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MAX_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                            }
                        });
                    }
                    else
                    {
                        for (var i = 0; i < _animationProgresses.Count; i++)
                        {
                            UpdateProgress(i);

                            if (!Singular)
                            {
                                if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MIN_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                                else if ((_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] == MAX_VALUE) || (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] == MIN_VALUE) || (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] == MIN_VALUE))
                                {
                                    _animationProgresses.RemoveAt(i);
                                    _animationSources.RemoveAt(i);
                                    _animationDirections.RemoveAt(i);
                                    _animationDatas.RemoveAt(i);
                                }
                            }
                            else
                            {
                                if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MAX_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                            }
                        }
                    }

                    OnAnimationProgress?.Invoke(this);
                }
                catch
                {
                    //
                }
            }

            public bool IsAnimating()
            {
                return _animationTimer.Enabled;
            }

            public void StartNewAnimation(AnimationDirection animationDirection, object[] data = null)
            {
                StartNewAnimation(animationDirection, new Point(0, 0), data);
            }

            public void StartNewAnimation(AnimationDirection animationDirection, Point animationSource, object[] data = null)
            {
                if (!IsAnimating() || InterruptAnimation)
                {
                    if (Singular && _animationDirections.Count > 0)
                        _animationDirections[0] = animationDirection;
                    else
                        _animationDirections.Add(animationDirection);

                    if (Singular && _animationSources.Count > 0)
                        _animationSources[0] = animationSource;
                    else
                        _animationSources.Add(animationSource);

                    if (!(Singular && _animationProgresses.Count > 0))
                    {
                        switch (_animationDirections[_animationDirections.Count - 1])
                        {
                            case AnimationDirection.InOutRepeatingIn:
                            case AnimationDirection.InOutIn:
                            case AnimationDirection.In:
                                _animationProgresses.Add(MIN_VALUE);
                                break;
                            case AnimationDirection.InOutRepeatingOut:
                            case AnimationDirection.InOutOut:
                            case AnimationDirection.Out:
                                _animationProgresses.Add(MAX_VALUE);
                                break;
                            default:
                                throw new Exception("Invalid AnimationDirection");
                        }
                    }

                    if (Singular && _animationDatas.Count > 0)
                        _animationDatas[0] = data ?? new object[] { };
                    else
                        _animationDatas.Add(data ?? new object[] { });
                }

                _animationTimer.Start();
            }

            public void UpdateProgress(int index)
            {
                switch (_animationDirections[index])
                {
                    case AnimationDirection.InOutRepeatingIn:
                    case AnimationDirection.InOutIn:
                    case AnimationDirection.In:
                        IncrementProgress(index);
                        break;
                    case AnimationDirection.InOutRepeatingOut:
                    case AnimationDirection.InOutOut:
                    case AnimationDirection.Out:
                        DecrementProgress(index);
                        break;
                    default:
                        throw new Exception("No AnimationDirection has been set");
                }
            }

            private void IncrementProgress(int index)
            {
                try
                {
                    _animationProgresses[index] += Increment;
                    if (_animationProgresses[index] > MAX_VALUE)
                    {
                        _animationProgresses[index] = MAX_VALUE;

                        if (AnimationRun == AnimationRunType.Fast)
                        {
                            Parallel.For(0, GetAnimationCount(), i =>
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MAX_VALUE)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] != MAX_VALUE)
                                    return;
                            });
                        }
                        else
                        {
                            for (int i = 0; i < GetAnimationCount(); i++)
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MAX_VALUE)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] != MAX_VALUE)
                                    return;
                            }
                        }

                        _animationTimer.Stop();
                        OnAnimationFinished?.Invoke(this);
                    }
                }
                catch
                {
                    //
                }
            }

            private void DecrementProgress(int index)
            {
                try
                {
                    _animationProgresses[index] -= (_animationDirections[index] == AnimationDirection.InOutOut || _animationDirections[index] == AnimationDirection.InOutRepeatingOut) ? SecondaryIncrement : Increment;
                    if (_animationProgresses[index] < MIN_VALUE)
                    {
                        _animationProgresses[index] = MIN_VALUE;

                        if (AnimationRun == AnimationRunType.Fast)
                        {
                            Parallel.For(0, GetAnimationCount(), i =>
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MIN_VALUE)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] != MIN_VALUE)
                                    return;
                            });
                        }
                        else
                        {
                            for (var i = 0; i < GetAnimationCount(); i++)
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MIN_VALUE)
                                    return;

                                if (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] != MIN_VALUE)
                                    return;
                            }
                        }

                        _animationTimer.Stop();
                        OnAnimationFinished?.Invoke(this);
                    }
                }
                catch
                {
                    //
                }
            }

            public double GetProgress()
            {
                if (!Singular)
                    throw new Exception("Animation is not set to Singular.");

                if (_animationProgresses.Count == 0)
                    throw new Exception("Invalid animation");

                return GetProgress(0);
            }

            public double GetProgress(int index)
            {
                if (!(index < GetAnimationCount()))
                    throw new IndexOutOfRangeException("Invalid animation index");

                switch (AnimationType)
                {
                    case AnimationType.Linear:
                        return AnimationLinear.CalculateProgress(_animationProgresses[index]);
                    case AnimationType.EaseInOut:
                        return AnimationEaseInOut.CalculateProgress(_animationProgresses[index]);
                    case AnimationType.EaseOut:
                        return AnimationEaseOut.CalculateProgress(_animationProgresses[index]);
                    case AnimationType.CustomQuadratic:
                        return AnimationCustomQuadratic.CalculateProgress(_animationProgresses[index]);
                    default:
                        throw new NotImplementedException("The given AnimationType is not implemented");
                }
            }

            public Point GetSource(int index)
            {
                if (!(index < GetAnimationCount()))
                    throw new IndexOutOfRangeException("Invalid animation index");

                return _animationSources[index];
            }

            public Point GetSource()
            {
                if (!Singular)
                    throw new Exception("Animation is not set to Singular.");

                if (_animationSources.Count == 0)
                    throw new Exception("Invalid animation");

                return _animationSources[0];
            }

            public AnimationDirection GetDirection()
            {
                if (!Singular)
                    throw new Exception("Animation is not set to Singular.");

                if (_animationDirections.Count == 0)
                    throw new Exception("Invalid animation");

                return _animationDirections[0];
            }

            public AnimationDirection GetDirection(int index)
            {
                if (!(index < _animationDirections.Count))
                    throw new IndexOutOfRangeException("Invalid animation index");

                return _animationDirections[index];
            }

            public object[] GetData()
            {
                if (!Singular)
                    throw new Exception("Animation is not set to Singular.");

                if (_animationDatas.Count == 0)
                    throw new Exception("Invalid animation");

                return _animationDatas[0];
            }

            public object[] GetData(int index)
            {
                if (!(index < _animationDatas.Count))
                    throw new IndexOutOfRangeException("Invalid animation index");

                return _animationDatas[index];
            }

            public int GetAnimationCount()
            {
                return _animationProgresses.Count;
            }

            public void SetProgress(double progress)
            {
                if (!Singular)
                    throw new Exception("Animation is not set to Singular.");

                if (_animationProgresses.Count == 0)
                    throw new Exception("Invalid animation");

                _animationProgresses[0] = progress;
            }

            public void SetDirection(AnimationDirection direction)
            {
                if (!Singular)
                    throw new Exception("Animation is not set to Singular.");

                if (_animationProgresses.Count == 0)
                    throw new Exception("Invalid animation");

                _animationDirections[0] = direction;
            }

            public void SetData(object[] data)
            {
                if (!Singular)
                    throw new Exception("Animation is not set to Singular.");

                if (_animationDatas.Count == 0)
                    throw new Exception("Invalid animation");

                _animationDatas[0] = data;
            }
        }
    }

    #endregion
}