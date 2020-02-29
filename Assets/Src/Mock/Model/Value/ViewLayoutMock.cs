using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Mock.Model.Value
{
    public class ViewLayoutMock : IViewLayout
    {
        public static ViewLayoutMock Generate(Func<Dictionary<IViewKey, int>, Dictionary<IViewKey, List<Vector2>>> getPositionMap) => new ViewLayoutMock(getPositionMap);

        ViewLayoutMock(Func<Dictionary<IViewKey, int>, Dictionary<IViewKey, List<Vector2>>> getPositionMap)
        {
            this.getPositionMap = getPositionMap;
        }

        readonly Func<Dictionary<IViewKey, int>, Dictionary<IViewKey, List<Vector2>>> getPositionMap;
        public Dictionary<IViewKey, List<Vector2>> GetPositionMap(Dictionary<IViewKey, int> totalMap)
            => getPositionMap.Invoke(totalMap);
    }
}
