using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Repository;
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
            public Move(
                IViewAbst targetObject,
                TextAnchor targetObjectAnchor,
                IViewAbst pivotObject,
                TextAnchor pivotObjectAnchor,
                Easing easing)
                : base(targetObject, targetObjectAnchor, pivotObject, pivotObjectAnchor)
            {
                this.easing = easing;
            }
            public Easing easing { get; }
        }
    }
}
