using Assets.Src.Domain.Model.Entity;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Model.Abstract
{
    public static class ViewStateAbstTest
    {
        [Test]
        public static void AddViewStationerysTest_正常系_元のリストが空()
        {
            var viewState = ViewStateAbstMock.Generate();
            var viewStationery1 = new ViewEntity(ViewValueMock.Generate(1));
            var viewStationery2 = new ViewEntity(ViewValueMock.Generate(2));

            var result = viewState.AddViewStationerys(new[] { viewStationery1, viewStationery2 });

            result.IsNotNull();
            result.Count().Is(2);
            result.Contains(viewStationery1).IsTrue();
            result.Contains(viewStationery2).IsTrue();
        }
        [Test]
        public static void AddViewStationerysTest_正常系_元のリストが空ではない()
        {
            var viewStationery1 = new ViewEntity(ViewValueMock.Generate(1));
            var viewStationery2 = new ViewEntity(ViewValueMock.Generate(2));
            var viewState = ViewStateAbstMock.Generate(new[] { viewStationery1, viewStationery2 });

            var viewStationery3 = new ViewEntity(ViewValueMock.Generate(3));
            var viewStationery4 = new ViewEntity(ViewValueMock.Generate(4));

            var result = viewState.AddViewStationerys(new[] { viewStationery3, viewStationery4 });

            result.IsNotNull();
            result.Count().Is(4);
            result.Contains(viewStationery1).IsTrue();
            result.Contains(viewStationery2).IsTrue();
            result.Contains(viewStationery3).IsTrue();
            result.Contains(viewStationery4).IsTrue();
        }
        [Test]
        public static void AddViewStationerysTest_正常系_元のリストが空_加えるリストが空()
        {
            var viewState = ViewStateAbstMock.Generate();

            var result = viewState.AddViewStationerys(new ViewEntity[] { });

            result.IsNotNull();
            result.Count().Is(0);
        }
        [Test]
        public static void AddViewStationerysTest_正常系_元のリストが空ではない_加えるリストが空()
        {
            var viewStationery1 = new ViewEntity(ViewValueMock.Generate(1));
            var viewStationery2 = new ViewEntity(ViewValueMock.Generate(2));
            var viewState = ViewStateAbstMock.Generate(new[] { viewStationery1, viewStationery2 });

            var result = viewState.AddViewStationerys(new ViewEntity[] { });

            result.IsNotNull();
            result.Count().Is(2);
            result.Contains(viewStationery1).IsTrue();
            result.Contains(viewStationery2).IsTrue();
        }
        [Test]
        public static void AddViewStationerysTest_正常系_元のリストが空_加えるリストがNull()
        {
            var viewState = ViewStateAbstMock.Generate();

            var result = viewState.AddViewStationerys(null);

            result.IsNotNull();
            result.Count().Is(0);
        }
        [Test]
        public static void AddViewStationerysTest_正常系_元のリストが空ではない_加えるリストがNull()
        {
            var viewStationery1 = new ViewEntity(ViewValueMock.Generate(1));
            var viewStationery2 = new ViewEntity(ViewValueMock.Generate(2));
            var viewState = ViewStateAbstMock.Generate(new[] { viewStationery1, viewStationery2 });

            var result = viewState.AddViewStationerys(null);

            result.IsNotNull();
            result.Count().Is(2);
            result.Contains(viewStationery1).IsTrue();
            result.Contains(viewStationery2).IsTrue();
        }
    }
}
