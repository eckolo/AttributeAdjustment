using Assets.Src.Domain.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// ビューの動作定義雛形オブジェクト
    /// </summary>
    public abstract partial class ViewAction
    {
        protected ViewAction(
            IViewAbst targetObject,
            TextAnchor? targetObjectAnchor,
            IViewAbst pivotObject,
            TextAnchor? pivotObjectAnchor)
        {
            this.targetObject = targetObject ?? this.targetObject;
            this.targetObjectAnchor = targetObjectAnchor ?? this.targetObjectAnchor;
            this.pivotObject = pivotObject ?? this.pivotObject;
            this.pivotObjectAnchor = pivotObjectAnchor ?? this.pivotObjectAnchor;
        }
        /// <summary>
        /// 動作対象オブジェクト
        /// </summary>
        public IViewAbst targetObject { get; }
        /// <summary>
        /// 動作対象オブジェクトの座標軸
        /// </summary>
        public TextAnchor targetObjectAnchor { get; }
        /// <summary>
        /// 動作基準座標オブジェクト
        /// </summary>
        public IViewAbst pivotObject { get; }
        /// <summary>
        /// 動作基準座標オブジェクトの座標軸
        /// </summary>
        public TextAnchor pivotObjectAnchor { get; }
    }
}
