using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public class BattleStateMock : BattleState
    {
        public BattleStateMock(Dictionary<MotionTip, int> deckStationeryMap)
            : base(new List<Actor>(), deckStationeryMap)
        {
        }
        public BattleStateMock()
            : base(new List<Actor>(), new Dictionary<MotionTip, int>())
        {
        }

        public static BattleStateMock Generate(Dictionary<MotionTip, int> deckStationeryMap)
            => new BattleStateMock(deckStationeryMap);
        public static BattleStateMock Generate()
            => new BattleStateMock();
    }
}
