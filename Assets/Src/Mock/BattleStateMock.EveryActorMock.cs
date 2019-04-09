using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public partial class BattleStateMock
    {
        public class EveryActorMock : EveryActor
        {
            EveryActorMock(List<MotionTip> handTips) : base()
            {
                this.handTips = handTips;
            }
            EveryActorMock() : base() { }

            public static EveryActorMock Generate(List<MotionTip> handTips) => new EveryActorMock(handTips);
            public static EveryActorMock Generate() => new EveryActorMock();
        }
    }
}
