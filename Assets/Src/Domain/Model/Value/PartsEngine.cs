using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Value
{
    public class PartsEngine
    {
        /// <summary>
        /// 火力
        /// </summary>
        public int power { get; }
        /// <summary>
        /// 機動力
        /// </summary>
        public int speed { get; }
        /// <summary>
        /// 障壁出力
        /// </summary>
        public int barrier { get; }
    }
}
