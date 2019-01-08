using Assets.Src.Domain.Service;
using Assets.Src.Infrastructure.Service;
using NUnit.Framework;
using UnityEngine;
namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// メッセージ制御クラスのテストモジュール
    /// </summary>
    public static class MessageManagerTest
    {
        static readonly IFileManager fileManager = new FileManager();

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
        public static void SetTextTest_単一生成_全パラメータ設定()
        {
            var text = nameof(SetTextTest_単一生成_全パラメータ設定);
            var position = new Vector2(2, -4);
            var size = 0.6f;
            var color = Color.green;
            var pivot = TextAnchor.LowerLeft;
            var alignment = TextAlignment.Right;

            var textObj = text.SetText(position, size, color, pivot, alignment);

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

            var textObj = text.SetText(position);

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

            var textObj1 = text.SetText(position);
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

            var textObj2 = text.SetText(position);
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
