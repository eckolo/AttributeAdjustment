﻿namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 戦闘処理時の行動主体
    /// </summary>
    public class BattleActor : Actor
    {
        /// <summary>
        /// 変動パラメータを全て初期化する
        /// </summary>
        public BattleActor Initialize()
        {
            maxVitalityValue = maxVitality;
            offenseValue = offense;
            defenseValue = defense;
            speedValue = speed;

            return this;
        }

        /// <summary>
        /// 現在の最大体力
        /// </summary>
        public int maxVitalityValue { get; protected set; }
        /// <summary>
        /// 現在の攻撃力
        /// </summary>
        public int offenseValue { get; protected set; }
        /// <summary>
        /// 現在の防御力
        /// </summary>
        public int defenseValue { get; protected set; }
        /// <summary>
        /// 現在の素早さ
        /// </summary>
        public int speedValue { get; protected set; }
    }
}
