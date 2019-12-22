using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Factory
{
    /// <summary>
    /// <see cref="ChoiceStateFactory"/>のテスト
    /// </summary>
    public static class ChoiceStateFactoryTest
    {
        [Test]
        public static void ToChoiceStateTest_正常系_初期値指定無し()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };

            var result = choiceList.ToChoiceState();

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(0);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.Count.Is(2);
            {
                var resultAction = result.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<ChoiceState>();
                resultAction.actor.IsSameReferenceAs(result);
            }
            {
                var resultAction = result.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<TextMeshKey>();
                if(resultAction.actor is TextMeshKey resultText)
                {
                    resultText.text.Is($">\t{text1}\r\n\t{text2}\r\n\t{text3}");
                    resultText.position.x.Is(Vector2.zero.x);
                    resultText.position.y.Is(Vector2.zero.y);
                }
            }
        }
        [Test]
        public static void ToChoiceStateTest_正常系_初期値指定有り_選択肢範囲内()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var initialChoiced = choiceList.Count - 1;

            var result = choiceList.ToChoiceState(initialChoiced);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(initialChoiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.Count.Is(2);
            {
                var resultAction = result.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<ChoiceState>();
                resultAction.actor.IsSameReferenceAs(result);
            }
            {
                var resultAction = result.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<TextMeshKey>();
                if(resultAction.actor is TextMeshKey resultText)
                {
                    resultText.text.Is($"\t{text1}\r\n\t{text2}\r\n>\t{text3}");
                    resultText.position.x.Is(Vector2.zero.x);
                    resultText.position.y.Is(Vector2.zero.y);
                }
            }
        }
        [Test]
        public static void ToChoiceStateTest_正常系_初期値指定有り_選択肢範囲外_超過()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var initialChoiced = choiceList.Count;

            var result = choiceList.ToChoiceState(initialChoiced);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(0);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.Count.Is(2);
            {
                var resultAction = result.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<ChoiceState>();
                resultAction.actor.IsSameReferenceAs(result);
            }
            {
                var resultAction = result.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<TextMeshKey>();
                if(resultAction.actor is TextMeshKey resultText)
                {
                    resultText.text.Is($">\t{text1}\r\n\t{text2}\r\n\t{text3}");
                    resultText.position.x.Is(Vector2.zero.x);
                    resultText.position.y.Is(Vector2.zero.y);
                }
            }
        }
        [Test]
        public static void ToChoiceStateTest_正常系_初期値指定有り_選択肢範囲外_負の値()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var initialChoiced = -1;

            var result = choiceList.ToChoiceState(initialChoiced);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(0);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.Count.Is(2);
            {
                var resultAction = result.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<ChoiceState>();
                resultAction.actor.IsSameReferenceAs(result);
            }
            {
                var resultAction = result.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<TextMeshKey>();
                if(resultAction.actor is TextMeshKey resultText)
                {
                    resultText.text.Is($">\t{text1}\r\n\t{text2}\r\n\t{text3}");
                    resultText.position.x.Is(Vector2.zero.x);
                    resultText.position.y.Is(Vector2.zero.y);
                }
            }
        }
        [Test]
        public static void ToChoiceStateTest_正常系_選択肢リストが空()
        {
            var choiceList = new List<string> { };

            var result = choiceList.ToChoiceState();

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiced.Is(null);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.Count.Is(2);
            {
                var resultAction = result.viewActionQueue.ToArray()[0];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<ChoiceState>();
                resultAction.actor.IsSameReferenceAs(result);
            }
            {
                var resultAction = result.viewActionQueue.ToArray()[1];
                resultAction.IsNotNull();
                resultAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultAction.actor.IsNotNull();
                resultAction.actor.IsInstanceOf<TextMeshKey>();
                if(resultAction.actor is TextMeshKey resultText)
                {
                    resultText.text.Is($"");
                    resultText.position.x.Is(Vector2.zero.x);
                    resultText.position.y.Is(Vector2.zero.y);
                }
            }
        }
        [Test]
        public static void ToChoiceStateTest_異常系_選択肢リストがNull()
        {
            var choiceList = (List<string>)null;

            Assert.Throws<ArgumentNullException>(() => choiceList.ToChoiceState());
        }
    }
}
