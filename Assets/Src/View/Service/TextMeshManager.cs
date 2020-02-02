using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Service
{
    /// <summary>
    /// <see cref="TextMesh"/>操作サービス
    /// </summary>
    public static class TextMeshManager
    {
        public static TViewState UpdateText<TViewState>(
            this TViewState state,
            ITextMeshKey key,
            string text,
            Vector2? position = null)
            where TViewState : ViewState
        {
            if(state is null)
                throw new ArgumentNullException(nameof(state));

            var origin = state.Search<ITextMeshKey, TextMesh>(key);
            if(origin is null)
                return state;

            if(text is string textNonNull)
                origin.SetText(textNonNull);
            if(position is Vector2 positionNonNull)
                origin.SetPosition(positionNonNull);
            return state;
        }
        public static TViewState UpdateText<TViewState>(
            this TViewState state,
            ITextMeshKey key,
            Vector2 position)
            where TViewState : ViewState
            => state.UpdateText(key, null, position);
        static TextMesh SetPosition(
           this TextMesh origin,
           Vector2 position)
        {
            if(origin is null)
                return origin;

            origin.transform.localPosition = position;
            return origin;
        }
        static TextMesh SetText(
           this TextMesh origin,
           string text)
        {
            if(origin is null)
                return origin;

            origin.text = text;
            return origin;
        }

        public static TViewState DestroyText<TViewState>(this TViewState state, ITextMeshKey key)
            where TViewState : ViewState
        {
            if(state is null)
                throw new ArgumentNullException(nameof(state));

            var target = state.Search<ITextMeshKey, TextMesh>(key);
            if(target is null)
                return state;

            target.Destroy();

            return state;
        }
    }
}
