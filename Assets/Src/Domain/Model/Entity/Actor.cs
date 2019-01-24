using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 動作主体
    /// </summary>
    public class Actor : Named, IDuplicatable<Actor>
    {
        /// <summary>
        /// 最大体力
        /// </summary>
        public int maxVitality { get; protected set; }
        /// <summary>
        /// 現在の体力
        /// </summary>
        public int vitality { get; protected set; }
        /// <summary>
        /// 攻撃力
        /// </summary>
        public int offense { get; protected set; }
        /// <summary>
        /// 防御力
        /// </summary>
        public int defense { get; protected set; }
        /// <summary>
        /// 素早さ
        /// </summary>
        public int speed { get; protected set; }

        public Actor MemberwiseClonePublic() => (Actor)MemberwiseClone();
    }
}
