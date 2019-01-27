using Assets.Src.Domain.Model.Value;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// 乱数関係のサービスクラス
    /// </summary>
    public static class RandomManager
    {
        /// <summary>
        /// 選択基準値を新規生成する
        /// </summary>
        /// <param name="radix">基準値の母数値</param>
        /// <returns>選択基準値</returns>
        public static Fraction SetupRandomNorm(this int radix)
            => Random.Range(0, radix - 1).DividedBy(radix);
    }
}
