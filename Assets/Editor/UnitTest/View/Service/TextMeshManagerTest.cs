using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.Mock.Model.Value;
using Assets.Src.View.Factory;
using Assets.Src.View.Service;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Editor.UnitTest.View.Service
{
    public class TextMeshManagerTest
    {
        [Test]
        public static void UpdateText_正常系_対象有_文字更新有_位置更新有()
        {
            var viewState = ViewStateMock.Generate(nameof(UpdateText_正常系_対象有_文字更新有_位置更新有));
            var positionView = new Vector2(2, -5);
            viewState.transform.position = positionView;
            var textOrigin = "textOrigin";
            var textUpdated = "textUpdated";
            var positionOrigin = new Vector2(-5, 6);
            var positionUpdated = new Vector2(3, -12);
            var development = new ViewDeployment(positionOrigin);
            var targetDeploy = new ViewDeployment(positionUpdated);
            var key = TextMeshKeyMock.Generate(textOrigin);

            var result = viewState
                .SetText(development, key)
                .UpdateText(development, key, targetDeploy, textUpdated);
            result.IsNotNull();
            result.IsSameReferenceAs(viewState);
            result.Search<TextMesh>(development, key).IsNull();

            var resultTextMesh = result.Search<TextMesh>(targetDeploy, key);
            resultTextMesh.IsNotNull();
            resultTextMesh.name.Contains(textOrigin).IsTrue();
            resultTextMesh.text.Is(textUpdated);
            resultTextMesh.transform.position.x.Is((positionView + positionOrigin).x);
            resultTextMesh.transform.position.y.Is((positionView + positionOrigin).y);
        }
        [Test]
        public static void UpdateText_正常系_対象無()
        {
            var viewState = ViewStateMock.Generate(nameof(UpdateText_正常系_対象無));
            var positionView = new Vector2(2, -5);
            viewState.transform.position = positionView;
            var textOrigin = "textOrigin";
            var textUpdated = "textUpdated";
            var positionOrigin = new Vector2(-5, 6);
            var positionUpdated = new Vector2(3, -12);
            var development = new ViewDeployment(positionOrigin);
            var targetDeploy = new ViewDeployment(positionUpdated);
            var key = TextMeshKeyMock.Generate(textOrigin);

            var result = viewState.UpdateText(development, key, targetDeploy, textUpdated);
            result.IsNotNull();
            result.IsSameReferenceAs(viewState);

            var resultTextMesh = result.Search<TextMesh>(development, key);
            resultTextMesh.IsNull();
        }
        [Test]
        public static void UpdateText_異常系_状態がNull()
        {
            var viewState = (ViewStateMock)null;
            var textOrigin = "textOrigin";
            var textUpdated = "textUpdated";
            var positionOrigin = new Vector2(-5, 6);
            var positionUpdated = new Vector2(3, -12);
            var development = new ViewDeployment(positionOrigin);
            var targetDeploy = new ViewDeployment(positionUpdated);
            var key = TextMeshKeyMock.Generate(textOrigin);

            Assert.Throws<ArgumentNullException>(() => viewState.UpdateText(development, key, targetDeploy, textUpdated));
        }
    }
}
