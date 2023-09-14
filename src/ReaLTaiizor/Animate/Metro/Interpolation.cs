#region Imports

using ReaLTaiizor.Enum.Metro;
using System;

#endregion

namespace ReaLTaiizor.Animate.Metro
{
    #region InterpolationAnimate

    public class Interpolation
    {
        public static double ValueAt(double initial, double end, double alpha, EasingType easing)
        {
            switch (easing)
            {
                default:
                    return (end * alpha) + (initial * (1 - alpha));
                case EasingType.QuadIn:
                    {
                        double factor = alpha * alpha;
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.QuadOut:
                    {
                        double factor = (2 - alpha) * alpha;
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.QuadInOut:
                    {
                        double mid = initial + ((end - initial) / 2.0);
                        if (alpha <= 0.5)
                        {
                            return ValueAt(initial, mid, alpha * 2, EasingType.QuadIn);
                        }
                        else
                        {
                            return ValueAt(mid, end, (alpha - 0.5) * 2, EasingType.QuadOut);
                        }
                    }
                case EasingType.CubeIn:
                    {
                        double factor = alpha * alpha * alpha;
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.CubeOut:
                    {
                        double factor = -(alpha - 1);
                        factor = -(factor * factor * factor) + 1;
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.CubeInOut:
                    {
                        double mid = initial + ((end - initial) / 2.0);
                        if (alpha <= 0.5)
                        {
                            return ValueAt(initial, mid, alpha * 2, EasingType.CubeIn);
                        }
                        else
                        {
                            return ValueAt(mid, end, (alpha - 0.5) * 2, EasingType.CubeOut);
                        }
                    }
                case EasingType.QuartIn:
                    {
                        double factor = alpha * alpha * alpha * alpha;
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.QuartOut:
                    {
                        double factor = -(alpha - 1);
                        factor = 1 - (factor * factor * factor * factor);
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.QuartInOut:
                    {
                        double mid = initial + ((end - initial) / 2.0);
                        if (alpha <= 0.5)
                        {
                            return ValueAt(initial, mid, alpha * 2, EasingType.QuartIn);
                        }
                        else
                        {
                            return ValueAt(mid, end, (alpha - 0.5) * 2, EasingType.QuartOut);
                        }
                    }
                case EasingType.QuintIn:
                    {
                        double factor = alpha * alpha * alpha * alpha * alpha;
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.QuintOut:
                    {
                        double factor = -(alpha - 1);
                        factor = 1 - (factor * factor * factor * factor * factor);
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.QuintInOut:
                    {
                        double mid = initial + ((end - initial) / 2.0);
                        if (alpha <= 0.5)
                        {
                            return ValueAt(initial, mid, alpha / 0.5, EasingType.QuintIn);
                        }
                        else
                        {
                            return ValueAt(mid, end, (alpha - 0.5) / 0.5, EasingType.QuintOut);
                        }
                    }
                case EasingType.SineIn:
                    {
                        double factor = 1 - Math.Cos(Math.PI / 2 * alpha);
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.SineOut:
                    {
                        double factor = Math.Sin(Math.PI / 2 * alpha);
                        return (end * factor) + (initial * (1 - factor));
                    }
                case EasingType.SineInOut:
                    {
                        if (alpha <= 0.5)
                        {
                            return ValueAt(initial, (initial + end) / 2.0, alpha * 2, EasingType.SineIn);
                        }
                        else
                        {
                            return ValueAt((initial + end) / 2.0, end, (alpha - 0.5) * 2, EasingType.SineOut);
                        }
                    }
            }
        }
    }

    #endregion
}