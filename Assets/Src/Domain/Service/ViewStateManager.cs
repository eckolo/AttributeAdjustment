using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
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
        public static TViewState SetNewView<TViewState, TViewValue>(
            this TViewState state,
            TViewValue value)
            where TViewState : ViewStateAbst
            where TViewValue : IViewValue
            => state.SetNewView(new[] { value });
        public static TViewState SetNewView<TViewState, TViewValue>(
            this TViewState state,
            IEnumerable<TViewValue> values)
            where TViewState : ViewStateAbst
            where TViewValue : IViewValue
        {
            var viewStationerys = values.Select(value => new ViewStationery(value));
            state.AddViewStationerys(viewStationerys);

            var action = new ViewAction.Generate(viewStationerys);
            state.viewActionQueue.Enqueue(action);

            return state;
        }
    }
}
