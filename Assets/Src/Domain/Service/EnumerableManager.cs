using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// <see cref="IEnumerable{T}">関連の汎用操作クラス
    /// </summary>
    public static class EnumerableManager
    {
        /// <summary>
        /// リストから所定の最大値を持つ要素を抽出する
        /// </summary>
        /// <typeparam name="TSource">リストを構成する要素型</typeparam>
        /// <typeparam name="TComparable">最大値判定時の型</typeparam>
        /// <param name="source">元リスト</param>
        /// <param name="selector">最大値判定値算出関数</param>
        /// <returns></returns>
        public static IEnumerable<TSource> MaxKeys<TSource, TComparable>(this IEnumerable<TSource> source, System.Func<TSource, TComparable> selector)
            where TComparable : System.IComparable<TComparable>
        {
            if(!source?.Any() ?? true)
                return new List<TSource>();
            var maxValue = source.Max(selector);
            return source.Where(elem => selector(elem).CompareTo(maxValue) == 0);
        }
        /// <summary>
        /// リストから所定の最小値を持つ要素を抽出する
        /// </summary>
        /// <typeparam name="TSource">リストを構成する要素型</typeparam>
        /// <typeparam name="TComparable">最小値判定時の型</typeparam>
        /// <param name="source">元リスト</param>
        /// <param name="selector">最小値判定値算出関数</param>
        /// <returns></returns>
        public static IEnumerable<TSource> MinKeys<TSource, TComparable>(this IEnumerable<TSource> source, System.Func<TSource, TComparable> selector)
            where TComparable : System.IComparable<TComparable>
        {
            if(!source?.Any() ?? true)
                return new List<TSource>();
            var minValue = source.Min(selector);
            return source.Where(elem => selector(elem).CompareTo(minValue) == 0);
        }

        /// <summary>
        /// インスペクタから管理する<see cref="IKeyValueLike{TKey, TValue}">のリストを<see cref="Dictionary{TKey, TValue}"/>に変換する
        /// </summary>
        /// <typeparam name="TKey">キ－型</typeparam>
        /// <typeparam name="TValue">内容型</typeparam>
        /// <param name="origin">変換元のKeyValueっぽいクラスのリスト</param>
        /// <returns>生成された辞書型リスト</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<IKeyValueLike<TKey, TValue>> origin)
            => origin.ToDictionary(keyValue => keyValue.key, keyValue => keyValue.value);

        /// <summary>
        /// 辞書型オブジェクトが所定のキーを含んでいればキーに紐づく値、含んでいなければデフォルト値を取得する
        /// </summary>
        /// <typeparam name="TKey">キ－型</typeparam>
        /// <typeparam name="TValue">内容型</typeparam>
        /// <param name="origin">取得対象の辞書型オブジェクト</param>
        /// <param name="key">指定のキー</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <returns>所定のキーに紐づく値もしくはデフォルト値</returns>
        public static TValue GetOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> origin,
            TKey key,
            TValue defaultValue = default)
            => (origin?.ContainsKey(key) ?? false) ? origin[key] : defaultValue;

        /// <summary>
        /// 辞書型オブジェクト1要素を上書き、もしくは追加する
        /// </summary>
        /// <typeparam name="TKey">キ－型</typeparam>
        /// <typeparam name="TValue">内容型</typeparam>
        /// <param name="origin">上書き対象の辞書型オブジェクト</param>
        /// <param name="key">上書き対象となるキー値</param>
        /// <param name="updateValue">上書きする値</param>
        /// <returns>1要素を上書きもしくは追加された辞書型オブジェクト</returns>
        public static Dictionary<TKey, TValue> UpdateOrInsert<TKey, TValue>(
            this Dictionary<TKey, TValue> origin,
            TKey key,
            TValue updateValue)
            => origin.UpdateOrInsert(new Dictionary<TKey, TValue> { { key, updateValue } });
        /// <summary>
        /// 辞書型オブジェクトの要素を一部上書き、もしくは追加する
        /// </summary>
        /// <typeparam name="TKey">キ－型</typeparam>
        /// <typeparam name="TValue">内容型</typeparam>
        /// <param name="origin">上書き対象の辞書型オブジェクト</param>
        /// <param name="updateMap">上書き対象のキーと上書きする値のセット</param>
        /// <returns>一部の要素を上書きもしくは追加された辞書型オブジェクト</returns>
        public static Dictionary<TKey, TValue> UpdateOrInsert<TKey, TValue>(
            this Dictionary<TKey, TValue> origin,
            Dictionary<TKey, TValue> updateMap)
        {
            var addMap = updateMap
                .Where(update => !origin.ContainsKey(update.Key))
                .Select(update => new { update.Key, update.Value });

            var resultMap = origin
                .Select(elem => new { elem.Key, Value = updateMap.GetOrDefault(elem.Key, elem.Value) })
                .Concat(addMap)
                .ToDictionary(elem => elem.Key, elem => elem.Value);

            return resultMap;
        }

        /// <summary>
        /// ある数値が<see cref="List{T}">のインデックスとして正当か否か判定する
        /// </summary>
        /// <typeparam name="TValue">内容型</typeparam>
        /// <param name="origin">判定対象のリスト</param>
        /// <param name="index">判定対象のインデックス</param>
        /// <returns>指定した数値がリストのインデックスに含まれているか否か</returns>
        public static bool ContainsIndex<TValue>(this List<TValue> origin, int index)
            => 0 <= index && index < origin.Count;

        /// <summary>
        /// オブジェクトと個数のセットをオブジェクトのリストに具体化する
        /// </summary>
        /// <typeparam name="TValue">オブジェクトの型</typeparam>
        /// <param name="abstractMap">オブジェクトと個数のセット</param>
        /// <returns>具体化されたリスト</returns>
        public static IEnumerable<TValue> Embody<TValue>(this Dictionary<TValue, int> abstractMap)
            => abstractMap
                .SelectMany(elem => Enumerable.Range(0, Mathf.Max(elem.Value, 0)).Select(_ => elem.Key));

        /// <summary>
        /// リストから選択基準値に基づき1要素を抜き出す
        /// </summary>
        /// <typeparam name="TValue">リストの要素型</typeparam>
        /// <param name="valueList">選択されるリストのセット</param>
        /// <param name="norm">選択基準値</param>
        /// <param name="rateList">確率分布</param>
        /// <returns>選択された値</returns>
        public static TValue Pick<TValue>(
            this IEnumerable<TValue> valueList,
            Fraction norm,
            List<int> rateList = null)
        {
            var actualRates = rateList?.Select(rate => Mathf.Max(rate, 0)) ?? valueList.Select(_ => 1);
            var ratevalues = actualRates.Zip(valueList, (rate, value) => new { rate, value });
            var selection = ratevalues.Sum(ratevalue => ratevalue.rate) * norm;
            if(selection < 0)
                return default;

            foreach(var ratevalue in ratevalues)
            {
                selection -= ratevalue.rate;
                if(selection < 0)
                    return ratevalue.value;
            }
            return default;
        }
        /// <summary>
        /// リストからランダムに1要素を抜き出す
        /// </summary>
        /// <typeparam name="TValue">リストの要素型</typeparam>
        /// <param name="valueList">選択されるリストのセット</param>
        /// <param name="rateList">確率分布</param>
        /// <returns>選択された値</returns>
        public static TValue Pick<TValue>(this IEnumerable<TValue> valueList, List<int> rateList = null)
            => valueList.Pick((rateList?.Sum() ?? valueList.Count()).SetupRandomNorm(), rateList);
        /// <summary>
        /// 渡されたリストの内容の順序をシャッフルしてリストとして返す
        /// </summary>
        /// <typeparam name="TValue">リストの要素型</typeparam>
        /// <param name="origin">シャッフル元のリスト</param>
        /// <returns>シャッフルされた後のリスト</returns>
        public static IOrderedEnumerable<TValue> Shuffle<TValue>(this IEnumerable<TValue> origin)
            => origin.OrderBy(_ => Random.Range(int.MinValue, int.MaxValue));
    }
}
