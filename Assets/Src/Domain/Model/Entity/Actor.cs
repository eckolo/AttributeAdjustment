using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 動作主体
    /// </summary>
    public partial class Actor : IViewKey, IDuplicatable<Actor>
    {
        public Actor(string name)
        {
            this.name = name;
        }

        public string name { get; }

        /// <summary>
        /// 経験値
        /// </summary>
        public int experience { get; set; }

        /// <summary>
        /// 現在の体力値
        /// </summary>
        public int vitality { get; set; }

        /// <summary>
        /// 頭パーツ
        /// </summary>
        public PartsBrain brain { get; set; }
        /// <summary>
        /// 動力部
        /// </summary>
        public PartsEngine engine { get; set; }
        /// <summary>
        /// 骨格
        /// </summary>
        public PartsFlame flame { get; set; }
        /// <summary>
        /// 自由武装
        /// </summary>
        public IEnumerable<Weapon> weaponsFree { get; set; }

        /// <summary>
        /// 使用可能アビリティリスト
        /// </summary>
        public List<Ability> abilityList => weaponsAll.SelectMany(weapon => weapon.abilities).ToList();

        /// <summary>
        /// レベル
        /// </summary>
        public int level => experience + parameter.offense + parameter.defense + parameter.speed;

        /// <summary>
        /// 現在のパラメータ
        /// </summary>
        public virtual Parameter parameter => new Parameter(
            maxVitality: flame?.armor ?? 0,
            offense: engine?.power ?? 0,
            defense: 0,
            speed: flame?.speed ?? 0);

        /// <summary>
        /// 武装一覧
        /// </summary>
        public IEnumerable<Weapon> weaponsAll => flame.weaponsFix.Concat(weaponsFree);

        /// <summary>
        /// 表示位置
        /// </summary>
        public Vector2 position { get; set; }

        /// <summary>
        /// プレイヤー操作対象であるか否かのフラグ
        /// </summary>
        public virtual bool isPlayer => false;

        public ulong hashCode => (ulong)GetHashCode();

        public Actor MemberwiseClonePublic() => (Actor)MemberwiseClone();
    }
}
