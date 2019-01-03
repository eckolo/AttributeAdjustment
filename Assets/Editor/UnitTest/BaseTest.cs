using NUnit.Framework;
using UnityEngine;
/// <summary>
/// ユニットテストモジュール
/// </summary>
namespace Assets.Editor.UnitTest
{
    /// <summary>
    /// 各種機能に関係しない基盤テスト
    /// </summary>
    public static class BaseTest
    {
        /// <summary>
        /// ユニットテスト自体が正常に行われているか否かの確認
        /// 必ず成功するはずのテストを行う
        /// </summary>
        [Test]
        public static void ParanoiaTest()
        {
            //Arrange
            var gameObject = new GameObject();

            //Act
            //Try to rename the GameObject
            var newGameObjectName = "testBase";
            gameObject.name = newGameObjectName;

            //Assert
            //The object has a new name
            Assert.AreEqual(newGameObjectName, gameObject.name);
        }
    }
}
