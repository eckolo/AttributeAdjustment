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

        /// <summary>
        /// 表示テキスト情報一覧
        /// </summary>
        public IEnumerable<TextMeshStationery> texts => new[]
        {
            new TextMeshStationery(energy.GetName(), new Vector2(0, -0.2f)),
            new TextMeshStationery(energyValue.ToString(), new Vector2(0, 0.2f)),
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

        /// <summary>
        /// このオブジェクトに対応する画面表示パーツ
        /// </summary>
        public TextSet entity { get; protected set; }

        /// <summary>
        /// 画面表示パーツ初期化
        /// </summary>
        /// <param name="parent">表示物体の親オブジェクト</param>
        /// <param name="localPosition">表示座標</param>
        /// <returns>生成された画面表示パーツ</returns>
        public TextSet InitializeEntity(Component parent, Vector2 localPosition)
        {
            if(entity != default) entity.Destroy();
            return entity = parent.SetTextSet(this, localPosition);
        }
    }
}
