using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Value
{
    public class PartsBrain
    {
        /// <summary>
        /// 山札雛形
        /// </summary>
        public Dictionary<MotionTip, int> defaultDeckTipMap { get; protected set; }
        /// <summary>
        /// 特性
        /// </summary>
        public IEnumerable<Feature> features { get; protected set; }
    }
}
