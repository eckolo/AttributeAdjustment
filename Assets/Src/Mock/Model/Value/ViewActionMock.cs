using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Mock.Model.Value
{
    public class ViewActionMock : ViewAction
    {
        ViewActionMock(Pattern actionType, ViewDeployment actorDeployment, IViewKey actor)
            : base(actionType, actorDeployment, actor)
        { }
        ViewActionMock(
           Pattern actionType,
           ViewDeployment actorDeployment,
           IViewKey actor,
           IViewKey target,
           Easing easing)
            : base(actionType, actorDeployment, actor, target, easing)
        { }

        public static ViewActionMock GenerateMock(
           Pattern actionType = default,
           ViewDeployment actorDeployment = default,
           IViewKey actor = null,
           IViewKey target = null,
           Easing easing = null,
           ViewAction nextAction = null)
        {
            actorDeployment = actorDeployment ?? new ViewDeployment(SpriteAlignment.Center);

            var mock = target is null && easing is null
                ? new ViewActionMock(actionType, actorDeployment, actor)
                : new ViewActionMock(actionType, actorDeployment, actor, target, easing ?? new Easing(Easing.Pattern.Linear));
            return mock.AddNextAction<ViewActionMock>(nextAction);
        }
    }
}
