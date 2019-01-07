using Assets.Editor.UnitTest.Domain.Model.Mock;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="NumberManager"/>クラスのテスト
    /// </summary>
    public static class NumberManagerTest
    {
        [Test]
        public static void EuclideanTest()
        {
            var value1 = 2 * 2 * 3 * 11;
            var value2 = 2 * 2 * 5;
            var value3 = -1 * 3 * 5 * 7 * 11;
            var value4 = 1;
            var value5 = 0;

            value1.Euclidean(value1).Is(value1);
            value1.Euclidean(value2).Is(4);
            value1.Euclidean(value3).Is(33);
            value1.Euclidean(value4).Is(1);
            value1.Euclidean(value5).Is(1);
            value2.Euclidean(value2).Is(value2);
            value2.Euclidean(value3).Is(5);
            value2.Euclidean(value4).Is(1);
            value2.Euclidean(value5).Is(1);
            value3.Euclidean(value3).Is(value3);
            value3.Euclidean(value4).Is(1);
            value3.Euclidean(value5).Is(1);
            value4.Euclidean(value4).Is(value4);
            value4.Euclidean(value5).Is(1);
            value5.Euclidean(value5).Is(1);
        }
        [Test]
        public static void CorrectTest()
        {
            var scalar1 = 3f;
            var scalar2 = 5f;

            NumberManager.Correct(scalar1, scalar2).Is(4f);
            NumberManager.Correct(scalar1, scalar2, 1).Is(3f);
            NumberManager.Correct(scalar1, scalar2, 0).Is(5f);
        }
        [Test]
        public static void ToIntTest()
        {
            NumberManager.ToInt(true).Is(1);
            NumberManager.ToInt(false).Is(0);
        }
        [Test]
        public static void LimitUpperTest1()
        {
            NumberManager.LimitUpper(17, 52).Is(17);
            NumberManager.LimitUpper(102, 54).Is(54);
        }
        [Test]
        public static void LimitUpperTest2()
        {
            NumberManager.LimitUpper(1.7f, 5.2f).Is(1.7f);
            NumberManager.LimitUpper(10.2f, 0.54f).Is(0.54f);
        }
        [Test]
        public static void LimitLowerTest1()
        {
            NumberManager.LimitLower(17, 52).Is(52);
            NumberManager.LimitLower(102, 54).Is(102);
        }
        [Test]
        public static void LimitLowerTest2()
        {
            NumberManager.LimitLower(1.7f, 5.2f).Is(5.2f);
            NumberManager.LimitLower(10.2f, 0.54f).Is(10.2f);
        }
        [Test]
        public static void ToSignTest1()
        {
            NumberManager.ToSign(true).Is(1);
            NumberManager.ToSign(false).Is(-1);
        }
        [Test]
        public static void ToSignTest2()
        {
            NumberManager.ToSign(108).Is(1);
            NumberManager.ToSign(-62).Is(-1);
            NumberManager.ToSign(0).Is(0);
        }
        [Test]
        public static void SelectRandomTest()
        {
            var list1 = new List<float> { 0.5f, 7.8f, 832 };
            var list2 = new List<int> { 1, 2 };

            for(int i = 0; i < 120; i++)
            {
                var result = NumberManager.SelectRandom(list1);
                (result == 0.5f || result == 7.8f || result == 832).IsTrue();
            }
            for(int i = 0; i < 120; i++)
            {
                var result = NumberManager.SelectRandom(list1, list2);
                (result == 0.5f || result == 7.8f || result == 832).IsTrue();
            }
        }
        [Test]
        public static void LogTest1()
        {
            var value1 = Mathf.Exp(11.3f) - 1;
            var value2 = -Mathf.Exp(26.43f) + 1;
            var value3 = 0f;

            value1.Log().Is(11.3f);
            value2.Log().Is(-26.43f);
            value3.Log().Is(0);
        }
        [Test]
        public static void LogTest2()
        {
            var value1 = Mathf.Pow(2.6f, 11.3f) - 1;
            var value2 = -Mathf.Pow(4.63f, 26.43f) + 1;
            var value3 = 0f;

            value1.Log(2.6f).Is(11.3f);
            value2.Log(4.63f).Is(-26.43f);
            value3.Log(2.6f).Is(0);
        }
        [Test]
        public static void Copy()
        {
            var list1 = new List<ClassMock>{
                new ClassMock
                {
                    text1 = "test1",
                    text2 = "test2",
                    number = 3
                },
                new ClassMock
                {
                    text1 = "test3",
                    text2 = "test4",
                    number = 5
                }
            };
            var list2 = new List<ClassMock>{
                new ClassMock
                {
                    text1 = "test5",
                    text2 = "test6",
                    number = 4
                },
                new ClassMock
                {
                    text1 = "test7",
                    text2 = "test8",
                    number = 9
                }
            };
            var list3 = new List<ClassMock> { };
            List<ClassMock> list4 = null;

            var list1Copied = list1.Copy();

            list1Copied.Is(list1);
            list1Copied.IsNotSameReferenceAs(list1);
            list1Copied.Count.Is(list1.Count);
            list1Copied[0].Is(list1[0]);
            list1Copied[1].Is(list1[1]);
            list1Copied[0].IsSameReferenceAs(list1[0]);
            list1Copied[1].IsSameReferenceAs(list1[1]);

            list1Copied[0].IsNot(list2[0]);
            list1Copied[1].IsNot(list2[1]);

            list3.Copy().Is(list3);
            list4.Copy().Is(list4);
        }
        [Test]
        public static void ToPercentage()
        {
            float? number1 = 1.31f;
            float? number2 = 0.013f;
            float? number3 = 5f;
            float? number4 = null;

            number1.ToPercentage().Is("1.31");
            number2.ToPercentage().Is("0.01");
            number3.ToPercentage().Is("5.00");
            number4.ToPercentage().Is("-");
        }
    }
}
