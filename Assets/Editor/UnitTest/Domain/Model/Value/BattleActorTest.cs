using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.Mock.Model.Value;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    public static partial class BattleActorTest
    {
        [Test]
        public static void MaxVitalityTest()
        {
            const string MEMBER_NAME = "_parameterAdjust";
            var maxVitality = 2;
            var battleActor = BattleActorMock.Generate(new Actor.Parameter(maxVitality, 0, 0, 0));

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.parameterVariable.maxVitality.Is(maxVitality);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).maxVitality.Is(0);

            battleActor.parameterVariable += new Actor.Parameter(maxVitality: 10, 0, 0, 0);

            battleActor.parameterVariable.maxVitality.Is(maxVitality + 10);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).maxVitality.Is(0 + 10);

            battleActor.parameterVariable -= new Actor.Parameter(maxVitality: 100, 0, 0, 0);

            battleActor.parameterVariable.maxVitality.Is(maxVitality + 10 - 100);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).maxVitality.Is(0 + 10 - 100);
        }
        [Test]
        public static void OffenseTest()
        {
            const string MEMBER_NAME = "_parameterAdjust";
            var offense = 2;
            var battleActor = BattleActorMock.Generate(new Actor.Parameter(0, offense, 0, 0));

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.parameterVariable.offense.Is(offense);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).offense.Is(0);

            battleActor.parameterVariable += new Actor.Parameter(0, offense: 10, 0, 0);

            battleActor.parameterVariable.offense.Is(offense + 10);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).offense.Is(0 + 10);

            battleActor.parameterVariable -= new Actor.Parameter(0, offense: 100, 0, 0);

            battleActor.parameterVariable.offense.Is(offense + 10 - 100);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).offense.Is(0 + 10 - 100);
        }
        [Test]
        public static void SpeedTest()
        {
            const string MEMBER_NAME = "_parameterAdjust";
            var speed = 2;
            var battleActor = BattleActorMock.Generate(new Actor.Parameter(0, 0, 0, speed));

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.parameterVariable.speed.Is(speed);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).speed.Is(0);

            battleActor.parameterVariable += new Actor.Parameter(0, 0, 0, speed: 10);

            battleActor.parameterVariable.speed.Is(speed + 10);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).speed.Is(0 + 10);

            battleActor.parameterVariable -= new Actor.Parameter(0, 0, 0, speed: 100);

            battleActor.parameterVariable.speed.Is(speed + 10 - 100);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).speed.Is(0 + 10 - 100);
        }
    }
}
