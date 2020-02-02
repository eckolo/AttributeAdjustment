using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// <see cref="TextMesh"/>クラスの雛形インターフェース
    /// </summary>
    public interface ITextMeshKey : IViewKey
    {
        /// <summary>
        /// 表示文字列
        /// </summary>
        string text { get; }

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
