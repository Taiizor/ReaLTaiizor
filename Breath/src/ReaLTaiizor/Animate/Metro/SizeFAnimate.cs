#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Animate.Metro
{
    #region SizeFAnimateAnimate

    public class SizeFAnimate : Animate<SizeF>
    {
        public override SizeF Value => new
        (
            (float)Interpolation.ValueAt(InitialValue.Width, EndValue.Width, Alpha, EasingType),
            (float)Interpolation.ValueAt(InitialValue.Height, EndValue.Height, Alpha, EasingType)
        );
    }

    #endregion
}