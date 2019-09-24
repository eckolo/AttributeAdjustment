using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Repository
{
    public static class ViewStateRepository
    {
        /// <summary>
        /// <see cref="ViewState"/>の検索
        /// </summary>
        /// <typeparam name="TKey">検索キーの型</typeparam>
        /// <param name="key">検索キー</param>
        /// <returns>検索結果</returns>
        public static ViewState SearchViewState<TKey>(this ViewRoot viewRoot, TKey key) where TKey : ViewStateKey
            => key is TKey
                ? viewRoot.viewStateMap.GetOrDefault(key)
                : default;
    }
}
