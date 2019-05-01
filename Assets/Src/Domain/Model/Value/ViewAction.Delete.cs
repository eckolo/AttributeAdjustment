using Assets.Src.Domain.Model.Entity;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Value
{
    public abstract partial class ViewAction
    {
        /// <summary>
        /// ビューの削除動作
        /// </summary>
        public class Delete : ViewAction
        {
            public Delete(IEnumerable<ViewStationery> actors) : base(ActionType.DELETE, actors)
            { }
        }
    }
}
