using Assets.Src.Controller;
using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 戦闘の状態保持クラス
    /// </summary>
    public partial class BattleState : ViewStateKey, IDisposable
    //TODO ビューオブジェクトの移動点をビューのルートで定義する
    {
        /// <summary>
        /// 戦闘状態の生成
        /// </summary>
        /// <param name="battleActors">戦闘参加者</param>
        /// <param name="topography">初期地形</param>
        public BattleState(IEnumerable<BattleActor> battleActors, Topography topography)
        {
            //戦闘者毎の戦闘状態初期化
            this.battleActors = battleActors ?? throw new ArgumentNullException(nameof(battleActors));
            //山札の雛形生成
            this.topography = topography ?? throw new ArgumentNullException(nameof(topography));
        }

        /// <summary>
        /// 戦闘終了フラグ
        /// </summary>
        public bool isEnd { get; } = false;

        /// <summary>
        /// 現在の地形
        /// </summary>
        public Topography topography { get; set; }

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
        /// 山札の初期化
        /// </summary>
        /// <param name="deckStationery">初期化内容</param>
        public void CleanupDeckTips(IEnumerable<MotionTip> deckStationery)
        {
            deckTips = deckStationery != null
                ? new Queue<MotionTip>(deckStationery)
                : new Queue<MotionTip>();
        }

        /// <summary>
        /// 場札
        /// </summary>
        public IEnumerable<MotionTip> boardTips { get; set; } = Enumerable.Empty<MotionTip>();

        /// <summary>
        /// 場札の初期化
        /// </summary>
        /// <param name="boardStationery">初期化内容</param>
        public void CleanupBoardTips(IEnumerable<MotionTip> boardStationery)
        {
            boardTips = boardStationery?.ToList() ?? Enumerable.Empty<MotionTip>();
        }

        /// <summary>
        /// 行動者毎の戦闘状態リスト情報
        /// </summary>
        public IEnumerable<BattleActor> battleActors { get; set; }

        public BattleActor thisTimeActor { get; set; }

        public override Vector2 position { get; set; }

        public async void Dispose() => await this.EndBattle();
    }
}
