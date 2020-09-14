namespace ReaLTaiizor.Animate.Metro
{
    #region IntAnimateAnimate

    public class IntAnimate : Animate<int>
    {
        public override int Value => (int)Interpolation.ValueAt(InitialValue, EndValue, Alpha, EasingType);
    }

    #endregion
}