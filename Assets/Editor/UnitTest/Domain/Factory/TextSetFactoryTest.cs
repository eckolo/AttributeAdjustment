using Assets.Editor.UnitTest.Domain.Service;
using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.Mock.Model.Value;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Factory
{
    /// <summary>
    /// 文字列集合の生成処理テスト
    /// </summary>
    public static class TextSetFactoryTest
    {
        static readonly ViewState view = ViewStateMock.Generate(nameof(MessageManagerTest));

        [Test]
        public static void SetTextSetTest_単一生成_全パラメータ設定()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => ($"{nameof(SetTextSetTest_単一生成_全パラメータ設定)}_{index}", new Vector2(0, index)));
            var position = new Vector2(2, -4);
            var size = 0.6f;
            var color = Color.green;
            var alignment = TextAlignment.Right;

            var textSet = view.SetTextSet(texts, position, size, color, alignment);

            textSet.IsNotNull();
            textSet.gameObject.name.Is($"{nameof(SetTextSetTest_単一生成_全パラメータ設定)}_1 {nameof(SetTextSetTest_単一生成_全パラメータ設定)}_2 {nameof(SetTextSetTest_単一生成_全パラメータ設定)}_3");
            textSet.transform.position.x.Is(position.x);
            textSet.transform.position.y.Is(position.y);
            textSet.texts.IsNotNull();
            var textList = textSet.texts.ToList();
            textList.IsNotNull();
            textList.Count.Is(texts.Count());
            {
                var text = textList[0];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(1);
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
            {
                var text = textList[1];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(2);
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
            {
                var text = textList[2];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(3);
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
        }
        [Test]
        public static void SetTextSetTest_単一生成_最小限のパラメータのみ設定()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => ($"{nameof(SetTextSetTest_単一生成_最小限のパラメータのみ設定)}_{index}", new Vector2(0, index)));
            var position = new Vector2(2, -4);

            var textSet = view.SetTextSet(texts, position);

            textSet.IsNotNull();
            textSet.gameObject.name.Is($"{nameof(SetTextSetTest_単一生成_最小限のパラメータのみ設定)}_1 {nameof(SetTextSetTest_単一生成_最小限のパラメータのみ設定)}_2 {nameof(SetTextSetTest_単一生成_最小限のパラメータのみ設定)}_3");
            textSet.transform.position.x.Is(position.x);
            textSet.transform.position.y.Is(position.y);
            textSet.texts.IsNotNull();
            var textList = textSet.texts.ToList();
            textList.IsNotNull();
            textList.Count.Is(texts.Count());
            {
                var text = textList[0];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(1);
                text.characterSize.Is(1);
                text.color.Is(Color.white);
                text.alignment.Is(TextAlignment.Center);
            }
            {
                var text = textList[1];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(2);
                text.characterSize.Is(1);
                text.color.Is(Color.white);
                text.alignment.Is(TextAlignment.Center);
            }
            {
                var text = textList[2];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(3);
                text.characterSize.Is(1);
                text.color.Is(Color.white);
                text.alignment.Is(TextAlignment.Center);
            }
        }
        [Test]
        public static void SetTextSetTest_複数生成_最小限のパラメータのみ設定()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => ($"{nameof(SetTextSetTest_複数生成_最小限のパラメータのみ設定)}_{index}", new Vector2(0, index)));
            var position = new Vector2(2, -4);
            var textSetName = $"{nameof(SetTextSetTest_複数生成_最小限のパラメータのみ設定)}_1 {nameof(SetTextSetTest_複数生成_最小限のパラメータのみ設定)}_2 {nameof(SetTextSetTest_複数生成_最小限のパラメータのみ設定)}_3";

            var textSet1 = view.SetTextSet(texts, position);
            {
                var textSet = textSet1;
                textSet.IsNotNull();
                textSet.gameObject.name.Is(textSetName);
                textSet.transform.position.x.Is(position.x);
                textSet.transform.position.y.Is(position.y);
                textSet.texts.IsNotNull();
                var textList = textSet.texts.ToList();
                textList.IsNotNull();
                textList.Count.Is(texts.Count());
                {
                    var text = textList[0];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(1);
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[1];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(2);
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[2];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(3);
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
            }

            var textSet2 = view.SetTextSet(texts, position);
            {
                var textSet = textSet2;
                textSet.IsNotNull();
                textSet.gameObject.name.Is(textSetName);
                textSet.transform.position.x.Is(position.x);
                textSet.transform.position.y.Is(position.y);
                textSet.texts.IsNotNull();
                var textList = textSet.texts.ToList();
                textList.IsNotNull();
                textList.Count.Is(texts.Count());
                {
                    var text = textList[0];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(1);
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[1];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(2);
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[2];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(3);
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
            }
        }

        [Test]
        public static void SetTextSetTest_単一生成_雛形から生成()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => new TextMeshKey($"{nameof(SetTextSetTest_単一生成_雛形から生成)}_{index}", new Vector2(0, index)));
            var position = new Vector2(2, -4);
            var size = 0.6f;
            var color = Color.green;
            var alignment = TextAlignment.Right;
            var stationery = TextMeshValueMock.Generate(texts, size, color, alignment);

            var textSet = view.SetTextSet(stationery, position);

            textSet.IsNotNull();
            textSet.gameObject.name.Is($"{nameof(SetTextSetTest_単一生成_雛形から生成)}_1 {nameof(SetTextSetTest_単一生成_雛形から生成)}_2 {nameof(SetTextSetTest_単一生成_雛形から生成)}_3");
            textSet.transform.position.x.Is(position.x);
            textSet.transform.position.y.Is(position.y);
            textSet.texts.IsNotNull();
            var textList = textSet.texts.ToList();
            textList.IsNotNull();
            textList.Count.Is(texts.Count());
            {
                var text = textList[0];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(1);
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
            {
                var text = textList[1];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(2);
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
            {
                var text = textList[2];
                text.transform.localPosition.x.Is(0);
                text.transform.localPosition.y.Is(3);
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
        }
        [Test]
        public static void SetTextSetTest_雛形から生成_複数生成_雛形から生成()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => new TextMeshKey($"{nameof(SetTextSetTest_雛形から生成_複数生成_雛形から生成)}_{index}", new Vector2(0, index)));
            var textSetName = $"{nameof(SetTextSetTest_雛形から生成_複数生成_雛形から生成)}_1 {nameof(SetTextSetTest_雛形から生成_複数生成_雛形から生成)}_2 {nameof(SetTextSetTest_雛形から生成_複数生成_雛形から生成)}_3";
            var position = new Vector2(2, -4);
            var size = 0.6f;
            var color = Color.green;
            var alignment = TextAlignment.Right;
            var stationery = TextMeshValueMock.Generate(texts, size, color, alignment);

            var textSet1 = view.SetTextSet(stationery, position);
            {
                var textSet = textSet1;
                textSet.IsNotNull();
                textSet.gameObject.name.Is(textSetName);
                textSet.transform.position.x.Is(position.x);
                textSet.transform.position.y.Is(position.y);
                textSet.texts.IsNotNull();
                var textList = textSet.texts.ToList();
                textList.IsNotNull();
                textList.Count.Is(texts.Count());
                {
                    var text = textList[0];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(1);
                    text.characterSize.Is(size);
                    text.color.Is(color);
                    text.alignment.Is(alignment);
                }
                {
                    var text = textList[1];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(2);
                    text.characterSize.Is(size);
                    text.color.Is(color);
                    text.alignment.Is(alignment);
                }
                {
                    var text = textList[2];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(3);
                    text.characterSize.Is(size);
                    text.color.Is(color);
                    text.alignment.Is(alignment);
                }
            }

            var textSet2 = view.SetTextSet(stationery, position);
            {
                var textSet = textSet2;
                textSet.IsNotNull();
                textSet.gameObject.name.Is(textSetName);
                textSet.transform.position.x.Is(position.x);
                textSet.transform.position.y.Is(position.y);
                textSet.texts.IsNotNull();
                var textList = textSet.texts.ToList();
                textList.IsNotNull();
                textList.Count.Is(texts.Count());
                {
                    var text = textList[0];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(1);
                    text.characterSize.Is(size);
                    text.color.Is(color);
                    text.alignment.Is(alignment);
                }
                {
                    var text = textList[1];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(2);
                    text.characterSize.Is(size);
                    text.color.Is(color);
                    text.alignment.Is(alignment);
                }
                {
                    var text = textList[2];
                    text.transform.localPosition.x.Is(0);
                    text.transform.localPosition.y.Is(3);
                    text.characterSize.Is(size);
                    text.color.Is(color);
                    text.alignment.Is(alignment);
                }
            }
        }
    }
}
