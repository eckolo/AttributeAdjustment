using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Factory
{
    public static class BattleStateFactory
    {
        public static BattleState SetupBattleState(
            this GameState state,
            IEnumerable<Actor> enemys,
            Topography topography)
        {
            var actiors = enemys.Concat(new[] { state.player }).Select(actor => actor.ToBattleActor()).ToArray();

            var battleState = new BattleState(actiors, topography);
            battleState.viewActionList.Add(battleState.ToViewAction(new ViewDeployment(), ViewAction.Pattern.GENERATE));

            return battleState;
        }
    }
}
