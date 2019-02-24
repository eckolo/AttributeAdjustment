using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Mock
{
    public class TextSetStationeryMock : ITextSetStationery
    {
        TextSetStationeryMock(IEnumerable<TextMeshStationery> texts, float size, Color32 color, TextAlignment alignment)
        {
            this.texts = texts ?? throw new ArgumentNullException(nameof(texts));
            this.size = size;
            this.color = color;
            this.alignment = alignment;
        }

        public IEnumerable<TextMeshStationery> texts { get; }

        public float size { get; }

        public Color32 color { get; }

        public TextAlignment alignment { get; }

        public static TextSetStationeryMock Generate(IEnumerable<TextMeshStationery> texts, float size, Color32 color, TextAlignment alignment) => new TextSetStationeryMock(texts, size, color, alignment);

        public TextSet InitializeEntity(Component parent, Vector2 localPosition)
        {
            throw new NotImplementedException();
        }

        public TextSet entity => throw new NotImplementedException();
    }
}
