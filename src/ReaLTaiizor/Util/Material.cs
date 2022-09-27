#region Imports

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region MaterialUtil

    public enum MaterialTextShade
    {
        WHITE = 0xFFFFFF,
        BLACK = 0x212121
    }

    public sealed class MaterialNativeTextRenderer : IDisposable
    {
        #region Fields and Consts

        private static readonly int[] _charFit = new int[1];

        private static readonly int[] _charFitWidth = new int[1000];

        private static readonly Dictionary<string, Dictionary<float, Dictionary<FontStyle, IntPtr>>> _fontsCache = new(StringComparer.InvariantCultureIgnoreCase);

        private readonly Graphics _g;

        private IntPtr _hdc;

        #endregion Fields and Consts

        public MaterialNativeTextRenderer(Graphics g)
        {
            _g = g;

            IntPtr clip = _g.Clip.GetHrgn(_g);

            _hdc = _g.GetHdc();
            SetBkMode(_hdc, 1);

            SelectClipRgn(_hdc, clip);

            DeleteObject(clip);
        }

        public Size MeasureString(string str, Font font)
        {
            SetFont(font);

            Size size = new();
            if (string.IsNullOrEmpty(str))
            {
                return size;
            }

            GetTextExtentPoint32(_hdc, str, str.Length, ref size);
            return size;
        }

        public Size MeasureLogString(string str, IntPtr LogFont)
        {
            SelectObject(_hdc, LogFont);

            Size size = new();
            if (string.IsNullOrEmpty(str))
            {
                return size;
            }

            GetTextExtentPoint32(_hdc, str, str.Length, ref size);
            return size;
        }

        public Size MeasureString(string str, Font font, float maxWidth, out int charFit, out int charFitWidth)
        {
            SetFont(font);

            Size size = new();
            GetTextExtentExPoint(_hdc, str, str.Length, (int)Math.Round(maxWidth), _charFit, _charFitWidth, ref size);
            charFit = _charFit[0];
            charFitWidth = charFit > 0 ? _charFitWidth[charFit - 1] : 0;
            return size;
        }

        public void DrawString(string str, Font font, Color color, Point point)
        {
            SetFont(font);
            SetTextColor(color);

            TextOut(_hdc, point.X, point.Y, str, str.Length);
        }

        public void DrawString(string str, Font font, Color color, Rectangle rect, TextFormatFlags flags)
        {
            SetFont(font);
            SetTextColor(color);

            Rect rect2 = new(rect);
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
            BitMapInfo info = new();
            info.biSize = Marshal.SizeOf(info);
            info.biWidth = size.Width;
            info.biHeight = -size.Height;
            info.biPlanes = 1;
            info.biBitCount = 32;
            info.biCompression = 0; // BI_RGB
            IntPtr dib = CreateDIBSection(_hdc, ref info, 0, out IntPtr ppvBits, IntPtr.Zero, 0);
            SelectObject(memoryHdc, dib);

            try
            {
                // copy target background to memory HDC so when copied back it will have the proper background
                BitBlt(memoryHdc, 0, 0, size.Width, size.Height, _hdc, point.X, point.Y, 0x00CC0020);

                // Create and select font
                SelectObject(memoryHdc, fontHandle);
                SetTextColor(memoryHdc, ((color.B & 0xFF) << 16) | ((color.G & 0xFF) << 8) | color.R);

                Size strSize = new();
                Point pos = new();

                if (multilineSupport)
                {
                    TextFormatFlags fmtFlags = TextFormatFlags.WordBreak;
                    // Aligment
                    if (flags.HasFlag(TextAlignFlags.Center))
                    {
                        fmtFlags |= TextFormatFlags.Center;
                    }

                    if (flags.HasFlag(TextAlignFlags.Right))
                    {
                        fmtFlags |= TextFormatFlags.Right;
                    }

                    // Calculate the string size
                    Rect strRect = new(new Rectangle(point, size));
                    DrawText(memoryHdc, str, str.Length, ref strRect, TextFormatFlags.CalcRect | fmtFlags);

                    if (flags.HasFlag(TextAlignFlags.Middle))
                    {
                        pos.Y = ((size.Height) >> 1) - (strRect.Height >> 1);
                    }

                    if (flags.HasFlag(TextAlignFlags.Bottom))
                    {
                        pos.Y = size.Height - strRect.Height;
                    }

                    // Draw Text for multiline format
                    Rect region = new(new Rectangle(pos, size));
                    DrawText(memoryHdc, str, -1, ref region, fmtFlags);
                }
                else
                {
                    // Calculate the string size
                    GetTextExtentPoint32(memoryHdc, str, str.Length, ref strSize);
                    // Aligment
                    if (flags.HasFlag(TextAlignFlags.Center))
                    {
                        pos.X = ((size.Width) >> 1) - (strSize.Width >> 1);
                    }

                    if (flags.HasFlag(TextAlignFlags.Right))
                    {
                        pos.X = size.Width - strSize.Width;
                    }

                    if (flags.HasFlag(TextAlignFlags.Middle))
                    {
                        pos.Y = ((size.Height) >> 1) - (strSize.Height >> 1);
                    }

                    if (flags.HasFlag(TextAlignFlags.Bottom))
                    {
                        pos.Y = size.Height - strSize.Height;
                    }

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

        #region Private methods

        private void SetFont(Font font)
        {
            SelectObject(_hdc, GetCachedHFont(font));
        }

        private static IntPtr GetCachedHFont(Font font)
        {
            IntPtr hfont = IntPtr.Zero;
            if (_fontsCache.TryGetValue(font.Name, out Dictionary<float, Dictionary<FontStyle, IntPtr>> dic1))
            {
                if (dic1.TryGetValue(font.Size, out Dictionary<FontStyle, IntPtr> dic2))
                {
                    dic2.TryGetValue(font.Style, out hfont);
                }
                else
                {
                    dic1[font.Size] = new Dictionary<FontStyle, IntPtr>();
                }
            }
            else
            {
                _fontsCache[font.Name] = new Dictionary<float, Dictionary<FontStyle, IntPtr>>();
                _fontsCache[font.Name][font.Size] = new Dictionary<FontStyle, IntPtr>();
            }

            if (hfont == IntPtr.Zero)
            {
                _fontsCache[font.Name][font.Size][font.Style] = hfont = font.ToHfont();
            }

            return hfont;
        }

        private void SetTextColor(Color color)
        {
            int rgb = ((color.B & 0xFF) << 16) | ((color.G & 0xFF) << 8) | color.R;
            SetTextColor(_hdc, rgb);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
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

            public int Height => _bottom - _top;
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

        // Text Alignment Options
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

        public static AnimationRunType AnimationRun { get; set; } = AnimationRunType.Normal;

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
                return s - (Math.Sin(s * 2 * PI) / (2 * PI));
            }
        }

        public static class AnimationEaseOut
        {
            public static double CalculateProgress(double progress)
            {
                return -1 * progress * (progress - 2);
            }
        }

        public static class AnimationCustomQuadratic
        {
            public static double CalculateProgress(double progress)
            {
                double kickoff = 0.6;
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

            private readonly Timer _animationTimer = new() { Interval = 5, Enabled = false };

            public AnimationManager(bool singular = true)
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

                _animationTimer.Tick += AnimationTimerOnTick;
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
                                if (_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MIN_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                                }
                                else if (
                                    (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] == MAX_VALUE) ||
                                    (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] == MIN_VALUE) ||
                                    (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] == MIN_VALUE))
                                {
                                    _animationProgresses.RemoveAt(i);
                                    _animationSources.RemoveAt(i);
                                    _animationDirections.RemoveAt(i);
                                    _animationDatas.RemoveAt(i);
                                }
                            }
                            else
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MAX_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                                }
                            }
                        });
                    }
                    else
                    {
                        for (int i = 0; i < _animationProgresses.Count; i++)
                        {
                            UpdateProgress(i);

                            if (!Singular)
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MIN_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                                }
                                else if (
                                    (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] == MAX_VALUE) ||
                                    (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] == MIN_VALUE) ||
                                    (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] == MIN_VALUE))
                                {
                                    _animationProgresses.RemoveAt(i);
                                    _animationSources.RemoveAt(i);
                                    _animationDirections.RemoveAt(i);
                                    _animationDatas.RemoveAt(i);
                                }
                            }
                            else
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MAX_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                                }
                                else if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE)
                                {
                                    _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                                }
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
                    {
                        _animationDirections[0] = animationDirection;
                    }
                    else
                    {
                        _animationDirections.Add(animationDirection);
                    }

                    if (Singular && _animationSources.Count > 0)
                    {
                        _animationSources[0] = animationSource;
                    }
                    else
                    {
                        _animationSources.Add(animationSource);
                    }

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
                    {
                        _animationDatas[0] = data ?? new object[] { };
                    }
                    else
                    {
                        _animationDatas.Add(data ?? new object[] { });
                    }
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
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MAX_VALUE)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] != MAX_VALUE)
                                {
                                    return;
                                }
                            });
                        }
                        else
                        {
                            for (int i = 0; i < GetAnimationCount(); i++)
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MAX_VALUE)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] != MAX_VALUE)
                                {
                                    return;
                                }
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
                    _animationProgresses[index] -= (_animationDirections[index] is AnimationDirection.InOutOut or AnimationDirection.InOutRepeatingOut) ? SecondaryIncrement : Increment;
                    if (_animationProgresses[index] < MIN_VALUE)
                    {
                        _animationProgresses[index] = MIN_VALUE;

                        if (AnimationRun == AnimationRunType.Fast)
                        {
                            Parallel.For(0, GetAnimationCount(), i =>
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MIN_VALUE)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] != MIN_VALUE)
                                {
                                    return;
                                }
                            });
                        }
                        else
                        {
                            for (int i = 0; i < GetAnimationCount(); i++)
                            {
                                if (_animationDirections[i] == AnimationDirection.InOutIn)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MIN_VALUE)
                                {
                                    return;
                                }

                                if (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] != MIN_VALUE)
                                {
                                    return;
                                }
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
                {
                    throw new Exception("Animation is not set to Singular.");
                }

                if (_animationProgresses.Count == 0)
                {
                    throw new Exception("Invalid animation");
                }

                return GetProgress(0);
            }

            public double GetProgress(int index)
            {
                if (!(index < GetAnimationCount()))
                {
                    throw new IndexOutOfRangeException("Invalid animation index");
                }

                return AnimationType switch
                {
                    AnimationType.Linear => AnimationLinear.CalculateProgress(_animationProgresses[index]),
                    AnimationType.EaseInOut => AnimationEaseInOut.CalculateProgress(_animationProgresses[index]),
                    AnimationType.EaseOut => AnimationEaseOut.CalculateProgress(_animationProgresses[index]),
                    AnimationType.CustomQuadratic => AnimationCustomQuadratic.CalculateProgress(_animationProgresses[index]),
                    _ => throw new NotImplementedException("The given AnimationType is not implemented"),
                };
            }

            public Point GetSource(int index)
            {
                if (!(index < GetAnimationCount()))
                {
                    throw new IndexOutOfRangeException("Invalid animation index");
                }

                return _animationSources[index];
            }

            public Point GetSource()
            {
                if (!Singular)
                {
                    throw new Exception("Animation is not set to Singular.");
                }

                if (_animationSources.Count == 0)
                {
                    throw new Exception("Invalid animation");
                }

                return _animationSources[0];
            }

            public AnimationDirection GetDirection()
            {
                if (!Singular)
                {
                    throw new Exception("Animation is not set to Singular.");
                }

                if (_animationDirections.Count == 0)
                {
                    throw new Exception("Invalid animation");
                }

                return _animationDirections[0];
            }

            public AnimationDirection GetDirection(int index)
            {
                if (!(index < _animationDirections.Count))
                {
                    throw new IndexOutOfRangeException("Invalid animation index");
                }

                return _animationDirections[index];
            }

            public object[] GetData()
            {
                if (!Singular)
                {
                    throw new Exception("Animation is not set to Singular.");
                }

                if (_animationDatas.Count == 0)
                {
                    throw new Exception("Invalid animation");
                }

                return _animationDatas[0];
            }

            public object[] GetData(int index)
            {
                if (!(index < _animationDatas.Count))
                {
                    throw new IndexOutOfRangeException("Invalid animation index");
                }

                return _animationDatas[index];
            }

            public int GetAnimationCount()
            {
                return _animationProgresses.Count;
            }

            public void SetProgress(double progress)
            {
                if (!Singular)
                {
                    throw new Exception("Animation is not set to Singular.");
                }

                if (_animationProgresses.Count == 0)
                {
                    throw new Exception("Invalid animation");
                }

                _animationProgresses[0] = progress;
            }

            public void SetDirection(AnimationDirection direction)
            {
                if (!Singular)
                {
                    throw new Exception("Animation is not set to Singular.");
                }

                if (_animationProgresses.Count == 0)
                {
                    throw new Exception("Invalid animation");
                }

                _animationDirections[0] = direction;
            }

            public void SetData(object[] data)
            {
                if (!Singular)
                {
                    throw new Exception("Animation is not set to Singular.");
                }

                if (_animationDatas.Count == 0)
                {
                    throw new Exception("Invalid animation");
                }

                _animationDatas[0] = data;
            }
        }
    }

    public class MaterialMouseMessageFilter : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public static event MouseEventHandler MouseMove;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                if (MouseMove != null)
                {
                    int x = Control.MousePosition.X, y = Control.MousePosition.Y;

                    MouseMove(null, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
                }
            }
            return false;
        }
    }

    public class MaterialMouseWheelRedirector : IMessageFilter
    {
        private static MaterialMouseWheelRedirector instance = null;
        private static bool _active = false;

        public static bool Active
        {
            set
            {
                if (_active != value)
                {
                    _active = value;
                    if (_active)
                    {
                        if (instance == null)
                        {
                            instance = new MaterialMouseWheelRedirector();
                        }

                        Application.AddMessageFilter(instance);
                    }
                    else if (instance != null)
                    {
                        Application.RemoveMessageFilter(instance);
                    }
                }
            }
            get => _active;
        }

        public static void Attach(Control control)
        {
            if (!_active)
            {
                Active = true;
            }

            control.MouseEnter += instance.ControlMouseEnter;
            control.MouseLeave += instance.ControlMouseLeaveOrDisposed;
            control.Disposed += instance.ControlMouseLeaveOrDisposed;
        }

        public static void Detach(Control control)
        {
            if (instance == null)
            {
                return;
            }

            control.MouseEnter -= instance.ControlMouseEnter;
            control.MouseLeave -= instance.ControlMouseLeaveOrDisposed;
            control.Disposed -= instance.ControlMouseLeaveOrDisposed;
            if (instance.currentControl == control)
            {
                instance.currentControl = null;
            }
        }

        public MaterialMouseWheelRedirector()
        {
        }

        private Control currentControl;

        private void ControlMouseEnter(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            if (!control.Focused)
            {
                currentControl = control;
            }
            else
            {
                currentControl = null;
            }
        }

        private void ControlMouseLeaveOrDisposed(object sender, System.EventArgs e)
        {
            if (currentControl == sender)
            {
                currentControl = null;
            }
        }

        private const int WM_MOUSEWHEEL = 0x20A;
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (currentControl != null && m.Msg == WM_MOUSEWHEEL)
            {
                SendMessage(currentControl.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }
            else
            {
                return false;
            }
        }

        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }

    #endregion
}