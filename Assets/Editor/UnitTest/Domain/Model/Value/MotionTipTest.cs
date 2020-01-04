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
            motionTipOrigin.hashCode.Is(motionTipOrigin.hashCode);
            motionTipDiffName.hashCode.Is(motionTipDiffName.hashCode);
            motionTipDiffEnergy.hashCode.Is(motionTipDiffEnergy.hashCode);
            motionTipDiffValue.hashCode.Is(motionTipDiffValue.hashCode);
            motionTipEqual.hashCode.Is(motionTipEqual.hashCode);
        }

        [Test]
        public static void GetHashCodeTest_通常値_同値別変数比較()
        {
            motionTipOrigin.hashCode.Is(motionTipEqual.hashCode);
            motionTipEqual.hashCode.Is(motionTipOrigin.hashCode);
        }

        [Test]
        public static void GetHashCodeTest_通常値_異値別変数比較()
        {
            motionTipOrigin.hashCode.IsNot(motionTipDiffName.hashCode);
            motionTipOrigin.hashCode.IsNot(motionTipDiffEnergy.hashCode);
            motionTipOrigin.hashCode.IsNot(motionTipDiffValue.hashCode);

            motionTipDiffName.hashCode.IsNot(motionTipOrigin.hashCode);
            motionTipDiffName.hashCode.IsNot(motionTipDiffEnergy.hashCode);
            motionTipDiffName.hashCode.IsNot(motionTipDiffValue.hashCode);
            motionTipDiffName.hashCode.IsNot(motionTipEqual.hashCode);

            motionTipDiffEnergy.hashCode.IsNot(motionTipOrigin.hashCode);
            motionTipDiffEnergy.hashCode.IsNot(motionTipDiffName.hashCode);
            motionTipDiffEnergy.hashCode.IsNot(motionTipDiffValue.hashCode);
            motionTipDiffEnergy.hashCode.IsNot(motionTipEqual.hashCode);

            motionTipDiffValue.hashCode.IsNot(motionTipOrigin.hashCode);
            motionTipDiffValue.hashCode.IsNot(motionTipDiffName.hashCode);
            motionTipDiffValue.hashCode.IsNot(motionTipDiffEnergy.hashCode);
            motionTipDiffValue.hashCode.IsNot(motionTipEqual.hashCode);

            motionTipEqual.hashCode.IsNot(motionTipDiffName.hashCode);
            motionTipEqual.hashCode.IsNot(motionTipDiffEnergy.hashCode);
            motionTipEqual.hashCode.IsNot(motionTipDiffValue.hashCode);
        }
    }
}
