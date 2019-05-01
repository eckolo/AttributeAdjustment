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
            ViewStationery target,
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
        public ViewStationery target { get; }
        /// <summary>
        /// 動作処理のイージング
        /// </summary>
        public Easing easing { get; }
    }
}
