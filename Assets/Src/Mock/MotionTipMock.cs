using Assets.Src.Domain.Model.Value;

namespace Assets.Src.Mock
{
    public class MotionTipMock : MotionTip
    {
        public MotionTipMock(Energy energy, int energyValue) : base(energy, energyValue)
        {
        }

        public static MotionTipMock Generate(Energy energy, int energyValue)
            => new MotionTipMock(energy, energyValue);
    }
}
