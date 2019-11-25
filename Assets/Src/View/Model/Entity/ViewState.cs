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
        Dictionary<IViewKey, Component> _viewMap = new Dictionary<IViewKey, Component>();

        public Component Search<TKey>(TKey key) where TKey : IViewKey
            => key is TKey
                ? _viewMap.GetOrDefault(key)
                : default;
        public TComponent Search<TKey, TComponent>(TKey key)
            where TKey : IViewKey
            where TComponent : Component
            => Search(key)?.GetComponent<TComponent>();
        public Component Save<TKey, TComponent>(TKey key, TComponent component)
            where TKey : IViewKey
            where TComponent : Component
        {
            if(_viewMap.ContainsKey(key) && _viewMap[key] != component)
                _viewMap[key].Destroy();

            _viewMap = _viewMap.UpdateOrInsert(key, component);
            return _viewMap[key];
        }

        public bool isDestroied { get; private set; } = false;
        public ViewState Destroy()
        {
            isDestroied = true;
            return this;
        }
    }
}