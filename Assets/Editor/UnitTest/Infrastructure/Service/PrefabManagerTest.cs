using Assets.Src.Infrastructure.Service;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.UnitTest.Infrastructure.Service
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
        public static void SetObjectTest()
        {
            var name1 = "object1";
            var object1 = name1.SetObject<TestMonoBehaviour>();
            var object2 = GameObject.Find(name1).GetComponent<TestMonoBehaviour>();
            var object3 = PrefabManager.SetObject<TestMonoBehaviour>(null);

            object1.IsSameReferenceAs(object2);
            object1.name.Is(name1);
            object2.name.Is(name1);
            object3.name.Is(PrefabManager.ANONYMOUS_NAME);
        }
        [Test]
        public static void SetObjectTest2()
        {
            var name1 = "TestMonoBehaviour";
            var object1 = PrefabManager.SetObject<TestMonoBehaviour>();
            var object2 = GameObject.Find(name1).GetComponent<TestMonoBehaviour>();

            object1.IsSameReferenceAs(object2);
            object1.name.Is(name1);
            object2.name.Is(name1);
        }
        [Test]
        public static void SetParentTest()
        {
            var object1 = new GameObject("object1", typeof(TestMonoBehaviour)).GetComponent<TestMonoBehaviour>();
            var object2 = new GameObject("object2", typeof(TestMonoBehaviour)).GetComponent<TestMonoBehaviour>();
            var object3 = object1.SetParent(object2);

            object1.IsSameReferenceAs(object3);
            object1.transform.parent.IsSameReferenceAs(object2.transform);
            object3.transform.parent.IsSameReferenceAs(object2.transform);
        }
    }
}
