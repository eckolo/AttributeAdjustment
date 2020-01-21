using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Mock.Model.Abstract
{
    public class ViewStateKeyMock : ViewStateKey
    {
        ViewStateKeyMock(IEnumerable<IViewKey> views, ViewAction[] viewActionQueue, bool? isGenerated)
        {
            this.isGenerated = isGenerated ?? this.isGenerated;
            foreach(var action in viewActionQueue ?? new ViewAction[] { })
            {
                viewActionList.Add(action);
            }
        }

        public override Vector2 position
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public static ViewStateKeyMock Generate(
            IEnumerable<IViewKey> views = null,
            ViewAction[] viewActionQueue = null)
            => new ViewStateKeyMock(views, viewActionQueue, null);
        public static ViewStateKeyMock Generate(
            IEnumerable<IViewKey> views,
            bool isGenerated)
            => new ViewStateKeyMock(views, new ViewAction[] { }, isGenerated);
    }
}
