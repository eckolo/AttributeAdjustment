using Assets.Src.Domain.Model.Abstract;
using System.Collections.Generic;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 戦闘で使用する技
    /// </summary>
    public class Ability
    {
        public Ability(string name)
        {
            this.name = name;
        }

        public string name { get; }

        /// <summary>
        /// 対応モーションチップ条件
        /// </summary>
        public List<List<(Energy? energy, int? energyValue)>> tipList { get; }

        /// <summary>
        /// 威力倍率
        /// </summary>
        public float magnification { get; } = 1;

        /// <summary>
        /// 固定補正値
        /// </summary>
        public int fixedCorrection { get; } = 0;
    }
}
