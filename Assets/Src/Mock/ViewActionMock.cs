﻿using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using System.Collections.Generic;

namespace Assets.Src.Mock
{
    public class ViewActionMock : ViewAction
    {
        ViewActionMock(ActionType actionType, IEnumerable<ViewStationery> actors)
            : base(actionType, actors)
        { }
        ViewActionMock(
           ActionType actionType,
           IEnumerable<ViewStationery> actors,
           ViewStationery target,
           Easing easing)
            : base(actionType, actors, target, easing)
        { }

        public static ViewActionMock GenerateMock(
           ActionType actionType,
           IEnumerable<ViewStationery> actors,
           ViewStationery target = null,
           Easing easing = null,
           ViewAction nextAction = null)
        {
            var mock = target is null && easing is null
                ? new ViewActionMock(actionType, actors)
                : new ViewActionMock(actionType, actors, target, easing);
            return mock.AddNextAction<ViewActionMock>(nextAction);
        }
    }
}
