using Assets.Src.Domain.Service;
using Assets.Src.Infrastructure.Service;
using NUnit.Framework;
using System;
using UnityEngine;
namespace Assets.Editor.UnitTest.Infrastructure.Service
{
    /// <summary>
    /// ファイルの入出力制御クラスのテストモジュール
    /// </summary>
    public static class FileManagerTest
    {
        static readonly IFileManager fileManager = new FileManager();

        [Test]
        public static void WriteReadTest()
        {
            var path = $"{Application.dataPath}/UnitTest";
            var filename = $"file1.txt";
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";

            fileManager.Write(path, filename, text1);
            var result1 = fileManager.Read(path, filename);
            result1.Is($"{text1}{Environment.NewLine}");

            fileManager.Write(path, filename, text2);
            var result2 = fileManager.Read(path, filename);
            result2.Is($"{text2}{Environment.NewLine}");

            fileManager.Write(path, filename, text3, true);
            var result3 = fileManager.Read(path, filename);
            result3.Is($"{text2}{Environment.NewLine}{text3}{Environment.NewLine}");
        }
    }
}
