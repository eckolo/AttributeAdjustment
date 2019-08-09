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
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品無し_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateAbstMock.Generate(null, new[] { actionOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品有り_既存の表示処理無し()
        {
            var viewOrigin = new ViewEntity(IViewKeyMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品有り_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = new ViewEntity(IViewKeyMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateAbstMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateAbstMock.Generate(null, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理無し()
        {
            var viewOrigin = new ViewEntity(IViewKeyMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = new ViewEntity(IViewKeyMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }

        [Test]
        public static void MoveView_正常系_移動元未指定_既存の表示処理無し()
        {
            var valuesMoved = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2), IViewKeyMock.Generate(1) };
            var values = new[] {
                IViewKeyMock.Generate(3),
                IViewKeyMock.Generate(2),
                IViewKeyMock.Generate(1),
                IViewKeyMock.Generate(2),
                IViewKeyMock.Generate(1),
                IViewKeyMock.Generate(1)
            };
            var toView = new ViewEntity(IViewKeyMock.Generate(10));
            var entities = values.Select(value => new ViewEntity(value)).Concat(new[] { toView }).ToArray();
            var state = ViewStateAbstMock.Generate(entities);

            var resultState = state.MoveView(valuesMoved, toView);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(valuesMoved.Length);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[1].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[2].value);
            }
        }
        [Test]
        public static void MoveView_正常系_移動元指定_既存の表示処理無し()
        {
            var valuesMoved = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2), IViewKeyMock.Generate(1) };
            var values = new[] {
                IViewKeyMock.Generate(3),
                IViewKeyMock.Generate(2),
                IViewKeyMock.Generate(1),
                IViewKeyMock.Generate(2),
                IViewKeyMock.Generate(1),
                IViewKeyMock.Generate(1)
            };
            var toView = new ViewEntity(IViewKeyMock.Generate(10));
            var fromView = new ViewEntity(IViewKeyMock.Generate(11));
            var entities = values.Select(value => new ViewEntity(value)).Concat(new[] { toView, fromView }).ToArray();
            var state = ViewStateAbstMock.Generate(entities);
            entities[2] = entities[2].SetParent(fromView);
            entities[3] = entities[3].SetParent(fromView);
            entities[4] = entities[4].SetParent(fromView);
            entities[5] = entities[5].SetParent(fromView);

            var resultState = state.MoveView(valuesMoved, toView, fromView);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(valuesMoved.Length * 2);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[1].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[2].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[3];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[4];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[1].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[5];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[2].value);
            }
        }
        [Test]
        public static void MoveView_正常系_移動元未指定_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE);
            var valuesMoved = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2), IViewKeyMock.Generate(1) };
            var values = new[] {
                IViewKeyMock.Generate(3),
                IViewKeyMock.Generate(2),
                IViewKeyMock.Generate(1),
                IViewKeyMock.Generate(2),
                IViewKeyMock.Generate(1),
                IViewKeyMock.Generate(1)
            };
            var toView = new ViewEntity(IViewKeyMock.Generate(10));
            var entities = values.Select(value => new ViewEntity(value)).Concat(new[] { toView }).ToArray();
            var state = ViewStateAbstMock.Generate(entities, new[] { actionOrigin });

            var resultState = state.MoveView(valuesMoved, toView);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(valuesMoved.Length + 1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[0].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[1].value);
            }
            {
                var resultAction = resultState.viewActionQueue.ToArray()[3];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(valuesMoved[2].value);
            }
        }
    }
}
