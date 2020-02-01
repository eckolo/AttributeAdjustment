using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            var deckStationery = state.deckStationeryMap?.Embody().Shuffle();

            state.SetViewActions(state.deckTips, ViewAction.Pattern.DELETE);
            state.CleanupDeckTips(deckStationery);
            state.SetNewView(state.deckTips);

            return state;
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
        {
            if(state is null)
                return state;

            state.SetViewActions(state.boardTips, ViewAction.Pattern.DELETE);
            var popedTips = state.PopDeckTips(tipNumbers);
            state.CleanupBoardTips(popedTips);
            state.SetTipMoving(popedTips, MotionTip.Destination.BOARD);

            return state;
        }

        /// <summary>
        /// 山札の取り出し
        /// </summary>
        /// <param name="popTipNumber">取り出し枚数</param>
        /// <returns>山札から取り出されたモーションチップ一覧</returns>
        public static IEnumerable<MotionTip> PopDeckTips(this BattleState state, int popTipNumber)
        {
            var _popTipNumber = Mathf.Max(popTipNumber, 0);
            var popedTips = Enumerable.Range(0, _popTipNumber)
                .Select(_ => state.deckTips.Any() ? state.deckTips.Dequeue() : null)
                .Where(tip => tip is MotionTip)
                .ToList();

            if(!state.deckTips.Any())
                state.SetupDeck();

            return popedTips;
        }
        /// <summary>
        /// 場札の取り出し
        /// </summary>
        /// <param name="popTips">取り出すモーションチップ一覧</param>
        /// <returns>取り出しに成功したモーションチップの一覧</returns>
        public static IEnumerable<MotionTip> PopBoardTips(this BattleState state, IEnumerable<MotionTip> popTips)
        {
            var popMap = popTips?.GroupBy(tip => tip).ToDictionary(tip => tip.Key, tip => tip.Count());
            var boardTipMap = state.boardTips?.GroupBy(tip => tip).ToDictionary(tip => tip.Key, tip => tip.Count());

            state.boardTips = boardTipMap?
                .Select(tip => (tip: tip.Key, number: tip.Value - popMap.GetOrDefault(tip.Key, 0)))
                .Where(data => data.number > 0)
                .SelectMany(data => Enumerable.Range(0, data.number).Select(_ => data.tip))
                .ToList();

            var popedTips = popMap?
                .Select(tip => (
                    tip: tip.Key,
                    number: Mathf.Min(tip.Value, boardTipMap.GetOrDefault(tip.Key, 0))))
                .Where(data => data.number > 0)
                .SelectMany(data => Enumerable.Range(0, data.number).Select(_ => data.tip))
                .ToList()
                ?? new List<MotionTip>();

            state.SetViewActions(popedTips, ViewAction.Pattern.DELETE);
            return popedTips;
        }
        /// <summary>
        /// 全行動主体の手札を総初期化する
        /// </summary>
        /// <param name="state">手札初期化対象の戦闘状態</param>
        /// <param name="tipNumbers">初期化手札枚数</param>
        /// <returns></returns>
        public static BattleState SetupAllHandTips(this BattleState state)
        {
            if(state is null)
                throw new ArgumentNullException(nameof(state));

            foreach(var actor in state.battleActors)
                actor.ReloadHandTips();

            return state;
        }
        /// <summary>
        /// 戦闘状態内の全行動主体の行動力を一斉更新する
        /// </summary>
        /// <param name="state">対象の戦闘状態</param>
        /// <returns>行動力更新後の戦闘状態</returns>
        public static BattleState UpdateEnergy(this BattleState state)
        {
            if(state is null)
                return state;
            var actors = state.battleActors;
            if(!(actors?.Any() ?? false))
                return state;

            var baseEnergy = actors.Min(actor => actor.energy);

            foreach(var actor in actors)
                actor.energy += actor.GetEnergyIncrease() - baseEnergy;

            return state;
        }
        /// <summary>
        /// 戦闘状態に次の行動者を設定する
        /// </summary>
        /// <param name="state">対象の戦闘状態</param>
        /// <returns>行動力更新後の戦闘状態</returns>
        public static BattleState SetNextActor(this BattleState state)
            => state?.SetThisTimeActor(state.battleActors?.MaxKeys(actor => actor.energy).FirstOrDefault());
        /// <summary>
        /// 現在行動者の設定
        /// </summary>
        /// <param name="nextActor">次の行動者</param>
        /// <returns>行動者の設定された戦闘状態</returns>
        public static BattleState SetThisTimeActor(this BattleState state, BattleActor nextActor)
        {
            state.thisTimeActor = (state.battleActors?.Contains(nextActor) ?? false) ? nextActor : default;
            return state;
        }
    }
}
