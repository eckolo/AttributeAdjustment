using Assets.Src.Domain.Model.Entity;
using Assets.Src.Mock;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Model.Entity
{
    public class ViewTest
    {
        [Test]
        public static void CleateNewTest_正常系_親有()
        {
            var name1 = nameof(CleateNewTest_正常系_親有);
            var name2 = $"{nameof(CleateNewTest_正常系_親有)}_parent";
            var parent = new GameObject(name2, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
            var view = View.CleateNew(name1, parent);

            view.IsNotNull();
            view.name.Is($"View_{name1}");
            view.transform.parent.IsSameReferenceAs(parent.transform);
        }
        [Test]
        public static void CleateNewTest_正常系_親無()
        {
            var name1 = nameof(CleateNewTest_正常系_親無);
            var view = View.CleateNew(name1);

            view.IsNotNull();
            view.name.Is($"View_{name1}");
            view.transform.parent.IsNull();
        }
        [Test]
        public static void CleateNewTest_正常系_親有_名前がNull()
        {
            var name1 = (string)null;
            var name2 = $"{nameof(CleateNewTest_正常系_親有)}_parent";
            var parent = new GameObject(name2, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
            var view = View.CleateNew(name1, parent);

            view.IsNotNull();
            view.name.Is($"View");
            view.transform.parent.IsSameReferenceAs(parent.transform);
        }
        [Test]
        public static void CleateNewTest_正常系_親無_名前がNull()
        {
            var name1 = (string)null;
            var view = View.CleateNew(name1);

            view.IsNotNull();
            view.name.Is($"View");
            view.transform.parent.IsNull();
        }
    }
}
