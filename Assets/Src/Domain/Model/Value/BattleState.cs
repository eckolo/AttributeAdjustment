using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Abstract;
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
    public partial class BattleState : ViewStateAbst
    {
        /// <summary>
        /// 戦闘状態の生成
        /// </summary>
        /// <param name="actiors">戦闘参加者</param>
        /// <param name="topography">初期地形</param>
        public BattleState(IEnumerable<Actor> actiors, Topography topography)
        {
            //戦闘者毎の戦闘状態初期化
            battleActorList = actiors.ToDictionary(actor => actor.ConvertForBattle(), _ => new EveryActor());
            //山札の雛形生成
            this.topography = topography;
        }

        /// <summary>
        /// 戦闘終了フラグ
        /// </summary>
        public bool isEnd { get; } = false;

        /// <summary>
        /// 現在の地形
        /// </summary>
        public Topography topography { get; protected set; }

        /// <summary>
        /// 山札の雛形
        /// チップの種別+枚数
        /// </summary>
        public Dictionary<MotionTip, int> deckStationeryMap => topography.baseTipSet;

        /// <summary>
        /// 山札
        /// </summary>
        public Queue<MotionTip> deckTips { get; protected set; } = new Queue<MotionTip>();

        /// <summary>
        /// 場札
        /// </summary>
        public IEnumerable<MotionTip> boardTips { get; protected set; } = Enumerable.Empty<MotionTip>();

        /// <summary>
        /// 行動者毎の戦闘状態リスト情報
        /// </summary>
        public Dictionary<BattleActor, EveryActor> battleActorList { get; protected set; }

        /// <summary>
        /// 山札の設定
        /// </summary>
        /// <returns>山札設定後の戦闘状態</returns>
        public BattleState SetDeckTips(IEnumerable<MotionTip> deckStationery)
        {
            deckTips = deckStationery != null ? new Queue<MotionTip>(deckStationery) : new Queue<MotionTip>();
            return this;
        }
        /// <summary>
        /// 山札の取り出し
        /// </summary>
        /// <param name="popTipNumber">取り出し枚数</param>
        /// <returns>山札から取り出されたモーションチップ一覧</returns>
        public IEnumerable<MotionTip> PopDeckTips(int popTipNumber)
        {
            var _popTipNumber = Mathf.Max(popTipNumber, 0);
            var popedTips = Enumerable.Range(0, _popTipNumber)
                .Select(_ => deckTips.Any() ? deckTips.Dequeue() : null)
                .Where(tip => tip is MotionTip)
                .ToList();

            this.SetNewView(popedTips);

            if(!deckTips.Any())
                this.SetupDeck();

            return popedTips;
        }
        /// <summary>
        /// 場札の設定
        /// </summary>
        /// <returns>場札設定後の戦闘状態</returns>
        public BattleState SetBoardTips(IEnumerable<MotionTip> boardStationery)
        {
            boardTips = boardStationery?.ToList() ?? Enumerable.Empty<MotionTip>();
            return this;
        }
        /// <summary>
        /// 場札の取り出し
        /// </summary>
        /// <param name="popTips">取り出すモーションチップ一覧</param>
        /// <returns>取り出しに成功したモーションチップの一覧</returns>
        public IEnumerable<MotionTip> PopBoardTips(IEnumerable<MotionTip> popTips)
        {
            var popMap = popTips?.GroupBy(tip => tip).ToDictionary(tip => tip.Key, tip => tip.Count());
            var boardTipMap = boardTips?.GroupBy(tip => tip).ToDictionary(tip => tip.Key, tip => tip.Count());

            boardTips = boardTipMap?
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

            return popedTips;
        }
    }
}
