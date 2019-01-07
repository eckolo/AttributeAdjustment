using System;
using System.Collections.Generic;
using System.Linq;
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
        public static async UniTask Wait(int delay, List<KeyCode> interruptions)
        {
            await Wait(delay, () => interruptions.Judge());
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
        public static async UniTask Wait(List<KeyCode> receiveableKeys, Action<KeyCode?, bool> endProcess = null)
        {
            if(receiveableKeys.Count <= 0) return;

            KeyCode? receivedKey = null;
            bool first = false;
            do
            {
                await Wait(1);

                if(receiveableKeys.Judge(KeyTiming.DOWN, keys => receivedKey = keys.FirstOrDefault()))
                {
                    first = true;
                    break;
                }
                if(receiveableKeys.Judge(KeyTiming.ON, keys => receivedKey = keys.FirstOrDefault()))
                {
                    break;
                }
            } while(receivedKey == null);

            endProcess?.Invoke(receivedKey, first);
        }
        /// <summary>
        /// 特定キー押下まで待機する
        /// </summary>
        /// <param name="receiveableKey">押下が終了条件となるキー</param>
        /// <param name="endProcess">待機終了時処理</param>
        /// <returns>待機タスク</returns>
        public static async UniTask Wait(KeyCode receiveableKey, Action<KeyCode?, bool> endProcess = null)
        {
            await Wait(new List<KeyCode> { receiveableKey }, endProcess);
        }
    }
}
