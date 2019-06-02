using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public class ViewActionMock : ViewAction
    {
        ViewActionMock(ActionType actionType, IEnumerable<ViewEntity> actors)
            : base(actionType, actors)
        { }
        ViewActionMock(
           ActionType actionType,
           IEnumerable<ViewEntity> actors,
           ViewEntity target,
           Easing easing)
            : base(actionType, actors, target, easing)
        { }

        public static ViewActionMock GenerateMock(
           ActionType actionType = default,
           IEnumerable<ViewEntity> actors = null,
           ViewEntity target = null,
           Easing? easing = null,
           ViewAction nextAction = null)
        {
            var mock = target is null && easing is null
                ? new ViewActionMock(actionType, actors)
                : new ViewActionMock(actionType, actors, target, easing ?? Easing.Linear);
            return mock.AddNextAction<ViewActionMock>(nextAction);
        }
    }
}
