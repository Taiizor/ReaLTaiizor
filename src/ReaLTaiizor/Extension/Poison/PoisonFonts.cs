#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Extension.Poison
{
    #region PoisonFontsExtension

    #region General

    public enum PoisonLabelSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonLabelWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonWaterMarkWeight
    {
        Light,
        Regular,
        Bold,
        Italic
    }

    public enum PoisonTileTextSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonTileTextWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonLinkLabelSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonLinkLabelWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonComboBoxSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonComboBoxWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonDateTimeSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonDateTimeWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonTextBoxSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonTextBoxWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonProgressBarSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonProgressBarWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonTabControlSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonTabControlWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonCheckBoxSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonCheckBoxWeight
    {
        Light,
        Regular,
        Bold
    }

    public enum PoisonButtonSize
    {
        Small,
        Medium,
        Tall
    }

    public enum PoisonButtonWeight
    {
        Light,
        Regular,
        Bold
    }

    #endregion

    public static class PoisonFonts
    {
        #region Font Resolver

        internal interface IPoisonFontResolver
        {
            Font ResolveFont(string familyName, float emSize, FontStyle fontStyle, GraphicsUnit unit);
        }

        private class DefaultFontResolver : IPoisonFontResolver
        {
            public Font ResolveFont(string familyName, float emSize, FontStyle fontStyle, GraphicsUnit unit)
            {
                return new Font(familyName, emSize, fontStyle, unit);
            }
        }

        private static readonly IPoisonFontResolver FontResolver;

        static PoisonFonts()
        {
            FontResolver = new DefaultFontResolver();
        }

        #endregion

        #region Function

        public static Font DefaultLight(float size)
        {
            return FontResolver.ResolveFont("Segoe UI Light", size, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public static Font Default(float size)
        {
            return FontResolver.ResolveFont("Segoe UI", size, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public static Font DefaultBold(float size)
        {
            return FontResolver.ResolveFont("Segoe UI", size, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public static Font DefaultItalic(float size)
        {
            return FontResolver.ResolveFont("Segoe UI", size, FontStyle.Italic, GraphicsUnit.Pixel);
        }

        public static Font Title => DefaultLight(24f);

        public static Font Subtitle => Default(14f);

        public static Font Tile(PoisonTileTextSize labelSize, PoisonTileTextWeight labelWeight)
        {
            if (labelSize == PoisonTileTextSize.Small)
            {
                if (labelWeight == PoisonTileTextWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (labelWeight == PoisonTileTextWeight.Regular)
                {
                    return Default(12f);
                }

                if (labelWeight == PoisonTileTextWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (labelSize == PoisonTileTextSize.Medium)
            {
                if (labelWeight == PoisonTileTextWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (labelWeight == PoisonTileTextWeight.Regular)
                {
                    return Default(14f);
                }

                if (labelWeight == PoisonTileTextWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (labelSize == PoisonTileTextSize.Tall)
            {
                if (labelWeight == PoisonTileTextWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (labelWeight == PoisonTileTextWeight.Regular)
                {
                    return Default(18f);
                }

                if (labelWeight == PoisonTileTextWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return DefaultLight(14f);
        }

        public static Font TileCount => Default(44f);

        public static Font LinkLabel(PoisonLinkLabelSize linkSize, PoisonLinkLabelWeight linkWeight)
        {
            if (linkSize == PoisonLinkLabelSize.Small)
            {
                if (linkWeight == PoisonLinkLabelWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (linkWeight == PoisonLinkLabelWeight.Regular)
                {
                    return Default(12f);
                }

                if (linkWeight == PoisonLinkLabelWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (linkSize == PoisonLinkLabelSize.Medium)
            {
                if (linkWeight == PoisonLinkLabelWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (linkWeight == PoisonLinkLabelWeight.Regular)
                {
                    return Default(14f);
                }

                if (linkWeight == PoisonLinkLabelWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (linkSize == PoisonLinkLabelSize.Tall)
            {
                if (linkWeight == PoisonLinkLabelWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (linkWeight == PoisonLinkLabelWeight.Regular)
                {
                    return Default(18f);
                }

                if (linkWeight == PoisonLinkLabelWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return Default(12f);
        }

        public static Font ComboBox(PoisonComboBoxSize linkSize, PoisonComboBoxWeight linkWeight)
        {
            if (linkSize == PoisonComboBoxSize.Small)
            {
                if (linkWeight == PoisonComboBoxWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (linkWeight == PoisonComboBoxWeight.Regular)
                {
                    return Default(12f);
                }

                if (linkWeight == PoisonComboBoxWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (linkSize == PoisonComboBoxSize.Medium)
            {
                if (linkWeight == PoisonComboBoxWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (linkWeight == PoisonComboBoxWeight.Regular)
                {
                    return Default(14f);
                }

                if (linkWeight == PoisonComboBoxWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (linkSize == PoisonComboBoxSize.Tall)
            {
                if (linkWeight == PoisonComboBoxWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (linkWeight == PoisonComboBoxWeight.Regular)
                {
                    return Default(18f);
                }

                if (linkWeight == PoisonComboBoxWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return Default(12f);
        }

        public static Font DateTime(PoisonDateTimeSize linkSize, PoisonDateTimeWeight linkWeight)
        {
            if (linkSize == PoisonDateTimeSize.Small)
            {
                if (linkWeight == PoisonDateTimeWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (linkWeight == PoisonDateTimeWeight.Regular)
                {
                    return Default(12f);
                }

                if (linkWeight == PoisonDateTimeWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (linkSize == PoisonDateTimeSize.Medium)
            {
                if (linkWeight == PoisonDateTimeWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (linkWeight == PoisonDateTimeWeight.Regular)
                {
                    return Default(14f);
                }

                if (linkWeight == PoisonDateTimeWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (linkSize == PoisonDateTimeSize.Tall)
            {
                if (linkWeight == PoisonDateTimeWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (linkWeight == PoisonDateTimeWeight.Regular)
                {
                    return Default(18f);
                }

                if (linkWeight == PoisonDateTimeWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return Default(12f);
        }

        public static Font Label(PoisonLabelSize labelSize, PoisonLabelWeight labelWeight)
        {
            if (labelSize == PoisonLabelSize.Small)
            {
                if (labelWeight == PoisonLabelWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (labelWeight == PoisonLabelWeight.Regular)
                {
                    return Default(12f);
                }

                if (labelWeight == PoisonLabelWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (labelSize == PoisonLabelSize.Medium)
            {
                if (labelWeight == PoisonLabelWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (labelWeight == PoisonLabelWeight.Regular)
                {
                    return Default(14f);
                }

                if (labelWeight == PoisonLabelWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (labelSize == PoisonLabelSize.Tall)
            {
                if (labelWeight == PoisonLabelWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (labelWeight == PoisonLabelWeight.Regular)
                {
                    return Default(18f);
                }

                if (labelWeight == PoisonLabelWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return DefaultLight(14f);
        }

        public static Font TextBox(PoisonTextBoxSize linkSize, PoisonTextBoxWeight linkWeight)
        {
            if (linkSize == PoisonTextBoxSize.Small)
            {
                if (linkWeight == PoisonTextBoxWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (linkWeight == PoisonTextBoxWeight.Regular)
                {
                    return Default(12f);
                }

                if (linkWeight == PoisonTextBoxWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (linkSize == PoisonTextBoxSize.Medium)
            {
                if (linkWeight == PoisonTextBoxWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (linkWeight == PoisonTextBoxWeight.Regular)
                {
                    return Default(14f);
                }

                if (linkWeight == PoisonTextBoxWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (linkSize == PoisonTextBoxSize.Tall)
            {
                if (linkWeight == PoisonTextBoxWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (linkWeight == PoisonTextBoxWeight.Regular)
                {
                    return Default(18f);
                }

                if (linkWeight == PoisonTextBoxWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return Default(12f);
        }

        public static Font ProgressBar(PoisonProgressBarSize labelSize, PoisonProgressBarWeight labelWeight)
        {
            if (labelSize == PoisonProgressBarSize.Small)
            {
                if (labelWeight == PoisonProgressBarWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (labelWeight == PoisonProgressBarWeight.Regular)
                {
                    return Default(12f);
                }

                if (labelWeight == PoisonProgressBarWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (labelSize == PoisonProgressBarSize.Medium)
            {
                if (labelWeight == PoisonProgressBarWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (labelWeight == PoisonProgressBarWeight.Regular)
                {
                    return Default(14f);
                }

                if (labelWeight == PoisonProgressBarWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (labelSize == PoisonProgressBarSize.Tall)
            {
                if (labelWeight == PoisonProgressBarWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (labelWeight == PoisonProgressBarWeight.Regular)
                {
                    return Default(18f);
                }

                if (labelWeight == PoisonProgressBarWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return DefaultLight(14f);
        }

        public static Font TabControl(PoisonTabControlSize labelSize, PoisonTabControlWeight labelWeight)
        {
            if (labelSize == PoisonTabControlSize.Small)
            {
                if (labelWeight == PoisonTabControlWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (labelWeight == PoisonTabControlWeight.Regular)
                {
                    return Default(12f);
                }

                if (labelWeight == PoisonTabControlWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (labelSize == PoisonTabControlSize.Medium)
            {
                if (labelWeight == PoisonTabControlWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (labelWeight == PoisonTabControlWeight.Regular)
                {
                    return Default(14f);
                }

                if (labelWeight == PoisonTabControlWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (labelSize == PoisonTabControlSize.Tall)
            {
                if (labelWeight == PoisonTabControlWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (labelWeight == PoisonTabControlWeight.Regular)
                {
                    return Default(18f);
                }

                if (labelWeight == PoisonTabControlWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return DefaultLight(14f);
        }

        public static Font CheckBox(PoisonCheckBoxSize linkSize, PoisonCheckBoxWeight linkWeight)
        {
            if (linkSize == PoisonCheckBoxSize.Small)
            {
                if (linkWeight == PoisonCheckBoxWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (linkWeight == PoisonCheckBoxWeight.Regular)
                {
                    return Default(12f);
                }

                if (linkWeight == PoisonCheckBoxWeight.Bold)
                {
                    return DefaultBold(12f);
                }
            }
            else if (linkSize == PoisonCheckBoxSize.Medium)
            {
                if (linkWeight == PoisonCheckBoxWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (linkWeight == PoisonCheckBoxWeight.Regular)
                {
                    return Default(14f);
                }

                if (linkWeight == PoisonCheckBoxWeight.Bold)
                {
                    return DefaultBold(14f);
                }
            }
            else if (linkSize == PoisonCheckBoxSize.Tall)
            {
                if (linkWeight == PoisonCheckBoxWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (linkWeight == PoisonCheckBoxWeight.Regular)
                {
                    return Default(18f);
                }

                if (linkWeight == PoisonCheckBoxWeight.Bold)
                {
                    return DefaultBold(18f);
                }
            }

            return Default(12f);
        }

        public static Font WaterMark(PoisonLabelSize labelSize, PoisonWaterMarkWeight labelWeight)
        {
            if (labelSize == PoisonLabelSize.Small)
            {
                if (labelWeight == PoisonWaterMarkWeight.Light)
                {
                    return DefaultLight(12f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Regular)
                {
                    return Default(12f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Bold)
                {
                    return DefaultBold(12f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Italic)
                {
                    return DefaultItalic(12f);
                }
            }
            else if (labelSize == PoisonLabelSize.Medium)
            {
                if (labelWeight == PoisonWaterMarkWeight.Light)
                {
                    return DefaultLight(14f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Regular)
                {
                    return Default(14f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Bold)
                {
                    return DefaultBold(14f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Italic)
                {
                    return DefaultItalic(14f);
                }
            }
            else if (labelSize == PoisonLabelSize.Tall)
            {
                if (labelWeight == PoisonWaterMarkWeight.Light)
                {
                    return DefaultLight(18f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Regular)
                {
                    return Default(18f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Bold)
                {
                    return DefaultBold(18f);
                }

                if (labelWeight == PoisonWaterMarkWeight.Italic)
                {
                    return DefaultItalic(18f);
                }
            }

            return DefaultLight(14f);
        }

        public static Font Button(PoisonButtonSize linkSize, PoisonButtonWeight linkWeight)
        {
            if (linkSize == PoisonButtonSize.Small)
            {
                if (linkWeight == PoisonButtonWeight.Light)
                {
                    return DefaultLight(11f);
                }

                if (linkWeight == PoisonButtonWeight.Regular)
                {
                    return Default(11f);
                }

                if (linkWeight == PoisonButtonWeight.Bold)
                {
                    return DefaultBold(11f);
                }
            }
            else if (linkSize == PoisonButtonSize.Medium)
            {
                if (linkWeight == PoisonButtonWeight.Light)
                {
                    return DefaultLight(13f);
                }

                if (linkWeight == PoisonButtonWeight.Regular)
                {
                    return Default(13f);
                }

                if (linkWeight == PoisonButtonWeight.Bold)
                {
                    return DefaultBold(13f);
                }
            }
            else if (linkSize == PoisonButtonSize.Tall)
            {
                if (linkWeight == PoisonButtonWeight.Light)
                {
                    return DefaultLight(16f);
                }

                if (linkWeight == PoisonButtonWeight.Regular)
                {
                    return Default(16f);
                }

                if (linkWeight == PoisonButtonWeight.Bold)
                {
                    return DefaultBold(16f);
                }
            }

            return Default(11f);
        }

        #endregion
    }

    #endregion
}