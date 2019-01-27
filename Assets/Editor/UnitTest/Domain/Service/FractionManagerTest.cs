using Assets.Src.Domain.Service;
using NUnit.Framework;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="FractionManager"/>クラスのテスト
    /// </summary>
    public static class FractionManagerTest
    {
        [Test]
        public static void LimitUpperTest()
        {
            FractionManager.LimitUpper(17.DividedBy(3), 52.DividedBy(4)).Is(17.DividedBy(3));
            FractionManager.LimitUpper(102.DividedBy(5), 54.DividedBy(6)).Is(54.DividedBy(6));
        }
        [Test]
        public static void LimitLowerTest()
        {
            FractionManager.LimitLower(17.DividedBy(3), 52.DividedBy(4)).Is(52.DividedBy(4));
            FractionManager.LimitLower(92.DividedBy(5), 54.DividedBy(6)).Is(92.DividedBy(5));
        }
    }
}
