using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public class ViewStateAbstMock : ViewStateAbst
    {
        public ViewStateAbstMock(IEnumerable<ViewStationery> views, ViewAction[] viewActionQueue)
        {
            this.views = views;
            if(viewActionQueue != null)
                this.viewActionQueue.CopyTo(viewActionQueue, 0);
        }

        public static ViewStateAbstMock Generate(IEnumerable<ViewStationery> views = null, ViewAction[] viewActionQueue = null)
            => new ViewStateAbstMock(views, viewActionQueue);
    }
}
