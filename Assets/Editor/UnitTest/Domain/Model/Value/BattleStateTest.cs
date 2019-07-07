using Assets.Src.Domain.Model.Entity;
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
    public static class BattleStateTest
    {
        [Test]
        public static void SetDeckTipsTest_通常処理()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(0);

            var result = state.SetDeckTips(tipList).deckTips;

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
        public static void SetDeckTipsTest_元データが空()
        {
            var tipList = new List<MotionTip>();
            var state = BattleStateMock.Generate();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(0);

            var result = state.SetDeckTips(tipList).deckTips;

            result.IsNotNull();
            result.Count.Is(0);
        }
        [Test]
        public static void SetDeckTipsTest_元データがNull()
        {
            var state = BattleStateMock.Generate();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(0);

            var result = state.SetDeckTips(null).deckTips;

            result.IsNotNull();
            result.Count.Is(0);
        }

        [Test]
        public static void PopDeckTipsTest_通常処理_山札数が取り出し数より大きい()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTips(tipList);
            var popTipNumber = 5;

            var result = state.PopDeckTips(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count - popTipNumber);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsTest_通常処理_山札数が取り出し数と等しい()
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

            var result = state.PopDeckTips(popTipNumber);

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
        public static void PopDeckTipsTest_通常処理_山札数が取り出し数未満()
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

            var result = state.PopDeckTips(popTipNumber);

            result.IsNotNull();
            result.Count().Is(tipList.Count);
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
        public static void PopDeckTipsTest_取り出し数が0()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTips(tipList);
            var popTipNumber = 0;

            var result = state.PopDeckTips(popTipNumber);

            result.IsNotNull();
            result.Count().Is(0);

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsTest_取り出し数が負の値()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate().SetDeckTips(tipList);
            var popTipNumber = -5;

            var result = state.PopDeckTips(popTipNumber);

            result.IsNotNull();
            result.Count().Is(0);

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }

        [Test]
        public static void SetBoardTipsTest_通常処理()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate();

            state.boardTips.IsNotNull();
            state.boardTips.Count().Is(0);

            var result = state.SetBoardTips(tipList).boardTips;

            result.IsNotNull();
            result.Count().Is(tipList.Count);
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
        public static void SetBoardTipsTest_元データが空()
        {
            var tipList = new List<MotionTip>();
            var state = BattleStateMock.Generate();

            state.boardTips.IsNotNull();
            state.boardTips.Count().Is(0);

            var result = state.SetBoardTips(tipList).boardTips;

            result.IsNotNull();
            result.Count().Is(0);
        }
        [Test]
        public static void SetBoardTipsTest_元データがNull()
        {
            var state = BattleStateMock.Generate();

            state.boardTips.IsNotNull();
            state.boardTips.Count().Is(0);

            var result = state.SetBoardTips(null).boardTips;

            result.IsNotNull();
            result.Count().Is(0);
        }

        [Test]
        public static void SetThisTimeActorTest_正常動作_行動者が状態に含まれる()
        {
            var name1 = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれる)}_1";
            var name2 = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれる)}_2";
            var name3 = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれる)}_3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new[] { actor1, actor2, actor3 };
            var state = BattleStateMock.Generate(actorList);

            var result = state.SetThisTimeActor(actor2);

            result.IsNotNull();
            result.thisTimeActor.IsSameReferenceAs(actor2);
        }
        [Test]
        public static void SetThisTimeActorTest_正常動作_行動者が状態に含まれない()
        {
            var name1 = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれない)}_1";
            var name2 = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれない)}_2";
            var name3 = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれない)}_3";
            var name4 = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれない)}_4";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var actor4 = BattleActorMock.Generate(name4);

            var actorList = new[] { actor1, actor2, actor3 };
            var state = BattleStateMock.Generate(actorList);

            var result = state.SetThisTimeActor(actor4);

            result.IsNotNull();
            result.thisTimeActor.IsNull();
        }
        [Test]
        public static void SetThisTimeActorTest_行動者無し()
        {
            var name = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれる)}_1";
            var actor = BattleActorMock.Generate(name);

            var state = BattleStateMock.Generate(new BattleActor[] { });

            var result = state.SetThisTimeActor(actor);

            result.IsNotNull();
            result.thisTimeActor.IsNull();
        }
        [Test]
        public static void SetThisTimeActorTest_行動者Null()
        {
            var name = $"{nameof(SetThisTimeActorTest_正常動作_行動者が状態に含まれる)}_1";
            var actor = BattleActorMock.Generate(name);

            var state = BattleStateMock.Generate((List<BattleActor>)null);

            var result = state.SetThisTimeActor(actor);

            result.IsNotNull();
            result.thisTimeActor.IsNull();
        }
    }
}
