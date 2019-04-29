using Assets.Src.Domain.Model.Abstract;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    public abstract partial class ViewAction
    {
        /// <summary>
        /// ビューの生成動作
        /// </summary>
        public class Generate : ViewAction
        {
            public Generate(
                IViewAbst targetObject,
                TextAnchor targetObjectAnchor,
                IViewAbst pivotObject,
                TextAnchor pivotObjectAnchor,
                bool displayed)
                : base(targetObject, targetObjectAnchor, pivotObject, pivotObjectAnchor)
            {
                this.displayed = displayed;
            }

            public bool displayed { get; }
        }
    }
}
