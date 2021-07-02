namespace ReaLTaiizor.Animate.Metro
{
    #region DoubleAnimateAnimate

    public class DoubleAnimate : Animate<double>
    {
        public override double Value => Interpolation.ValueAt(InitialValue, EndValue, Alpha, EasingType);
    }

    #endregion
}