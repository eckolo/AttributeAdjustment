using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Mock;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Editor.UnitTest.View.Factory
{
    /// <summary>
    /// <see cref="ViewStateFactory"/>のテスト
    /// </summary>
    public static class ViewStateFactoryTest
    {
        static readonly ViewStateKey[] keys = new[]
        {
            ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) }),
            ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) }),
        };
        static readonly ViewState[] states = new[]
        {
            ViewStateMock.Generate($"{nameof(ViewStateFactoryTest)}_1"),
        };
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存無()
        {
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>(),
            };

            var result = viewRoot.GenerateViewState(keys[0]);

            result.IsNotNull();
            viewRoot.viewStateMap.Count.Is(1);
            viewRoot.viewStateMap.ContainsKey(keys[0]).IsTrue();
            viewRoot.viewStateMap[keys[0]].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存有()
        {
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keys[1], states[0] },
                },
            };

            var result = viewRoot.GenerateViewState(keys[0]);

            result.IsNotNull();
            viewRoot.viewStateMap.Count.Is(2);
            viewRoot.viewStateMap.ContainsKey(keys[0]).IsTrue();
            viewRoot.viewStateMap[keys[0]].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_既存取得()
        {
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keys[1], states[0] },
                },
            };

            var result = viewRoot.GenerateViewState(keys[1]);

            result.IsNotNull();
            result.Is(states[0]);
            viewRoot.viewStateMap.Count.Is(1);
            viewRoot.viewStateMap.ContainsKey(keys[1]).IsTrue();
            viewRoot.viewStateMap[keys[1]].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_異常系_RootがNull()
        {
            var viewRoot = (ViewRoot)null;

            Assert.Throws<ArgumentNullException>(() => viewRoot.GenerateViewState(keys[0]));
        }
        [Test]
        public static void GenerateViewStateTest_異常系_検索キーがNull()
        {
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>(),
            };
            var nullKey = (ViewStateKey)null;

            Assert.Throws<ArgumentNullException>(() => viewRoot.GenerateViewState(nullKey));
        }
        [Test]
        public static void GenerateViewStateTest_異常系_仮想ツリーがNull()
        {
            var viewRoot = new ViewRoot
            {
                viewStateMap = null,
            };

            Assert.Throws<ArgumentNullException>(() => viewRoot.GenerateViewState(keys[0]));
        }
    }
}
