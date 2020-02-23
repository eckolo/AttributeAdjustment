﻿using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.View.Model.Entity;
using UnityEngine;

namespace Assets.Src.View.Factory
{
    /// <summary>
    /// <see cref="TextMesh"/>の生成処理
    /// </summary>
    public static class TextMeshFactory
    {
        /// <summary>
        /// システムテキストへの文字設定
        /// </summary>
        /// <param name="state">設定先のビュールート</param>
        /// <param name="stationery">テキスト情報</param>
        /// <returns>設定されたビュールート</returns>
        public static TViewState SetText<TViewState>(
            this TViewState state,
            ViewDeployment deployment,
            ITextMeshKey stationery)
            where TViewState : ViewState
        {
            var nameModel = stationery.text;
            var _textName = nameModel;
            for(var index = 0; GameObject.Find(_textName) != null; index++)
                _textName = $"{nameModel}_{index}";

            var textObject = state.SetPrefab<TextMesh>(_textName);
            state.Save(deployment, stationery, textObject);

            textObject.text = stationery.text;
            textObject.color = stationery.color;
            textObject.characterSize = stationery.size;
            textObject.alignment = stationery.alignment;
            textObject.anchor = TextAnchor.MiddleCenter;

            var transform = textObject.GetComponent<Transform>();
            transform.localScale = Vector3.one * 0.5f;
            transform.localPosition = deployment.pivotGap;

            return state;
        }
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
            this Component parent,
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
            for(var index = 0; GameObject.Find(_textName) != null; index++)
                _textName = $"{nameModel}_{index}";

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
