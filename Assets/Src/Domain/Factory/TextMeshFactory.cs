using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Factory
{
    /// <summary>
    /// <see cref="TextMesh"/>の生成処理
    /// </summary>
    public static class TextMeshFactory
    {
        /// <summary>
        /// システムテキストへの文字設定
        /// </summary>
        /// <param name="parent">描画先</param>
        /// <param name="setText">表示する文字列</param>
        /// <param name="position">表示位置</param>
        /// <param name="size">文字サイズ</param>
        /// <param name="color">文字色</param>
        /// <param name="pivot">表示位置基準点</param>
        /// <param name="alignment">文字の横位置</param>
        /// <param name="textName">描画文字列とは別の文字オブジェクト名称</param>
        /// <returns>生成された文字列オブジェクト</returns>
        public static TextMesh SetText(
            this MonoBehaviour parent,
            string setText,
            Vector2 position,
            float size = 1f,
            Color32? color = null,
            TextAnchor pivot = TextAnchor.MiddleCenter,
            TextAlignment alignment = TextAlignment.Center,
            string textName = null)
        {
            var nameModel = textName ?? setText;
            var _textName = nameModel;
            for(var index = 0; GameObject.Find(_textName) != null; index++) _textName = $"{nameModel}_{index}";

            var textObject = parent.SetPrefab<TextMesh>(_textName);

            textObject.text = setText;
            textObject.anchor = pivot;
            textObject.alignment = alignment;
            textObject.characterSize = size;
            textObject.color = color ?? Color.white;

            var transform = textObject.GetComponent<Transform>();
            transform.localPosition = position;

            return textObject;
        }
    }
}
