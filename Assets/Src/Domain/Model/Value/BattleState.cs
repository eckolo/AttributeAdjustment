﻿using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 戦闘の状態保持クラス
    /// </summary>
    public partial class BattleState
    {
        /// <summary>
        /// 戦闘状態の生成
        /// </summary>
        /// <param name="actiors">戦闘参加者</param>
        /// <param name="deckStationeryMap">山札の雛形</param>
        public BattleState(IEnumerable<Actor> actiors, Dictionary<MotionTip, int> deckStationeryMap)
        {
            //戦闘者毎の戦闘状態初期化
            battleActorList = actiors.ToDictionary(actor => actor.ConvertForBattle(), _ => new EveryActor());
            //山札の雛形生成
            this.deckStationeryMap = deckStationeryMap;
        }

        /// <summary>
        /// 山札の雛形
        /// チップの種別+枚数
        /// </summary>
        public Dictionary<MotionTip, int> deckStationeryMap { get; protected set; }

        /// <summary>
        /// 山札
        /// </summary>
        public Queue<MotionTip> deckTips { get; protected set; }

        /// <summary>
        /// 場札
        /// </summary>
        public IEnumerable<MotionTip> boardTips { get; protected set; }

        /// <summary>
        /// 行動者毎の戦闘状態リスト情報
        /// </summary>
        public Dictionary<BattleActor, EveryActor> battleActorList { get; protected set; }

        /// <summary>
        /// 山札の設定
        /// </summary>
        /// <returns>山札設定後の戦闘状態</returns>
        public BattleState SetDeckTip(IEnumerable<MotionTip> deckStationery)
        {
            deckTips = deckStationery != null ? new Queue<MotionTip>(deckStationery) : null;
            return this;
        }
        /// <summary>
        /// 山札の取り出し
        /// </summary>
        /// <param name="popTipNumber">取り出し枚数</param>
        /// <returns>山札から取り出されたモーションチップ一覧</returns>
        public IEnumerable<MotionTip> PopDeckTip(int popTipNumber)
        {
            var _popTipNumber = Mathf.Max(popTipNumber, 0);
            var popedTips = Enumerable.Range(0, _popTipNumber)
                .Select(_ => deckTips.Any() ? deckTips.Dequeue() : null)
                .Where(tip => tip is MotionTip)
                .ToList();

            if(!deckTips.Any()) this.SetupDeck();

            return popedTips;
        }
    }
}
