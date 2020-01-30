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
        public int armor { get; protected set; }
        /// <summary>
        /// 機動力
        /// </summary>
        public int speed { get; protected set; }
        /// <summary>
        /// 武装スロット数
        /// </summary>
        public int weaponSlot { get; protected set; }
        /// <summary>
        /// 固定武装
        /// </summary>
        public IEnumerable<Weapon> weaponsFix { get; protected set; } = Enumerable.Empty<Weapon>();
    }
}
