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
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存無_未生成()
        {
            var key = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) });
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>(),
            };

            var result = viewRoot.GenerateViewState(key);

            result.IsNotNull();
            key.isGenerated.IsTrue();
            viewRoot.viewStateMap.Count.Is(1);
            viewRoot.viewStateMap.ContainsKey(key).IsTrue();
            viewRoot.viewStateMap[key].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存有_未生成()
        {
            var key = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) });
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) });
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_新規生成_既存有_未生成));
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                },
            };

            var result = viewRoot.GenerateViewState(key);

            result.IsNotNull();
            key.isGenerated.IsTrue();
            keyOrigin.isGenerated.IsFalse();
            viewRoot.viewStateMap.Count.Is(2);
            viewRoot.viewStateMap.ContainsKey(key).IsTrue();
            viewRoot.viewStateMap[key].IsSameReferenceAs(result);
            viewRoot.viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewRoot.viewStateMap[keyOrigin].IsSameReferenceAs(stateOrigin);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_既存取得_未生成()
        {
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) });
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_既存取得_未生成));
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                },
            };

            var result = viewRoot.GenerateViewState(keyOrigin);

            result.IsNotNull();
            result.IsSameReferenceAs(stateOrigin);
            keyOrigin.isGenerated.IsFalse();
            viewRoot.viewStateMap.Count.Is(1);
            viewRoot.viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewRoot.viewStateMap[keyOrigin].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存無_生成済()
        {
            var key = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) }, isGenerated: true);
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>(),
            };

            var result = viewRoot.GenerateViewState(key);

            result.IsNull();
            key.isGenerated.IsTrue();
            viewRoot.viewStateMap.Count.Is(0);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存有_生成済()
        {
            var key = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) }, isGenerated: true);
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) }, isGenerated: true);
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_新規生成_既存有_生成済));
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                },
            };

            var result = viewRoot.GenerateViewState(key);

            result.IsNull();
            key.isGenerated.IsTrue();
            keyOrigin.isGenerated.IsTrue();
            viewRoot.viewStateMap.Count.Is(1);
            viewRoot.viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewRoot.viewStateMap[keyOrigin].IsSameReferenceAs(stateOrigin);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_既存取得_生成済()
        {
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) }, isGenerated: true);
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_既存取得_生成済));
            var viewRoot = new ViewRoot
            {
                viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                },
            };

            var result = viewRoot.GenerateViewState(keyOrigin);

            result.IsNotNull();
            result.IsSameReferenceAs(stateOrigin);
            keyOrigin.isGenerated.IsTrue();
            viewRoot.viewStateMap.Count.Is(1);
            viewRoot.viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewRoot.viewStateMap[keyOrigin].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_異常系_RootがNull()
        {
            var viewRoot = (ViewRoot)null;

            Assert.Throws<ArgumentNullException>(() => viewRoot.GenerateViewState(ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) })));
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

            Assert.Throws<ArgumentNullException>(() => viewRoot.GenerateViewState(ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) })));
        }
    }
}
