using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using Assets.Src.Infrastructure.Model.Entity;
using Assets.Src.Infrastructure.Service;
using System;
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
            LogHub.TRACE.LeaveLog($"{typeof(CentralController).FullName} StartUp", new FileManager());
            var myself = new GameObject(nameof(CentralController), typeof(CentralController));
        }

        /// <summary>
        /// シーン読み込み後に呼ばれるコールバック
        /// </summary>
        async void Start()
        {
            LogHub.TRACE.LeaveLog($"{typeof(CentralController).FullName} Awake", new FileManager());
            await SetUp();
        }

        /// <summary>
        /// システム的な初期処理
        /// </summary>
        /// <returns>初期処理正常完了フラグ</returns>
        async UniTask<bool> SetUp()
        {
            var viewRoot = new GameObject(nameof(View), typeof(View)).GetComponent<View>().SetParent(this);
            gameFoundation = GameFoundation.CreateNewState(DateTime.Now.GetHashCode(), viewRoot);

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
            //デバッグ実行時は原因箇所追いやすくするために直接エラーを投げる
            if(Debug.isDebugBuild)
            {
                await ExecuteMainRoutine();
            }
            else
            {
                try
                {
                    await ExecuteMainRoutine();
                }
                catch(Exception error)
                {
                    LogHub.ERROR.LeaveLog(error.ToString(), new FileManager());
                    throw error;
                }
            }
        }
        /// <summary>
        /// メインルーチン制御の実行
        /// </summary>
        /// <returns>イテレータ</returns>
        async UniTask ExecuteMainRoutine()
        {
            while(true)
            {
                LogHub.DEBUG.LeaveLog($"{gameFoundation.nowState} TurnByTurn", new FileManager());
                await Wait.Until(120);
            }
        }
    }
}
