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
    /// <see cref="MotionTip"/>のメンバとしてふるまう値の取得
    /// </summary>
    public static class MotionTipProperties
    {
        /// <summary>
        /// 表示位置取得
        /// </summary>
        /// <param name="target">表示対象</param>
        /// <returns>表示位置</returns>
        public static Vector2 GetCenterPosition(this MotionTip.Destination target)
            => centerPosition[target];

        static readonly Dictionary<MotionTip.Destination, Vector2> centerPosition
            = new Dictionary<MotionTip.Destination, Vector2>
            {
                { MotionTip.Destination.DECK, new Vector2(0, -2f) },
                { MotionTip.Destination.BOARD, new Vector2(0, 0) },
            };
    }
}
