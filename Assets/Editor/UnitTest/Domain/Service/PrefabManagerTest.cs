using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// プレファブなどの実体化されたオブジェクト類の管理クラスのテストモジュール
    /// </summary>
    public static class PrefabManagerTest
    {
        /// <summary>
        /// ユニットテスト用MonoBehaviour
        /// </summary>
        public class TestMonoBehaviour : MonoBehaviour
        {

        }

        [Test]
        public static void SetPrefabTest()
        {
            var view = new GameObject(nameof(View), typeof(View)).GetComponent<View>();

            var name1 = nameof(SetPrefabTest);
            var object1 = view.SetPrefab<TestMonoBehaviour>(name1);
            var object2 = GameObject.Find(name1).GetComponent<TestMonoBehaviour>();
            var object3 = view.SetPrefab<TestMonoBehaviour>(null);

            object1.IsSameReferenceAs(object2);
            object1.name.Is(name1);
            object2.name.Is(name1);
            object3.name.Is(PrefabManager.ANONYMOUS_NAME);
        }
        [Test]
        public static void SetPrefabTest2()
        {
            var view = new GameObject(nameof(View), typeof(View)).GetComponent<View>();

            var object1 = view.SetPrefab<TestMonoBehaviour>();
            var object2 = GameObject.Find(nameof(TestMonoBehaviour)).GetComponent<TestMonoBehaviour>();

            object1.IsSameReferenceAs(object2);
            object1.name.Is(nameof(TestMonoBehaviour));
            object2.name.Is(nameof(TestMonoBehaviour));
        }
        [Test]
        public static void SetParentTest_親指定()
        {
            var name1 = nameof(SetParentTest_親指定);
            var name2 = $"{nameof(SetParentTest_親指定)}_parent";
            var object1 = new GameObject(name1, typeof(TestMonoBehaviour)).GetComponent<TestMonoBehaviour>();
            var object2 = new GameObject(name2, typeof(TestMonoBehaviour)).GetComponent<TestMonoBehaviour>();
            var object3 = object1.SetParent(object2);

            object1.IsSameReferenceAs(object3);
            object1.transform.parent.IsSameReferenceAs(object2.transform);
            object3.transform.parent.IsSameReferenceAs(object2.transform);
        }
        [Test]
        public static void SetParentTest_引数がnull()
        {
            var name1 = nameof(SetParentTest_引数がnull);
            var name2 = $"{nameof(SetParentTest_引数がnull)}_parent";
            var object1 = new GameObject(name1, typeof(TestMonoBehaviour)).GetComponent<TestMonoBehaviour>();
            var object2 = new GameObject(name2, typeof(TestMonoBehaviour)).GetComponent<TestMonoBehaviour>();
            var object3 = object1.SetParent((TestMonoBehaviour)null);

            Assert.Throws<ArgumentNullException>(() => ((TestMonoBehaviour)null).SetParent(object2));

            object1.IsSameReferenceAs(object3);
            object1.transform.parent.IsNull();
            object3.transform.parent.IsNull();
        }
    }
}
