using Assets.Src.Domain.Model.Abstract;
using System.Collections.Generic;
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
            public Delete(IEnumerable<IViewValue> actors) : base(ActionType.DELETE, actors)
            { }
        }
    }
}
