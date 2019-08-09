using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// <see cref="TextSet"/>内の<see cref="TextMesh"/>クラス雛形
    /// </summary>
    [Serializable]
    public class TextMeshStationeryValue : IViewKey
    {
        public TextMeshStationeryValue(string text, Vector2 position)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _position = position;
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
        /// 相対表示位置
        /// </summary>
        [SerializeField]
        Vector2 _position = Vector2.zero;
        /// <summary>
        /// 相対表示位置
        /// </summary>
        public Vector2 position => _position;
    }
}
