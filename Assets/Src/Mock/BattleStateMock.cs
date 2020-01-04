using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Mock
{
    public class BattleStateMock : BattleState
    {
        BattleStateMock(IEnumerable<BattleActor> battleActors, Topography topography)
           : base(battleActors, topography)
        { }
        BattleStateMock(IEnumerable<IViewKey> views, ViewAction[] viewActionQueue, bool? isGenerated)
           : base(null, null)
        {
            this.isGenerated = isGenerated ?? this.isGenerated;
            foreach(var action in viewActionQueue ?? new ViewAction[] { })
            {
                viewActionList.Add(action);
            }
        }

        public static BattleStateMock Generate(IEnumerable<Actor> actiors, Dictionary<MotionTip, int> deckStationeryMap)
        {
            var battleActors = actiors?.Select(actor => actor.ToBattleActor()).ToArray();
            var topography = new Topography(deckStationeryMap);

            return new BattleStateMock(battleActors, topography);
        }
        public static BattleStateMock Generate(IEnumerable<BattleActor> battleActors)
            => new BattleStateMock(battleActors, null);
        public static BattleStateMock Generate(Dictionary<MotionTip, int> deckStationeryMap)
            => Generate(new List<Actor>(), deckStationeryMap);
        public static BattleStateMock Generate()
            => Generate(new List<Actor>(), new Dictionary<MotionTip, int>());
        public static BattleStateMock Generate(
            IEnumerable<IViewKey> views = null,
            ViewAction[] viewActionQueue = null)
            => new BattleStateMock(views, viewActionQueue, null);
    }
}
