using System;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// プレファブなどの実体化されたオブジェクト類の管理クラス
    /// </summary>
    public static class PrefabManager
    {
        /// <summary>
        /// ゲームオブジェクトに親オブジェクトを設定して返す
        /// 親オブジェクトとしてnull指定した場合は親の解除
        /// </summary>
        /// <typeparam name="TPrefab">ゲームオブジェクト型</typeparam>
        /// <typeparam name="TParent">親のオブジェクト型</typeparam>
        /// <param name="prefab">ゲームオブジェクト</param>
        /// <param name="parent">親のオブジェクト</param>
        /// <returns>親の設定されたゲームオブジェクトk</returns>
        public static TPrefab SetParent<TPrefab, TParent>(this TPrefab prefab, TParent parent = null)
            where TPrefab : Component
            where TParent : Component
        {
            if(prefab == null) throw new ArgumentNullException(nameof(prefab));

            //Unityのgameobjectがnullを独自実装してるので2項演算で判定する
            var parentTransform = parent != null ? parent?.transform : null;

            prefab.transform.SetParent(parentTransform);
            return prefab;
        }

        /// <summary>
        /// ゲームオブジェクトを削除する
        /// </summary>
        /// <typeparam name="TPrefab">削除対象オブジェクトの型</typeparam>
        /// <param name="prefab">削除対象オブジェクト</param>
        public static TPrefab Destroy<TPrefab>(this TPrefab prefab) where TPrefab : Component
        {
            if(prefab == null) return prefab;

            foreach(Component child in prefab.transform) if(child is Component) child.Destroy();

            UnityEngine.Object.Destroy(prefab.gameObject);
            return prefab;
        }
    }
}
