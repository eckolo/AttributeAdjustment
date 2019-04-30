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
        static readonly MotionTip motionTipOrigin
            = new MotionTip($"{nameof(MotionTipTest)}", Energy.BLOW, 1);
        static readonly MotionTip motionTipDiffName
            = new MotionTip($"{nameof(MotionTipTest)}_DiffName", motionTipOrigin.energy, motionTipOrigin.energyValue);
        static readonly MotionTip motionTipDiffEnergy
            = new MotionTip(motionTipOrigin.name, Energy.DARKNESS, motionTipOrigin.energyValue);
        static readonly MotionTip motionTipDiffValue
            = new MotionTip(motionTipOrigin.name, motionTipOrigin.energy, 12);
        static readonly MotionTip motionTipEqual
            = new MotionTip(motionTipOrigin.name, motionTipOrigin.energy, motionTipOrigin.energyValue);

        static readonly MotionTip motionTipNull = null;

        [Test]
        public static void GetHashCodeTest_通常値_同変数比較()
        {
            motionTipOrigin.GetHashCode().Is(motionTipOrigin.GetHashCode());
            motionTipDiffName.GetHashCode().Is(motionTipDiffName.GetHashCode());
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
            motionTipOrigin.GetHashCode().IsNot(motionTipDiffName.GetHashCode());
            motionTipOrigin.GetHashCode().IsNot(motionTipDiffEnergy.GetHashCode());
            motionTipOrigin.GetHashCode().IsNot(motionTipDiffValue.GetHashCode());

            motionTipDiffName.GetHashCode().IsNot(motionTipOrigin.GetHashCode());
            motionTipDiffName.GetHashCode().IsNot(motionTipDiffEnergy.GetHashCode());
            motionTipDiffName.GetHashCode().IsNot(motionTipDiffValue.GetHashCode());
            motionTipDiffName.GetHashCode().IsNot(motionTipEqual.GetHashCode());

            motionTipDiffEnergy.GetHashCode().IsNot(motionTipOrigin.GetHashCode());
            motionTipDiffEnergy.GetHashCode().IsNot(motionTipDiffName.GetHashCode());
            motionTipDiffEnergy.GetHashCode().IsNot(motionTipDiffValue.GetHashCode());
            motionTipDiffEnergy.GetHashCode().IsNot(motionTipEqual.GetHashCode());

            motionTipDiffValue.GetHashCode().IsNot(motionTipOrigin.GetHashCode());
            motionTipDiffValue.GetHashCode().IsNot(motionTipDiffName.GetHashCode());
            motionTipDiffValue.GetHashCode().IsNot(motionTipDiffEnergy.GetHashCode());
            motionTipDiffValue.GetHashCode().IsNot(motionTipEqual.GetHashCode());

            motionTipEqual.GetHashCode().IsNot(motionTipDiffName.GetHashCode());
            motionTipEqual.GetHashCode().IsNot(motionTipDiffEnergy.GetHashCode());
            motionTipEqual.GetHashCode().IsNot(motionTipDiffValue.GetHashCode());
        }

        [Test]
        public static void EqualsTest_メソッド_通常値_同変数比較()
        {
            motionTipOrigin.Equals(motionTipOrigin).IsTrue();
            motionTipDiffName.Equals(motionTipDiffName).IsTrue();
            motionTipDiffEnergy.Equals(motionTipDiffEnergy).IsTrue();
            motionTipDiffValue.Equals(motionTipDiffValue).IsTrue();
            motionTipEqual.Equals(motionTipEqual).IsTrue();
        }

        [Test]
        public static void EqualsTest_メソッド_通常値_同値別変数比較()
        {
            motionTipOrigin.Equals(motionTipEqual).IsTrue();
            motionTipEqual.Equals(motionTipOrigin).IsTrue();
        }

        [Test]
        public static void EqualsTest_メソッド_通常値_異値別変数比較()
        {
            motionTipOrigin.Equals(motionTipDiffName).IsFalse();
            motionTipOrigin.Equals(motionTipDiffEnergy).IsFalse();
            motionTipOrigin.Equals(motionTipDiffValue).IsFalse();

            motionTipDiffName.Equals(motionTipOrigin).IsFalse();
            motionTipDiffName.Equals(motionTipDiffEnergy).IsFalse();
            motionTipDiffName.Equals(motionTipDiffValue).IsFalse();
            motionTipDiffName.Equals(motionTipEqual).IsFalse();

            motionTipDiffEnergy.Equals(motionTipOrigin).IsFalse();
            motionTipDiffEnergy.Equals(motionTipDiffName).IsFalse();
            motionTipDiffEnergy.Equals(motionTipDiffValue).IsFalse();
            motionTipDiffEnergy.Equals(motionTipEqual).IsFalse();

            motionTipDiffValue.Equals(motionTipOrigin).IsFalse();
            motionTipDiffValue.Equals(motionTipDiffName).IsFalse();
            motionTipDiffValue.Equals(motionTipDiffEnergy).IsFalse();
            motionTipDiffValue.Equals(motionTipEqual).IsFalse();

            motionTipEqual.Equals(motionTipDiffName).IsFalse();
            motionTipEqual.Equals(motionTipDiffEnergy).IsFalse();
            motionTipEqual.Equals(motionTipDiffValue).IsFalse();
        }

        [Test]
        public static void EqualsTest_メソッド_Object()
        {
            object objectDiffName = new MotionTip($"{nameof(MotionTipTest)}_DiffName", motionTipOrigin.energy, motionTipOrigin.energyValue);
            object objectDiffEnergy = new MotionTip(motionTipOrigin.name, Energy.DARKNESS, motionTipOrigin.energyValue);
            object objectDiffValue = new MotionTip(motionTipOrigin.name, motionTipOrigin.energy, 12);
            object objectEqual = new MotionTip(motionTipOrigin.name, motionTipOrigin.energy, motionTipOrigin.energyValue);

            motionTipOrigin.Equals(objectDiffName).IsFalse();
            motionTipOrigin.Equals(objectDiffEnergy).IsFalse();
            motionTipOrigin.Equals(objectDiffValue).IsFalse();
            motionTipOrigin.Equals(objectEqual).IsTrue();
        }

        [Test]
        public static void EqualsTest_メソッド_Null値()
        {
            motionTipOrigin.Equals(motionTipNull).IsFalse();
        }

        [Test]
        public static void EqualsTest_演算子オーバーライド_通常値_同変数比較()
        {
#pragma warning disable CS1718 // 同値演算子のテストのため同変数比較の警告を無効化  ココカラ

            (motionTipOrigin == motionTipOrigin).IsTrue();
            (motionTipOrigin != motionTipOrigin).IsFalse();

            (motionTipDiffName == motionTipDiffName).IsTrue();
            (motionTipDiffName != motionTipDiffName).IsFalse();

            (motionTipDiffEnergy == motionTipDiffEnergy).IsTrue();
            (motionTipDiffEnergy != motionTipDiffEnergy).IsFalse();

            (motionTipDiffValue == motionTipDiffValue).IsTrue();
            (motionTipDiffValue != motionTipDiffValue).IsFalse();

            (motionTipEqual == motionTipEqual).IsTrue();
            (motionTipEqual != motionTipEqual).IsFalse();

#pragma warning restore CS1718 // 同値演算子のテストのため同変数比較の警告を無効化  ココマデ
        }

        [Test]
        public static void EqualsTest_演算子オーバーライド_通常値_同値別変数比較()
        {
            (motionTipOrigin == motionTipEqual).IsTrue();
            (motionTipOrigin != motionTipEqual).IsFalse();
            (motionTipEqual == motionTipOrigin).IsTrue();
            (motionTipEqual != motionTipOrigin).IsFalse();
        }

        [Test]
        public static void EqualsTest_演算子オーバーライド_通常値_異値別変数比較()
        {
            (motionTipOrigin == motionTipDiffName).IsFalse();
            (motionTipOrigin != motionTipDiffName).IsTrue();
            (motionTipOrigin == motionTipDiffEnergy).IsFalse();
            (motionTipOrigin != motionTipDiffEnergy).IsTrue();
            (motionTipOrigin == motionTipDiffValue).IsFalse();
            (motionTipOrigin != motionTipDiffValue).IsTrue();

            (motionTipDiffName == motionTipOrigin).IsFalse();
            (motionTipDiffName != motionTipOrigin).IsTrue();
            (motionTipDiffName == motionTipDiffEnergy).IsFalse();
            (motionTipDiffName != motionTipDiffEnergy).IsTrue();
            (motionTipDiffName == motionTipDiffValue).IsFalse();
            (motionTipDiffName != motionTipDiffValue).IsTrue();
            (motionTipDiffName == motionTipEqual).IsFalse();
            (motionTipDiffName != motionTipEqual).IsTrue();

            (motionTipDiffEnergy == motionTipOrigin).IsFalse();
            (motionTipDiffEnergy != motionTipOrigin).IsTrue();
            (motionTipDiffEnergy == motionTipDiffName).IsFalse();
            (motionTipDiffEnergy != motionTipDiffName).IsTrue();
            (motionTipDiffEnergy == motionTipDiffValue).IsFalse();
            (motionTipDiffEnergy != motionTipDiffValue).IsTrue();
            (motionTipDiffEnergy == motionTipEqual).IsFalse();
            (motionTipDiffEnergy != motionTipEqual).IsTrue();

            (motionTipDiffValue == motionTipOrigin).IsFalse();
            (motionTipDiffValue != motionTipOrigin).IsTrue();
            (motionTipDiffValue == motionTipDiffName).IsFalse();
            (motionTipDiffValue != motionTipDiffName).IsTrue();
            (motionTipDiffValue == motionTipDiffEnergy).IsFalse();
            (motionTipDiffValue != motionTipDiffEnergy).IsTrue();
            (motionTipDiffValue == motionTipEqual).IsFalse();
            (motionTipDiffValue != motionTipEqual).IsTrue();

            (motionTipEqual == motionTipDiffName).IsFalse();
            (motionTipEqual != motionTipDiffName).IsTrue();
            (motionTipEqual == motionTipDiffEnergy).IsFalse();
            (motionTipEqual != motionTipDiffEnergy).IsTrue();
            (motionTipEqual == motionTipDiffValue).IsFalse();
            (motionTipEqual != motionTipDiffValue).IsTrue();
        }

        [Test]
        public static void EqualsTest_演算子オーバーライド_Null値()
        {
#pragma warning disable CS1718 // 同値演算子のテストのため同変数比較の警告を無効化  ココカラ

            (motionTipOrigin == motionTipNull).IsFalse();
            (motionTipOrigin != motionTipNull).IsTrue();
            (motionTipNull == motionTipOrigin).IsFalse();
            (motionTipNull != motionTipOrigin).IsTrue();
            (motionTipNull == motionTipNull).IsTrue();
            (motionTipNull != motionTipNull).IsFalse();

#pragma warning restore CS1718 // 同値演算子のテストのため同変数比較の警告を無効化  ココマデ
        }
    }
}
