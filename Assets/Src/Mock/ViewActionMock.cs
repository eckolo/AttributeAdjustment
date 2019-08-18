using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public class ViewActionMock : ViewAction
    {
        ViewActionMock(Pattern actionType, IViewKey actor)
            : base(actionType, actor)
        { }
        ViewActionMock(
           Pattern actionType,
           IViewKey actor,
           IViewKey target,
           Easing easing)
            : base(actionType, actor, target, easing)
        { }

        public static ViewActionMock GenerateMock(
           Pattern actionType = default,
           IViewKey actor = null,
           IViewKey target = null,
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
