using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="ChoiceManager"/>クラスのテスト
    /// </summary>
    public static class ChoiceManagerTest
    {
        static readonly KeyConfigs keyConfigs = new KeyConfigs();

        [Test]
        public static void UpdateTest_正常系_上限_決定()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.decide;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_キャンセル()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.cancel;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.IsNull();
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_上押下_継続0()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + choiceList.Count - 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_上押下_継続有り()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.DOWN;
            var keepUpTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepUpTime)).SetValue(state, keepUpTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + choiceList.Count - 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_上押続け_継続規定値未満()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.ON;
            var keepUpTime = Constants.Choice.KEEP_VERTICAL_LIMIT - 1;
            typeof(ChoiceState).GetProperty(nameof(keepUpTime)).SetValue(state, keepUpTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(keepUpTime + 1);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_上押続け_継続規定値以上()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.ON;
            var keepUpTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepUpTime)).SetValue(state, keepUpTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + choiceList.Count - 1) % choiceList.Count);
            result.keepUpTime.Is(Constants.Choice.KEEP_VERTICAL_LIMIT - Constants.Choice.KEEP_VERTICAL_INTERVAL);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_下押下_継続0()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_下押下_継続有り()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.DOWN;
            var keepDownTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepDownTime)).SetValue(state, keepDownTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_下押続け_継続規定値未満()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.ON;
            var keepDownTime = Constants.Choice.KEEP_VERTICAL_LIMIT - 1;
            typeof(ChoiceState).GetProperty(nameof(keepDownTime)).SetValue(state, keepDownTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(keepDownTime + 1);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_下押続け_継続規定値以上()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.ON;
            var keepDownTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepDownTime)).SetValue(state, keepDownTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(Constants.Choice.KEEP_VERTICAL_LIMIT - Constants.Choice.KEEP_VERTICAL_INTERVAL);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_上限_同時押し()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.decide
                .Concat(keyConfigs.cancel)
                .Concat(keyConfigs.ups)
                .Concat(keyConfigs.downs);
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(null);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }

        [Test]
        public static void UpdateTest_正常系_下限_決定()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.decide;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_キャンセル()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.cancel;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.IsNull();
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_上押下_継続0()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + choiceList.Count - 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_上押下_継続有り()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.DOWN;
            var keepUpTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepUpTime)).SetValue(state, keepUpTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + choiceList.Count - 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_上押続け_継続規定値未満()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.ON;
            var keepUpTime = Constants.Choice.KEEP_VERTICAL_LIMIT - 1;
            typeof(ChoiceState).GetProperty(nameof(keepUpTime)).SetValue(state, keepUpTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(keepUpTime + 1);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_上押続け_継続規定値以上()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.ups;
            var keyTiming = KeyTiming.ON;
            var keepUpTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepUpTime)).SetValue(state, keepUpTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + choiceList.Count - 1) % choiceList.Count);
            result.keepUpTime.Is(Constants.Choice.KEEP_VERTICAL_LIMIT - Constants.Choice.KEEP_VERTICAL_INTERVAL);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_下押下_継続0()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_下押下_継続有り()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.DOWN;
            var keepDownTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepDownTime)).SetValue(state, keepDownTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_下押続け_継続規定値未満()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.ON;
            var keepDownTime = Constants.Choice.KEEP_VERTICAL_LIMIT - 1;
            typeof(ChoiceState).GetProperty(nameof(keepDownTime)).SetValue(state, keepDownTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(keepDownTime + 1);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_下押続け_継続規定値以上()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.downs;
            var keyTiming = KeyTiming.ON;
            var keepDownTime = Constants.Choice.KEEP_VERTICAL_LIMIT;
            typeof(ChoiceState).GetProperty(nameof(keepDownTime)).SetValue(state, keepDownTime);

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is((choiced + 1) % choiceList.Count);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(Constants.Choice.KEEP_VERTICAL_LIMIT - Constants.Choice.KEEP_VERTICAL_INTERVAL);
            result.isFinished.IsFalse();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }
        [Test]
        public static void UpdateTest_正常系_下限_同時押し()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var inputKeys = keyConfigs.decide
                .Concat(keyConfigs.cancel)
                .Concat(keyConfigs.ups)
                .Concat(keyConfigs.downs);
            var keyTiming = KeyTiming.DOWN;

            var result = state.Update(keyConfigs, inputKeys, keyTiming);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(null);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
            result.viewActionQueue.IsNotNull();
            result.viewActionQueue.ToArray().Length.Is(1);
            result.viewActionQueue.ToArray()[0].IsNotNull();
            result.viewActionQueue.ToArray()[0].actor.IsSameReferenceAs(state);
            result.viewActionQueue.ToArray()[0].actionType.Is(ViewAction.Pattern.UPDATE);
        }

        [Test]
        public static void ToChoiceTextTest_単数_選択肢が0()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = 0;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($">\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_単数_選択肢が負の値()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = -1;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($"\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_単数_選択肢が上限超過()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = choiceList.Count;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($"\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_単数_選択肢がNull()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = (int?)null;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($"\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_複数_選択肢が0()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($">\t{text1}\r\n\t{text2}\r\n\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_複数_選択肢が上限()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($"\t{text1}\r\n\t{text2}\r\n>\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_複数_選択肢が負の値()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = -1;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($"\t{text1}\r\n\t{text2}\r\n\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_複数_選択肢が上限超過()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($"\t{text1}\r\n\t{text2}\r\n\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_複数_選択肢がNull()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = (int?)null;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($"\t{text1}\r\n\t{text2}\r\n\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_複数Null含み_選択肢が0()
        {
            var textNull = (string)null;
            var text1 = "text2";
            var text2 = "text3";
            var choiceList = new List<string> { textNull, text1, text2 };
            var choiced = 0;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($">\t{textNull}\r\n\t{text1}\r\n\t{text2}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_空リスト()
        {
            var choiceList = new List<string>();
            var choiced = 0;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is(string.Empty);
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_Nullリスト()
        {
            var choiceList = (List<string>)null;
            var choiced = 0;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is(string.Empty);
            result.position.x.Is(0);
            result.position.y.Is(0);
        }

        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_単数_選択肢が0()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = 0;

            var result = choiceList.ToChoiceText(choiced);
            result.IsNotNull();
            result.text.Is($">\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_単数_選択肢が負の値()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = -1;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($">\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_単数_選択肢が上限超過()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = choiceList.Count;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($">\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_単数_選択肢がNull()
        {
            var text1 = "text1";
            var choiceList = new List<string> { text1 };
            var choiced = (int?)null;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($"\t{text1}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_複数_選択肢が0()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($">\t{text1}\r\n\t{text2}\r\n\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_複数_選択肢が上限()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($"\t{text1}\r\n\t{text2}\r\n>\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_複数_選択肢が負の値()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = -1;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($"\t{text1}\r\n\t{text2}\r\n>\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_複数_選択肢が上限超過()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($">\t{text1}\r\n\t{text2}\r\n\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_複数_選択肢がNull()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = (int?)null;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($"\t{text1}\r\n\t{text2}\r\n\t{text3}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_複数Null含み_選択肢が0()
        {
            var textNull = (string)null;
            var text1 = "text2";
            var text2 = "text3";
            var choiceList = new List<string> { textNull, text1, text2 };
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is($">\t{textNull}\r\n\t{text1}\r\n\t{text2}");
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_空リスト()
        {
            var choiceList = new List<string>();
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is(string.Empty);
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクト_Nullリスト()
        {
            var choiceList = (List<string>)null;
            var choiced = 0;
            var state = new ChoiceState(choiceList, choiced);

            var result = state.ToChoiceText();
            result.IsNotNull();
            result.text.Is(string.Empty);
            result.position.x.Is(0);
            result.position.y.Is(0);
        }
        [Test]
        public static void ToChoiceTextTest_状態オブジェクトがNull()
        {
            var state = (ChoiceState)null;

            Assert.Throws<ArgumentNullException>(() => state.ToChoiceText());
        }
    }
}
