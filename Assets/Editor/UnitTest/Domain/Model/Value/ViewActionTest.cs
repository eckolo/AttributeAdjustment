using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Value;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    /// <summary>
    /// <see cref="ViewAction"/>組み込み処理のテスト
    /// </summary>
    public static class ViewActionTest
    {
        [Test]
        public static void AddNextActionTest_正常系_単一処理追加()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.GENERATE, default);
            var actionAdded = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE, default);

            var result = actionOrigin.AddNextAction<ViewActionMock>(actionAdded);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded);
            result.nextAction.nextAction.IsNull();
        }
        [Test]
        public static void AddNextActionTest_正常系_複数処理追加_連続()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE, default);
            var actionAdded1 = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE, default);
            var actionAdded2 = ViewActionMock.GenerateMock(ViewAction.Pattern.GENERATE, default);

            var result = actionOrigin
                .AddNextAction<ViewActionMock>(actionAdded1)
                .AddNextAction<ViewActionMock>(actionAdded2);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded1);
            result.nextAction.nextAction.IsNotNull();
            result.nextAction.nextAction.IsSameReferenceAs(actionAdded2);
            result.nextAction.nextAction.nextAction.IsNull();
        }
        [Test]
        public static void AddNextActionTest_正常系_複数処理追加_断続()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE, default);
            var actionAdded1 = ViewActionMock.GenerateMock(ViewAction.Pattern.GENERATE, default);
            var actionAdded2 = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE, default);

            var actionOriginAdded = actionOrigin.AddNextAction<ViewActionMock>(actionAdded1);
            var result = actionOriginAdded.AddNextAction<ViewActionMock>(actionAdded2);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.IsSameReferenceAs(actionOriginAdded);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded1);
            result.nextAction.nextAction.IsNotNull();
            result.nextAction.nextAction.IsSameReferenceAs(actionAdded2);
            result.nextAction.nextAction.nextAction.IsNull();
        }
        [Test]
        public static void AddNextActionTest_正常系_複数処理追加_入れ子()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE, default);
            var actionAdded1 = ViewActionMock.GenerateMock(ViewAction.Pattern.GENERATE, default);
            var actionAdded2 = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE, default);

            var actionAddedAdded = actionAdded1.AddNextAction<ViewActionMock>(actionAdded2);
            var result = actionOrigin.AddNextAction<ViewActionMock>(actionAddedAdded);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded1);
            result.nextAction.IsSameReferenceAs(actionAddedAdded);
            result.nextAction.nextAction.IsNotNull();
            result.nextAction.nextAction.IsSameReferenceAs(actionAdded2);
            result.nextAction.nextAction.nextAction.IsNull();
        }

        [Test]
        public static void AddNextActionTest_異常系_追加される処理がNull_単一処理追加()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.GENERATE, default);
            var actionNull = (ViewActionMock)null;

            var result = actionOrigin.AddNextAction<ViewActionMock>(actionNull);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.nextAction.IsNull();
        }
        [Test]
        public static void AddNextActionTest_異常系_追加される処理がNull_複数処理追加_連続()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE, default);
            var actionAdded = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE, default);
            var actionNull = (ViewActionMock)null;

            var result = actionOrigin
                .AddNextAction<ViewActionMock>(actionAdded)
                .AddNextAction<ViewActionMock>(actionNull);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded);
            result.nextAction.nextAction.IsNull();
        }
        [Test]
        public static void AddNextActionTest_異常系_追加される処理がNull_複数処理追加_断続()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE, default);
            var actionAdded = ViewActionMock.GenerateMock(ViewAction.Pattern.GENERATE, default);
            var actionNull = (ViewActionMock)null;

            var actionOriginAdded = actionOrigin.AddNextAction<ViewActionMock>(actionAdded);
            var result = actionOriginAdded.AddNextAction<ViewActionMock>(actionNull);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.IsSameReferenceAs(actionOriginAdded);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded);
            result.nextAction.nextAction.IsNull();
        }
        [Test]
        public static void AddNextActionTest_異常系_追加される処理がNull_複数処理追加_入れ子()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE, default);
            var actionAdded1 = ViewActionMock.GenerateMock(ViewAction.Pattern.GENERATE, default);
            var actionNull = (ViewActionMock)null;

            var actionAddedAdded = actionAdded1.AddNextAction<ViewActionMock>(actionNull);
            var result = actionOrigin.AddNextAction<ViewActionMock>(actionAddedAdded);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded1);
            result.nextAction.IsSameReferenceAs(actionAddedAdded);
            result.nextAction.nextAction.IsNull();
        }

        [Test]
        public static void AddNextActionTest_異常系_参照ループ()
        {
            var actionOrigin = ViewActionMock.GenerateMock(ViewAction.Pattern.DELETE, default);
            var actionAdded = ViewActionMock.GenerateMock(ViewAction.Pattern.UPDATE, default);

            var result = actionOrigin
                .AddNextAction<ViewActionMock>(actionAdded)
                .AddNextAction<ViewActionMock>(actionOrigin);

            result.IsNotNull();
            result.IsSameReferenceAs(actionOrigin);
            result.nextAction.IsNotNull();
            result.nextAction.IsSameReferenceAs(actionAdded);
            result.nextAction.nextAction.IsNotNull();
            result.nextAction.nextAction.IsSameReferenceAs(actionOrigin);
            result.nextAction.nextAction.nextAction.IsNotNull();
        }
    }
}
