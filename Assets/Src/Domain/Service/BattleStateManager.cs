﻿using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System;
using System.Linq;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// <see cref="BattleState"/>クラスの制御関連サービス
    /// </summary>
    public static class BattleStateManager
    {
        /// <summary>
        /// 山札の初期化
        /// </summary>
        /// <param name="state">山札初期化対象の戦闘状態</param>
        /// <returns>山札の初期化された戦闘状態</returns>
        public static BattleState SetupDeck(this BattleState state)
        {
            var deckStationery = state.deckStationeryMap?
                .SelectMany(tip => Enumerable.Range(0, tip.Value).Select(_ => tip.Key))
                .Shuffle();
            state.SetDeckTip(deckStationery);
            return state;
        }
        /// <summary>
        /// 手札の初期化
        /// </summary>
        /// <param name="state">手札初期化対象の戦闘状態</param>
        /// <param name="actor">手札初期化対象の動作主体</param>
        /// <param name="tipNumbers">初期化手札枚数</param>
        /// <returns>所定の動作主体の手札が初期化された戦闘状態</returns>
        public static BattleState SetupHandTip(
            this BattleState state,
            BattleActor actor,
            int tipNumbers = Constants.Battle.DEFAULT_HAND_TIP_NUMBERS)
        {
            if(state is null) throw new ArgumentNullException();
            if(actor is null) throw new ArgumentNullException();

            if(!state.battleActorList.ContainsKey(actor)) return state;

            var actorState = state.battleActorList[actor];
            actorState.PopHandTip(actorState.handTips);

            actorState.AddHandTip(state.PopDeckTip(tipNumbers));
            state.battleActorList[actor] = actorState;

            return state;
        }
    }
}
