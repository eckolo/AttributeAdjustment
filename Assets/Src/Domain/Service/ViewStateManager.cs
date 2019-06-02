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
    /// <summary>
    /// <see cref="ViewStateAbst"/>操作系のサービス
    /// </summary>
    public static class ViewStateManager
    {
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
            where TViewState : ViewStateAbst
            where TViewValue : IViewValue
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
            where TViewState : ViewStateAbst
            where TViewValue : IViewValue
        {
            var viewStationeries = values.Select(value => new ViewEntity(value).SetParent(state)).ToArray();
            state.AddViewStationerys(viewStationeries);

            var action = new ViewAction.Generate(viewStationeries);
            state.viewActionQueue.Enqueue(action);

            return state;
        }
        public static TView Move<TViewState, TView, TFromView, TToView>(
            this TViewState state,
            TView value,
            TFromView fromView,
            TToView toView)
            where TViewState : ViewStateAbst
            where TView : IViewValue
            where TFromView : IViewValue
            where TToView : IViewValue
        {
            //TODO ビューの移動処理メソッド実装
            throw new NotImplementedException(nameof(Move));
        }
    }
}
