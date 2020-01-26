using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Mock.Model.Value
{
    public class PartsBrainMock : PartsBrain
    {
        PartsBrainMock(Dictionary<MotionTip, int> defaultDeckTipMap, IEnumerable<Feature> features)
        {
            this.defaultDeckTipMap = defaultDeckTipMap;
            this.features = features;
        }

        public static PartsBrainMock Generate(
            Dictionary<MotionTip, int> defaultDeckTipMap,
            IEnumerable<Feature> features)
            => new PartsBrainMock(defaultDeckTipMap, features);
        public static PartsBrainMock Generate()
            => new PartsBrainMock(new Dictionary<MotionTip, int>(), Enumerable.Empty<Feature>());
    }
}
