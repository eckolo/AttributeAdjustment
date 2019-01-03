using Assets.Src.Domain.Service;
using Assets.Src.Infrastructure.Model.Entity;
using Assets.Src.Infrastructure.Model.Value;
using Assets.Src.Infrastructure.Service;
using System;
using System.Collections;
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
            PrefabManager.SetObject<CentralController>();
        }

        /// <summary>
        /// オブジェクト生成直後に呼ばれるコールバック
        /// </summary>
        void Awake()
        {
            LogHub.TRACE.LeaveLog($"{typeof(CentralController).FullName} Awake", new FileManager());
            SetUp();
        }

        /// <summary>
        /// システム的な初期処理
        /// </summary>
        /// <returns>初期処理正常完了フラグ</returns>
        bool SetUp()
        {
            viewRoot = viewRoot ?? PrefabManager.SetObject<ViewRoot>().SetParent(this);

            if(mainThread == default) mainThread = StartCoroutine(IntroductionMainRoutine());
            return true;
        }
        ViewRoot viewRoot = null;

        /// <summary>
        /// メインルーチン制御への導入
        /// </summary>
        /// <returns>イテレータ</returns>
        IEnumerator IntroductionMainRoutine()
        {
            //デバッグ実行時は原因箇所追いやすくするために直接エラーを投げる
            if(Debug.isDebugBuild) return ExecuteMainRoutine();
            try
            {
                return ExecuteMainRoutine();
            }
            catch(Exception error)
            {
                LogHub.ERROR.LeaveLog(error.ToString(), new FileManager());
                throw error;
            }
        }
        /// <summary>
        /// メインルーチン制御の実行
        /// </summary>
        /// <returns>イテレータ</returns>
        IEnumerator ExecuteMainRoutine()
        {
            var gameFoundation = GameFoundation.CreateNewState(DateTime.Now.GetHashCode());
            while(true)
            {
                LogHub.DEBUG.LeaveLog($"{gameFoundation.nowState} TurnByTurn", new FileManager());
                yield return null;
            }
        }

        /// <summary>
        /// メインルーチンがのっているコルーチン保持変数
        /// </summary>
        Coroutine mainThread = default;
    }
}
