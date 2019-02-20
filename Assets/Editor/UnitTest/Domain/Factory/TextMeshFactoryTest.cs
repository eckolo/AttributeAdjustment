using Assets.Editor.UnitTest.Domain.Service;
using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Factory
{
    /// <summary>
    /// テキストオブジェクト生成処理のテスト
    /// </summary>
    public static class TextMeshFactoryTest
    {
        static readonly View view = View.CleateNew(nameof(MessageManagerTest));
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
