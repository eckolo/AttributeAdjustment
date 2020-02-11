using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    /// <summary>
    /// <see cref="MotionTip"/>クラスのテスト
    /// </summary>
    public static class MotionTipTest
    {
        static readonly MotionTip motionTipOrigin = new MotionTip(Energy.BLOW, 1);
        static readonly MotionTip motionTipDiffEnergy
            = new MotionTip(Energy.DARKNESS, motionTipOrigin.energyValue);
        static readonly MotionTip motionTipDiffValue
            = new MotionTip(motionTipOrigin.energy, 12);
        static readonly MotionTip motionTipEqual
            = new MotionTip(motionTipOrigin.energy, motionTipOrigin.energyValue);
        static readonly MotionTip motionTipNull = null;

        [Test]
        public static void GetHashCodeTest_通常値_同変数比較()
        {
            motionTipOrigin.GetHashCode().Is(motionTipOrigin.GetHashCode());
            motionTipDiffEnergy.GetHashCode().Is(motionTipDiffEnergy.GetHashCode());
            motionTipDiffValue.GetHashCode().Is(motionTipDiffValue.GetHashCode());
            motionTipEqual.GetHashCode().Is(motionTipEqual.GetHashCode());
        }

        [Test]
        public static void GetHashCodeTest_通常値_同値別変数比較()
        {
            motionTipOrigin.GetHashCode().Is(motionTipEqual.GetHashCode());
            motionTipEqual.GetHashCode().Is(motionTipOrigin.GetHashCode());
        }

        [Test]
        public static void GetHashCodeTest_通常値_異値別変数比較()
        {
            motionTipOrigin.GetHashCode().IsNot(motionTipDiffEnergy.GetHashCode());
            motionTipOrigin.GetHashCode().IsNot(motionTipDiffValue.GetHashCode());

            motionTipDiffEnergy.GetHashCode().IsNot(motionTipOrigin.GetHashCode());
            motionTipDiffEnergy.GetHashCode().IsNot(motionTipDiffValue.GetHashCode());
            motionTipDiffEnergy.GetHashCode().IsNot(motionTipEqual.GetHashCode());

            motionTipDiffValue.GetHashCode().IsNot(motionTipOrigin.GetHashCode());
            motionTipDiffValue.GetHashCode().IsNot(motionTipDiffEnergy.GetHashCode());
            motionTipDiffValue.GetHashCode().IsNot(motionTipEqual.GetHashCode());

            motionTipEqual.GetHashCode().IsNot(motionTipDiffEnergy.GetHashCode());
            motionTipEqual.GetHashCode().IsNot(motionTipDiffValue.GetHashCode());
        }

        [Test]
        public static void EqualsTest_通常値_同変数比較()
        {
            motionTipOrigin.Equals(motionTipOrigin).IsTrue();
            motionTipDiffEnergy.Equals(motionTipDiffEnergy).IsTrue();
            motionTipDiffValue.Equals(motionTipDiffValue).IsTrue();
            motionTipEqual.Equals(motionTipEqual).IsTrue();
        }
        [Test]
        public static void EqualsTest_通常値_同値別変数比較()
        {
            motionTipOrigin.Equals(motionTipEqual).IsTrue();
            motionTipEqual.Equals(motionTipOrigin).IsTrue();
        }
        [Test]
        public static void EqualsTest_通常値_異値別変数比較()
        {
            motionTipOrigin.Equals(motionTipDiffEnergy).IsFalse();
            motionTipOrigin.Equals(motionTipDiffValue).IsFalse();

            motionTipDiffEnergy.Equals(motionTipOrigin).IsFalse();
            motionTipDiffEnergy.Equals(motionTipDiffValue).IsFalse();
            motionTipDiffEnergy.Equals(motionTipEqual).IsFalse();

            motionTipDiffValue.Equals(motionTipOrigin).IsFalse();
            motionTipDiffValue.Equals(motionTipDiffEnergy).IsFalse();
            motionTipDiffValue.Equals(motionTipEqual).IsFalse();

            motionTipEqual.Equals(motionTipDiffEnergy).IsFalse();
            motionTipEqual.Equals(motionTipDiffValue).IsFalse();
        }
        [Test]
        public static void EqualsTest_通常値_Null比較()
        {
            motionTipOrigin.Equals(motionTipNull).IsFalse();
        }

        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_同変数比較()
        {
#pragma warning disable CS1718 // 同値比較演算子オーバーライドのテストのため一時的に警告を抑制　ココカラ
            (motionTipOrigin == motionTipOrigin).IsTrue();
            (motionTipDiffEnergy == motionTipDiffEnergy).IsTrue();
            (motionTipDiffValue == motionTipDiffValue).IsTrue();
            (motionTipEqual == motionTipEqual).IsTrue();
#pragma warning restore CS1718 // 同値比較演算子オーバーライドのテストのため一時的に警告を抑制　ココマデ
        }
        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_同値別変数比較()
        {
            (motionTipOrigin == motionTipEqual).IsTrue();
            (motionTipEqual == motionTipOrigin).IsTrue();
        }
        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_異値別変数比較()
        {
            (motionTipOrigin == motionTipDiffEnergy).IsFalse();
            (motionTipOrigin == motionTipDiffValue).IsFalse();

            (motionTipDiffEnergy == motionTipOrigin).IsFalse();
            (motionTipDiffEnergy == motionTipDiffValue).IsFalse();
            (motionTipDiffEnergy == motionTipEqual).IsFalse();

            (motionTipDiffValue == motionTipOrigin).IsFalse();
            (motionTipDiffValue == motionTipDiffEnergy).IsFalse();
            (motionTipDiffValue == motionTipEqual).IsFalse();

            (motionTipEqual == motionTipDiffEnergy).IsFalse();
            (motionTipEqual == motionTipDiffValue).IsFalse();
        }
        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_Null比較()
        {
            (motionTipOrigin == motionTipNull).IsFalse();
            (motionTipNull == motionTipOrigin).IsFalse();
        }

        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_同変数比較_否定()
        {
#pragma warning disable CS1718 // 同値比較演算子オーバーライドのテストのため一時的に警告を抑制　ココカラ
            (motionTipOrigin != motionTipOrigin).IsFalse();
            (motionTipDiffEnergy != motionTipDiffEnergy).IsFalse();
            (motionTipDiffValue != motionTipDiffValue).IsFalse();
            (motionTipEqual != motionTipEqual).IsFalse();
#pragma warning restore CS1718 // 同値比較演算子オーバーライドのテストのため一時的に警告を抑制　ココマデ
        }
        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_同値別変数比較_否定()
        {
            (motionTipOrigin != motionTipEqual).IsFalse();
            (motionTipEqual != motionTipOrigin).IsFalse();
        }
        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_異値別変数比較_否定()
        {
            (motionTipOrigin != motionTipDiffEnergy).IsTrue();
            (motionTipOrigin != motionTipDiffValue).IsTrue();

            (motionTipDiffEnergy != motionTipOrigin).IsTrue();
            (motionTipDiffEnergy != motionTipDiffValue).IsTrue();
            (motionTipDiffEnergy != motionTipEqual).IsTrue();

            (motionTipDiffValue != motionTipOrigin).IsTrue();
            (motionTipDiffValue != motionTipDiffEnergy).IsTrue();
            (motionTipDiffValue != motionTipEqual).IsTrue();

            (motionTipEqual != motionTipDiffEnergy).IsTrue();
            (motionTipEqual != motionTipDiffValue).IsTrue();
        }
        [Test]
        public static void EqualsTest_通常値_演算子オーバーライド_Null比較_否定()
        {
            (motionTipOrigin != motionTipNull).IsTrue();
            (motionTipNull != motionTipOrigin).IsTrue();
        }
    }
}
