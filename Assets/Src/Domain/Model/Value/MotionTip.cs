using Assets.Src.Domain.Model.Abstract;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 部分動作を表すモーション
    /// </summary>
    public class MotionTip : Named
    {
        public MotionTip(string name, Energy energy, int energyValue) : base(name)
        {
            this.energy = energy;
            this.energyValue = energyValue;
        }

        /// <summary>
        /// 属性種別
        /// </summary>
        public Energy energy { get; }
        /// <summary>
        /// 属性値
        /// </summary>
        public int energyValue { get; }
    }
}
