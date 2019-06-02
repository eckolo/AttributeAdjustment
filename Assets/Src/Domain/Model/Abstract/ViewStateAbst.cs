using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// Viewに渡されることで画面描画処理の起点となる状態オブジェクト
    /// </summary>
    public abstract class ViewStateAbst : IViewRoot
    {
        public IEnumerable<ViewEntity> views { get; protected set; }

        public Queue<ViewAction> viewActionQueue { get; } = new Queue<ViewAction>();

        public IEnumerable<ViewEntity> AddViewStationerys(IEnumerable<ViewEntity> addeds)
        {
            var addedsNoNull = addeds ?? Enumerable.Empty<ViewEntity>();
            return views = views?.Concat(addedsNoNull) ?? addedsNoNull;
        }
    }
}
