using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    public static class KeyManager
    {
        /// <summary>
        /// 複数キーのOR押下判定
        /// </summary>
        /// <param name="keys">判定対象キー一覧</param>
        /// <param name="timing">判定対象タイミング</param>
        /// <param name="endProcess">一致キーに対する操作</param>
        /// <returns>判定結果</returns>
        public static (bool result, IEnumerable<KeyCode> keys) Judge(this List<KeyCode> keys, KeyTiming timing = KeyTiming.DOWN)
        {
            if(keys == null) return (false, default);
            if(keys.Count <= 0) return (false, default);

            var matchKeys = keys.Where(key => key.Judge(timing));
            var result = timing == KeyTiming.OFF ? keys.All(key => key.Judge(timing)) : matchKeys.Any();

            return (result, matchKeys);
        }
        /// <summary>
        /// 単数キーの押下判定
        /// </summary>
        /// <param name="key">判定対象キー</param>
        /// <param name="timing">判定対象タイミング</param>
        /// <returns>判定結果</returns>
        public static bool Judge(this KeyCode key, KeyTiming timing = KeyTiming.DOWN)
        {
            switch(timing)
            {
                case KeyTiming.DOWN:
                    return Input.GetKeyDown(key);
                case KeyTiming.ON:
                    return Input.GetKey(key);
                case KeyTiming.UP:
                    return Input.GetKeyUp(key);
                case KeyTiming.OFF:
                    return !Input.GetKey(key) && !Input.GetKey(key) && !Input.GetKeyUp(key);
                default:
                    return false;
            }
        }

        /// <summary>
        /// キーリストに指定キーが含まれるか否か判定
        /// </summary>
        /// <param name="judgedKey">指定のキー</param>
        /// <param name="keys">含まれる判定先キーリスト</param>
        /// <returns>含まれているか否か</returns>
        public static bool Judge(this KeyCode judgedKey, IEnumerable<KeyCode> keys) => keys.Contains(judgedKey);
        /// <summary>
        /// キーリストに指定キーが含まれるか否か判定
        /// </summary>
        /// <param name="judgedKey">指定のキー</param>
        /// <param name="keys">含まれる判定先キーリスト</param>
        /// <returns>含まれているか否か</returns>
        public static bool Judge(this KeyCode? judgedKey, IEnumerable<KeyCode> keys) => judgedKey?.Judge(keys) ?? false;
        /// <summary>
        /// 指定キーリストから別のキーリストに含まれるキーを抽出
        /// </summary>
        /// <param name="judgedKeys">指定のキーリスト</param>
        /// <param name="keys">含まれる判定先キーリスト</param>
        /// <returns>含まれていたキーのリスト</returns>
        public static IEnumerable<KeyCode> Judge(this IEnumerable<KeyCode> judgedKeys, IEnumerable<KeyCode> keys)
            => judgedKeys.Where(judgedKey => judgedKey.Judge(keys));
    }
}
