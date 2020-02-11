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
            motionTipOrigin.Is(motionTipOrigin);
            motionTipDiffEnergy.Is(motionTipDiffEnergy);
            motionTipDiffValue.Is(motionTipDiffValue);
            motionTipEqual.Is(motionTipEqual);
        }

        [Test]
        public static void GetHashCodeTest_通常値_同値別変数比較()
        {
            motionTipOrigin.Is(motionTipEqual);
            motionTipEqual.Is(motionTipOrigin);
        }

        [Test]
        public static void GetHashCodeTest_通常値_異値別変数比較()
        {
            motionTipOrigin.IsNot(motionTipDiffEnergy);
            motionTipOrigin.IsNot(motionTipDiffValue);

            motionTipDiffEnergy.IsNot(motionTipOrigin);
            motionTipDiffEnergy.IsNot(motionTipDiffValue);
            motionTipDiffEnergy.IsNot(motionTipEqual);

            motionTipDiffValue.IsNot(motionTipOrigin);
            motionTipDiffValue.IsNot(motionTipDiffEnergy);
            motionTipDiffValue.IsNot(motionTipEqual);

            motionTipEqual.IsNot(motionTipDiffEnergy);
            motionTipEqual.IsNot(motionTipDiffValue);
        }
    }
}
