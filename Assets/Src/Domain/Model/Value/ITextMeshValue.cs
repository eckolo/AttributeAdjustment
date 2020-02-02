using Assets.Src.Domain.Model.Abstract;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// <see cref="TextSet"/>クラスの生成元となるインターフェース
    /// </summary>
    public partial interface ITextMeshValue : IViewKey
    {
        /// <summary>
        /// 表示テキスト情報一覧
        /// </summary>
        IEnumerable<TextMeshKey> texts { get; }

        /// <summary>
        /// 文字サイズ
        /// </summary>
        float size { get; }

        /// <summary>
        /// 文字色
        /// </summary>
        Color32 color { get; }

        /// <summary>
        /// 文字の左右詰め
        /// </summary>
        TextAlignment alignment { get; }
    }
}
