using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using System.Collections.Generic;
using System.Linq;

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
        public Queue<MotionTip> deckTip { get; protected set; }

        /// <summary>
        /// 場札
        /// </summary>
        public IEnumerable<MotionTip> boardTip { get; protected set; }

        /// <summary>
        /// 行動者毎の戦闘状態リスト情報
        /// </summary>
        public Dictionary<BattleActor, EveryActor> battleActorList { get; protected set; }

        /// <summary>
        /// 山札の初期化
        /// </summary>
        /// <returns>山札の初期化された戦闘状態</returns>
        public BattleState SetupDeck()
        {
            var deckStationery = deckStationeryMap
                .SelectMany(tip => Enumerable.Range(0, tip.Value).Select(_ => tip.Key))
                .Shuffle();
            deckTip = new Queue<MotionTip>(deckStationery);
            return this;
        }
    }
}
