using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// イージングのパラメータ
    /// </summary>
    public partial class Easing
    {
        public Easing(Pattern pattern, int? timeCoefficient = null)
        {
            this.pattern = pattern;
            this.timeCoefficient = timeCoefficient ?? this.timeCoefficient;
        }

        /// <summary>
        /// イージング種別
        /// </summary>
        public Pattern pattern { get; }
        /// <summary>
        /// 所要時間係数
        /// </summary>
        public int timeCoefficient { get; } = 1;
    }
}
