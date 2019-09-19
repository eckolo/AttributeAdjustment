using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Factory
{
    public static class ViewStateFactory
    {
        /// <summary>
        /// ビューの新規生成
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">親オブジェクト</param>
        /// <returns>生成されたビュー</returns>
        public static ViewState ToViewState(this string name, MonoBehaviour parent = null)
        {
            var _name = name is null ? "" : $"_{name}";
            return new GameObject($"{nameof(ViewState)}{_name}", typeof(ViewState)).GetComponent<ViewState>().SetParent(parent);
        }
    }
}
