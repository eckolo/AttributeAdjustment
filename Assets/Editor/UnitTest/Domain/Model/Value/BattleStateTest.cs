using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    /// <summary>
    /// <see cref="BattleState"/>のテストクラス
    /// </summary>
    public static partial class BattleStateTest
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

        [Test]
        public static void PopDeckTipTest_通常処理_山札数が取り出し数より大きい()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTip(tipList);
            var popTipNumber = 5;

            var result = state.PopDeckTip(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTip.IsNotNull();
            state.deckTip.Count.Is(tipList.Count - popTipNumber);
            state.deckTip.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipTest_通常処理_山札数が取り出し数と等しい()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip1, tip2, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap).SetDeckTip(tipList);
            var popTipNumber = tipList.Count;

            var result = state.PopDeckTip(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTip.IsNotNull();
            state.deckTip.Count.Is(value1 + value2 + value3);
            state.deckTip
                .Where(elem => elem.energy == tip1.energy)
                .Count(elem => elem.energyValue == tip1.energyValue)
                .Is(value1);
            state.deckTip
                .Where(elem => elem.energy == tip2.energy)
                .Count(elem => elem.energyValue == tip2.energyValue)
                .Is(value2);
            state.deckTip
                .Where(elem => elem.energy == tip3.energy)
                .Count(elem => elem.energyValue == tip3.energyValue)
                .Is(value3);
        }
        [Test]
        public static void PopDeckTipTest_通常処理_山札数が取り出し数未満()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip1, tip2, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap).SetDeckTip(tipList);
            var popTipNumber = 10;

            var result = state.PopDeckTip(popTipNumber);

            result.IsNotNull();
            result.Count().Is(tipList.Count);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTip.IsNotNull();
            state.deckTip.Count.Is(value1 + value2 + value3);
            state.deckTip
                .Where(elem => elem.energy == tip1.energy)
                .Count(elem => elem.energyValue == tip1.energyValue)
                .Is(value1);
            state.deckTip
                .Where(elem => elem.energy == tip2.energy)
                .Count(elem => elem.energyValue == tip2.energyValue)
                .Is(value2);
            state.deckTip
                .Where(elem => elem.energy == tip3.energy)
                .Count(elem => elem.energyValue == tip3.energyValue)
                .Is(value3);
        }
        [Test]
        public static void PopDeckTipTest_取り出し数が0()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTip(tipList);
            var popTipNumber = 0;

            var result = state.PopDeckTip(popTipNumber);

            result.IsNotNull();
            result.Any().IsFalse();

            state.deckTip.IsNotNull();
            state.deckTip.Count.Is(tipList.Count);
            state.deckTip.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipTest_取り出し数が負の値()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTip(tipList);
            var popTipNumber = -5;

            var result = state.PopDeckTip(popTipNumber);

            result.IsNotNull();
            result.Any().IsFalse();

            state.deckTip.IsNotNull();
            state.deckTip.Count.Is(tipList.Count);
            state.deckTip.All(elem => tipList.Contains(elem)).IsTrue();
        }
    }
}
