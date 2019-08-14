using Assets.Src.Domain.Model.Entity;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Editor.UnitTest.Domain.Model.Entity
{
    /// <summary>
    /// <see cref="ChoiceState"/>クラスのテスト
    /// </summary>
    public static class ChoiceStateTest
    {
        [Test]
        public static void ConstructorTest_正常系_選択肢有_選択値Null()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = (int?)null;

            var result = new ChoiceState(choiceList, choiced);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
        }
        [Test]
        public static void ConstructorTest_正常系_選択肢有_選択値数値()
        {
            var text1 = "text1";
            var text2 = "text2";
            var text3 = "text3";
            var choiceList = new List<string> { text1, text2, text3 };
            var choiced = choiceList.Count - 1;

            var result = new ChoiceState(choiceList, choiced);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiceList[0].Is(choiceList[0]);
            result.choiceList[1].Is(choiceList[1]);
            result.choiceList[2].Is(choiceList[2]);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsFalse();
        }
        [Test]
        public static void ConstructorTest_正常系_選択肢無_選択値Null()
        {
            var choiceList = new List<string> { };
            var choiced = (int?)null;

            var result = new ChoiceState(choiceList, choiced);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
        }
        [Test]
        public static void ConstructorTest_正常系_選択肢無_選択値数値()
        {
            var choiceList = new List<string> { };
            var choiced = choiceList.Count - 1;

            var result = new ChoiceState(choiceList, choiced);

            result.IsNotNull();
            result.choiceList.IsNotNull();
            result.choiceList.Count.Is(choiceList.Count);
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
        }
        [Test]
        public static void ConstructorTest_正常系_選択肢Null_選択値Null()
        {
            var choiceList = (List<string>)null;
            var choiced = (int?)null;

            var result = new ChoiceState(choiceList, choiced);

            result.IsNotNull();
            result.choiceList.IsNull();
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
        }
        [Test]
        public static void ConstructorTest_正常系_選択肢Null_選択値数値()
        {
            var choiceList = (List<string>)null;
            var choiced = 2;

            var result = new ChoiceState(choiceList, choiced);

            result.IsNotNull();
            result.choiceList.IsNull();
            result.choiced.Is(choiced);
            result.keepUpTime.Is(0);
            result.keepDownTime.Is(0);
            result.isFinished.IsTrue();
        }
    }
}
