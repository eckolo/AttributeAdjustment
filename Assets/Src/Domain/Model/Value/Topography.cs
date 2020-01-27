using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 地形クラス
    /// </summary>
    public class Topography
    {
        /// <summary>
        /// 地形の基礎モーションチップリスト
        /// </summary>
        public Dictionary<MotionTip, int> baseTipSet { get; protected set; }
    }
}
