using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 基準点内での配置パターン
    /// </summary>
    public interface IViewLayout
    {
        Dictionary<IViewKey, List<Vector2>> GetPositionMap(Dictionary<IViewKey, int> totalMap);
    }
}
