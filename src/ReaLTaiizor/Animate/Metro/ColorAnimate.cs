#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Animate.Metro
{
    #region ColorAnimateAnimate

    public class ColorAnimate : Animate<Color>
    {
        public override Color Value => Color.FromArgb
        (
            (byte)Interpolation.ValueAt(InitialValue.A, EndValue.A, Alpha, EasingType),
            (byte)Interpolation.ValueAt(InitialValue.R, EndValue.R, Alpha, EasingType),
            (byte)Interpolation.ValueAt(InitialValue.G, EndValue.G, Alpha, EasingType),
            (byte)Interpolation.ValueAt(InitialValue.B, EndValue.B, Alpha, EasingType)
        );
    }

    #endregion
}