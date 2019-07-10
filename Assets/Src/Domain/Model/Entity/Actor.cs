using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 動作主体
    /// </summary>
    public partial class Actor : Named, IViewValue, IDuplicatable<Actor>
    {
        public Actor(string name) : base(name)
        { }

        /// <summary>
        /// 使用可能アビリティリスト
        /// </summary>
        public List<Ability> abilityList { get; set; }

        /// <summary>
        /// レベル
        /// </summary>
        public int level => experience + parameter.offense + parameter.defense + parameter.speed;

        /// <summary>
        /// 経験値
        /// </summary>
        public int experience { get; set; }

        /// <summary>
        /// 現在の体力値
        /// </summary>
        public int vitality { get; set; }

        /// <summary>
        /// 現在のパラメータ
        /// </summary>
        public virtual Parameter parameter { get; set; }

        /// <summary>
        /// プレイヤー操作対象であるか否かのフラグ
        /// </summary>
        public virtual bool isPlayer => false;

        public Actor MemberwiseClonePublic() => (Actor)MemberwiseClone();
    }
}
