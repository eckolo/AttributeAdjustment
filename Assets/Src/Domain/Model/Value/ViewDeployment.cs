using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 画面表示オブジェクトの配置情報
    /// </summary>
    public class ViewDeployment : IEquatable<ViewDeployment>
    {
        public ViewDeployment(SpriteAlignment? pivot = null, Vector2? pivotGap = null)
        {
            this.pivot = pivot ?? this.pivot;
            this.pivotGap = pivotGap ?? this.pivotGap;
        }
        public ViewDeployment(Vector2 pivotGap, int? layer = null)
            : this(null, pivotGap)
        {
            this.layer = layer ?? this.layer;
        }

        /// <summary>
        /// 基準点
        /// </summary>
        public SpriteAlignment pivot { get; } = SpriteAlignment.Center;
        /// <summary>
        /// 基準点からのズレ
        /// </summary>
        public Vector2 pivotGap { get; } = Vector2.zero;
        /// <summary>
        /// 見かけ上の同一点における層番号
        /// </summary>
        public int layer { get; } = 0;

        public override int GetHashCode() => pivot.GetHashCode() ^ pivotGap.GetHashCode() ^ layer.GetHashCode();

        public bool Equals(ViewDeployment other)
            => other is ViewDeployment dep
            && pivot == dep.pivot
            && pivotGap == dep.pivotGap
            && layer == dep.layer;
        public override bool Equals(object other) => other is ViewDeployment dep && Equals(dep);

        public static bool operator ==(ViewDeployment x, ViewDeployment y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(ViewDeployment x, ViewDeployment y) => !(x == y);
    }
}
