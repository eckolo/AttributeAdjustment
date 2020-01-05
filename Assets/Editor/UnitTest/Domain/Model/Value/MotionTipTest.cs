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

        [Test]
        public static void GetHashCodeTest_通常値_同変数比較()
        {
            motionTipOrigin.hashCode.Is(motionTipOrigin.hashCode);
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
            motionTipOrigin.hashCode.IsNot(motionTipDiffEnergy.hashCode);
            motionTipOrigin.hashCode.IsNot(motionTipDiffValue.hashCode);

            motionTipDiffEnergy.hashCode.IsNot(motionTipOrigin.hashCode);
            motionTipDiffEnergy.hashCode.IsNot(motionTipDiffValue.hashCode);
            motionTipDiffEnergy.hashCode.IsNot(motionTipEqual.hashCode);

            motionTipDiffValue.hashCode.IsNot(motionTipOrigin.hashCode);
            motionTipDiffValue.hashCode.IsNot(motionTipDiffEnergy.hashCode);
            motionTipDiffValue.hashCode.IsNot(motionTipEqual.hashCode);

            motionTipEqual.hashCode.IsNot(motionTipDiffEnergy.hashCode);
            motionTipEqual.hashCode.IsNot(motionTipDiffValue.hashCode);
        }
    }
}
