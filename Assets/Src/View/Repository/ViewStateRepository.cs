﻿using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Repository
{
    public class ViewStateRepository : IViewStateRepository
    {
        public ViewStateRepository(Dictionary<ViewStateKey, ViewState> viewStateMap = null)
        {
            _viewStateMap = viewStateMap ?? _viewStateMap;
        }

        Dictionary<ViewStateKey, ViewState> _viewStateMap = new Dictionary<ViewStateKey, ViewState>();

        /// <summary>
        /// <see cref="ViewState"/>の検索
        /// </summary>
        /// <typeparam name="TKey">検索キーの型</typeparam>
        /// <param name="key">検索キー</param>
        /// <returns>検索結果</returns>
        public ViewState Search<TKey>(TKey key) where TKey : ViewStateKey
            => key is TKey
                ? _viewStateMap.GetOrDefault(key)
                : default;

        public ViewState SearchOrGenerate<TKey>(TKey key) where TKey : ViewStateKey
            => Search(key) ?? this.GenerateViewState(key);

        public ViewState Save<TKey, TViewState>(TKey key, TViewState state)
            where TKey : ViewStateKey
            where TViewState : ViewState
        {
            if(key is null)
                throw new ArgumentNullException(nameof(key));

            if(_viewStateMap.ContainsKey(key) && _viewStateMap[key] != state)
                _viewStateMap[key].Destroy();

            _viewStateMap = _viewStateMap.UpdateOrInsert(key, state);
            return _viewStateMap[key];
        }
    }
}
