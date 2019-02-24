using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 部分動作を表すモーション
    /// </summary>
    public class MotionTip : Named, ITextSetStationery
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

        public IEnumerable<TextMeshStationery> texts => new[]
        {
            new TextMeshStationery(energy.GetName(),
            new Vector2(0, -0.2f)), new TextMeshStationery(energyValue.ToString(), new Vector2(0, 0.2f)),
        };

        public float size => Constants.MotionTip.CHAR_SIZE;

        public Color32 color => Constants.Texts.DEFAULT_COLOR;

        public TextAlignment alignment => TextAlignment.Center;

        public TextSet entity { get; protected set; }

        public TextSet InitializeEntity(Component parent, Vector2 localPosition)
        {
            if(entity != default) entity.Destroy();
            return entity = parent.SetTextSet(this, localPosition);
        }
    }
}
