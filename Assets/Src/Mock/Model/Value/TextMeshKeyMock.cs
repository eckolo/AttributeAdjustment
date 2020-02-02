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
        TextMeshKeyMock(string text, Vector2 position, float size, Color32 color, TextAlignment alignment)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
            this.position = position;
            this.size = size;
            this.color = color;
            this.alignment = alignment;
        }

        public static TextMeshKeyMock Generate(string text, Vector2 position, float size, Color32 color, TextAlignment alignment)
            => new TextMeshKeyMock(text, position, size, color, alignment);
        public static TextMeshKeyMock Generate(string text, Vector2 position)
            => new TextMeshKeyMock(text, position, default, default, default);
        public static TextMeshKeyMock Generate(string text)
            => new TextMeshKeyMock(text, default, default, default, default);

        /// <summary>
        /// 表示文字列
        /// </summary>
        public string text { get; }
        /// <summary>
        /// 相対表示位置
        /// </summary>
        public Vector2 position { get; }
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
        public ulong hashCode
            => (ulong)text.GetHashCode()
            ^ (ulong)position.GetHashCode()
            ^ (ulong)size.GetHashCode()
            ^ (ulong)color.GetHashCode()
            ^ (ulong)alignment.GetHashCode();
    }
}
