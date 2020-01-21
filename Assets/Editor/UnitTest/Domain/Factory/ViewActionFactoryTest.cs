using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Factory;
using Assets.Src.Mock.Model.Value;
using Assets.Src.Mock.Model.Abstract;
using NUnit.Framework;
using System.Linq;
using Assets.Src.Domain.Repository;

namespace Assets.Editor.UnitTest.Domain.Factory
{
    /// <summary>
    /// <see cref="ViewActionFactory"/>のテスト
    /// </summary>
    public static class ViewActionFactoryTest
    {
        [Test]
        public static void SetNewViewTest_正常系_単数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateKeyMock.Generate();
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
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
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(2);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
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

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
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
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var value = IViewKeyMock.Generate(1);

            var resultState = state.SetNewView(value);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(2);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(value.value);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
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
            var state = ViewStateKeyMock.Generate(null, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
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

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
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
            var viewOrigin = IViewKeyMock.Generate(0);
            var state = ViewStateKeyMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetNewView(values);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }

        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理無し_生成()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.GENERATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理無し_更新()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.UPDATE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[1].value);
            }
        }
        [Test]
        public static void SetViewActionsTest_正常系_既存の表示部品無し_既存の表示処理無し_削除()
        {
            var state = ViewStateKeyMock.Generate();
            var values = new[] { IViewKeyMock.Generate(1), IViewKeyMock.Generate(2) };

            var resultState = state.SetViewActions(values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
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

            var resultState = state.SetViewActions(values, ViewAction.Pattern.DELETE);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
                resultAction.actor.IsNotNull();
                ((IViewKeyMock)resultAction.actor).value.Is(values[0].value);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.DELETE);
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
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.BOARD;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;

            var resultState = state.SetTipMoving(values, actionTarget);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(Easing.Pattern.Quadratic);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.BOARD;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
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
            var actionTarget = MotionTip.Destination.DECK;
            var easing = Easing.Pattern.Cubic;

            var resultState = state.SetTipMoving(values, actionTarget, easing);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.viewActionList.IsNotNull();
            resultState.viewActionList.Count.Is(values.Length + 1);
            {
                var resultAction = resultState.viewActionList.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNull();
                resultAction.target.IsNull();
                resultAction.easing.IsNull();
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip1.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip1.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip1.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
            {
                var resultAction = resultState.viewActionList.ToArray()[2];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.UPDATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.hashCode.Is(tip2.hashCode);
                resultAction.target.IsNotNull();
                ((MotionTip)resultAction.target).energy.Is(tip2.energy);
                ((MotionTip)resultAction.target).energyValue.Is(tip2.energyValue);
                ((MotionTip)resultAction.target).position.Is(actionTarget.GetCenterPosition());
                resultAction.easing.IsNotNull();
                resultAction.easing.pattern.Is(easing);
                resultAction.easing.timeCoefficient.Is(1);
            }
        }
    }
}
