using Assets.Src.Domain.Model.Entity;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 戦闘の状態保持クラス
    /// </summary>
    public partial class BattleState
    {
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
        public Dictionary<BattleActor, EveryActor> battleActorList { get; set; }
    }
}
