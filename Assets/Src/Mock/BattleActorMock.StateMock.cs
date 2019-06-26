using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Mock
{
    public partial class BattleActorMock
    {
        public class StateMock : State
        {
            StateMock(List<MotionTip> handTips, IEnumerable<MotionTip> selfTips) : base()
            {
                this.handTips = handTips;
                this.selfTips = selfTips.ToList();
            }
            StateMock() : base() { }

            public static StateMock Generate(List<MotionTip> handTips, IEnumerable<MotionTip> selfTips = null) => new StateMock(handTips, selfTips ?? new List<MotionTip>());
            public static StateMock Generate() => new StateMock();
        }
    }
}
