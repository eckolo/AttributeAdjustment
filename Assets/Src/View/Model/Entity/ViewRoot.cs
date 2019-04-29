using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Abstract;
using UnityEngine;

namespace Assets.Src.View.Model.Entity
{
    /// <summary>
    /// ビュー類のルートになるオブジェクト
    /// それだけ
    /// </summary>
    public class ViewRoot : PrefabAbst
    {
        /// <summary>
        /// ビューの新規生成
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">親オブジェクト</param>
        /// <returns>生成されたビュー</returns>
        public static ViewRoot CleateNew(string name, MonoBehaviour parent = null)
        {
            var _name = name is null ? "" : $"_{name}";
            return new GameObject($"{nameof(ViewRoot)}{_name}", typeof(ViewRoot)).GetComponent<ViewRoot>().SetParent(parent);
        }
    }
}