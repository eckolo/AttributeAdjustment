using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Mock.Model.Entity
{
    public class ViewStateKeyMock : ViewStateKey
    {
        ViewStateKeyMock(IEnumerable<IViewKey> views, ViewAction[] viewActionQueue)
        {
            foreach(var action in viewActionQueue ?? new ViewAction[] { })
            {
                viewActionList.Add(action);
            }
        }

        public static ViewStateKeyMock Generate(
            IEnumerable<IViewKey> views = null,
            ViewAction[] viewActionQueue = null)
            => new ViewStateKeyMock(views, viewActionQueue);
        public static ViewStateKeyMock Generate(
            IEnumerable<IViewKey> views)
            => new ViewStateKeyMock(views, new ViewAction[] { });
    }
}
