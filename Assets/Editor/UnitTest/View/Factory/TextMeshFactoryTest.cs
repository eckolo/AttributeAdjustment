using Assets.Editor.UnitTest.Domain.Service;
using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.UnitTest.View.Factory
{
    /// <summary>
    /// テキストオブジェクト生成処理のテスト
    /// </summary>
    public static class TextMeshFactoryTest
    {
        static readonly ViewState view = ViewStateMock.Generate(nameof(MessageManagerTest));

        [Test]
        public static void SetTextTest_雛形生成_単一生成()
        {
            var text = nameof(SetTextTest_雛形生成_単一生成);
            var position = new Vector2(2, -4);
            var stationery = new TextMeshKey(text, position);

            var textObj = view.SetText(stationery).Search<TextMeshKey, TextMesh>(stationery);

            textObj.IsNotNull();
            textObj.gameObject.name.Is(text);
            textObj.text.Is(text);
            textObj.transform.position.x.Is(position.x);
            textObj.transform.position.y.Is(position.y);
        }
        [Test]
        public static void SetTextTest_雛形生成_複数生成()
        {
            var text = nameof(SetTextTest_雛形生成_複数生成);

            var position1 = new Vector2(2, -4);
            var stationery1 = new TextMeshKey(text, position1);
            var textObj1 = view.SetText(stationery1).Search<TextMeshKey, TextMesh>(stationery1);
            {
                var textObj = textObj1;
                textObj.IsNotNull();
                textObj.gameObject.name.Is(text);
                textObj.text.Is(text);
                textObj.transform.position.x.Is(position1.x);
                textObj.transform.position.y.Is(position1.y);
            }

            var position2 = new Vector2(-2, 4);
            var stationery2 = new TextMeshKey(text, position2);
            var textObj2 = view.SetText(stationery2).Search<TextMeshKey, TextMesh>(stationery2);
            {
                var textObj = textObj2;
                textObj.IsNotNull();
                textObj.gameObject.name.Is($"{text}_0");
                textObj.text.Is(text);
                textObj.transform.position.x.Is(position2.x);
                textObj.transform.position.y.Is(position2.y);
            }
        }

        [Test]
        public static void SetTextTest_単一生成_全パラメータ設定()
        {
            var text = nameof(SetTextTest_単一生成_全パラメータ設定);
            var position = new Vector2(2, -4);
            var size = 0.6f;
            var color = Color.green;
            var pivot = TextAnchor.LowerLeft;
            var alignment = TextAlignment.Right;

            var textObj = view.SetText(text, position, size, color, pivot, alignment);

            textObj.IsNotNull();
            textObj.gameObject.name.Is(text);
            textObj.text.Is(text);
            textObj.transform.position.x.Is(position.x);
            textObj.transform.position.y.Is(position.y);
            textObj.characterSize.Is(size);
            textObj.color.Is(color);
            textObj.anchor.Is(pivot);
            textObj.alignment.Is(alignment);
        }
        [Test]
        public static void SetTextTest_単一生成_最小限のパラメータのみ設定()
        {
            var text = nameof(SetTextTest_単一生成_最小限のパラメータのみ設定);
            var position = new Vector2(2, -4);

            var textObj = view.SetText(text, position);

            textObj.IsNotNull();
            textObj.gameObject.name.Is(text);
            textObj.text.Is(text);
            textObj.transform.position.x.Is(position.x);
            textObj.transform.position.y.Is(position.y);
            textObj.characterSize.Is(1);
            textObj.color.Is(Color.white);
            textObj.anchor.Is(TextAnchor.MiddleCenter);
            textObj.alignment.Is(TextAlignment.Center);
        }
        [Test]
        public static void SetTextTest_複数生成_最小限のパラメータのみ設定()
        {
            var text = nameof(SetTextTest_複数生成_最小限のパラメータのみ設定);
            var position = new Vector2(2, -4);

            var textObj1 = view.SetText(text, position);
            {
                var textObj = textObj1;
                textObj.IsNotNull();
                textObj.gameObject.name.Is(text);
                textObj.text.Is(text);
                textObj.transform.position.x.Is(position.x);
                textObj.transform.position.y.Is(position.y);
                textObj.characterSize.Is(1);
                textObj.color.Is(Color.white);
                textObj.anchor.Is(TextAnchor.MiddleCenter);
                textObj.alignment.Is(TextAlignment.Center);
            }

            var textObj2 = view.SetText(text, position);
            {
                var textObj = textObj2;
                textObj.IsNotNull();
                textObj.gameObject.name.Is($"{text}_0");
                textObj.text.Is(text);
                textObj.transform.position.x.Is(position.x);
                textObj.transform.position.y.Is(position.y);
                textObj.characterSize.Is(1);
                textObj.color.Is(Color.white);
                textObj.anchor.Is(TextAnchor.MiddleCenter);
                textObj.alignment.Is(TextAlignment.Center);
            }
        }
    }
}
