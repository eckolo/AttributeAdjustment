using Assets.Src.Domain.Model.Entity;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// プレファブなどの実体化されたオブジェクト類の管理クラス
    /// </summary>
    public static class PrefabManager
    {
        /// <summary>
        /// 名無しオブジェクトにつけられる名称
        /// </summary>
        public const string ANONYMOUS_NAME = "NoNameObject";
        /// <summary>
        /// ゲームオブジェクトの新規作成
        /// </summary>
        /// <typeparam name="TPrefab">作成されるオブジェクトに実装される型</typeparam>
        /// <param name="objectName">オブジェクト名称</param>
        /// <returns>生成されたオブジェクト</returns>
        public static TPrefab SetPrefab<TPrefab>(this View view, string objectName = null)
            where TPrefab : MonoBehaviour
            => new GameObject(objectName ?? ANONYMOUS_NAME, typeof(TPrefab))
            .GetComponent<TPrefab>()
            .SetParent(view);
        /// <summary>
        /// ゲームオブジェクトの新規作成
        /// 型名をそのままオブジェクト名とする
        /// </summary>
        /// <typeparam name="TPrefab">作成されるオブジェクトに実装される型</typeparam>
        /// <returns>生成されたオブジェクト</returns>
        public static TPrefab SetPrefab<TPrefab>(this View view) where TPrefab : MonoBehaviour
            => view.SetPrefab<TPrefab>(typeof(TPrefab).FullName.Split(new[] { '.', '+' }).Last());

        /// <summary>
        /// ゲームオブジェクトに親オブジェクトを設定して返す
        /// </summary>
        /// <typeparam name="Prefab">ゲームオブジェクト型</typeparam>
        /// <typeparam name="Parent">親のオブジェクト型</typeparam>
        /// <param name="prefab">ゲームオブジェクト</param>
        /// <param name="parent">親のオブジェクト</param>
        /// <returns>親の設定されたゲームオブジェクトk</returns>
        public static Prefab SetParent<Prefab, Parent>(this Prefab prefab, Parent parent = null)
            where Prefab : MonoBehaviour
            where Parent : MonoBehaviour
        {
            prefab.transform.SetParent(parent.transform);
            return prefab;
        }
    }
}
