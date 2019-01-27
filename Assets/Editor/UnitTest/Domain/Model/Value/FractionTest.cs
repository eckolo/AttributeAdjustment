using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    /// <summary>
    /// <see cref="Fraction"/>クラスのテスト
    /// </summary>
    public static class FractionTest
    {
        [Test]
        public static void ZeroTest()
        {
            var fraction1 = 0.ToFraction();
            var fraction2 = Fraction.zero;

            fraction1.numer.Is(fraction2.numer);
            fraction1.denom.Is(fraction2.denom);
            fraction1.value.Is(fraction2.value);
        }
        [Test]
        public static void OneTest()
        {
            var fraction1 = 1.ToFraction();
            var fraction2 = Fraction.one;

            fraction1.numer.Is(fraction2.numer);
            fraction1.denom.Is(fraction2.denom);
            fraction1.value.Is(fraction2.value);
        }
        [Test]
        public static void NewFractionTest()
        {
            var fraction1 = 3.ToFraction();
            var fraction2 = 3.DividedBy(1);
            var fraction3 = 6.DividedBy(2);
            var fraction4 = 9.DividedBy(-3);

            fraction1.numer.Is(fraction2.numer);
            fraction1.denom.Is(fraction2.denom);
            fraction3.numer.Is(fraction2.numer);
            fraction3.denom.Is(fraction2.denom);
            fraction1.value.Is(3);
            fraction2.value.Is(3);
            fraction3.value.Is(3);
            fraction4.value.Is(-3);
        }
        [Test]
        public static void CompareTest()
        {
            var fraction1 = 4.DividedBy(5);
            var fraction2 = 7.DividedBy(2);
            var fraction3 = 3.DividedBy(-8);
            var fraction4 = 4.DividedBy(5);
            var value1 = 3f;
            var value2 = -12;

            (fraction1 == fraction2).IsFalse();
            (fraction1 != fraction2).IsTrue();
            (fraction1 < fraction2).IsTrue();
            (fraction1 > fraction2).IsFalse();
            (fraction1 <= fraction2).IsTrue();
            (fraction1 >= fraction2).IsFalse();

            (fraction1 == fraction3).IsFalse();
            (fraction1 != fraction3).IsTrue();
            (fraction1 < fraction3).IsFalse();
            (fraction1 > fraction3).IsTrue();
            (fraction1 <= fraction3).IsFalse();
            (fraction1 >= fraction3).IsTrue();

            (fraction1 == fraction4).IsTrue();
            (fraction1 != fraction4).IsFalse();
            (fraction1 < fraction4).IsFalse();
            (fraction1 > fraction4).IsFalse();
            (fraction1 <= fraction4).IsTrue();
            (fraction1 >= fraction4).IsTrue();

            (fraction1 == value1).IsFalse();
            (fraction1 != value1).IsTrue();
            (fraction1 < value1).IsTrue();
            (fraction1 > value1).IsFalse();
            (fraction1 <= value1).IsTrue();
            (fraction1 >= value1).IsFalse();

            (fraction1 == value2).IsFalse();
            (fraction1 != value2).IsTrue();
            (fraction1 < value2).IsFalse();
            (fraction1 > value2).IsTrue();
            (fraction1 <= value2).IsFalse();
            (fraction1 >= value2).IsTrue();
        }
        [Test]
        public static void NegativeValueTest()
        {
            var fraction1 = 4.DividedBy(5);
            var fraction2 = 7.DividedBy(2);
            var fraction3 = 3.DividedBy(-8);

            var result1 = -fraction1;
            var result2 = -fraction2;
            var result3 = -fraction3;

            result1.numer.Is(-4);
            result1.denom.Is(5);
            result1.value.Is(-0.8f);
            result2.numer.Is(-7);
            result2.denom.Is(2);
            result2.value.Is(-3.5f);
            result3.numer.Is(3);
            result3.denom.Is(8);
            result3.value.Is(0.375f);
        }
        [Test]
        public static void AdditionTest()
        {
            var fraction1 = 4.DividedBy(5);
            var fraction2 = 7.DividedBy(2);
            var fraction3 = 3.DividedBy(-8);
            var value1 = 3;
            var value2 = -12;

            var result1 = fraction1 + fraction2;
            var result2 = fraction1 + fraction3;
            var result3 = fraction1 + value1;
            var result4 = fraction1 + value2;
            var result5 = value2 + fraction1;

            result1.numer.Is(43);
            result1.denom.Is(10);
            result1.value.Is(4.3f);
            result2.numer.Is(17);
            result2.denom.Is(40);
            result2.value.Is(0.425f);
            result3.numer.Is(19);
            result3.denom.Is(5);
            result3.value.Is(3.8f);
            result4.numer.Is(-56);
            result4.denom.Is(5);
            result4.value.Is(-11.2f);
            result5.numer.Is(-56);
            result5.denom.Is(5);
            result5.value.Is(-11.2f);
        }
        [Test]
        public static void SubtractionTest()
        {
            var fraction1 = 4.DividedBy(5);
            var fraction2 = 7.DividedBy(2);
            var fraction3 = 3.DividedBy(-8);
            var value1 = 3;
            var value2 = -12;

            var result1 = fraction1 - fraction2;
            var result2 = fraction1 - fraction3;
            var result3 = fraction1 - value1;
            var result4 = fraction1 - value2;
            var result5 = value2 - fraction1;

            result1.numer.Is(-27);
            result1.denom.Is(10);
            result1.value.Is(-2.7f);
            result2.numer.Is(47);
            result2.denom.Is(40);
            result2.value.Is(47f / 40f);
            result3.numer.Is(-11);
            result3.denom.Is(5);
            result3.value.Is(-2.2f);
            result4.numer.Is(64);
            result4.denom.Is(5);
            result4.value.Is(12.8f);
            result5.numer.Is(-64);
            result5.denom.Is(5);
            result5.value.Is(-12.8f);
        }
        [Test]
        public static void MultiplicationTest()
        {
            var fraction1 = 4.DividedBy(5);
            var fraction2 = 7.DividedBy(2);
            var fraction3 = 3.DividedBy(-8);
            var value1 = 3;
            var value2 = -12;

            var result1 = fraction1 * fraction2;
            var result2 = fraction1 * fraction3;
            var result3 = fraction1 * value1;
            var result4 = fraction1 * value2;
            var result5 = value2 * fraction1;

            result1.numer.Is(14);
            result1.denom.Is(5);
            result1.value.Is(2.8f);
            result2.numer.Is(-3);
            result2.denom.Is(10);
            result2.value.Is(-0.3f);
            result3.numer.Is(12);
            result3.denom.Is(5);
            result3.value.Is(2.4f);
            result4.numer.Is(-48);
            result4.denom.Is(5);
            result4.value.Is(-9.6f);
            result5.numer.Is(-48);
            result5.denom.Is(5);
            result5.value.Is(-9.6f);
        }
        [Test]
        public static void DivisionTest()
        {
            var fraction1 = 4.DividedBy(5);
            var fraction2 = 7.DividedBy(2);
            var fraction3 = 3.DividedBy(-8);
            var value1 = 3;
            var value2 = -12;

            var result1 = fraction1 / fraction2;
            var result2 = fraction1 / fraction3;
            var result3 = fraction1 / value1;
            var result4 = fraction1 / value2;
            var result5 = value2 / fraction1;

            result1.numer.Is(8);
            result1.denom.Is(35);
            result1.value.Is(8f / 35f);
            result2.numer.Is(-32);
            result2.denom.Is(15);
            result2.value.Is(-32f / 15f);
            result3.numer.Is(4);
            result3.denom.Is(15);
            result3.value.Is(4f / 15f);
            result4.numer.Is(-1);
            result4.denom.Is(15);
            result4.value.Is(-1f / 15f);
            result5.numer.Is(-15);
            result5.denom.Is(1);
            result5.value.Is(-15);
        }
    }
}
