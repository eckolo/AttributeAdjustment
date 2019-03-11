using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.Mock;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Factory
{
    /// <summary>
    /// プレファブなどの実体化されるオブジェクト類の生成処理クラスのテストモジュール
    /// </summary>
    public static class PrefabFactoryTest
    {
        [Test]
        public static void SetPrefabTest()
        {
            var view = new GameObject(nameof(SetPrefabTest), typeof(ViewRoot)).GetComponent<ViewRoot>();

            var name1 = $"{nameof(MonoBehaviourMock)}_{nameof(SetPrefabTest)}";
            var object1 = view.SetPrefab<MonoBehaviourMock>(name1);
            var object2 = GameObject.Find(name1).GetComponent<MonoBehaviourMock>();
            var object3 = view.SetPrefab<MonoBehaviourMock>(null);

            object1.IsSameReferenceAs(object2);
            object1.name.Is(name1);
            object2.name.Is(name1);
            object3.name.Is(Constants.Texts.ANONYMOUS_NAME);
        }
        [Test]
        public static void SetPrefabTest2()
        {
            var view = new GameObject(nameof(SetPrefabTest2), typeof(ViewRoot)).GetComponent<ViewRoot>();

            var object1 = view.SetPrefab<MonoBehaviourMock>();
            var object2 = GameObject.Find(nameof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();

            object1.IsSameReferenceAs(object2);
            object1.name.Is(nameof(MonoBehaviourMock));
            object2.name.Is(nameof(MonoBehaviourMock));
        }
        [Test]
        public static void SetParentTest_親指定()
        {
            var name1 = nameof(SetParentTest_親指定);
            var name2 = $"{nameof(SetParentTest_親指定)}_parent";
            var object1 = new GameObject(name1, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
            var object2 = new GameObject(name2, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
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
            var object1 = new GameObject(name1, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
            var object2 = new GameObject(name2, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
            var object3 = object1.SetParent((MonoBehaviourMock)null);

            Assert.Throws<ArgumentNullException>(() => ((MonoBehaviourMock)null).SetParent(object2));

            object1.IsSameReferenceAs(object3);
            object1.transform.parent.IsNull();
            object3.transform.parent.IsNull();
        }
    }
}
