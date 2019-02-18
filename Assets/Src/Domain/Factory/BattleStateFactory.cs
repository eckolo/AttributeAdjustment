using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Domain.Factory
{
    public static class BattleStateFactory
    {
        public static BattleState ToBattleState(
            this GameState state,
            IEnumerable<Actor> enemys,
            Topography topography)
        {
            var actiors = enemys.Concat(new[] { state.player });

            var battleState = new BattleState(actiors, topography);

            return battleState;
        }
    }
}
