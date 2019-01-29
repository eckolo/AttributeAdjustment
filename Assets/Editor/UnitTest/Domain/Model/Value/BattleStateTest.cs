using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    /// <summary>
    /// <see cref="BattleState"/>
    /// </summary>
    public static class BattleStateTest
    {
        [Test]
        public static void SetDeckTipTest_通常処理()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate();

            state.deckTip.Is(default);

            var result = state.SetDeckTip(tipList).deckTip;

            result.IsNotNull();
            result.Count.Is(tipList.Count);
            result
                .Where(elem => elem.energy == tip1.energy)
                .Count(elem => elem.energyValue == tip1.energyValue)
                .Is(3);
            result
                .Where(elem => elem.energy == tip2.energy)
                .Count(elem => elem.energyValue == tip2.energyValue)
                .Is(2);
            result
                .Where(elem => elem.energy == tip3.energy)
                .Count(elem => elem.energyValue == tip3.energyValue)
                .Is(1);
        }
        [Test]
        public static void SetDeckTipTest_元データが空()
        {
            var tipList = new List<MotionTip>();
            var state = BattleStateMock.Generate();

            state.deckTip.Is(default);

            var result = state.SetDeckTip(tipList).deckTip;

            result.IsNotNull();
            result.Count.Is(0);
        }
        [Test]
        public static void SetDeckTipTest_元データがNull()
        {
            var state = BattleStateMock.Generate();

            state.deckTip.Is(default);

            var result = state.SetDeckTip(null).deckTip;

            result.IsNull();
        }
    }
}
