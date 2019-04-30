using Assets.Src.Domain.Model.Abstract;
using System.Collections.Generic;
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
            public Generate(IEnumerable<IViewValue> actors) : base(ActionType.GENERATE, actors)
            { }
        }
    }
}
