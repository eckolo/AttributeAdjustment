using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.Mock.Model.Value;
using Assets.Src.View.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Editor.UnitTest.View.Service
{
    public class ComponentManagerTest
    {
        static readonly Func<Dictionary<IViewKey, int>, Dictionary<IViewKey, List<Vector2>>> getPositionMap
            = map => map.ToDictionary(
                  pair => pair.Key,
                  pair => Enumerable.Range(0, pair.Value)
                      .Select((num, index) => new Vector2(((ViewKeyMock)pair.Key).value, index))
                      .ToList());

        [Test]
        public static void GetPositionsText_正常系_コンポーネントキュー単数_レイアウト有()
        {
            var name = nameof(GetPositionsText_正常系_コンポーネントキュー単数_レイアウト有);
            var key1 = ViewKeyMock.Generate(1);
            var component1 = MonoBehaviourMock.Generate($"{name}_1");
            var component2 = MonoBehaviourMock.Generate($"{name}_2");
            var component3 = MonoBehaviourMock.Generate($"{name}_3");
            var components = new[] { component1, component2, component3 };
            var componentMap = new Dictionary<IViewKey, Queue<Component>>
            {
                { key1, new Queue<Component>(components) },
            };
            var layout = ViewLayoutMock.Generate(getPositionMap);

            var result = componentMap.GetPositions(layout)?.ToList();

            result.IsNotNull();
            result.Count().Is(components.Count());
            {
                var (resultComponent, resultVector) = result[0];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component1.name);
                resultVector.x.Is(key1.value);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[1];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component2.name);
                resultVector.x.Is(key1.value);
                resultVector.y.Is(1);
            }
            {
                var (resultComponent, resultVector) = result[2];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component3.name);
                resultVector.x.Is(key1.value);
                resultVector.y.Is(2);
            }
        }
        [Test]
        public static void GetPositionsText_正常系_コンポーネントキュー単数_レイアウト無()
        {
            var name = nameof(GetPositionsText_正常系_コンポーネントキュー単数_レイアウト無);
            var key1 = ViewKeyMock.Generate(1);
            var component1 = MonoBehaviourMock.Generate($"{name}_1");
            var component2 = MonoBehaviourMock.Generate($"{name}_2");
            var component3 = MonoBehaviourMock.Generate($"{name}_3");
            var components = new[] { component1, component2, component3 };
            var componentMap = new Dictionary<IViewKey, Queue<Component>>
            {
                { key1, new Queue<Component>(components) },
            };
            var layout = default(IViewLayout);

            var result = componentMap.GetPositions(layout)?.ToList();

            result.IsNotNull();
            result.Count().Is(components.Count());
            {
                var (resultComponent, resultVector) = result[0];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component1.name);
                resultVector.x.Is(0);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[1];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component2.name);
                resultVector.x.Is(0);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[2];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component3.name);
                resultVector.x.Is(0);
                resultVector.y.Is(0);
            }
        }
        [Test]
        public static void GetPositionsText_正常系_コンポーネントキュー複数_レイアウト有()
        {
            var name = nameof(GetPositionsText_正常系_コンポーネントキュー複数_レイアウト有);
            var key1 = ViewKeyMock.Generate(1);
            var key2 = ViewKeyMock.Generate(2);
            var component1 = MonoBehaviourMock.Generate($"{name}_1");
            var component2 = MonoBehaviourMock.Generate($"{name}_2");
            var component3 = MonoBehaviourMock.Generate($"{name}_3");
            var component4 = MonoBehaviourMock.Generate($"{name}_4");
            var components1 = new[] { component1, component2 };
            var components2 = new[] { component3, component4 };
            var componentMap = new Dictionary<IViewKey, Queue<Component>>
            {
                { key1, new Queue<Component>(components1) },
                { key2, new Queue<Component>(components2) },
            };
            var layout = ViewLayoutMock.Generate(getPositionMap);

            var result = componentMap.GetPositions(layout)?.ToList();

            result.IsNotNull();
            result.Count().Is(components1.Count() + components2.Count());
            {
                var (resultComponent, resultVector) = result[0];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component1.name);
                resultVector.x.Is(key1.value);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[1];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component2.name);
                resultVector.x.Is(key1.value);
                resultVector.y.Is(1);
            }
            {
                var (resultComponent, resultVector) = result[2];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component3.name);
                resultVector.x.Is(key2.value);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[3];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component4.name);
                resultVector.x.Is(key2.value);
                resultVector.y.Is(1);
            }
        }
        [Test]
        public static void GetPositionsText_正常系_コンポーネントキュー複数_レイアウト無()
        {
            var name = nameof(GetPositionsText_正常系_コンポーネントキュー複数_レイアウト有);
            var key1 = ViewKeyMock.Generate(1);
            var key2 = ViewKeyMock.Generate(2);
            var component1 = MonoBehaviourMock.Generate($"{name}_1");
            var component2 = MonoBehaviourMock.Generate($"{name}_2");
            var component3 = MonoBehaviourMock.Generate($"{name}_3");
            var component4 = MonoBehaviourMock.Generate($"{name}_4");
            var components1 = new[] { component1, component2 };
            var components2 = new[] { component3, component4 };
            var componentMap = new Dictionary<IViewKey, Queue<Component>>
            {
                { key1, new Queue<Component>(components1) },
                { key2, new Queue<Component>(components2) },
            };
            var layout = default(IViewLayout);

            var result = componentMap.GetPositions(layout)?.ToList();

            result.IsNotNull();
            result.Count().Is(components1.Count() + components2.Count());
            {
                var (resultComponent, resultVector) = result[0];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component1.name);
                resultVector.x.Is(0);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[1];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component2.name);
                resultVector.x.Is(0);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[2];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component3.name);
                resultVector.x.Is(0);
                resultVector.y.Is(0);
            }
            {
                var (resultComponent, resultVector) = result[3];
                resultComponent.IsNotNull();
                resultComponent.name.Is(component4.name);
                resultVector.x.Is(0);
                resultVector.y.Is(0);
            }
        }
        [Test]
        public static void GetPositionsText_正常系_コンポーネントキュー空_レイアウト有()
        {
            var componentMap = new Dictionary<IViewKey, Queue<Component>>();
            var layout = ViewLayoutMock.Generate(getPositionMap);

            var result = componentMap.GetPositions(layout)?.ToList();

            result.IsNotNull();
            result.Count().Is(0);
        }
        [Test]
        public static void GetPositionsText_正常系_コンポーネントキュー空_レイアウト無()
        {
            var componentMap = new Dictionary<IViewKey, Queue<Component>>();
            var layout = default(IViewLayout);

            var result = componentMap.GetPositions(layout)?.ToList();

            result.IsNotNull();
            result.Count().Is(0);
        }
        [Test]
        public static void GetPositionsText_異常系_コンポーネントキューNull_レイアウト有()
        {
            var componentMap = (Dictionary<IViewKey, Queue<Component>>)null;
            var layout = ViewLayoutMock.Generate(getPositionMap);

            Assert.Throws<ArgumentNullException>(() => componentMap.GetPositions(layout));
        }
    }
}
