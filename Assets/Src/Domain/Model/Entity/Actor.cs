﻿using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 動作主体
    /// </summary>
    public class Actor : Named, IViewValue, IDuplicatable<Actor>
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
        public int level => experience + offense + defense + speed;

        /// <summary>
        /// 経験値
        /// </summary>
        public int experience { get; set; }

        /// <summary>
        /// 最大体力
        /// </summary>
        public virtual int maxVitality { get; set; }
        /// <summary>
        /// 現在の体力
        /// </summary>
        public virtual int vitality { get; set; }
        /// <summary>
        /// 攻撃力
        /// </summary>
        public virtual int offense { get; set; }
        /// <summary>
        /// 防御力
        /// </summary>
        public virtual int defense { get; set; }
        /// <summary>
        /// 素早さ
        /// </summary>
        public virtual int speed { get; set; }

        /// <summary>
        /// プレイヤー操作対象であるか否かのフラグ
        /// </summary>
        public virtual bool isPlayer => false;

        public Actor MemberwiseClonePublic() => (Actor)MemberwiseClone();
    }
}
