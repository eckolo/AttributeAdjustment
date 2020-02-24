using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Factory;
using Assets.Src.Mock.Model.Value;
using Assets.Src.Mock.Model.Entity;
using NUnit.Framework;
using System.Linq;
using Assets.Src.Domain.Repository;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Factory
{
    /// <summary>
    /// <see cref="ViewActionFactory"/>のテスト
    /// </summary>
    public static class ViewActionFactoryTest
    {
        static readonly ViewDeployment deployment = new ViewDeployment();

        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateKeyMock.Generate();
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(deployment, value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品無し_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(deployment, value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(2);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品有り_既存の表示処理無し()
        {
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(deployment, value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品有り_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(deployment, value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(2);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(deployment, values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(deployment, values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理無し()
        {
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(deployment, values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(deployment, values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }

        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理無し_生成()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理有り_生成()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品有り_既存の表示処理無し_生成()
        {
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品有り_既存の表示処理有り_生成()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理無し_更新()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理有り_更新()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品有り_既存の表示処理無し_更新()
        {
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品有り_既存の表示処理有り_更新()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理無し_削除()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理有り_削除()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品有り_既存の表示処理無し_削除()
        {
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品有り_既存の表示処理有り_削除()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(deployment, values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actorDeployment.Is(deployment);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }

        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理無し_場札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var state = ViewStateKeyMock.Generate();
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理有り_場札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理無し_場札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理有り_場札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理無し_山札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var state = ViewStateKeyMock.Generate();
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理有り_山札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理無し_山札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理有り_山札対象_イージング指定無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理無し_場札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var state = ViewStateKeyMock.Generate();
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理有り_場札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理無し_場札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理有り_場札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理無し_山札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var state = ViewStateKeyMock.Generate();
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品無し_既存の表示処理有り_山札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理無し_山札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
        [Test]
        public static void SetTipMovingTest_正常系_既存の表示部品有り_既存の表示処理有り_山札対象_イージング指定有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.PIERCING, 357);
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE);
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { tip1, tip2 };
            var originDestination = MotionTip.Destination.DECK;
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, originDestination, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(actionOrigin.actorDeployment);
                resultAction.actor.IsNull();
                resultAction.targetDeployment.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip1);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip1);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actorDeployment.Is(originDestination.GetCenterPosition());
                resultAction.actor.Is(tip2);
                resultAction.targetDeployment.Is(actionTarget.GetCenterPosition());
                resultAction.target.Is(tip2);
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
    }
}
