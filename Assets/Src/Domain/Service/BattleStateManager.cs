﻿using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
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

            var result = state.SetDeckTips(deckStationery);
            return result;
        }
        /// <summary>
        /// 山札の強制取り出し
        /// 足りなければ補充して必要枚数取り出す
        /// </summary>
        /// <param name="popTipNumber">取り出し枚数</param>
        /// <returns>山札から取り出されたモーションチップ一覧</returns>
        public static IEnumerable<MotionTip> PopDeckTipsForced(this BattleState state, int popTipNumber)
            => state.deckTips.Count >= popTipNumber || (state.deckStationeryMap?.Any() ?? false)
                ? Enumerable.Empty<MotionTip>().JoinDeckTips(state, popTipNumber)
                : Enumerable.Empty<MotionTip>();
        static IEnumerable<MotionTip> JoinDeckTips(
            this IEnumerable<MotionTip> tips,
            BattleState state,
            int popTipNumber)
            => (tips.Count() < popTipNumber)
                ? tips.Concat(state.PopDeckTips(popTipNumber - tips.Count())).JoinDeckTips(state, popTipNumber)
                : tips;
        /// <summary>
        /// 場札の初期化
        /// </summary>
        /// <param name="state">場札初期化対象の戦闘状態</param>
        /// <param name="tipNumbers">初期化場札枚数</param>
        /// <returns>場札の初期化された戦闘状態</returns>
        public static BattleState SetupBoard(
            this BattleState state,
            int tipNumbers = Constants.Battle.DEFAULT_BOARD_TIP_NUMBERS)
            => state?.SetBoardTips(state.PopDeckTips(tipNumbers));
        /// <summary>
        /// 手札の初期化
        /// </summary>
        /// <param name="state">手札初期化対象の戦闘状態</param>
        /// <param name="actor">手札初期化対象の動作主体</param>
        /// <param name="tipNumbers">初期化手札枚数</param>
        /// <returns>所定の動作主体の手札が初期化された戦闘状態</returns>
        public static BattleState SetupHandTips(
            this BattleState state,
            BattleActor actor,
            int tipNumbers = Constants.Battle.DEFAULT_HAND_TIP_NUMBERS)
        {
            if(state is null) return state;
            if(actor is null) return state;

            if(!state.battleActorList.ContainsKey(actor)) return state;

            state.battleActorList[actor] = state.battleActorList[actor]
                .ClearHandTips()
                .AddHandTips(state.PopDeckTips(tipNumbers));

            return state;
        }
        /// <summary>
        /// 全行動主体の手札を総初期化する
        /// </summary>
        /// <param name="state">手札初期化対象の戦闘状態</param>
        /// <param name="tipNumbers">初期化手札枚数</param>
        /// <returns></returns>
        public static BattleState SetupAllHandTips(
            this BattleState state,
            int tipNumbers = Constants.Battle.DEFAULT_HAND_TIP_NUMBERS)
        {
            if(state is null) return state;

            var setTips = state.battleActorList
                .ToDictionary(
                    actor => actor.Key,
                    actor => actor.Value.ClearHandTips().AddHandTips(state.PopDeckTipsForced(tipNumbers)));

            foreach(var tips in setTips) state.battleActorList[tips.Key] = tips.Value;

            return state;
        }
    }
}
