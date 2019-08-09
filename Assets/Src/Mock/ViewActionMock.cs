using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public class ViewActionMock : ViewAction
    {
        ViewActionMock(Pattern actionType, ViewEntity actor)
            : base(actionType, actor)
        { }
        ViewActionMock(
           Pattern actionType,
           ViewEntity actor,
           ViewEntity target,
           Easing easing)
            : base(actionType, actor, target, easing)
        { }

        public static ViewActionMock GenerateMock(
           Pattern actionType = default,
           ViewEntity actor = null,
           ViewEntity target = null,
           Easing? easing = null,
           ViewAction nextAction = null)
        {
            var mock = target is null && easing is null
                ? new ViewActionMock(actionType, actor)
                : new ViewActionMock(actionType, actor, target, easing ?? Easing.Linear);
            return mock.AddNextAction<ViewActionMock>(nextAction);
        }
    }
}
