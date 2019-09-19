﻿using Assets.Src.Domain.Model.Entity;
using Assets.Src.Mock;
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
            var parent = new GameObject(name2, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
            var view = name1.ToViewState(parent);

            view.IsNotNull();
            view.name.Is($"ViewRoot_{name1}");
            view.transform.parent.IsSameReferenceAs(parent.transform);
        }
        [Test]
        public static void CleateNewTest_正常系_親無()
        {
            var name1 = nameof(CleateNewTest_正常系_親無);
            var view = name1.ToViewState();

            view.IsNotNull();
            view.name.Is($"ViewRoot_{name1}");
            view.transform.parent.IsNull();
        }
        [Test]
        public static void CleateNewTest_正常系_親有_名前がNull()
        {
            var name1 = (string)null;
            var name2 = $"{nameof(CleateNewTest_正常系_親有)}_parent";
            var parent = new GameObject(name2, typeof(MonoBehaviourMock)).GetComponent<MonoBehaviourMock>();
            var view = name1.ToViewState(parent);

            view.IsNotNull();
            view.name.Is($"ViewRoot");
            view.transform.parent.IsSameReferenceAs(parent.transform);
        }
        [Test]
        public static void CleateNewTest_正常系_親無_名前がNull()
        {
            var name1 = (string)null;
            var view = name1.ToViewState();

            view.IsNotNull();
            view.name.Is($"ViewRoot");
            view.transform.parent.IsNull();
        }
    }
}
