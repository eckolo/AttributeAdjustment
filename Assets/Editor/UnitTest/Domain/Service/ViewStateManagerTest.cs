using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="ViewStateManager"/>サービスのテスト
    /// </summary>
    public static class ViewStateManagerTest
    {
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateAbstMock.Generate();
            var value = ViewValueMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].value.Is(value);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(1);
                resultAction.actors.ToArray()[0].value.Is(value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品無し_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.ActionType.MOVE);
            var state = ViewStateAbstMock.Generate(null, new[] { actionOrigin });
            var value = ViewValueMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].value.Is(value);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.MOVE);
                resultAction.actors.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(1);
                resultAction.actors.ToArray()[0].value.Is(value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品有り_既存の表示処理無し()
        {
            var viewOrigin = new ViewEntity(ViewValueMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin });
            var value = ViewValueMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1 + 1);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].Is(viewOrigin);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.views.ToArray()[1].IsNotNull();
            resultState.views.ToArray()[1].value.Is(value);
            resultState.views.ToArray()[1].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(1);
                resultAction.actors.ToArray()[0].value.Is(value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品有り_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.ActionType.MOVE);
            var viewOrigin = new ViewEntity(ViewValueMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var value = ViewValueMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1 + 1);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].Is(viewOrigin);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.views.ToArray()[1].IsNotNull();
            resultState.views.ToArray()[1].value.Is(value);
            resultState.views.ToArray()[1].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.MOVE);
                resultAction.actors.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(1);
                resultAction.actors.ToArray()[0].value.Is(value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateAbstMock.Generate();
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(values.Length);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].value.Is(values[0]);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.views.ToArray()[1].IsNotNull();
            resultState.views.ToArray()[1].value.Is(values[1]);
            resultState.views.ToArray()[1].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(values.Length);
                resultAction.actors.ToArray()[0].value.Is(values[0]);
                resultAction.actors.ToArray()[1].value.Is(values[1]);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.ActionType.MOVE);
            var state = ViewStateAbstMock.Generate(null, new[] { actionOrigin });
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(values.Length);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].value.Is(values[0]);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.views.ToArray()[1].IsNotNull();
            resultState.views.ToArray()[1].value.Is(values[1]);
            resultState.views.ToArray()[1].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.MOVE);
                resultAction.actors.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(values.Length);
                resultAction.actors.ToArray()[0].value.Is(values[0]);
                resultAction.actors.ToArray()[1].value.Is(values[1]);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理無し()
        {
            var viewOrigin = new ViewEntity(ViewValueMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin });
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1 + values.Length);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].Is(viewOrigin);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.views.ToArray()[1].IsNotNull();
            resultState.views.ToArray()[1].value.Is(values[0]);
            resultState.views.ToArray()[1].parent.Is(state);
            resultState.views.ToArray()[2].IsNotNull();
            resultState.views.ToArray()[2].value.Is(values[1]);
            resultState.views.ToArray()[2].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(values.Length);
                resultAction.actors.ToArray()[0].value.Is(values[0]);
                resultAction.actors.ToArray()[1].value.Is(values[1]);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.ActionType.MOVE);
            var viewOrigin = new ViewEntity(ViewValueMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1 + values.Length);
            resultState.views.ToArray()[0].IsNotNull();
            resultState.views.ToArray()[0].Is(viewOrigin);
            resultState.views.ToArray()[0].parent.Is(state);
            resultState.views.ToArray()[1].IsNotNull();
            resultState.views.ToArray()[1].value.Is(values[0]);
            resultState.views.ToArray()[1].parent.Is(state);
            resultState.views.ToArray()[2].IsNotNull();
            resultState.views.ToArray()[2].value.Is(values[1]);
            resultState.views.ToArray()[2].parent.Is(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.MOVE);
                resultAction.actors.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(values.Length);
                resultAction.actors.ToArray()[0].value.Is(values[0]);
                resultAction.actors.ToArray()[1].value.Is(values[1]);
            }
        }

        [Test]
        public static void MoveView_正常系_移動元未指定_既存の表示処理無し()
        {
            var valuesMoved = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2), ViewValueMock.Generate(1) };
            var values = new[] {
                ViewValueMock.Generate(3),
                ViewValueMock.Generate(2),
                ViewValueMock.Generate(1),
                ViewValueMock.Generate(2),
                ViewValueMock.Generate(1),
                ViewValueMock.Generate(1)
            };
            var toView = new ViewEntity(ViewValueMock.Generate(10));
            var entities = values.Select(value => new ViewEntity(value)).Concat(new[] { toView }).ToArray();
            var state = ViewStateAbstMock.Generate(entities);

            var resultState = state.MoveView(valuesMoved, toView);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(7);
            resultState.views.All(view => view != null).IsTrue();
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(1)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(2)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(3)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(10)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(1)))
                .Where(view => view.parent.Equals(toView))
                .Count().Is(2);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(2)))
                .Where(view => view.parent.Equals(toView))
                .Count().Is(1);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.MOVE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(3);
                resultAction.actors.Count(view => view.value.Equals(ViewValueMock.Generate(1))).Is(2);
                resultAction.actors.Count(view => view.value.Equals(ViewValueMock.Generate(2))).Is(1);
            }
        }
        [Test]
        public static void MoveView_正常系_移動元指定_既存の表示処理無し()
        {
            var valuesMoved = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2), ViewValueMock.Generate(1) };
            var values = new[] {
                ViewValueMock.Generate(3),
                ViewValueMock.Generate(2),
                ViewValueMock.Generate(1),
                ViewValueMock.Generate(2),
                ViewValueMock.Generate(1),
                ViewValueMock.Generate(1)
            };
            var toView = new ViewEntity(ViewValueMock.Generate(10));
            var fromView = new ViewEntity(ViewValueMock.Generate(11));
            var entities = values.Select(value => new ViewEntity(value)).Concat(new[] { toView, fromView }).ToArray();
            var state = ViewStateAbstMock.Generate(entities);
            entities[2] = entities[2].SetParent(fromView);
            entities[3] = entities[3].SetParent(fromView);
            entities[4] = entities[4].SetParent(fromView);
            entities[5] = entities[5].SetParent(fromView);

            var resultState = state.MoveView(valuesMoved, toView, fromView);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(8);
            resultState.views.All(view => view != null).IsTrue();
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(2)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(3)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(10)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(11)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(1)))
                .Where(view => view.parent.Equals(toView))
                .Count().Is(2);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(2)))
                .Where(view => view.parent.Equals(toView))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(1)))
                .Where(view => view.parent.Equals(fromView))
                .Count().Is(1);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.MOVE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(3);
                resultAction.actors.Count(view => view.value.Equals(ViewValueMock.Generate(1))).Is(2);
                resultAction.actors.Count(view => view.value.Equals(ViewValueMock.Generate(2))).Is(1);
            }
        }
        [Test]
        public static void MoveView_正常系_移動元未指定_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.ActionType.DELETE);
            var valuesMoved = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2), ViewValueMock.Generate(1) };
            var values = new[] {
                ViewValueMock.Generate(3),
                ViewValueMock.Generate(2),
                ViewValueMock.Generate(1),
                ViewValueMock.Generate(2),
                ViewValueMock.Generate(1),
                ViewValueMock.Generate(1)
            };
            var toView = new ViewEntity(ViewValueMock.Generate(10));
            var entities = values.Select(value => new ViewEntity(value)).Concat(new[] { toView }).ToArray();
            var state = ViewStateAbstMock.Generate(entities, new[] { actionOrigin });

            var resultState = state.MoveView(valuesMoved, toView);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(7);
            resultState.views.All(view => view != null).IsTrue();
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(1)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(2)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(3)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(10)))
                .Where(view => view.parent.Equals(state))
                .Count().Is(1);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(1)))
                .Where(view => view.parent.Equals(toView))
                .Count().Is(2);
            resultState.views
                .Where(view => view.value.Equals(ViewValueMock.Generate(2)))
                .Where(view => view.parent.Equals(toView))
                .Count().Is(1);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.DELETE);
                resultAction.actors.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.MOVE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(3);
                resultAction.actors.Count(view => view.value.Equals(ViewValueMock.Generate(1))).Is(2);
                resultAction.actors.Count(view => view.value.Equals(ViewValueMock.Generate(2))).Is(1);
            }
        }
    }
}
