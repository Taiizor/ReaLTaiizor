#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Animate.Metro
{
    #region PointFAnimateAnimate

    public class PointFAnimate : Animate<PointF>
    {
        public override PointF Value => new
        (
            (float)Interpolation.ValueAt(InitialValue.X, EndValue.X, Alpha, EasingType),
            (float)Interpolation.ValueAt(InitialValue.Y, EndValue.Y, Alpha, EasingType)
        );
    }

    #endregion
}