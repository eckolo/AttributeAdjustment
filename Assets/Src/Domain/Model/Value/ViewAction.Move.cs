using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Value
{
    public abstract partial class ViewAction
    {
        /// <summary>
        /// ビューの移動動作
        /// </summary>
        public class Move : ViewAction
        {
            public Move(IEnumerable<ViewStationery> actors, IViewRoot target, Easing easing)
                : base(ActionType.MOVE, actors, target, easing)
            { }
        }
    }
}
