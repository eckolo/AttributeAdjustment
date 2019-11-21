using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Mock;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using Assets.Src.View.Repository;
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
            var viewStateMap = new Dictionary<ViewStateKey, ViewState>();
            var repository = new ViewStateRepositoryMock(viewStateMap);

            var result = repository.GenerateViewState(key);

            result.IsNotNull();
            key.isGenerated.IsTrue();
            viewStateMap.Count.Is(1);
            viewStateMap.ContainsKey(key).IsTrue();
            viewStateMap[key].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存有_未生成()
        {
            var key = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) });
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) });
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_新規生成_既存有_未生成));
            var viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                };
            var repository = new ViewStateRepositoryMock(viewStateMap);

            var result = repository.GenerateViewState(key);

            result.IsNotNull();
            key.isGenerated.IsTrue();
            keyOrigin.isGenerated.IsFalse();
            viewStateMap.Count.Is(2);
            viewStateMap.ContainsKey(key).IsTrue();
            viewStateMap[key].IsSameReferenceAs(result);
            viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewStateMap[keyOrigin].IsSameReferenceAs(stateOrigin);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_既存取得_未生成()
        {
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) });
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_既存取得_未生成));
            var viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                };
            var repository = new ViewStateRepositoryMock(viewStateMap);

            var result = repository.GenerateViewState(keyOrigin);

            result.IsNotNull();
            result.IsSameReferenceAs(stateOrigin);
            keyOrigin.isGenerated.IsFalse();
            viewStateMap.Count.Is(1);
            viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewStateMap[keyOrigin].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存無_生成済()
        {
            var key = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) }, isGenerated: true);
            var viewStateMap = new Dictionary<ViewStateKey, ViewState>();
            var repository = new ViewStateRepositoryMock(viewStateMap);

            var result = repository.GenerateViewState(key);

            result.IsNull();
            key.isGenerated.IsTrue();
            viewStateMap.Count.Is(0);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_新規生成_既存有_生成済()
        {
            var key = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) }, isGenerated: true);
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) }, isGenerated: true);
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_新規生成_既存有_生成済));
            var viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                };
            var repository = new ViewStateRepositoryMock(viewStateMap);

            var result = repository.GenerateViewState(key);

            result.IsNull();
            key.isGenerated.IsTrue();
            keyOrigin.isGenerated.IsTrue();
            viewStateMap.Count.Is(1);
            viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewStateMap[keyOrigin].IsSameReferenceAs(stateOrigin);
        }
        [Test]
        public static void GenerateViewStateTest_正常系_既存取得_生成済()
        {
            var keyOrigin = ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) }, isGenerated: true);
            var stateOrigin = ViewStateMock.Generate(nameof(GenerateViewStateTest_正常系_既存取得_生成済));
            var viewStateMap = new Dictionary<ViewStateKey, ViewState>
                {
                    { keyOrigin, stateOrigin },
                };
            var repository = new ViewStateRepositoryMock(viewStateMap);

            var result = repository.GenerateViewState(keyOrigin);

            result.IsNotNull();
            result.IsSameReferenceAs(stateOrigin);
            keyOrigin.isGenerated.IsTrue();
            viewStateMap.Count.Is(1);
            viewStateMap.ContainsKey(keyOrigin).IsTrue();
            viewStateMap[keyOrigin].IsSameReferenceAs(result);
        }
        [Test]
        public static void GenerateViewStateTest_異常系_RootがNull()
        {
            var repository = (ViewStateRepository)null;

            Assert.Throws<ArgumentNullException>(() => repository.GenerateViewState(ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) })));
        }
        [Test]
        public static void GenerateViewStateTest_異常系_検索キーがNull()
        {
            var viewStateMap = new Dictionary<ViewStateKey, ViewState>();
            var repository = new ViewStateRepositoryMock(viewStateMap);
            var nullKey = (ViewStateKey)null;

            Assert.Throws<ArgumentNullException>(() => repository.GenerateViewState(nullKey));
        }
    }
}
