using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// 数値計算用クラス
    /// </summary>
    public static class NumberManager
    {
        /// <summary>
        /// 最大公約数を求める
        /// </summary>
        /// <param name="x">公約数を求める対象その１</param>
        /// <param name="y">公約数を求める対象その２</param>
        /// <returns>２引数の最大公約数</returns>
        public static int Euclidean(this int x, int y)
        {
            if(x == 0) return 1;
            if(y == 0) return 1;
            var absX = Mathf.Abs(x);
            var absY = Mathf.Abs(y);
            if(absY > absX) return Euclidean(absY, absX);
            if(absX % absY == 0) return y;
            return Euclidean(absY, absX % y);
        }
        /// <summary>
        /// mainの数値をsubに合わせて補正する
        /// </summary>
        /// <param name="main">元値</param>
        /// <param name="sub">補正値</param>
        /// <param name="degree">元値への寄せ度合い</param>
        /// <returns>補正済みの値</returns>
        public static float Correct(this float main, float sub, float degree = 0.5f)
            => main * degree + sub * (1 - degree);

        /// <summary>
        /// 上限値をかける
        /// </summary>
        /// <param name="origin">元の値</param>
        /// <param name="upper">上限値</param>
        /// <returns>上限値以下の元の値</returns>
        public static int LimitUpper(this int origin, int upper) => Mathf.Min(origin, upper);
        /// <summary>
        /// 上限値をかける
        /// </summary>
        /// <param name="origin">元の値</param>
        /// <param name="upper">上限値</param>
        /// <returns>上限値以下の元の値</returns>
        public static float LimitUpper(this float origin, float upper) => Mathf.Min(origin, upper);

        /// <summary>
        /// 下限値をかける
        /// </summary>
        /// <param name="origin">元の値</param>
        /// <param name="upper">下限値</param>
        /// <returns>下限値以上の元の値</returns>
        public static int LimitLower(this int origin, int lower) => Mathf.Max(origin, lower);
        /// <summary>
        /// 下限値をかける
        /// </summary>
        /// <param name="origin">元の値</param>
        /// <param name="upper">下限値</param>
        /// <returns>下限値以上の元の値</returns>
        public static float LimitLower(this float origin, float lower) => Mathf.Max(origin, lower);

        /// <summary>
        /// boolを整数0,1に変換
        /// </summary>
        public static int ToInt(this bool value) => value ? 1 : 0;

        /// <summary>
        /// boolを正負符号に変換
        /// </summary>
        public static int ToSign(this bool value) => value ? 1 : -1;
        /// <summary>
        /// 適当な数を正負符号に変換
        /// </summary>
        public static int ToSign(this float value)
        {
            if(value > 0) return 1;
            if(value < 0) return -1;
            return 0;
        }

        /// <summary>
        /// 選択肢と選択可能性からランダムで選択肢を選び出す
        /// </summary>
        public static Result SelectRandom<Result>(this IEnumerable<Result> values, IEnumerable<int> rates = null)
        {
            rates = rates ?? values.Select(_ => 1);
            var ratedatas = values.Zip(rates, (value, rate) => new KeyValuePair<Result, int>(value, rate));
            return SelectRandom(ratedatas);
        }
        /// <summary>
        /// 選択肢と選択可能性からランダムで選択肢を選び出す
        /// </summary>
        public static Result SelectRandom<Result>(this IEnumerable<KeyValuePair<Result, int>> ratedatas)
        {
            var selection = Random.Range(1, ratedatas.Sum(ratedata => Mathf.Max(ratedata.Value, 0)));
            foreach(var ratedata in ratedatas)
            {
                selection -= ratedata.Value;
                if(selection <= 0) return ratedata.Key;
            }
            return ratedatas.Select(ratedata => ratedata.Key).First();
        }

        /// <summary>
        /// 数値の上昇度合いを抑える補正関数
        /// </summary>
        /// <param name="origin">元値</param>
        /// <param name="_baseNumber">補正の底数</param>
        /// <returns>補正後の数値</returns>
        public static float Log(this float origin, float? _baseNumber = null)
        {
            var baseNumber = _baseNumber ?? Mathf.Exp(1);
            return Mathf.Log(Mathf.Abs(origin) + 1, baseNumber) * origin.ToSign();
        }

        /// <summary>
        /// リストの値コピー
        /// </summary>
        /// <typeparam name="Type">コピー対象リストの要素タイプ</typeparam>
        /// <param name="origin">コピー対象リスト</param>
        /// <returns>コピー後のリスト</returns>
        public static List<Type> Copy<Type>(this List<Type> origin) => origin?.Select(value => value).ToList();

        /// <summary>
        /// nullableのfloat値を所定の表記（デフォルトで百分率）に直す
        /// </summary>
        /// <param name="origin">元の数値</param>
        /// <param name="format">表記方法指定</param>
        /// <returns>所定の表記（デフォルトで百分率）の文字列</returns>
        public static string ToPercentage(this float? origin, string format = "F2") => origin?.ToString(format) ?? "-";

        /// <summary>
        /// まろやかな乱数を生成する
        /// </summary>
        /// <param name="range">乱数範囲</param>
        /// <param name="center">中央値</param>
        /// <param name="centering">まろやか度合</param>
        /// <returns>まろやかな乱数</returns>
        public static float ToMildRandom(this float range, float center = 0, int centering = 5)
        {
            float sum = 0;
            for(int count = 0; count < centering; count++) sum += Random.Range(-range, range);
            return center + sum;
        }
    }
}
