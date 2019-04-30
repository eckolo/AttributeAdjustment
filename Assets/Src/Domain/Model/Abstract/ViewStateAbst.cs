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
        public List<IViewValue> viewList { get; } = new List<IViewValue>();

        public Queue<ViewAction> viewActionQueue { get; } = new Queue<ViewAction>();
    }
}
