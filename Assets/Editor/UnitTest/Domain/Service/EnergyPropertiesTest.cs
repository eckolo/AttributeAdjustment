using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;
using UnityEngine;

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
            Energy.FLAME.GetName().Is("炎");
            Energy.ICE.GetName().Is("氷");
            Energy.WIND.GetName().Is("風");
            Energy.GRAVITY.GetName().Is("重");
            Energy.LIGHT.GetName().Is("光");
            Energy.DARKNESS.GetName().Is("闇");
            Energy.THUNDER.GetName().Is("雷");
            Energy.EARTH.GetName().Is("地");
            Energy.LIFE.GetName().Is("命");
            Energy.POISON.GetName().Is("毒");
            Energy.SLASH.GetName().Is("斬");
            Energy.BLOW.GetName().Is("打");
            Energy.IMPACT.GetName().Is("衝");
            Energy.PIERCING.GetName().Is("突");
        }
        [Test]
        public static void GetNameTest_未定義()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Energy)(-1)).GetName());
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Energy)Enum.GetValues(typeof(Energy)).Length).GetName());
        }

        [Test]
        public static void GetColorTest_正常系()
        {
            Energy.FLAME.GetColor().Is(new Color32(r: 255, g: 000, b: 000, a: 255));
            Energy.ICE.GetColor().Is(new Color32(r: 000, g: 000, b: 255, a: 255));
            Energy.WIND.GetColor().Is(new Color32(r: 000, g: 255, b: 000, a: 255));
            Energy.GRAVITY.GetColor().Is(new Color32(r: 120, g: 120, b: 120, a: 255));
            Energy.LIGHT.GetColor().Is(new Color32(r: 255, g: 255, b: 255, a: 255));
            Energy.DARKNESS.GetColor().Is(new Color32(r: 000, g: 000, b: 000, a: 255));
            Energy.THUNDER.GetColor().Is(new Color32(r: 255, g: 255, b: 000, a: 255));
            Energy.EARTH.GetColor().Is(new Color32(r: 120, g: 000, b: 000, a: 255));
            Energy.LIFE.GetColor().Is(new Color32(r: 000, g: 120, b: 000, a: 255));
            Energy.POISON.GetColor().Is(new Color32(r: 120, g: 000, b: 120, a: 255));
            Energy.SLASH.GetColor().Is(new Color32(r: 000, g: 255, b: 255, a: 255));
            Energy.BLOW.GetColor().Is(new Color32(r: 120, g: 120, b: 000, a: 255));
            Energy.IMPACT.GetColor().Is(new Color32(r: 000, g: 120, b: 120, a: 255));
            Energy.PIERCING.GetColor().Is(new Color32(r: 000, g: 000, b: 120, a: 255));
        }
        [Test]
        public static void GetColorTest_未定義()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Energy)(-1)).GetColor());
            Assert.Throws<ArgumentOutOfRangeException>(() => ((Energy)Enum.GetValues(typeof(Energy)).Length).GetColor());
        }
    }
}
