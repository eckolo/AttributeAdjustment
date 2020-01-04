using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx.Async;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// ゲーム実行を待つ処理群
    /// </summary>
    public static class Wait
    {
        /// <summary>
        /// 指定条件を満たすまで待機する
        /// </summary>
        /// <param name="endCondition">終了条件</param>
        /// <param name="token">キャンセルトークン</param>
        /// <returns>待機タスク</returns>
        public static async UniTask Until(Func<bool> endCondition, CancellationToken token = default)
        {
            await UniTask.WaitUntil(endCondition, cancellationToken: token);
        }
        /// <summary>
        /// 指定フレーム数の経過もしくは指定条件を満たすまで待機する
        /// </summary>
        /// <param name="delay">待機フレーム数</param>
        /// <param name="endCondition">終了条件</param>
        /// <returns>待機タスク</returns>
        public static async UniTask Until(int delay, Func<bool> endCondition = null)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            var delayTask = UniTask.DelayFrame(delay, cancellationToken: token);

            if(endCondition != null) await UniTask.WhenAny(delayTask, Until(endCondition, token));
            else await delayTask;

            cancellationTokenSource?.Cancel();
        }
        /// <summary>
        /// 指定フレーム数の経過もしくは特定キー群いずれかの押下まで待機する
        /// </summary>
        /// <param name="delay">待機フレーム数</param>
        /// <param name="interruptions">押下が終了条件となるキーのリスト</param>
        /// <returns>待機タスク</returns>
        public static async UniTask<IEnumerable<KeyCode>> Until(int delay, List<KeyCode> interruptions)
        {
            IEnumerable<KeyCode> keyCodes = null;
            await Until(delay, () => {
                bool result;
                (result, keyCodes) = interruptions.Judge();
                return result;
            });

            return keyCodes;
        }
        /// <summary>
        /// 指定フレーム数の経過もしくは特定キーの押下まで待機する
        /// </summary>
        /// <param name="delay">待機フレーム数</param>
        /// <param name="interruption">押下が終了条件となるキー</param>
        /// <returns>待機タスク</returns>
        public static async UniTask Until(int delay, KeyCode interruption)
        {
            var interruptions = new List<KeyCode> { interruption };
            await Until(delay, interruptions);
        }
        /// <summary>
        /// 特定キー群いずれかの押下まで待機する
        /// </summary>
        /// <param name="receiveableKeys">押下が終了条件となるキーのリスト</param>
        /// <param name="endProcess">待機終了時処理</param>
        /// <returns>待機タスク</returns>
        public static async UniTask<(IEnumerable<KeyCode> receiveKeys, KeyTiming timing)> Until(List<KeyCode> receiveableKeys)
        {
            if(!receiveableKeys.Any()) return default;
            (IEnumerable<KeyCode> receiveKeys, KeyTiming timing) = (default, default);

            await Until(() => {
                {
                    var receive = false;
                    timing = KeyTiming.DOWN;
                    (receive, receiveKeys) = receiveableKeys.Judge(timing);
                    if(receive) return true;
                }
                {
                    var receive = false;
                    timing = KeyTiming.ON;
                    (receive, receiveKeys) = receiveableKeys.Judge(timing);
                    if(receive) return true;
                }
                return false;
            });

            return (receiveKeys, timing);
        }
        /// <summary>
        /// 特定キー押下まで待機する
        /// </summary>
        /// <param name="receiveableKey">押下が終了条件となるキー</param>
        /// <param name="endProcess">待機終了時処理</param>
        /// <returns>待機タスク</returns>
        public static async UniTask<(IEnumerable<KeyCode> receiveKeys, KeyTiming timing)> Until(KeyCode receiveableKey)
            => await Until(new List<KeyCode> { receiveableKey });
    }
}
