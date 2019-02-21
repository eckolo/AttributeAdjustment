using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Factory
{
    /// <summary>
    /// プレファブなどの実体化されるオブジェクト類の生成処理クラス
    /// </summary>
    public static class PrefabFactory
    {
        /// <summary>
        /// ゲームオブジェクトの新規作成
        /// </summary>
        /// <typeparam name="TPrefab">作成されるオブジェクトに実装される型</typeparam>
        /// <param name="parent">オブジェクトの親</param>
        /// <param name="objectName">オブジェクト名称</param>
        /// <returns>生成されたオブジェクト</returns>
        public static TPrefab SetPrefab<TPrefab>(this Component parent, string objectName = null)
            where TPrefab : Component
            => new GameObject(objectName ?? Constants.Texts.ANONYMOUS_NAME, typeof(TPrefab))
            .GetComponent<TPrefab>()
            .SetParent(parent);
        /// <summary>
        /// ゲームオブジェクトの新規作成
        /// 型名をそのままオブジェクト名とする
        /// </summary>
        /// <typeparam name="TPrefab">作成されるオブジェクトに実装される型</typeparam>
        /// <returns>生成されたオブジェクト</returns>
        public static TPrefab SetPrefab<TPrefab>(this Component parent)
            where TPrefab : Component
            => parent.SetPrefab<TPrefab>(typeof(TPrefab).FullName.Split(new[] { '.', '+' }).Last());
    }
}
