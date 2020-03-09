using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.View.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.View.Model.Entity
{
    /// <summary>
    /// ビュー類のルートになるオブジェクト
    /// それだけ
    /// </summary>
    public class ViewState : PrefabAbst, IComponentRepository
    {
        protected Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>> _viewMap
              = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>();

        public ViewStateKey stateKey { get; set; }

        public Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>> GetAllMap() => _viewMap;

        public Component Search((ViewDeployment deploy, IViewKey view) key)
        {
            var queue = GetQueue(key);
            return queue.AnyNotNull() ? queue.Peek() : default;
        }

        public Component Search(ViewDeployment deploy, IViewKey viewKey) => Search((deploy, viewKey));

        public TComponent Search<TComponent>(ViewDeployment deploy, IViewKey viewKey)
            where TComponent : Component
            => (Search(deploy, viewKey) is Component component)
                ? component.GetComponent<TComponent>()
                : default;

        public Component Pop((ViewDeployment deploy, IViewKey view) key)
        {
            var queue = GetQueue(key);
            return queue.AnyNotNull() ? queue.Dequeue() : default;
        }

        public Component Pop(ViewDeployment deploy, IViewKey viewKey)
            => Pop((deploy, viewKey));

        public TComponent Pop<TComponent>(ViewDeployment deploy, IViewKey viewKey)
            where TComponent : Component
            => (Pop(deploy, viewKey) is Component component)
                ? component.GetComponent<TComponent>()
                : default;

        Queue<Component> GetQueue((ViewDeployment deploy, IViewKey view) key)
            => key.deploy is ViewDeployment && key.view is IViewKey
                ? _viewMap.GetOrDefault(key.deploy)?.GetOrDefault(key.view)
                : default;

        public TComponent Save<TComponent>((ViewDeployment deploy, IViewKey view) key, TComponent component)
            where TComponent : Component
        {
            if(!_viewMap.ContainsKey(key.deploy))
                _viewMap.Add(key.deploy, new Dictionary<IViewKey, Queue<Component>>());

            var targetDployMap = _viewMap[key.deploy];
            if(!targetDployMap.ContainsKey(key.view))
                targetDployMap.Add(key.view, new Queue<Component>());

            GetQueue(key).Enqueue(component);

            return component;
        }
        public TComponent Save<TComponent>(ViewDeployment deploy, IViewKey viewKey, TComponent component)
            where TComponent : Component
            => Save((deploy, viewKey), component);

        public bool isDestroied { get; private set; } = false;
        public ViewState Destroy()
        {
            isDestroied = true;
            return this;
        }
    }
}