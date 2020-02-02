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
    }
}
