﻿using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="BattleActorManager"/>サービスのテスト
    /// </summary>
    public static class BattleActorManagerTest
    {
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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - tipNumbers);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - tipNumbers);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupHandTips(battleActor, tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3);
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

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
            result.battleActors.IsNotNull();
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
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = BattleActorManager.SetupHandTips(null, battleActor);

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
            result.battleActors.IsNotNull();
        }
    }
}
