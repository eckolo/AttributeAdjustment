﻿using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock;
using Assets.Src.View.Factory;
using Assets.Src.View.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var positionOrigin = new Vector2(-1, 3);
            var textUpdated = "textUpdated";
            var positionUpdated = new Vector2(3, -12);
            var key = new TextMeshStationeryValue(textOrigin, positionOrigin);

            var result = viewState.SetText(key).UpdateText(key, textUpdated, positionUpdated);
            result.IsNotNull();
            result.IsSameReferenceAs(viewState);

            var resultTextMesh = result.Search<TextMeshStationeryValue, TextMesh>(key);
            resultTextMesh.IsNotNull();
            resultTextMesh.name.Contains(textOrigin).IsTrue();
            resultTextMesh.text.Is(textUpdated);
            resultTextMesh.transform.position.x.Is((positionView + positionUpdated).x);
            resultTextMesh.transform.position.y.Is((positionView + positionUpdated).y);
            resultTextMesh.transform.localPosition.x.Is(positionUpdated.x);
            resultTextMesh.transform.localPosition.y.Is(positionUpdated.y);
        }
        [Test]
        public static void UpdateText_正常系_対象有_文字更新有_位置更新無()
        {
            var viewState = ViewStateMock.Generate(nameof(UpdateText_正常系_対象有_文字更新有_位置更新無));
            var positionView = new Vector2(2, -5);
            viewState.transform.position = positionView;
            var textOrigin = "textOrigin";
            var positionOrigin = new Vector2(-1, 3);
            var textUpdated = "textUpdated";
            var key = new TextMeshStationeryValue(textOrigin, positionOrigin);

            var result = viewState.SetText(key).UpdateText(key, textUpdated);
            result.IsNotNull();
            result.IsSameReferenceAs(viewState);

            var resultTextMesh = result.Search<TextMeshStationeryValue, TextMesh>(key);
            resultTextMesh.IsNotNull();
            resultTextMesh.name.Contains(textOrigin).IsTrue();
            resultTextMesh.text.Is(textUpdated);
            resultTextMesh.transform.position.x.Is((positionView + positionOrigin).x);
            resultTextMesh.transform.position.y.Is((positionView + positionOrigin).y);
            resultTextMesh.transform.localPosition.x.Is(positionOrigin.x);
            resultTextMesh.transform.localPosition.y.Is(positionOrigin.y);
        }
        [Test]
        public static void UpdateText_正常系_対象有_文字更新無_位置更新有()
        {
            var viewState = ViewStateMock.Generate(nameof(UpdateText_正常系_対象有_文字更新無_位置更新有));
            var positionView = new Vector2(2, -5);
            viewState.transform.position = positionView;
            var textOrigin = "textOrigin";
            var positionOrigin = new Vector2(-1, 3);
            var positionUpdated = new Vector2(3, -12);
            var key = new TextMeshStationeryValue(textOrigin, positionOrigin);

            var result = viewState.SetText(key).UpdateText(key, positionUpdated);
            result.IsNotNull();
            result.IsSameReferenceAs(viewState);

            var resultTextMesh = result.Search<TextMeshStationeryValue, TextMesh>(key);
            resultTextMesh.IsNotNull();
            resultTextMesh.name.Contains(textOrigin).IsTrue();
            resultTextMesh.text.Is(textOrigin);
            resultTextMesh.transform.position.x.Is((positionView + positionUpdated).x);
            resultTextMesh.transform.position.y.Is((positionView + positionUpdated).y);
            resultTextMesh.transform.localPosition.x.Is(positionUpdated.x);
            resultTextMesh.transform.localPosition.y.Is(positionUpdated.y);
        }
        [Test]
        public static void UpdateText_正常系_対象無()
        {
            var viewState = ViewStateMock.Generate(nameof(UpdateText_正常系_対象無));
            var positionView = new Vector2(2, -5);
            viewState.transform.position = positionView;
            var textOrigin = "textOrigin";
            var positionOrigin = new Vector2(-1, 3);
            var textUpdated = "textUpdated";
            var positionUpdated = new Vector2(3, -12);
            var key = new TextMeshStationeryValue(textOrigin, positionOrigin);

            var result = viewState.UpdateText(key, textUpdated, positionUpdated);
            result.IsNotNull();
            result.IsSameReferenceAs(viewState);

            var resultTextMesh = result.Search<TextMeshStationeryValue, TextMesh>(key);
            resultTextMesh.IsNull();
        }
        [Test]
        public static void UpdateText_異常系_状態がNull()
        {
            var viewState = (ViewStateMock)null;
            var textOrigin = "textOrigin";
            var positionOrigin = new Vector2(-1, 3);
            var textUpdated = "textUpdated";
            var positionUpdated = new Vector2(3, -12);
            var key = new TextMeshStationeryValue(textOrigin, positionOrigin);

            Assert.Throws<ArgumentNullException>(() => viewState.UpdateText(key, textUpdated, positionUpdated));
        }
    }
}
