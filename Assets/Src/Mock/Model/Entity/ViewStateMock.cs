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
    }
}
