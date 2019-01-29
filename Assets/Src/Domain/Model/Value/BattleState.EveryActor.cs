using Assets.Src.Domain.Service;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    public partial class BattleState
    {
        /// <summary>
        /// 行動者毎の戦闘状態
        /// </summary>
        public class EveryActor
        {
            /// <summary>
            /// 戦闘者毎の戦闘状態生成
            /// </summary>
            public EveryActor()
            {
                selfTips = new List<MotionTip>();
                handTips = new List<MotionTip>();
            }

            /// <summary>
            /// 懐札
            /// </summary>
            public IEnumerable<MotionTip> selfTips { get; protected set; }
            /// <summary>
            /// 手札
            /// </summary>
            public IEnumerable<MotionTip> handTips { get; protected set; }

            /// <summary>
            /// 手札の追加
            /// </summary>
            /// <param name="addedTips">追加されるモーションチップ一覧</param>
            /// <returns>手札に指定したモーションチップが追加された行動主体</returns>
            public EveryActor AddHandTip(IEnumerable<MotionTip> addedTips)
            {
                var empties = Enumerable.Empty<MotionTip>();
                handTips = (handTips ?? empties).Concat(addedTips ?? empties).ToList();
                return this;
            }
        }
    }
}
