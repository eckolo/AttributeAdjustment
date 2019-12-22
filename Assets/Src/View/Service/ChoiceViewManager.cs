using Assets.Src.Domain.Model.Entity;
using Assets.Src.View.Factory;
using Assets.Src.View.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx.Async;
using UnityEngine;

namespace Assets.Src.View.Service
{
    public static class ChoiceViewManager
    {
        public static async UniTask<ChoiceState> Indicate(
            this ChoiceState state,
            IViewStateRepository repository)
        {
            while(state.viewActionQueue.Any())
            {
                var action = state.viewActionQueue.Dequeue();
                state = await state.IndicateViewAction(action, repository);
            }
            return state;
        }
    }
}
