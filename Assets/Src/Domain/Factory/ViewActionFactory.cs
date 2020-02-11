﻿using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Factory
{
    /// <summary>
    /// <see cref="ViewAction"/>生成処理関連
    /// </summary>
    public static class ViewActionFactory
    {
        /// <summary>
        /// 単一ビューオブジェクトに対するアクションの作成
        /// </summary>
        /// <param name="view">アクション対象のビューオブジェクト</param>
        /// <param name="pattern">アクション種別</param>
        /// <returns>生成されたビューアクション</returns>
        public static ViewAction ToViewAction(this IViewKey view, ViewAction.Pattern pattern)
            => new ViewAction(pattern, view);
        /// <summary>
        /// 複数ビューオブジェクトに対するアクションの作成
        /// </summary>
        /// <param name="views">アクション対象のビューオブジェクト群</param>
        /// <param name="pattern">アクション種別</param>
        /// <returns>生成されたビューアクション</returns>
        public static ViewAction ToViewAction<TViewKey>(this IEnumerable<TViewKey> views, ViewAction.Pattern pattern)
            where TViewKey : IViewKey
            => views
                .Select(view => view.ToViewAction(pattern))
                .Aggregate((action1, action2) => action1.AddNextAction<ViewAction>(action2));
        /// <summary>
        /// 単一ビューオブジェクトに対する対象有りアクションの作成
        /// </summary>
        /// <param name="view">アクション対象のビューオブジェクト</param>
        /// <param name="pattern">アクション種別</param>
        /// <param name="target">アクション内容を表すビューオブジェクト</param>
        /// <param name="easing">アクション実行イージング</param>
        /// <returns>生成されたビューアクション</returns>
        public static ViewAction ToViewAction(
            this IViewKey view,
            ViewAction.Pattern pattern,
            IViewKey target,
            Easing easing = null)
            => new ViewAction(pattern, view, target, easing ?? new Easing(Easing.Pattern.Linear));
        /// <summary>
        /// 新たな画面表示パーツを追加する
        /// </summary>
        /// <typeparam name="TViewState">対象の画面表示状態型</typeparam>
        /// <typeparam name="TViewValue">配置されるパーツのパラメータ型</typeparam>
        /// <param name="state">追加対象状態オブジェクト</param>
        /// <param name="value">追加されるパーツのパラメータ</param>
        /// <returns>追加された後の状態オブジェクト</returns>
        public static TViewState SetNewView<TViewState, TViewValue>(
            this TViewState state,
            TViewValue value)
            where TViewState : ViewStateKey
            where TViewValue : IViewKey
            => state.SetNewView(new[] { value });
        /// <summary>
        /// 新たな画面表示パーツ群を追加する
        /// </summary>
        /// <typeparam name="TViewState">対象の画面表示状態型</typeparam>
        /// <typeparam name="TViewValue">配置されるパーツのパラメータ型</typeparam>
        /// <param name="state">追加対象状態オブジェクト</param>
        /// <param name="values">追加されるパーツ群のパラメータリスト</param>
        /// <returns>追加された後の状態オブジェクト</returns>
        public static TViewState SetNewView<TViewState, TViewValue>(
            this TViewState state,
            IEnumerable<TViewValue> values)
            where TViewState : ViewStateKey
            where TViewValue : IViewKey
            => state.SetViewActions(values, ViewAction.Pattern.GENERATE);
        /// <summary>
        /// 新たな画面表示アクションの生成と設置
        /// </summary>
        /// <typeparam name="TViewState">対象の画面表示状態型</typeparam>
        /// <typeparam name="TViewValue">配置されるパーツのパラメータ型</typeparam>
        /// <param name="state">追加対象状態オブジェクト</param>
        /// <param name="values">追加されるパーツ群のパラメータリスト</param>
        /// <param name="pattern">アクションのパターン</param>
        /// <returns>アクションの設定された状態オブジェクト</returns>
        public static TViewState SetViewActions<TViewState, TViewValue>(
             this TViewState state,
             IEnumerable<TViewValue> values,
             ViewAction.Pattern pattern)
             where TViewState : ViewStateKey
             where TViewValue : IViewKey
        {
            var addedActions = values.Select(value => new ViewAction(pattern, value));
            state.viewActionList.AddRange(addedActions);

            return state;
        }
        /// <summary>
        /// <see cref="MotionTip"/>の移動アクションを追加
        /// </summary>
        /// <typeparam name="TViewState">対象の画面表示状態型</typeparam>
        /// <param name="state">追加対象状態オブジェクト</param>
        /// <param name="tips">追加されるパーツ群のパラメータリスト</param>
        /// <param name="target">アクションのパターン</param>
        /// <param name="easingPattern">アクション経過のイージングパターン</param>
        /// <returns>アクションの設定された状態オブジェクト</returns>
        public static TViewState SetTipMoving<TViewState>(
             this TViewState state,
             IEnumerable<MotionTip> tips,
             MotionTip.Destination target,
             Easing.Pattern easingPattern = Easing.Pattern.Quadratic)
             where TViewState : ViewStateKey
        {
            var targetPosition = target.GetCenterPosition();
            var actionPattern = ViewAction.Pattern.UPDATE;
            var easing = new Easing(easingPattern);

            var addedActions = tips.Select(tip => new ViewAction(actionPattern, tip, tip, easing));
            state.viewActionList.AddRange(addedActions);

            return state;
        }
    }
}
