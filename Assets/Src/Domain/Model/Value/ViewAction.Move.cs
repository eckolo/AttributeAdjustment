using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Repository;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    public abstract partial class ViewAction
    {
        /// <summary>
        /// ビューの移動動作
        /// </summary>
        public class Move : ViewAction
        {
            public Move(IEnumerable<IViewValue> actors, IViewValue target, Easing easing)
                : base(ActionType.MOVE, actors)
            { }
        }
    }
}
