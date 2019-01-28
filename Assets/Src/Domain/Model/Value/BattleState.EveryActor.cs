using System.Collections.Generic;

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
                selfTip = new List<MotionTip>();
                handTip = new List<MotionTip>();
            }

            /// <summary>
            /// 懐札
            /// </summary>
            public IEnumerable<MotionTip> selfTip { get; protected set; }
            /// <summary>
            /// 手札
            /// </summary>
            public IEnumerable<MotionTip> handTip { get; protected set; }
        }
    }
}
