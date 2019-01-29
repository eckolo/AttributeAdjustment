using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public partial class BattleStateMock : BattleState
    {
        BattleStateMock(Dictionary<MotionTip, int> deckStationeryMap)
           : base(new List<Actor>(), deckStationeryMap)
        {
        }
        BattleStateMock()
           : base(new List<Actor>(), new Dictionary<MotionTip, int>())
        {
        }

        public static BattleStateMock Generate(Dictionary<MotionTip, int> deckStationeryMap)
            => new BattleStateMock(deckStationeryMap);
        public static BattleStateMock Generate()
            => new BattleStateMock();
    }
}
