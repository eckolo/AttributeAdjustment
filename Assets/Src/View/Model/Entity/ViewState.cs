using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Abstract;
using Assets.Src.View.Repository;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.View.Model.Entity
{
    /// <summary>
    /// ビュー類のルートになるオブジェクト
    /// それだけ
    /// </summary>
    public class ViewState : PrefabAbst, IComponentRepository
    {
        readonly Dictionary<IViewKey, Component> _viewMap = new Dictionary<IViewKey, Component>();

        public Component SearchView<TKey>(TKey key) where TKey : IViewKey
            => key is TKey
                ? _viewMap.GetOrDefault(key)
                : default;
        public Component SaveView<TKey>(TKey key, Component component) where TKey : IViewKey
        {
            if(_viewMap.ContainsKey(key))
                return component;

            _viewMap.Add(key, component);
            return component;
        }
    }
}