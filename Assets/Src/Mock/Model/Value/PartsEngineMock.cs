using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock.Model.Value
{
    public class PartsEngineMock : PartsEngine
    {
        PartsEngineMock(int power, Dictionary<MotionTip, int> defaultHandTipMap)
        {
            this.power = power;
            this.defaultHandTipMap = defaultHandTipMap;
        }

        public static PartsEngineMock Generate(int power, Dictionary<MotionTip, int> defaultHandTipMap)
            => new PartsEngineMock(power, defaultHandTipMap);
        public static PartsEngineMock Generate(int power)
            => new PartsEngineMock(power, new Dictionary<MotionTip, int>());
        public static PartsEngineMock Generate()
            => new PartsEngineMock(default, new Dictionary<MotionTip, int>());
    }
}
