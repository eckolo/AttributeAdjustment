using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Service
{
    /// <summary>
    /// <see cref="Component"/>周りの演算処理系
    /// </summary>
    public static class ComponentManager
    {
        /// <summary>
        /// コンポーネント一覧とレイアウト情報からコンポーネント毎の配置情報一覧を算出する
        /// </summary>
        /// <param name="componentMap"></param>
        /// <param name="layout"></param>
        /// <returns></returns>
        public static IEnumerable<(Component, Vector2)> GetPositions(
             this Dictionary<IViewKey, Queue<Component>> componentMap,
             IViewLayout layout)
        {
            if(layout == default)
                return componentMap
                    .SelectMany(map => map.Value)
                    .Select(comp => (comp, Vector2.zero))
                    .ToList();

            var totalMap = componentMap.ToDictionary(map => map.Key, map => map.Value.Count);
            var positionMap = layout.GetPositionMap(totalMap);

            var positionList = componentMap
                .Select(pair => (components: pair.Value, positions: positionMap.GetOrDefault(pair.Key)))
                .SelectMany(pair => pair.components
                    .Select((comp, index) => (comp, pair.positions.GetOrDefault(index, Vector2.zero))))
                .ToList();

            return positionList;
        }
    }
}
