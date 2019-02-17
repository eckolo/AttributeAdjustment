using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 地形クラス
    /// </summary>
    public class Topography
    {
        public Topography(Dictionary<MotionTip, int> baseTipSet)
        {
            this.baseTipSet = baseTipSet ?? new Dictionary<MotionTip, int>();
        }

        /// <summary>
        /// 地形の基礎モーションチップリスト
        /// </summary>
        public Dictionary<MotionTip, int> baseTipSet { get; }
    }
}
