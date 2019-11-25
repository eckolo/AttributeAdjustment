using Assets.Src.Domain.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Repository
{
    public interface IComponentRepository
    {
        Component SearchView<TKey>(TKey key) where TKey : IViewKey;

        Component SaveView<TKey>(TKey key, Component state) where TKey : IViewKey;
    }
}
