using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public partial class BattleActorMock
    {
        public class StateMock : State
        {
            StateMock(List<MotionTip> handTips) : base()
            {
                this.handTips = handTips;
            }
            StateMock() : base() { }

            public static StateMock Generate(List<MotionTip> handTips) => new StateMock(handTips);
            public static StateMock Generate() => new StateMock();
        }
    }
}
