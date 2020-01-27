using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Mock.Model.Value
{
    public class TopographyMock : Topography
    {
        TopographyMock(Dictionary<MotionTip, int> baseTipSet)
        {
            this.baseTipSet = baseTipSet;
        }

        public static TopographyMock Generate(Dictionary<MotionTip, int> baseTipSet) => new TopographyMock(baseTipSet);
    }
}
