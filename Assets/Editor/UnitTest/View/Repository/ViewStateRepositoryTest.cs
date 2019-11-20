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

namespace Assets.Editor.UnitTest.View.Repository
{
    /// <summary>
    /// <see cref="ViewStateRepository"/>のテスト
    /// </summary>
    public static class ViewStateRepositoryTest
    {
        static readonly ViewStateKey[] keys = new[]
        {
            ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(0) }),
            ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(1) }),
            ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(2) }),
            ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(3) }),
            ViewStateKeyMock.Generate(new[] { IViewKeyMock.Generate(4) }),
        };
        static readonly ViewState[] states = new[]
        {
            ViewStateMock.Generate($"{nameof(ViewStateRepositoryTest)}_0"),
            ViewStateMock.Generate($"{nameof(ViewStateRepositoryTest)}_1"),
            ViewStateMock.Generate($"{nameof(ViewStateRepositoryTest)}_2"),
        };
        static readonly Dictionary<ViewStateKey, ViewState> viewStateMap
            = new Dictionary<ViewStateKey, ViewState>
            {
                { keys[0], states[0] },
                { keys[1], states[1] },
                { keys[2], states[2] },
            };
        static readonly ViewStateRepository viewStateRepository = new ViewStateRepository(viewStateMap);

        [Test]
        public static void SearchViewStateTest_正常系_該当有()
        {
            viewStateRepository.SearchViewState(keys[0]).Is(states[0]);
            viewStateRepository.SearchViewState(keys[1]).Is(states[1]);
            viewStateRepository.SearchViewState(keys[2]).Is(states[2]);
        }
        [Test]
        public static void SearchViewStateTest_正常系_該当無()
        {
            viewStateRepository.SearchViewState(keys[3]).IsNull();
        }
        [Test]
        public static void SearchViewStateTest_正常系_Null検索()
        {
            var nullKey = (ViewStateKey)null;
            viewStateRepository.SearchViewState(nullKey).IsNull();
        }
    }
}
