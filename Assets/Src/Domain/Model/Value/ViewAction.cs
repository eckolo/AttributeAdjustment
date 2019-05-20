using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// ビューの動作定義雛形オブジェクト
    /// </summary>
    public abstract partial class ViewAction
    {
        protected ViewAction(ActionType actionType, IEnumerable<ViewStationery> actors)
        {
            this.actionType = actionType;
            this.actors = actors ?? this.actors;
        }
        protected ViewAction(
            ActionType actionType,
            IEnumerable<ViewStationery> actors,
            IViewRoot target,
            Easing easing)
            : this(actionType, actors)
        {
            this.target = target;
            this.easing = easing;
        }
        /// <summary>
        /// 動作種別
        /// </summary>
        public ActionType actionType { get; }
        /// <summary>
        /// 動作対象オブジェクト
        /// </summary>
        public IEnumerable<ViewStationery> actors { get; }
        /// <summary>
        /// 動作起点オブジェクト
        /// </summary>
        public IViewRoot target { get; }
        /// <summary>
        /// 動作処理のイージング
        /// </summary>
        public Easing easing { get; }
        /// <summary>
        /// 後続の画面表示処理
        /// </summary>
        public ViewAction nextAction { get; private set; } = null;
        /// <summary>
        /// 後続処理追加時の無限ループを回避するための内部フラグ
        /// 1度の再起ループで<see cref="AddNextAction"/>が複数回呼ばれてないか判別する
        /// </summary>
        bool alreadyAddAction { get; set; } = false;

        /// <summary>
        /// 後続の画面表示処理を追加する
        /// </summary>
        /// <param name="nextAction">追加される処理</param>
        /// <returns>後続処理を追加された先頭の処理</returns>
        public TViewAction AddNextAction<TViewAction>(ViewAction nextAction)
            where TViewAction : ViewAction
        {
            //この後続処理追加処理は1回目？
            if(!alreadyAddAction)
            {
                alreadyAddAction = true;
                this.nextAction = this.nextAction?.AddNextAction<TViewAction>(nextAction) ?? nextAction;
            }

            alreadyAddAction = false;
            return (TViewAction)this;
        }
    }
}
