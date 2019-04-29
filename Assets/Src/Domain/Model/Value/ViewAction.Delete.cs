using Assets.Src.Domain.Model.Abstract;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    public abstract partial class ViewAction
    {
        /// <summary>
        /// ビューの削除動作
        /// </summary>
        public class Delete : ViewAction
        {
            public Delete(
                IViewAbst targetObject,
                TextAnchor targetObjectAnchor,
                IViewAbst pivotObject,
                TextAnchor pivotObjectAnchor)
                : base(targetObject, targetObjectAnchor, pivotObject, pivotObjectAnchor)
            { }
        }
    }
}
