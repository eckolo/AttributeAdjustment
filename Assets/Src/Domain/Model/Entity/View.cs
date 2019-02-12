using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using UnityEngine;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// ビュー類のルートになるオブジェクト
    /// それだけ
    /// </summary>
    public class View : PrefabAbst
    {
        /// <summary>
        /// ビューの新規生成
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">親オブジェクト</param>
        /// <returns>生成されたビュー</returns>
        public static View CleateNew(string name, MonoBehaviour parent = null)
        {
            var _name = name is null ? "" : $"_{name}";
            return new GameObject($"{nameof(View)}{_name}", typeof(View)).GetComponent<View>().SetParent(parent);
        }
    }
}