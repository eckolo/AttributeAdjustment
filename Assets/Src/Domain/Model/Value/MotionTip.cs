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
    public class MotionTip : Named, ITextSetStationeryValue, IDuplicatable<MotionTip>
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

        /// <summary>
        /// 表示テキスト情報一覧
        /// </summary>
        public IEnumerable<TextMeshKey> texts => new[]
        {
            new TextMeshKey(energy.GetName(), new Vector2(0, -0.2f)),
            new TextMeshKey(energyValue.ToString(), new Vector2(0, 0.2f)),
        };

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

        public ulong hashCode
            => (ulong)name.GetHashCode()
            ^ (ulong)energy.GetHashCode()
            ^ (ulong)energyValue.GetHashCode();

        public MotionTip MemberwiseClonePublic() => (MotionTip)MemberwiseClone();
    }
}
