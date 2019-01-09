using System;
using System.Collections.Generic;
using System.Threading;
using UniRx.Async;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// ゲーム実行に割り込みで処理を掛けるメソッド群
    /// </summary>
    public static class Interruptor
    {
        /// <summary>
        /// 指定条件を満たすまで待機する
        /// </summary>
        /// <param name="endCondition">終了条件</param>
        /// <param name="token">キャンセルトークン</param>
        /// <returns>待機タスク</returns>
        public static async UniTask Wait(Func<bool> endCondition, CancellationToken token = default)
        {
            await UniTask.WaitUntil(endCondition, cancellationToken: token);
        }
        /// <summary>
        /// 指定フレーム数の経過もしくは指定条件を満たすまで待機する
        /// </summary>
        /// <param name="delay">待機フレーム数</param>
        /// <param name="endCondition">終了条件</param>
        /// <returns>待機タスク</returns>
        public static async UniTask Wait(int delay, Func<bool> endCondition = null)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            var delayTask = UniTask.DelayFrame(delay, cancellationToken: token);

            if(endCondition != null) await UniTask.WhenAny(delayTask, Wait(endCondition, token));
            else await delayTask;

            cancellationTokenSource?.Cancel();
        }
        /// <summary>
        /// 指定フレーム数の経過もしくは特定キー群いずれかの押下まで待機する
        /// </summary>
        /// <param name="delay">待機フレーム数</param>
        /// <param name="interruptions">押下が終了条件となるキーのリスト</param>
        /// <returns>待機タスク</returns>
        public static async UniTask<IEnumerable<KeyCode>> Wait(int delay, List<KeyCode> interruptions)
        {
            IEnumerable<KeyCode> keyCodes = null;
            await Wait(delay, () => {
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
        public static async UniTask Wait(int delay, KeyCode interruption)
        {
            var interruptions = new List<KeyCode> { interruption };
            await Wait(delay, interruptions);
        }
        /// <summary>
        /// 特定キー群いずれかの押下まで待機する
        /// </summary>
        /// <param name="receiveableKeys">押下が終了条件となるキーのリスト</param>
        /// <param name="endProcess">待機終了時処理</param>
        /// <returns>待機タスク</returns>
        public static async UniTask<(IEnumerable<KeyCode> receiveKeys, bool first)> Wait(List<KeyCode> receiveableKeys)
        {
            if(receiveableKeys.Count <= 0) return default;

            while(true)
            {
                await Wait(1);

                var keyDown = receiveableKeys.Judge(KeyTiming.DOWN);
                var keyOn = receiveableKeys.Judge(KeyTiming.ON);

                if(keyDown.result) return (keyDown.keys, true);
                else if(keyOn.result) return (keyOn.keys, false);
            }

        }
        /// <summary>
        /// 特定キー押下まで待機する
        /// </summary>
        /// <param name="receiveableKey">押下が終了条件となるキー</param>
        /// <param name="endProcess">待機終了時処理</param>
        /// <returns>待機タスク</returns>
        public static async UniTask<(IEnumerable<KeyCode> receiveKeys, bool first)> Wait(KeyCode receiveableKey)
            => await Wait(new List<KeyCode> { receiveableKey });
    }
}
