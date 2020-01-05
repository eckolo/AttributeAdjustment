using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Repository
{
    /// <summary>
    /// <see cref="MotionTipDestination"/>のメンバとしてふるまう値の取得
    /// </summary>
    public static class MotionTipDestinationProperties
    {
        /// <summary>
        /// 表示位置取得
        /// </summary>
        /// <param name="target">表示対象</param>
        /// <returns>表示位置</returns>
        public static Vector2 GetCenterPosition(this MotionTipDestination target)
            => centerPosition[target];

        static readonly Dictionary<MotionTipDestination, Vector2> centerPosition
            = new Dictionary<MotionTipDestination, Vector2>
            {
                { MotionTipDestination.DECK, new Vector2(0, -2f) },
                { MotionTipDestination.BOARD, new Vector2(0, 0) },
            };
    }
}
