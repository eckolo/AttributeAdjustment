using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public partial class BattleStateMock
    {
        public class EveryActorMock : EveryActor
        {
            EveryActorMock(IEnumerable<MotionTip> handTips) : base()
            {
                this.handTips = handTips;
            }
            EveryActorMock() : base() { }

            public static EveryActorMock Generate(IEnumerable<MotionTip> handTips) => new EveryActorMock(handTips);
            public static EveryActorMock Generate() => new EveryActorMock();
        }
    }
}
