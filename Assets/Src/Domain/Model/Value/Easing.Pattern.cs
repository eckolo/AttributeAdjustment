namespace Assets.Src.Domain.Model.Value
{
    public partial class Easing
    {
        /// <summary>
        /// イージングの種別
        /// </summary>
        public enum Pattern
        {
            /// <summary>
            /// 線形変動
            /// </summary>
            Linear,
            /// <summary>
            /// 二乗変動
            /// </summary>
            Quadratic,
            /// <summary>
            /// 三乗変動
            /// </summary>
            Cubic,
            /// <summary>
            /// 四乗変動
            /// </summary>
            Quartic,
            /// <summary>
            /// 五乗変動
            /// </summary>
            Quintic,
            /// <summary>
            /// 円形変動
            /// </summary>
            Sinusoidal,
            /// <summary>
            /// 累乗変動
            /// </summary>
            Exponential,
            /// <summary>
            /// 乗根変動
            /// </summary>
            Circular,
        }
    }
}
