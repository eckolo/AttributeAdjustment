using Assets.Src.Domain.Model.Entity;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Value
{
    public abstract partial class ViewAction
    {
        /// <summary>
        /// ビューの生成動作
        /// </summary>
        public class Generate : ViewAction
        {
            public Generate(IEnumerable<ViewEntity> actors) : base(ActionType.GENERATE, actors)
            { }
        }
    }
}
