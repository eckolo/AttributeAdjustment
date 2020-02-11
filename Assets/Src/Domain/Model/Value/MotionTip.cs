using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Repository;
using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 部分動作を表すモーション
    /// </summary>
    public partial class MotionTip : ITextMeshKey, IDuplicatable<MotionTip>, IEquatable<MotionTip>
    {
        public MotionTip(Energy energy, int energyValue)
        {
            this.energy = energy;
            this.energyValue = energyValue;

            text = $"{energy.GetName()}\r\n{energyValue.ToString()}";
        }

        /// <summary>
        /// 属性種別
        /// </summary>
        public Energy energy { get; }
        /// <summary>
        /// 属性値
        /// </summary>
        public int energyValue { get; }

        /// <summary>
        /// 表示テキスト
        /// </summary>
        public string text { get; }

        /// <summary>
        /// 文字サイズ
        /// </summary>
        public float size => Constants.MotionTip.CHAR_SIZE;

        /// <summary>
        /// 文字色
        /// </summary>
        public Color32 color => energy.GetColor();

        /// <summary>
        /// 文字の左右詰め
        /// </summary>
        public TextAlignment alignment => TextAlignment.Center;

        public override int GetHashCode() => energy.GetHashCode() ^ energyValue.GetHashCode();

        public MotionTip MemberwiseClonePublic() => (MotionTip)MemberwiseClone();

        public bool Equals(MotionTip other)
            => other is MotionTip tip && energy == tip.energy && energyValue == tip.energyValue;
        public override bool Equals(object other) => other is MotionTip tip && Equals(tip);

        public static bool operator ==(MotionTip x, MotionTip y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(MotionTip x, MotionTip y) => !(x == y);
    }
}
