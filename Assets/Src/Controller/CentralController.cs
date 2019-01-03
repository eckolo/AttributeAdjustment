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
        }

        /// <summary>
        /// オブジェクト生成直後に呼ばれるコールバック
        /// </summary>
        void Awake()
        {
            SetUp();
        }

        /// <summary>
        /// システム的な初期処理
        /// </summary>
        /// <returns>初期処理正常完了フラグ</returns>
        bool SetUp()
        {
            if(mainThread == default(Coroutine)) mainThread = StartCoroutine(IntroductionMainRoutine());
            return true;
        }

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
                throw error;
            }
        }
        /// <summary>
        /// メインルーチン制御の実行
        /// </summary>
        /// <returns>イテレータ</returns>
        IEnumerator ExecuteMainRoutine()
        {
            yield break;
        }

        /// <summary>
        /// メインルーチンがのっているコルーチン保持変数
        /// </summary>
        Coroutine mainThread = default(Coroutine);
    }
}
