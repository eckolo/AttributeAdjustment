using Assets.Src.Domain.Model.Entity;
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
    /// <see cref="BattleState.ActionTarget"/>のメンバとしてふるまう値の取得
    /// </summary>
    public static class BattleStateActionTargetProperties
    {
        /// <summary>
        /// 表示位置取得
        /// </summary>
        /// <param name="target">表示対象</param>
        /// <returns>表示位置</returns>
        public static Vector2 GetCenterPosition(this BattleState.ActionTarget target)
            => centerPosition[target];

        static readonly Dictionary<BattleState.ActionTarget, Vector2> centerPosition
            = new Dictionary<BattleState.ActionTarget, Vector2>
            {
                { BattleState.ActionTarget.DECK, new Vector2(0, -2f) },
                { BattleState.ActionTarget.BOARD, new Vector2(0, 0) },
            };
    }
}
