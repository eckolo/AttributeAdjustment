using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Mock
{
    public class ViewStateKeyMock : ViewStateKey
    {
        ViewStateKeyMock(IEnumerable<IViewKey> views, ViewAction[] viewActionQueue)
        {
            foreach(var action in viewActionQueue ?? new ViewAction[] { })
            {
                this.viewActionQueue.Enqueue(action);
            }
        }

        public static ViewStateKeyMock Generate(
            IEnumerable<IViewKey> views = null,
            ViewAction[] viewActionQueue = null)
            => new ViewStateKeyMock(views, viewActionQueue);
    }
}
