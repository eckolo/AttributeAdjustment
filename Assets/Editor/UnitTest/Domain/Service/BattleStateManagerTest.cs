using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.Mock;
using NUnit.Framework;
using System;
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

        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数デフォルト値_元の手札は空()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor];
            var result = state.SetupHandTip(battleActor);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数指定_元の手札は空()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 4;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor];
            var result = state.SetupHandTip(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3 - tipNumbers);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数デフォルト値_元の手札有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor].AddHandTip(tipList);
            var result = state.SetupHandTip(battleActor);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数指定_元の手札有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 4;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor].AddHandTip(tipList);
            var result = state.SetupHandTip(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3 - tipNumbers);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数デフォルト値_元の手札は空_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor];
            var result = state.SetupHandTip(battleActor);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数指定_元の手札は空_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 12;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor];
            var result = state.SetupHandTip(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数デフォルト値_元の手札有り_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor].AddHandTip(tipList);
            var result = state.SetupHandTip(battleActor);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_通常処理_補充枚数指定_元の手札有り_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 12;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor].AddHandTip(tipList);
            var result = state.SetupHandTip(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipTest_該当行動主体無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = BattleActorMock.Generate(name1);

            var result = state.SetupHandTip(battleActor);

            result.IsNotNull();
            result.deckTip.IsNotNull();
            result.deckTip.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
        }
        [Test]
        public static void SetupHandTipTest_戦闘状態がNull()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActorList.First().Key;

            var actorState = state.battleActorList[battleActor];
            Assert.Throws<ArgumentNullException>(() => BattleStateManager.SetupHandTip(null, battleActor));
        }
        [Test]
        public static void SetupHandTipTest_行動主体がNull()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();

            Assert.Throws<ArgumentNullException>(() => state.SetupHandTip(null));
        }
    }
}
