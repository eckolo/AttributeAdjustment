using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// Viewに渡されることで画面描画処理の起点となる状態オブジェクト
    /// </summary>
    public abstract class ViewStateAbst
    {
        public IEnumerable<ViewStationery> views { get; protected set; }

        public Queue<ViewAction> viewActionQueue { get; } = new Queue<ViewAction>();

        public IEnumerable<ViewStationery> AddViewStationerys(IEnumerable<ViewStationery> addeds)
        {
            var addedsNoNull = addeds ?? Enumerable.Empty<ViewStationery>();
            return views = views?.Concat(addedsNoNull) ?? addedsNoNull;
        }
    }
}
