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
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理無し()
        {
            var state = ViewStateAbstMock.Generate();
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values, out var resultViews);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(values.Length);
            resultState.views.ToArray()[0].Is(resultViews.ToArray()[0]);
            resultState.views.ToArray()[1].Is(resultViews.ToArray()[1]);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(values.Length);
                resultAction.actors.ToArray()[0].Is(resultViews.ToArray()[0]);
                resultAction.actors.ToArray()[1].Is(resultViews.ToArray()[1]);
            }

            resultViews.IsNotNull();
            resultViews.Count().Is(values.Length);
            {
                var resultView = resultViews.ToArray()[0];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[0]);
            }
            {
                var resultView = resultViews.ToArray()[1];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[1]);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品無し_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.ActionType.MOVE);
            var state = ViewStateAbstMock.Generate(null, new[] { actionOrigin });
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values, out var resultViews);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(values.Length);
            resultState.views.ToArray()[0].Is(resultViews.ToArray()[0]);
            resultState.views.ToArray()[1].Is(resultViews.ToArray()[1]);
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
                resultAction.actors.ToArray()[0].Is(resultViews.ToArray()[0]);
                resultAction.actors.ToArray()[1].Is(resultViews.ToArray()[1]);
            }

            resultViews.IsNotNull();
            resultViews.Count().Is(values.Length);
            {
                var resultView = resultViews.ToArray()[0];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[0]);
            }
            {
                var resultView = resultViews.ToArray()[1];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[1]);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理無し()
        {
            var viewOrigin = new ViewStationery(ViewValueMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin });
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values, out var resultViews);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1 + values.Length);
            resultState.views.ToArray()[0].Is(viewOrigin);
            resultState.views.ToArray()[1].Is(resultViews.ToArray()[0]);
            resultState.views.ToArray()[2].Is(resultViews.ToArray()[1]);
            resultState.viewActionQueue.IsNotNull();
            resultState.viewActionQueue.Count.Is(1);
            {
                var resultAction = resultState.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.ActionType.GENERATE);
                resultAction.actors.IsNotNull();
                resultAction.actors.Count().Is(values.Length);
                resultAction.actors.ToArray()[0].Is(resultViews.ToArray()[0]);
                resultAction.actors.ToArray()[1].Is(resultViews.ToArray()[1]);
            }

            resultViews.IsNotNull();
            resultViews.Count().Is(values.Length);
            {
                var resultView = resultViews.ToArray()[0];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[0]);
            }
            {
                var resultView = resultViews.ToArray()[1];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[1]);
            }
        }
        [Test]
        public static void SetNewViewTest_正常系_複数追加_既存の表示部品有り_既存の表示処理有り()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.ActionType.MOVE);
            var viewOrigin = new ViewStationery(ViewValueMock.Generate(0));
            var state = ViewStateAbstMock.Generate(new[] { viewOrigin }, new[] { actionOrigin });
            var values = new[] { ViewValueMock.Generate(1), ViewValueMock.Generate(2) };

            var resultState = state.SetNewView(values, out var resultViews);

            resultState.IsNotNull();
            resultState.IsSameReferenceAs(state);
            resultState.views.IsNotNull();
            resultState.views.Count().Is(1 + values.Length);
            resultState.views.ToArray()[0].Is(viewOrigin);
            resultState.views.ToArray()[1].Is(resultViews.ToArray()[0]);
            resultState.views.ToArray()[2].Is(resultViews.ToArray()[1]);
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
                resultAction.actors.ToArray()[0].Is(resultViews.ToArray()[0]);
                resultAction.actors.ToArray()[1].Is(resultViews.ToArray()[1]);
            }

            resultViews.IsNotNull();
            resultViews.Count().Is(values.Length);
            {
                var resultView = resultViews.ToArray()[0];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[0]);
            }
            {
                var resultView = resultViews.ToArray()[1];
                resultView.IsNotNull();
                resultView.value.IsSameReferenceAs(values[1]);
            }
        }
    }
}
