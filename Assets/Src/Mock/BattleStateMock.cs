using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public class BattleStateMock : BattleState
    {
        BattleStateMock(IEnumerable<Actor> actiors, Dictionary<MotionTip, int> deckStationeryMap)
           : base(actiors, new Topography(deckStationeryMap))
        { }

        public static BattleStateMock Generate(IEnumerable<Actor> actiors, Dictionary<MotionTip, int> deckStationeryMap)
            => new BattleStateMock(actiors, deckStationeryMap);
        public static BattleStateMock Generate(Dictionary<MotionTip, int> deckStationeryMap)
            => new BattleStateMock(new List<Actor>(), deckStationeryMap);
        public static BattleStateMock Generate()
            => new BattleStateMock(new List<Actor>(), new Dictionary<MotionTip, int>());
    }
}
