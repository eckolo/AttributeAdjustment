using Assets.Src.Domain.Model.Entity;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Model.Entity
{
    public class ViewRootTest
    {
        [Test]
        public static void CleateNewTest_正常系_親有()
        {
            var name1 = nameof(CleateNewTest_正常系_親有);
            var name2 = $"{nameof(CleateNewTest_正常系_親有)}_parent";
            var parent = MonoBehaviourMock.Generate(name2);
            var view = ViewStateMock.Generate(name1, parent);

            view.IsNotNull();
            view.name.Is($"{nameof(ViewStateMock)}_{name1}");
            view.transform.parent.IsSameReferenceAs(parent.transform);
        }
        [Test]
        public static void CleateNewTest_正常系_親無()
        {
            var name1 = nameof(CleateNewTest_正常系_親無);
            var view = ViewStateMock.Generate(name1);

            view.IsNotNull();
            view.name.Is($"{nameof(ViewStateMock)}_{name1}");
            view.transform.parent.IsNull();
        }
        [Test]
        public static void CleateNewTest_正常系_親有_名前がNull()
        {
            var name1 = (string)null;
            var name2 = $"{nameof(CleateNewTest_正常系_親有)}_parent";
            var parent = MonoBehaviourMock.Generate(name2);
            var view = ViewStateMock.Generate(name1, parent);

            view.IsNotNull();
            view.name.Is($"{nameof(ViewStateMock)}");
            view.transform.parent.IsSameReferenceAs(parent.transform);
        }
        [Test]
        public static void CleateNewTest_正常系_親無_名前がNull()
        {
            var name1 = (string)null;
            var view = ViewStateMock.Generate(name1);

            view.IsNotNull();
            view.name.Is($"{nameof(ViewStateMock)}");
            view.transform.parent.IsNull();
        }
    }
}
