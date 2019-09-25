using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Entity;
using Assets.Src.View.Repository;
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
        /// <see cref="ViewState"/>の新規生成処理
        /// 既に存在していた場合は取得
        /// </summary>
        /// <typeparam name="TKey">生成元のキーの型</typeparam>
        /// <param name="key">生成元のキー</param>
        /// <returns>生成された<see cref="ViewState"/></returns>
        public static ViewState GenerateViewState<TKey>(this ViewRoot viewRoot, TKey key) where TKey : ViewStateKey
        {
            if(viewRoot is null)
                throw new ArgumentNullException(nameof(viewRoot));
            if(key is null)
                throw new ArgumentNullException(nameof(key));

            var stateMap = viewRoot.viewStateMap;
            var name = $"{nameof(ViewState)}_{key.GetType().FullName}";

            if(stateMap is null)
                throw new ArgumentNullException(nameof(viewRoot.viewStateMap));

            lock(stateMap)
            {
                if(!stateMap.ContainsKey(key))
                {
                    var state = new GameObject(name, typeof(ViewState)).AddComponent<ViewState>();
                    stateMap.Add(key, state);
                }
            }

            return stateMap.GetOrDefault(key);
        }
    }
}
