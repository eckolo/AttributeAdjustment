using Assets.Src.Domain.Model.Entity;
using Assets.Src.Mock.Model.Value;
using Assets.Src.Mock.Model.Entity;
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
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(0) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(1) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(2) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(3) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(4) }),
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
        public static void SaveTest_正常系_既存無し()
        {
            var addKey = keys[1];
            var addState = states[2];
            var map = new Dictionary<ViewStateKey, ViewState>
            {
                { keys[0], states[0] },
            };
            var repository = new ViewStateRepository(map);

            var result = repository.Save(addKey, addState);

            result.IsNotNull();
            result.IsSameReferenceAs(addState);

            repository.Search(addKey).IsNotNull();
            repository.Search(addKey).IsSameReferenceAs(addState);
        }
        [Test]
        public static void SaveTest_正常系_既存有り_既存と同一()
        {
            var addKey = keys[1];
            var addState = states[2];
            var map = new Dictionary<ViewStateKey, ViewState>
            {
                { keys[0], states[0] },
                { addKey, addState },
            };
            var repository = new ViewStateRepository(map);

            var result = repository.Save(addKey, addState);

            result.IsNotNull();
            result.IsSameReferenceAs(addState);

            repository.Search(addKey).IsNotNull();
            repository.Search(addKey).IsSameReferenceAs(addState);

            addState.isDestroied.IsFalse();
        }
        [Test]
        public static void SaveTest_正常系_既存有り_既存と同一ではない()
        {
            var addKey = keys[1];
            var addState = states[2];
            var existingState = states[1];
            var map = new Dictionary<ViewStateKey, ViewState>
            {
                { keys[0], states[0] },
                { addKey, existingState },
            };
            var repository = new ViewStateRepository(map);

            var result = repository.Save(addKey, addState);

            result.IsNotNull();
            result.IsSameReferenceAs(addState);

            repository.Search(addKey).IsNotNull();
            repository.Search(addKey).IsSameReferenceAs(addState);

            existingState.isDestroied.IsTrue();
        }
        [Test]
        public static void SaveTest_異常系_キーがNull()
        {
            ViewStateKey addKey = null;
            var addState = states[2];
            var map = new Dictionary<ViewStateKey, ViewState>
            {
                { keys[0], states[0] },
            };
            var repository = new ViewStateRepository(map);

            Assert.Throws<ArgumentNullException>(() => repository.Save(addKey, addState));
        }

        [Test]
        public static void SearchTest_正常系_該当有()
        {
            viewStateRepository.Search(keys[0]).Is(states[0]);
            viewStateRepository.Search(keys[1]).Is(states[1]);
            viewStateRepository.Search(keys[2]).Is(states[2]);
        }
        [Test]
        public static void SearchTest_正常系_該当無()
        {
            viewStateRepository.Search(keys[3]).IsNull();
        }
        [Test]
        public static void SearchTest_正常系_Null検索()
        {
            var nullKey = (ViewStateKey)null;
            viewStateRepository.Search(nullKey).IsNull();
        }
    }
}
