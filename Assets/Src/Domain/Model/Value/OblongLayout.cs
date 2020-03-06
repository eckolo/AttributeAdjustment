using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 楕円形配置
    /// </summary>
    public class OblongLayout : IViewLayout
    {
        public OblongLayout(Vector2 axis, float? startAngle = null)
        {
            this.startAngle = startAngle ?? this.startAngle;
            this.axis = axis;
        }

        /// <summary>
        /// 開始角度（X軸正方向から反時計回り）
        /// </summary>
        public float startAngle { get; } = 0;
        /// <summary>
        /// 縦軸・横軸の長さ
        /// </summary>
        public Vector2 axis { get; }

        public Dictionary<IViewKey, List<Vector2>> GetPositionMap(Dictionary<IViewKey, int> totalMap)
        {
            var total = totalMap.Sum(pair => pair.Value);
            var unitAngle = 360.DividedBy(total);

            var positionMap = totalMap
                .SelectMany(pair => Enumerable.Repeat(pair.Key, pair.Value))
                .Select((key, index) => (key, angle: ((startAngle + (unitAngle * index)) * Mathf.Deg2Rad).value))
                .Select(pair => (pair.key, x: Mathf.Cos(pair.angle) * axis.x, y: Mathf.Sin(pair.angle) * axis.y))
                .Select(pair => (pair.key, position: new Vector2(pair.x, pair.y)))
                .GroupBy(pair => pair.key, pair => pair.position)
                .ToDictionary(position => position.Key, position => position.ToList());

            return positionMap;
        }
    }
}
