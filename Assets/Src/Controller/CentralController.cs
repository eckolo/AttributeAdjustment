using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.Infrastructure.Model.Entity;
using Assets.Src.Infrastructure.Service;
using System;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;

namespace Assets.Src.Controller
{
    /// <summary>
    /// システム的なゲーム処理統括
    /// </summary>
    public class CentralController : MonoBehaviour
    {
        /// <summary>
        /// 最初期起動関数
        /// とりあえず自己生成するだけ
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void StartUp()
        {
            LogHub.TRACE.LeaveLog($"{typeof(CentralController).FullName} {nameof(SetUp)}", new FileManager());
            var myself = new GameObject(nameof(CentralController), typeof(CentralController));
        }

        /// <summary>
        /// シーン読み込み後に呼ばれるコールバック
        /// </summary>
        async void Start()
        {
            LogHub.TRACE.LeaveLog($"{typeof(CentralController).FullName} {nameof(Start)}", new FileManager());
            await SetUp();
        }

        /// <summary>
        /// システム的な初期処理
        /// </summary>
        /// <returns>初期処理正常完了フラグ</returns>
        async UniTask<bool> SetUp()
        {
            gameFoundation = GameFoundation.CreateNewState(DateTime.Now.GetHashCode());

            await IntroductionMainRoutine();
            return true;
        }
        /// <summary>
        /// ゲーム情報基盤
        /// </summary>
        GameFoundation gameFoundation = default;

        /// <summary>
        /// メインルーチン制御への導入
        /// </summary>
        /// <returns>イテレータ</returns>
        async UniTask IntroductionMainRoutine()
        {
            try
            {
                await ExecuteMainRoutine();
            }
            //デバッグ実行時は原因箇所追いやすくするために直接エラーを投げる
            catch(Exception error) when(!Debug.isDebugBuild)
            {
                LogHub.ERROR.LeaveLog(error.ToString(), new FileManager());
                throw error;
            }
        }
        /// <summary>
        /// メインルーチン制御の実行
        /// </summary>
        /// <returns>イテレータ</returns>
        async UniTask ExecuteMainRoutine()
        //TODO ビュー（ビューコントローラ）呼び出し処理実装
        //TODO 戦闘処理の呼び出しとか普段のゲームルーチン設計
        {
            while(true)
            {
                var list = new List<string> { "一富士", "二鷹", "三茄子", "四は無し" };
                int? result;
                using(var state = await list.Setup(1))
                {
                    await state.Choice(new KeyConfigs());
                    result = state.choiced;
                }

                LogHub.DEBUG.LeaveLog($"{nameof(result)} = {result}", new FileManager());
                LogHub.DEBUG.LeaveLog($"{gameFoundation.nowState} TurnByTurn", new FileManager());
                await Wait.Until(120);
            }
        }
    }
}
