using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Service
{
    public static class ViewStateManager
    {
        public static TViewState SetNewView<TViewState, TView>(
            this TViewState state,
            TView origin)
            where TViewState : ViewStateAbst
            where TView : IViewValue
            => state.SetNewView(new[] { origin });
        public static TViewState SetNewView<TViewState, TView>(
            this TViewState state,
            IEnumerable<TView> origins)
            where TViewState : ViewStateAbst
            where TView : IViewValue
        {
            foreach(var view in origins)
            {
                state.viewList.Add(view);
            }

            var action = new ViewAction.Generate(origins.Select(view => (IViewValue)view));
            state.viewActionQueue.Enqueue(action);

            return state;
        }
    }
}
