using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Factory;
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
    }
}
