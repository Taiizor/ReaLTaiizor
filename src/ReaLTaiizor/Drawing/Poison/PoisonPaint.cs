#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Extension.Poison;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Drawing.Poison
{
    #region PoisonPaintDrawing

    public class PoisonPaintEventArgs : EventArgs
    {
        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }
        public Graphics Graphics { get; private set; }

        public PoisonPaintEventArgs(Color backColor, Color foreColor, Graphics g)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            Graphics = g;
        }
    }

    public sealed class PoisonPaint
    {
        public sealed class BorderColor
        {
            public static Color Form(ThemeStyle theme)
            {
                if (theme == ThemeStyle.Dark)
                {
                    return Color.FromArgb(68, 68, 68);
                }

                return Color.FromArgb(204, 204, 204);
            }

            public static class Button
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(68, 68, 68);
                    }

                    return Color.FromArgb(204, 204, 204);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(102, 102, 102);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(238, 238, 238);
                    }

                    return Color.FromArgb(51, 51, 51);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(109, 109, 109);
                    }

                    return Color.FromArgb(155, 155, 155);
                }
            }

            public static class CheckBox
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(204, 204, 204);
                    }

                    return Color.FromArgb(51, 51, 51);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(85, 85, 85);
                    }

                    return Color.FromArgb(204, 204, 204);
                }
            }

            public static class ComboBox
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(204, 204, 204);
                    }

                    return Color.FromArgb(51, 51, 51);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(85, 85, 85);
                    }

                    return Color.FromArgb(204, 204, 204);
                }
            }

            public static class ProgressBar
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(68, 68, 68);
                    }

                    return Color.FromArgb(204, 204, 204);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(68, 68, 68);
                    }

                    return Color.FromArgb(204, 204, 204);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(68, 68, 68);
                    }

                    return Color.FromArgb(204, 204, 204);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(109, 109, 109);
                    }

                    return Color.FromArgb(155, 155, 155);
                }
            }

            public static class TabControl
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(68, 68, 68);
                    }

                    return Color.FromArgb(204, 204, 204);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(68, 68, 68);
                    }

                    return Color.FromArgb(204, 204, 204);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(68, 68, 68);
                    }

                    return Color.FromArgb(204, 204, 204);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(109, 109, 109);
                    }

                    return Color.FromArgb(155, 155, 155);
                }
            }
        }

        public sealed class BackColor
        {
            public static Color Form(ThemeStyle theme)
            {
                if (theme == ThemeStyle.Dark)
                {
                    return Color.FromArgb(17, 17, 17);
                }

                return Color.FromArgb(255, 255, 255);
            }

            public sealed class Button
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(34, 34, 34);
                    }

                    return Color.FromArgb(238, 238, 238);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(102, 102, 102);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(238, 238, 238);
                    }

                    return Color.FromArgb(51, 51, 51);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(80, 80, 80);
                    }

                    return Color.FromArgb(204, 204, 204);
                }
            }

            public sealed class TrackBar
            {
                public sealed class Thumb
                {
                    public static Color Normal(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(153, 153, 153);
                        }

                        return Color.FromArgb(102, 102, 102);
                    }

                    public static Color Hover(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(204, 204, 204);
                        }

                        return Color.FromArgb(17, 17, 17);
                    }

                    public static Color Press(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(204, 204, 204);
                        }

                        return Color.FromArgb(17, 17, 17);
                    }

                    public static Color Disabled(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(85, 85, 85);
                        }

                        return Color.FromArgb(179, 179, 179);
                    }
                }

                public sealed class Bar
                {
                    public static Color Normal(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(51, 51, 51);
                        }

                        return Color.FromArgb(204, 204, 204);
                    }

                    public static Color Hover(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(51, 51, 51);
                        }

                        return Color.FromArgb(204, 204, 204);
                    }

                    public static Color Press(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(51, 51, 51);
                        }

                        return Color.FromArgb(204, 204, 204);
                    }

                    public static Color Disabled(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(34, 34, 34);
                        }

                        return Color.FromArgb(230, 230, 230);
                    }
                }
            }

            public sealed class ScrollBar
            {
                public sealed class Thumb
                {
                    public static Color Normal(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(51, 51, 51);
                        }

                        return Color.FromArgb(221, 221, 221);
                    }

                    public static Color Hover(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(204, 204, 204);
                        }

                        return Color.FromArgb(96, 96, 96);
                    }

                    public static Color Press(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(204, 204, 204);
                        }

                        return Color.FromArgb(96, 96, 96);
                    }

                    public static Color Disabled(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(51, 51, 51);
                        }

                        return Color.FromArgb(221, 221, 221);
                    }
                }

                public sealed class Bar
                {
                    public static Color Normal(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(38, 38, 38);
                        }

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Hover(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(38, 38, 38);
                        }

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Press(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(38, 38, 38);
                        }

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Disabled(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(38, 38, 38);
                        }

                        return Color.FromArgb(234, 234, 234);
                    }
                }
            }

            public sealed class ProgressBar
            {
                public sealed class Bar
                {
                    public static Color Normal(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(38, 38, 38);
                        }

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Hover(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(38, 38, 38);
                        }

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Press(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(38, 38, 38);
                        }

                        return Color.FromArgb(234, 234, 234);
                    }

                    public static Color Disabled(ThemeStyle theme)
                    {
                        if (theme == ThemeStyle.Dark)
                        {
                            return Color.FromArgb(51, 51, 51);
                        }

                        return Color.FromArgb(221, 221, 221);
                    }
                }
            }
        }

        public sealed class ForeColor
        {
            public sealed class Button
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(204, 204, 204);
                    }

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(17, 17, 17);
                    }

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(17, 17, 17);
                    }

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(109, 109, 109);
                    }

                    return Color.FromArgb(136, 136, 136);
                }
            }

            public static Color Title(ThemeStyle theme)
            {
                if (theme == ThemeStyle.Dark)
                {
                    return Color.FromArgb(255, 255, 255);
                }

                return Color.FromArgb(0, 0, 0);
            }

            public sealed class Tile
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(255, 255, 255);
                    }

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(255, 255, 255);
                    }

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(255, 255, 255);
                    }

                    return Color.FromArgb(255, 255, 255);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(209, 209, 209);
                    }

                    return Color.FromArgb(209, 209, 209);
                }
            }

            public sealed class Link
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(93, 93, 93);
                    }

                    return Color.FromArgb(128, 128, 128);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(93, 93, 93);
                    }

                    return Color.FromArgb(128, 128, 128);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(51, 51, 51);
                    }

                    return Color.FromArgb(209, 209, 209);
                }
            }

            public sealed class Label
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(51, 51, 51);
                    }

                    return Color.FromArgb(209, 209, 209);
                }
            }

            public sealed class CheckBox
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(17, 17, 17);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(93, 93, 93);
                    }

                    return Color.FromArgb(136, 136, 136);
                }
            }

            public sealed class ComboBox
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Hover(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(17, 17, 17);
                }

                public static Color Press(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(153, 153, 153);
                    }

                    return Color.FromArgb(153, 153, 153);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(93, 93, 93);
                    }

                    return Color.FromArgb(136, 136, 136);
                }
            }

            public sealed class ProgressBar
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(51, 51, 51);
                    }

                    return Color.FromArgb(209, 209, 209);
                }
            }

            public sealed class TabControl
            {
                public static Color Normal(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(170, 170, 170);
                    }

                    return Color.FromArgb(0, 0, 0);
                }

                public static Color Disabled(ThemeStyle theme)
                {
                    if (theme == ThemeStyle.Dark)
                    {
                        return Color.FromArgb(51, 51, 51);
                    }

                    return Color.FromArgb(209, 209, 209);
                }
            }
        }

        #region Helper Methods

        public static Color GetStyleColor(ColorStyle style)
        {
            return style switch
            {
                ColorStyle.Black => PoisonColors.Black,
                ColorStyle.White => PoisonColors.White,
                ColorStyle.Silver => PoisonColors.Silver,
                ColorStyle.Blue => PoisonColors.Blue,
                ColorStyle.Green => PoisonColors.Green,
                ColorStyle.Lime => PoisonColors.Lime,
                ColorStyle.Teal => PoisonColors.Teal,
                ColorStyle.Orange => PoisonColors.Orange,
                ColorStyle.Brown => PoisonColors.Brown,
                ColorStyle.Pink => PoisonColors.Pink,
                ColorStyle.Magenta => PoisonColors.Magenta,
                ColorStyle.Purple => PoisonColors.Purple,
                ColorStyle.Red => PoisonColors.Red,
                ColorStyle.Yellow => PoisonColors.Yellow,
                _ => PoisonColors.Blue,
            };
        }

        public static SolidBrush GetStyleBrush(ColorStyle style)
        {
            return style switch
            {
                ColorStyle.Black => PoisonBrushes.Black,
                ColorStyle.White => PoisonBrushes.White,
                ColorStyle.Silver => PoisonBrushes.Silver,
                ColorStyle.Blue => PoisonBrushes.Blue,
                ColorStyle.Green => PoisonBrushes.Green,
                ColorStyle.Lime => PoisonBrushes.Lime,
                ColorStyle.Teal => PoisonBrushes.Teal,
                ColorStyle.Orange => PoisonBrushes.Orange,
                ColorStyle.Brown => PoisonBrushes.Brown,
                ColorStyle.Pink => PoisonBrushes.Pink,
                ColorStyle.Magenta => PoisonBrushes.Magenta,
                ColorStyle.Purple => PoisonBrushes.Purple,
                ColorStyle.Red => PoisonBrushes.Red,
                ColorStyle.Yellow => PoisonBrushes.Yellow,
                _ => PoisonBrushes.Blue,
            };
        }

        public static Pen GetStylePen(ColorStyle style)
        {
            return style switch
            {
                ColorStyle.Black => PoisonPens.Black,
                ColorStyle.White => PoisonPens.White,
                ColorStyle.Silver => PoisonPens.Silver,
                ColorStyle.Blue => PoisonPens.Blue,
                ColorStyle.Green => PoisonPens.Green,
                ColorStyle.Lime => PoisonPens.Lime,
                ColorStyle.Teal => PoisonPens.Teal,
                ColorStyle.Orange => PoisonPens.Orange,
                ColorStyle.Brown => PoisonPens.Brown,
                ColorStyle.Pink => PoisonPens.Pink,
                ColorStyle.Magenta => PoisonPens.Magenta,
                ColorStyle.Purple => PoisonPens.Purple,
                ColorStyle.Red => PoisonPens.Red,
                ColorStyle.Yellow => PoisonPens.Yellow,
                _ => PoisonPens.Blue,
            };
        }

        public static StringFormat GetStringFormat(ContentAlignment textAlign)
        {
            StringFormat stringFormat = new()
            {
                Trimming = StringTrimming.EllipsisCharacter
            };

            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomLeft:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomCenter:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
            }

            return stringFormat;
        }

        public static TextFormatFlags GetTextFormatFlags(ContentAlignment textAlign)
        {
            return GetTextFormatFlags(textAlign, false);
        }

        public static TextFormatFlags GetTextFormatFlags(ContentAlignment textAlign, bool WrapToLine)
        {
            TextFormatFlags controlFlags = TextFormatFlags.Default;

            switch (WrapToLine)
            {
                case true:
                    controlFlags = TextFormatFlags.WordBreak;
                    break;
                case false:
                    controlFlags = TextFormatFlags.EndEllipsis;
                    break;
            }
            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopCenter:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopRight:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;

                case ContentAlignment.MiddleLeft:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleCenter:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.MiddleRight:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;

                case ContentAlignment.BottomLeft:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomCenter:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomRight:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
            }

            return controlFlags;
        }

        #endregion
    }

    #endregion
}