using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="EnergyProperties">クラスのテスト
    /// </summary>
    public static class EnergyPropertiesTest
    {
        [Test]
        public static void GetNameTest_正常系()
        {
            Energy.FLAME.GetName().Is(Constants.Energy.Name.FLAME);
            Energy.ICE.GetName().Is(Constants.Energy.Name.ICE);
            Energy.WIND.GetName().Is(Constants.Energy.Name.WIND);
            Energy.GRAVITY.GetName().Is(Constants.Energy.Name.GRAVITY);
            Energy.LIGHT.GetName().Is(Constants.Energy.Name.LIGHT);
            Energy.DARKNESS.GetName().Is(Constants.Energy.Name.DARKNESS);
            Energy.THUNDER.GetName().Is(Constants.Energy.Name.THUNDER);
            Energy.EARTH.GetName().Is(Constants.Energy.Name.EARTH);
            Energy.LIFE.GetName().Is(Constants.Energy.Name.LIFE);
            Energy.POISON.GetName().Is(Constants.Energy.Name.POISON);
            Energy.SLASH.GetName().Is(Constants.Energy.Name.SLASH);
            Energy.BLOW.GetName().Is(Constants.Energy.Name.BLOW);
            Energy.IMPACT.GetName().Is(Constants.Energy.Name.IMPACT);
            Energy.PIERCING.GetName().Is(Constants.Energy.Name.PIERCING);
        }
        [Test]
        public static void GetNameTest_未定義()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Energy)(-1)).GetName());
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Energy)Enum.GetValues(typeof(Energy)).Length).GetName());
        }
    }
}
