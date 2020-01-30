using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using Assets.Src.Domain.Service;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.Mock.Model.Value;
using NUnit.Framework;
using System;
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
        public static void ReloadHandTipsTest_通常処理_元の手札は空()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var battleActor = BattleActorMock.Generate(defaultHandTipMap: tipMap);

            var result = battleActor.ReloadHandTips();

            result.IsNotNull();
            result.IsSameReferenceAs(battleActor);

            result.state.IsNotNull();
            result.state.IsSameReferenceAs(battleActor.state);
            result.state.handTips.IsNotNull();
            result.state.handTips.Count().Is(value1 + value2 + value3);
            result.state.handTips.Count(tip => tip.hashCode == tip1.hashCode).Is(value1);
            result.state.handTips.Count(tip => tip.hashCode == tip2.hashCode).Is(value2);
            result.state.handTips.Count(tip => tip.hashCode == tip3.hashCode).Is(value3);
        }
        [Test]
        public static void ReloadHandTipsTest_通常処理_元の手札有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tip4 = MotionTipMock.Generate(Energy.ICE, 10);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var battleActor = BattleActorMock.Generate(defaultHandTipMap: tipMap);

            var tipList = new List<MotionTip> { tip2, tip3, tip3, tip4 };
            battleActor.state.AddHandTips(tipList);

            var result = battleActor.ReloadHandTips();

            result.IsNotNull();
            result.IsSameReferenceAs(battleActor);

            result.state.IsNotNull();
            result.state.IsSameReferenceAs(battleActor.state);
            result.state.handTips.IsNotNull();
            result.state.handTips.Count().Is(value1 + value2 + value3);
            result.state.handTips.Count(tip => tip.hashCode == tip1.hashCode).Is(value1);
            result.state.handTips.Count(tip => tip.hashCode == tip2.hashCode).Is(value2);
            result.state.handTips.Count(tip => tip.hashCode == tip3.hashCode).Is(value3);
        }
        [Test]
        public static void ReloadHandTipsTest_行動主体がNull()
        {
            Assert.Throws<ArgumentNullException>(() => ((BattleActor)null).ReloadHandTips());
        }

        [Test]
        public static void GetEnergyIncreaseTest()
        {
            var actor = BattleActorMock.Generate(new Actor($"{nameof(Actor)}_{nameof(GetEnergyIncreaseTest)}"));
            actor.parameterVariable = new Actor.Parameter(0, 0, 0, speed: 12);

            var tips = new[] {
                MotionTipMock.Generate(Energy.DARKNESS, 1),
                MotionTipMock.Generate(Energy.BLOW, 2),
            };
            actor.state = BattleActorMock.StateMock.Generate(new List<MotionTip>(), tips);

            var result = actor.GetEnergyIncrease();

            result.Is(Constants.Battle.ENERGY_NORM + 12 - 2);
        }
    }
}
