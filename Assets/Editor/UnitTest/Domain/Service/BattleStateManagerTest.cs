using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="BattleStateManager"/>サービスのテスト
    /// </summary>
    public static class BattleStateManagerTest
    {
        [Test]
        public static void SetupDeckTest_通常処理()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap);

            var result = state.SetupDeck().deckTip;

            result.IsNotNull();
            result.Count.Is(value1 + value2 + value3);
            result
                .Where(elem => elem.energy == tip1.energy)
                .Count(elem => elem.energyValue == tip1.energyValue)
                .Is(value1);
            result
                .Where(elem => elem.energy == tip2.energy)
                .Count(elem => elem.energyValue == tip2.energyValue)
                .Is(value2);
            result
                .Where(elem => elem.energy == tip3.energy)
                .Count(elem => elem.energyValue == tip3.energyValue)
                .Is(value3);
        }
        [Test]
        public static void SetupDeckTest_元データが空()
        {
            var tipMap = new Dictionary<MotionTip, int> { };
            var state = BattleStateMock.Generate(tipMap);

            var result = state.SetupDeck().deckTip;

            result.IsNotNull();
            result.Count.Is(0);
        }
        [Test]
        public static void SetupDeckTest_元データがNull()
        {
            var state = BattleStateMock.Generate(null);

            var result = state.SetupDeck().deckTip;

            result.IsNull();
        }
    }
}
