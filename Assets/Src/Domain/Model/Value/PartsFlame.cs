using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Value
{
    public class PartsFlame
    {
        /// <summary>
        /// 装甲
        /// </summary>
        public int armor { get; }
        /// <summary>
        /// 重量
        /// </summary>
        public int weight { get; }
        /// <summary>
        /// 武装スロット数
        /// </summary>
        public int weaponSlot { get; }
        /// <summary>
        /// 固定武装
        /// </summary>
        public IEnumerable<Weapon> weapons { get; }
    }
}
