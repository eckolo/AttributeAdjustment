﻿using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

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
            state.viewActionList.Add(state.ToViewAction(ViewAction.Pattern.GENERATE));

            return battleState;
        }
    }
}
