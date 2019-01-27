using Assets.Src.Domain.Model.Value;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// <see cref="Fraction"/>クラスを扱うサービス
    /// </summary>
    public static class FractionManager
    {
        /// <summary>
        /// 割り算形式での分数型生成メソッド
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <returns>被除数を除数で割った結果と同値の分数型の値</returns>
        public static Fraction DividedBy(this int dividend, int divisor) => new Fraction(dividend) / divisor;

        /// <summary>
        /// 分数型の整数を生成する
        /// </summary>
        /// <param name="origin">元となる整数</param>
        /// <returns>元となる整数と同値の分数型の値</returns>
        public static Fraction ToFraction(this int origin) => new Fraction(origin);

        /// <summary>
        /// 上限値をかける
        /// </summary>
        /// <param name="origin">元の値</param>
        /// <param name="upper">上限値</param>
        /// <returns>上限値以下の元の値</returns>
        public static Fraction LimitUpper(this Fraction origin, Fraction upper) => origin < upper ? origin : upper;

        /// <summary>
        /// 下限値をかける
        /// </summary>
        /// <param name="origin">元の値</param>
        /// <param name="upper">下限値</param>
        /// <returns>下限値以上の元の値</returns>
        public static Fraction LimitLower(this Fraction origin, Fraction lower) => origin > lower ? origin : lower;
    }
}
