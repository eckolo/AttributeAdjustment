using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Mock.Model.Entity
{
    public class ViewStateMock : ViewState
    {
        public static ViewStateMock Generate(string name, MonoBehaviour parent = null)
        {
            var _name = name is null ? "" : $"_{name}";
            return new GameObject($"{nameof(ViewStateMock)}{_name}", typeof(ViewStateMock))
                .GetComponent<ViewStateMock>()
                .SetParent(parent);
        }
        public static ViewStateMock Generate(Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>> _viewMap)
        {
            var mock = new GameObject(nameof(ViewStateMock), typeof(ViewStateMock)).GetComponent<ViewStateMock>();
            mock._viewMap = _viewMap;
            return mock;
        }
    }
}
