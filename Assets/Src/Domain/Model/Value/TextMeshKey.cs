using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Repository;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// <see cref="TextSet"/>内の<see cref="TextMesh"/>クラス雛形
    /// </summary>
    [Serializable]
    public class TextMeshKey : ITextMeshKey
    {
        public TextMeshKey(string text)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <summary>
        /// 表示文字列
        /// </summary>
        [SerializeField]
        string _text = "";
        /// <summary>
        /// 表示文字列
        /// </summary>
        public string text => _text;

        /// <summary>
        /// 文字サイズ
        /// </summary>
        public float size => Constants.Texts.CHAR_SIZE;

        /// <summary>
        /// 文字色
        /// </summary>
        public Color32 color => default;

        /// <summary>
        /// 文字の左右詰め
        /// </summary>
        public TextAlignment alignment => TextAlignment.Center;
    }
}
