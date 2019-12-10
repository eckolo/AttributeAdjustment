using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
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
        public static async UniTask<TViewState> IndicateViewAction<TViewState>(
            this TViewState state,
            ViewAction action)
            where TViewState : ViewState
        {
            switch(action.actionType)
            {
                case ViewAction.Pattern.GENERATE:
                    return await state.Generate(action);
                case ViewAction.Pattern.UPDATE:
                    return await state.Update(action);
                case ViewAction.Pattern.DELETE:
                    return await state.Delete(action);
                default:
                    throw new ArgumentOutOfRangeException(nameof(action.actionType));
            }
        }

        static async UniTask<TViewState> Generate<TViewState>(this TViewState state, ViewAction action)
            where TViewState : ViewState
        {
            switch(action.actor)
            {
                case TextMeshStationeryValue textMeshStationery:
                    return state.SetText(textMeshStationery);
                default:
                    throw new ArgumentOutOfRangeException(action.actor.GetType().ToString());
            }
        }

        static async UniTask<TViewState> Update<TViewState>(this TViewState state, ViewAction action)
            where TViewState : ViewState
        {
            switch(action.actor)
            {
                case TextMeshStationeryValue actor:
                    switch(action.target)
                    {
                        case TextMeshStationeryValue target:
                            return state.UpdateText(actor, target.text, target.position);
                        default:
                            throw new ArgumentOutOfRangeException(action.target.GetType().ToString());
                    }
                default:
                    throw new ArgumentOutOfRangeException(action.actor.GetType().ToString());
            }
        }

        static async UniTask<TViewState> Delete<TViewState>(this TViewState state, ViewAction action)
            where TViewState : ViewState
        {
            switch(action.actor)
            {
                case TextMeshStationeryValue textMeshStationery:
                    return state.DestroyText(textMeshStationery);
                default:
                    throw new ArgumentOutOfRangeException(action.actor.GetType().ToString());
            }
        }
    }
}
