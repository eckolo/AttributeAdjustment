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
        public ViewDeployment(Vector2 pivotGap)
            : this(null, pivotGap)
        { }

        /// <summary>
        /// 基準点
        /// </summary>
        public SpriteAlignment pivot { get; } = SpriteAlignment.Center;
        /// <summary>
        /// 基準点からのズレ
        /// </summary>
        public Vector2 pivotGap { get; } = Vector2.zero;

        public override int GetHashCode() => pivot.GetHashCode() ^ pivotGap.GetHashCode();

        public bool Equals(ViewDeployment other)
            => other is ViewDeployment dep && pivot == dep.pivot && pivotGap == dep.pivotGap;
        public override bool Equals(object other) => other is ViewDeployment dep && Equals(dep);

        public static bool operator ==(ViewDeployment x, ViewDeployment y) => x?.Equals(y) ?? y is null;
        public static bool operator !=(ViewDeployment x, ViewDeployment y) => !(x == y);
    }
}
