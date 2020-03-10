using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using Assets.Src.Domain.Service;
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
            stateKey = await stateKey.IndicateViewMap(action, repository);

            stateKey = await stateKey.IndicateActually(action, repository);

            return stateKey;
        }

        public static async UniTask<TViewStateKey> IndicateViewMap<TViewStateKey>(
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
        static async UniTask<TViewStateKey> IndicateActually<TViewStateKey>(
            this TViewStateKey stateKey,
            ViewAction action,
            IViewStateRepository repository)
            where TViewStateKey : ViewStateKey
        {
            var state = repository.Search(stateKey);
            if(state is null)
                return stateKey;

            var viewMap = state.GetAllMap();
            var layoutMap = stateKey.viewLayoutMap;

            var viewPositions = new[] { action.actorDeployment, action.targetDeployment }
                .Where(dep => dep is ViewDeployment)
                .SelectMany(dep => viewMap
                    .GetOrDefault(dep)
                    .GetPositions(layoutMap.GetOrDefault(dep))
                    .Select(pair => (dep, pair.component, pair.position)));
            foreach(var (deployment, component, position) in viewPositions)
            {
                component.transform.localPosition = deployment.ToPosition() + position;
            }

            if(state is ViewState && state.isDestroied)
                UnityEngine.Object.Destroy(state.gameObject);

            return stateKey;
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
                    {
                        var stateNew = repository.GenerateViewState(viewStateKey);
                        stateNew.Save(ViewDeploymentProperties.stateMyself, stateKey, stateNew);
                        return stateKey;
                    }
                case ViewStateKey viewStateKey:
                    {
                        var parentState = repository.Search(stateKey);
                        var actorState = repository.GenerateViewState(viewStateKey);

                        parentState.Save((action.actorDeployment, viewStateKey), actorState);
                        actorState.SetParent(parentState);
                        return stateKey;
                    }
                case ITextMeshKey textMeshStationery:
                    repository.Search(stateKey).SetText(action.actorDeployment, textMeshStationery);
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
                            repository.Search(stateKey)
                                .UpdateText(action.actorDeployment, actor, action.targetDeployment, target.text);
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
                    repository.Search(stateKey).DestroyText(action.actorDeployment, textMeshStationery);
                    return stateKey;
                default:
                    throw new ArgumentOutOfRangeException(action.actor.GetType().ToString());
            }
        }
    }
}
