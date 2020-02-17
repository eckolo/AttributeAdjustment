using Assets.Src.Domain.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Mock.Model.Value
{
    public class TextMeshKeyMock : ITextMeshKey
    {
        TextMeshKeyMock(string text, float size, Color32 color, TextAlignment alignment)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
            this.size = size;
            this.color = color;
            this.alignment = alignment;
        }

        public static TextMeshKeyMock Generate(string text, float size, Color32 color, TextAlignment alignment)
            => new TextMeshKeyMock(text, size, color, alignment);
        public static TextMeshKeyMock Generate(string text)
            => new TextMeshKeyMock(text, default, default, default);

        /// <summary>
        /// 表示文字列
        /// </summary>
        public string text { get; }
        /// <summary>
        /// 文字サイズ
        /// </summary>
        public float size { get; }
        /// <summary>
        /// 文字色
        /// </summary>
        public Color32 color { get; }
        /// <summary>
        /// 文字の左右詰め
        /// </summary>
        public TextAlignment alignment { get; }
    }
}
