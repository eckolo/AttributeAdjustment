using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
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
    /// <summary>
    /// <see cref="ViewAction"/>を実際に実行する
    /// </summary>
    public static class ViewActionPractitioner
    {
        public static TViewStateKey Indicate<TViewStateKey>(
            this TViewStateKey state,
            IViewStateRepository repository)
            where TViewStateKey : ViewStateKey
        {
            if(!state.viewActionList.Any())
                return state;

            _ = state.viewActionList
                .Select(action => state.IndicateViewAction(action, repository))
                .Aggregate((task1, task2) => task1.ContinueWith(_ => task2));

            state.viewActionList.Clear();
            return state;
        }
        public static async UniTask<TViewStateKey> IndicateAsync<TViewStateKey>(
            this TViewStateKey state,
            IViewStateRepository repository)
            where TViewStateKey : ViewStateKey
        {
            if(!state.viewActionList.Any())
                return state;

            var tasks = state.viewActionList
                .Select(action => state.IndicateViewAction(action, repository))
                .Aggregate((task1, task2) => task1.ContinueWith(_ => task2));

            state.viewActionList.Clear();
            return await tasks.ContinueWith(_ => state);
        }
        public static async UniTask<TViewStateKey> IndicateViewAction<TViewStateKey>(
            this TViewStateKey stateKey,
            ViewAction action,
            IViewStateRepository repository)
            where TViewStateKey : ViewStateKey
        {
            switch(action.actionType)
            {
                case ViewAction.Pattern.GENERATE:
                    return await stateKey.Generate(action, repository);
                case ViewAction.Pattern.UPDATE:
                    return await stateKey.Update(action, repository);
                case ViewAction.Pattern.DELETE:
                    return await stateKey.Delete(action, repository);
                default:
                    throw new ArgumentOutOfRangeException(nameof(action.actionType));
            }
        }

        static async UniTask<TViewStateKey> Generate<TViewStateKey>(
            this TViewStateKey stateKey,
            ViewAction action,
            IViewStateRepository repository)
            where TViewStateKey : ViewStateKey
        {
            switch(action.actor)
            {
                case TViewStateKey viewStateKey:
                    repository.GenerateViewState(viewStateKey);
                    return stateKey;
                case ITextMeshKey textMeshStationery:
                    repository.Search(stateKey).SetText(textMeshStationery);
                    return stateKey;
                default:
                    throw new ArgumentOutOfRangeException(action.actor.GetType().ToString());
            }
        }

        static async UniTask<TViewStateKey> Update<TViewStateKey>(
            this TViewStateKey stateKey,
            ViewAction action,
            IViewStateRepository repository)
            where TViewStateKey : ViewStateKey
        {
            switch(action.actor)
            {
                case ITextMeshKey actor:
                    switch(action.target)
                    {
                        case ITextMeshKey target:
                            repository.Search(stateKey).UpdateText(actor, target.text, Vector2.zero);
                            return stateKey;
                        default:
                            throw new ArgumentOutOfRangeException(action.target.GetType().ToString());
                    }
                default:
                    throw new ArgumentOutOfRangeException(action.actor.GetType().ToString());
            }
        }

        static async UniTask<TViewStateKey> Delete<TViewStateKey>(
            this TViewStateKey stateKey,
            ViewAction action,
            IViewStateRepository repository)
            where TViewStateKey : ViewStateKey
        {
            switch(action.actor)
            {
                case TViewStateKey viewStateKey:
                    repository.Search(stateKey).Destroy();
                    return stateKey;
                case ITextMeshKey textMeshStationery:
                    repository.Search(stateKey).DestroyText(textMeshStationery);
                    return stateKey;
                default:
                    throw new ArgumentOutOfRangeException(action.actor.GetType().ToString());
            }
        }
    }
}
