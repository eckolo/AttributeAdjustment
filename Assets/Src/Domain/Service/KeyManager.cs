using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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
        public static bool Judge(this List<KeyCode> keys, KeyTiming timing = KeyTiming.DOWN, UnityAction<IEnumerable<KeyCode>> endProcess = null)
        {
            if(keys == null) return false;
            if(keys.Count <= 0) return false;
            var matchKeys = keys.Where(key => key.Judge(timing));
            endProcess?.Invoke(matchKeys);
            return timing == KeyTiming.OFF ? keys.All(key => key.Judge(timing)) : matchKeys.Any();
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
        public static bool Judge(this KeyCode judgedKey, List<KeyCode> keys) => keys.Contains(judgedKey);
        /// <summary>
        /// キーリストに指定キーが含まれるか否か判定
        /// </summary>
        /// <param name="judgedKey">指定のキー</param>
        /// <param name="keys">含まれる判定先キーリスト</param>
        /// <returns>含まれているか否か</returns>
        public static bool Judge(this KeyCode? judgedKey, List<KeyCode> keys) => judgedKey?.Judge(keys) ?? false;
    }
}
