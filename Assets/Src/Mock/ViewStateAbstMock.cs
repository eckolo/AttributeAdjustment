﻿using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Mock
{
    public class ViewStateAbstMock : ViewStateAbst
    {
        ViewStateAbstMock(IEnumerable<IViewKey> views, ViewAction[] viewActionQueue)
        {
            foreach(var action in viewActionQueue ?? new ViewAction[] { })
            {
                this.viewActionQueue.Enqueue(action);
            }
        }

        public static ViewStateAbstMock Generate(
            IEnumerable<IViewKey> views = null,
            ViewAction[] viewActionQueue = null)
            => new ViewStateAbstMock(views, viewActionQueue);
    }
}
