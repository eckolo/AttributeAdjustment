using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using Assets.Src.Infrastructure.Service;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// メッセージ制御クラスのテストモジュール
    /// </summary>
    public static class MessageManagerTest
    {
        static readonly IFileManager fileManager = new FileManager();

        static readonly View view = View.CleateNew(nameof(MessageManagerTest));

        /// <summary>
        /// ログ残すメソッドのテスト
        /// </summary>
        [Test]
        public static void LeaveLogTest()
        {
            var text1 = "text_trace";
            var text2 = "text_debug";
            var text3 = "text_error";
            var path = $"{Application.dataPath}/Logs";

            var file1 = LogHub.TRACE.LeaveLog(text1, fileManager);
            var result1 = fileManager.Read(path, file1);
            result1.Contains("【TRACE】").IsTrue();
            result1.Contains(text1).IsTrue();

            var file2 = LogHub.DEBUG.LeaveLog(text2, fileManager);
            var result2 = fileManager.Read(path, file2);
            result2.Contains("【DEBUG】").IsTrue();
            result2.Contains(text2).IsTrue();

            var file3 = LogHub.ERROR.LeaveLog(text3, fileManager);
            var result3 = fileManager.Read(path, file3);
            result3.Contains("【ERROR】").IsTrue();
            result3.Contains(text3).IsTrue();
        }

        [Test]
        public static void SetTextSetTest_単一生成_全パラメータ設定()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => $"{nameof(SetTextSetTest_単一生成_全パラメータ設定)}_{index}");
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
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
            {
                var text = textList[1];
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
            {
                var text = textList[2];
                text.characterSize.Is(size);
                text.color.Is(color);
                text.alignment.Is(alignment);
            }
        }
        [Test]
        public static void SetTextSetTest_単一生成_最小限のパラメータのみ設定()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => $"{nameof(SetTextSetTest_単一生成_最小限のパラメータのみ設定)}_{index}");
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
                text.characterSize.Is(1);
                text.color.Is(Color.white);
                text.alignment.Is(TextAlignment.Center);
            }
            {
                var text = textList[1];
                text.characterSize.Is(1);
                text.color.Is(Color.white);
                text.alignment.Is(TextAlignment.Center);
            }
            {
                var text = textList[2];
                text.characterSize.Is(1);
                text.color.Is(Color.white);
                text.alignment.Is(TextAlignment.Center);
            }
        }
        [Test]
        public static void SetTextSetTest_複数生成_最小限のパラメータのみ設定()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => $"{nameof(SetTextSetTest_複数生成_最小限のパラメータのみ設定)}_{index}");
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
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[1];
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[2];
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
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[1];
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
                {
                    var text = textList[2];
                    text.characterSize.Is(1);
                    text.color.Is(Color.white);
                    text.alignment.Is(TextAlignment.Center);
                }
            }
        }
    }
}
