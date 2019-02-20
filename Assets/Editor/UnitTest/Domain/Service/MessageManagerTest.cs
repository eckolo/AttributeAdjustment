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
    }
}
