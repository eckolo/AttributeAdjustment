using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.View.Model.Entity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.View.Factory
{
    /// <summary>
    /// <see cref="TextSet"/>の生成処理
    /// </summary>
    public static class TextSetFactory
    {
        /// <summary>
        /// システムテキストへの文字設定
        /// </summary>
        /// <param name="parent">描画先</param>
        /// <param name="setTexts">表示する文字列</param>
        /// <param name="position">表示位置</param>
        /// <param name="size">文字サイズ</param>
        /// <param name="color">文字色</param>
        /// <param name="pivot">表示位置基準点</param>
        /// <param name="alignment">文字の横位置</param>
        /// <param name="textName">描画文字列とは別の文字オブジェクト名称</param>
        /// <returns>生成された文字列オブジェクト</returns>
        public static TextSet SetTextSet(
            this Component parent,
            IEnumerable<(string text, Vector2 localPosition)> setTexts,
            Vector2 position,
            float size = 1f,
            Color32? color = null,
            TextAlignment alignment = TextAlignment.Center,
            string textName = null)
        {
            var name = setTexts.Select(text => text.text).Aggregate((text1, text2) => $"{text1} {text2}");
            var textSet = parent.SetPrefab<TextSet>(name);

            var texts = setTexts
                .Select(text => textSet.SetText(
                    setText: text.text,
                    position: text.localPosition,
                    size: size,
                    color: color,
                    pivot: TextAnchor.MiddleCenter,
                    alignment: alignment))
                .ToList();

            textSet.texts = texts;
            textSet.position = position;

            return textSet;
        }
    }
}
