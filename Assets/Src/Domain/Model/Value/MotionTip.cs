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
    public partial class MotionTip : ITextMeshKey, IDuplicatable<MotionTip>
    {
        public MotionTip(Energy energy, int energyValue, Vector2? position = null)
        {
            this.energy = energy;
            this.energyValue = energyValue;

            text = $"{energy.GetName()}\r\n{energyValue.ToString()}";
        }
        public MotionTip(MotionTip origin, Vector2 position)
            : this(origin.energy, origin.energyValue, position)
        { }

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


        public MotionTip MemberwiseClonePublic() => (MotionTip)MemberwiseClone();
    }
}
