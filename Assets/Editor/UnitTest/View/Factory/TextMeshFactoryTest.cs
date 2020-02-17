using Assets.Editor.UnitTest.Domain.Service;
using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.Mock.Model.Value;
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
            var size = 0.6f;
            var color = Color.green;
            var alignment = TextAlignment.Right;
            var stationery = TextMeshKeyMock.Generate(text, size, color, alignment);

            var textObj = view.SetText(stationery).Search<ITextMeshKey, TextMesh>(stationery);

            textObj.IsNotNull();
            textObj.gameObject.name.Is(text);
            textObj.text.Is(text);
            textObj.color.Is(color);
            textObj.characterSize.Is(size);
            textObj.alignment.Is(alignment);
            textObj.anchor.Is(TextAnchor.MiddleCenter);
            textObj.transform.localScale.x.Is(0.5f);
            textObj.transform.localScale.y.Is(0.5f);
            textObj.transform.localScale.z.Is(0.5f);
        }
        [Test]
        public static void SetTextTest_雛形生成_複数生成()
        {
            var text = nameof(SetTextTest_雛形生成_複数生成);

            var size1 = 0.6f;
            var color1 = Color.green;
            var alignment1 = TextAlignment.Right;
            var stationery1 = TextMeshKeyMock.Generate(text, size1, color1, alignment1);
            var textObj1 = view.SetText(stationery1).Search<ITextMeshKey, TextMesh>(stationery1);
            {
                var textObj = textObj1;
                textObj.IsNotNull();
                textObj.gameObject.name.Is(text);
                textObj.text.Is(text);
                textObj.color.Is(color1);
                textObj.characterSize.Is(size1);
                textObj.alignment.Is(alignment1);
                textObj.anchor.Is(TextAnchor.MiddleCenter);
                textObj.transform.localScale.x.Is(0.5f);
                textObj.transform.localScale.y.Is(0.5f);
                textObj.transform.localScale.z.Is(0.5f);
            }

            var size2 = 0.9f;
            var color2 = Color.yellow;
            var alignment2 = TextAlignment.Left;
            var stationery2 = TextMeshKeyMock.Generate(text, size2, color2, alignment2);
            var textObj2 = view.SetText(stationery2).Search<ITextMeshKey, TextMesh>(stationery2);
            {
                var textObj = textObj2;
                textObj.IsNotNull();
                textObj.gameObject.name.Is($"{text}_0");
                textObj.text.Is(text);
                textObj.color.Is(color2);
                textObj.characterSize.Is(size2);
                textObj.alignment.Is(alignment2);
                textObj.anchor.Is(TextAnchor.MiddleCenter);
                textObj.transform.localScale.x.Is(0.5f);
                textObj.transform.localScale.y.Is(0.5f);
                textObj.transform.localScale.z.Is(0.5f);
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
