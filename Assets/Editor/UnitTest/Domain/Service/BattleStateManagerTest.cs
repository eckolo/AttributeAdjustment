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

            var result = state.SetupDeck().deckTips;

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

            var result = state.SetupDeck().deckTips;

            result.IsNotNull();
            result.Count.Is(0);
        }
        [Test]
        public static void SetupDeckTest_元データがNull()
        {
            var state = BattleStateMock.Generate(null);

            var result = state.SetupDeck().deckTips;

            result.IsNotNull();
            result.Count.Is(0);
        }

        [Test]
        public static void PopDeckTipsForcedTest_通常処理_山札数が取り出し数より大きい()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTips(tipList);
            var popTipNumber = 5;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count - popTipNumber);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsForcedTest_通常処理_山札数が取り出し数と等しい()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip1, tip2, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap).SetDeckTips(tipList);
            var popTipNumber = tipList.Count;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(value1 + value2 + value3);
            state.deckTips
                .Where(elem => elem.energy == tip1.energy)
                .Count(elem => elem.energyValue == tip1.energyValue)
                .Is(value1);
            state.deckTips
                .Where(elem => elem.energy == tip2.energy)
                .Count(elem => elem.energyValue == tip2.energyValue)
                .Is(value2);
            state.deckTips
                .Where(elem => elem.energy == tip3.energy)
                .Count(elem => elem.energyValue == tip3.energyValue)
                .Is(value3);
        }
        [Test]
        public static void PopDeckTipsForcedTest_通常処理_山札数が取り出し数未満()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip1, tip2, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap).SetDeckTips(tipList);
            var popTipNumber = 10;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count + value1 + value2 + value3 - popTipNumber);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsForcedTest_取り出し数が0()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTips(tipList);
            var popTipNumber = 0;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(0);

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsForcedTest_取り出し数が負の値()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTips(tipList);
            var popTipNumber = -5;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(0);

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }

        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 4;
            var value3 = 2;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(tipMap);

            var result = state.SetupDeck().SetupBoard().boardTips;

            result.IsNotNull();
            result.Count().Is(Constants.Battle.DEFAULT_BOARD_TIP_NUMBERS);
            result.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(tipMap);

            var result = state.SetupDeck().SetupBoard().boardTips;

            result.IsNotNull();
            result.Count().Is(value1 + value2 + value3);
            result.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値_山札が空()
        {
            var result = BattleStateMock.Generate().SetupBoard().boardTips;

            result.IsNotNull();
            result.Any().IsFalse();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値_戦闘状態がNull()
        {
            var result = BattleStateManager.SetupBoard(null);

            result.IsNull();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 4;
            var value3 = 2;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(tipMap);
            var tipNumbers = 4;

            var result = state.SetupDeck().SetupBoard(tipNumbers).boardTips;

            result.IsNotNull();
            result.Count().Is(tipNumbers);
            result.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(tipMap);
            var tipNumbers = 8;

            var result = state.SetupDeck().SetupBoard(tipNumbers).boardTips;

            result.IsNotNull();
            result.Count().Is(value1 + value2 + value3);
            result.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定_山札が空()
        {
            var tipNumbers = 8;

            var result = BattleStateMock.Generate().SetupBoard(tipNumbers).boardTips;

            result.IsNotNull();
            result.Any().IsFalse();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定_戦闘状態がNull()
        {
            var tipNumbers = 8;

            var result = BattleStateManager.SetupBoard(null, tipNumbers);

            result.IsNull();
        }

        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数デフォルト値_元の手札は空()
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
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数指定_元の手札は空()
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
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - tipNumbers);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数デフォルト値_元の手札有り()
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

            var actorState = state.battleActorList[battleActor].AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数指定_元の手札有り()
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

            var actorState = state.battleActorList[battleActor].AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - tipNumbers);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数デフォルト値_元の手札は空_山札が補充枚数以下()
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
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数指定_元の手札は空_山札が補充枚数以下()
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
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数デフォルト値_元の手札有り_山札が補充枚数以下()
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

            var actorState = state.battleActorList[battleActor].AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_通常処理_補充枚数指定_元の手札有り_山札が補充枚数以下()
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

            var actorState = state.battleActorList[battleActor].AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
            result.battleActorList.ContainsKey(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(value1 + value2 + value3);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupHandTipsTest_該当行動主体無し()
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

            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
        }
        [Test]
        public static void SetupHandTipsTest_戦闘状態がNull()
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
            var result = BattleStateManager.SetupHandTips(null, battleActor);

            result.IsNull();
        }
        [Test]
        public static void SetupHandTipsTest_行動主体がNull()
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

            var result = state.SetupHandTips(null);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActorList.IsNotNull();
        }
    }
}
