using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Model.Entity
{
    public partial class BattleActor
    {
        /// <summary>
        /// 行動者毎の戦闘状態
        /// </summary>
        public class State : ViewStateKey
        //TODO ビューオブジェクトの移動点をビューのルートで定義する
        {
            /// <summary>
            /// 戦闘者毎の戦闘状態生成
            /// </summary>
            public State()
            {
                selfTips = new List<MotionTip>();
                handTips = new List<MotionTip>();
            }

            //TODO 懐札補充メソッドの実装
            /// <summary>
            /// 懐札
            /// </summary>
            public IEnumerable<MotionTip> selfTips { get; protected set; }
            /// <summary>
            /// 手札
            /// </summary>
            public List<MotionTip> handTips { get; protected set; }

            public override Dictionary<ViewDeployment, IViewLayout> viewLayoutMap
                => new Dictionary<ViewDeployment, IViewLayout>();

            /// <summary>
            /// 手札の追加
            /// </summary>
            /// <param name="addedTips">追加されるモーションチップ一覧</param>
            /// <returns>手札に指定したモーションチップが追加された行動主体</returns>
            public State AddHandTips(IEnumerable<MotionTip> addedTips)
            {
                var empties = Enumerable.Empty<MotionTip>();
                handTips = (handTips ?? empties).Concat(addedTips ?? empties).ToList();
                return this;
            }
            /// <summary>
            /// 手札の全消去
            /// </summary>
            /// <returns>手札の全消去された行動主体</returns>
            public State ClearHandTips()
            {
                PopHandTips(handTips);
                return this;
            }
            /// <summary>
            /// 手札の取り出し
            /// </summary>
            /// <param name="popTips">取り出すモーションチップ一覧</param>
            /// <returns>取り出しに成功したモーションチップの一覧</returns>
            public IEnumerable<MotionTip> PopHandTips(IEnumerable<MotionTip> popTips)
            {
                var popMap = popTips?.GroupBy(tip => tip).ToDictionary(tip => tip.Key, tip => tip.Count());
                var handTipMap = handTips?.GroupBy(tip => tip).ToDictionary(tip => tip.Key, tip => tip.Count());

                handTips = handTipMap?
                    .Select(tip => (tip: tip.Key, number: tip.Value - popMap.GetOrDefault(tip.Key, 0)))
                    .Where(data => data.number > 0)
                    .SelectMany(data => Enumerable.Range(0, data.number).Select(_ => data.tip))
                    .ToList();

                var popedTips = popMap?
                    .Select(tip => (
                        tip: tip.Key,
                        number: Mathf.Min(tip.Value, handTipMap.GetOrDefault(tip.Key, 0))))
                    .Where(data => data.number > 0)
                    .SelectMany(data => Enumerable.Range(0, data.number).Select(_ => data.tip))
                    .ToList()
                    ?? new List<MotionTip>();

                return popedTips;
            }
        }
    }
}
