using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Factory
{
    public static class ViewActionFactory
    {
        public static ViewAction ToViewAction(this IViewKey view, ViewAction.Pattern pattern)
            => new ViewAction(pattern, view);
    }
}
