using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using Assets.Src.View.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Mock
{
    public class ViewStateRepositoryMock : IViewStateRepository
    {
        public ViewStateRepositoryMock(Dictionary<ViewStateKey, ViewState> viewStateMap = null)
        {
            this.viewStateMap = viewStateMap ?? this.viewStateMap;
        }

        readonly Dictionary<ViewStateKey, ViewState> viewStateMap = new Dictionary<ViewStateKey, ViewState>();

        public ViewState Search<TKey>(TKey key) where TKey : ViewStateKey
            => key is TKey
                ? viewStateMap.GetOrDefault(key)
                : default;

        public ViewState SearchOrGenerate<TKey>(TKey key) where TKey : ViewStateKey
            => Search(key) ?? this.GenerateViewState(key);

        public ViewState Save<TKey, TViewState>(TKey key, TViewState state)
            where TKey : ViewStateKey
            where TViewState : ViewState
        {
            if(viewStateMap.ContainsKey(key))
                return state;

            viewStateMap.Add(key, state);
            return state;
        }
    }
}
