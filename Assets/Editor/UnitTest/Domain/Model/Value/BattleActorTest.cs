using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    public static partial class BattleActorTest
    {
        [Test]
        public static void ConstructorTest_Actor()
        {
            var abilityList = new List<Ability> {
                AbilityMock.Generate($"{nameof(AbilityMock)}_{nameof(ConstructorTest_Actor)}_0"),
                AbilityMock.Generate($"{nameof(AbilityMock)}_{nameof(ConstructorTest_Actor)}_1"),
            };
            var experience = 1;
            var vitality = 3;

            var maxVitality = 2;
            var offense = 4;
            var defense = 5;
            var speed = 6;

            var actor = new Actor($"{nameof(Actor)}_{nameof(ConstructorTest_Actor)}")
            {
                abilityList = abilityList,
                experience = experience,
                vitality = vitality,
                parameter = new Actor.Parameter(maxVitality, offense, defense, speed),
            };

            var result = new BattleActor(actor);

            result.IsNotNull();
            result.abilityList.IsNotNull();
            result.abilityList.Count.Is(abilityList.Count);
            result.abilityList[0].IsSameReferenceAs(abilityList[0]);
            result.abilityList[1].IsSameReferenceAs(abilityList[1]);
            result.experience.Is(experience);
            result.vitality.Is(vitality);
            result.parameter.maxVitality.Is(maxVitality);
            result.parameter.offense.Is(offense);
            result.parameter.defense.Is(defense);
            result.parameter.speed.Is(speed);
            result.isPlayer.Is(false);
        }
        [Test]
        public static void ConstructorTest_Player()
        {
            var abilityList = new List<Ability> {
                AbilityMock.Generate($"{nameof(AbilityMock)}_{nameof(ConstructorTest_Player)}_0"),
                AbilityMock.Generate($"{nameof(AbilityMock)}_{nameof(ConstructorTest_Player)}_1"),
            };
            var experience = 1;
            var vitality = 3;

            var maxVitality = 2;
            var offense = 4;
            var defense = 5;
            var speed = 6;

            var actor = new Player($"{nameof(Player)}_{nameof(ConstructorTest_Player)}")
            {
                abilityList = abilityList,
                experience = experience,
                vitality = vitality,
                parameter = new Actor.Parameter(maxVitality, offense, defense, speed),
            };

            var result = new BattleActor(actor);

            result.IsNotNull();
            result.abilityList.IsNotNull();
            result.abilityList.Count.Is(abilityList.Count);
            result.abilityList[0].IsSameReferenceAs(abilityList[0]);
            result.abilityList[1].IsSameReferenceAs(abilityList[1]);
            result.experience.Is(experience);
            result.vitality.Is(vitality);
            result.parameter.maxVitality.Is(maxVitality);
            result.parameter.offense.Is(offense);
            result.parameter.defense.Is(defense);
            result.parameter.speed.Is(speed);
            result.isPlayer.Is(true);
        }

        [Test]
        public static void MaxVitalityTest()
        {
            const string MEMBER_NAME = "_parameterAdjust";
            var maxVitality = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(MaxVitalityTest))
            {
                parameter = new Actor.Parameter(maxVitality, 0, 0, 0),
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.parameter.maxVitality.Is(maxVitality);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).maxVitality.Is(0);

            battleActor.parameter += new Actor.Parameter(maxVitality: 10, 0, 0, 0);

            battleActor.parameter.maxVitality.Is(maxVitality + 10);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).maxVitality.Is(0 + 10);

            battleActor.parameter -= new Actor.Parameter(maxVitality: 100, 0, 0, 0);

            battleActor.parameter.maxVitality.Is(maxVitality + 10 - 100);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).maxVitality.Is(0 + 10 - 100);
        }
        [Test]
        public static void OffenseTest()
        {
            const string MEMBER_NAME = "_parameterAdjust";
            var offense = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(OffenseTest))
            {
                parameter = new Actor.Parameter(0, offense, 0, 0),
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.parameter.offense.Is(offense);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).offense.Is(0);

            battleActor.parameter += new Actor.Parameter(0, offense: 10, 0, 0);

            battleActor.parameter.offense.Is(offense + 10);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).offense.Is(0 + 10);

            battleActor.parameter -= new Actor.Parameter(0, offense: 100, 0, 0);

            battleActor.parameter.offense.Is(offense + 10 - 100);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).offense.Is(0 + 10 - 100);
        }
        [Test]
        public static void DefenseTest()
        {
            const string MEMBER_NAME = "_parameterAdjust";
            var defense = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(DefenseTest))
            {
                parameter = new Actor.Parameter(0, 0, defense, 0),
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.parameter.defense.Is(defense);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).defense.Is(0);

            battleActor.parameter += new Actor.Parameter(0, 0, defense: 10, 0);

            battleActor.parameter.defense.Is(defense + 10);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).defense.Is(0 + 10);

            battleActor.parameter -= new Actor.Parameter(0, 0, defense: 100, 0);

            battleActor.parameter.defense.Is(defense + 10 - 100);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).defense.Is(0 + 10 - 100);
        }
        [Test]
        public static void SpeedTest()
        {
            const string MEMBER_NAME = "_parameterAdjust";
            var speed = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(SpeedTest))
            {
                parameter = new Actor.Parameter(0, 0, 0, speed),
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.parameter.speed.Is(speed);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).speed.Is(0);

            battleActor.parameter += new Actor.Parameter(0, 0, 0, speed: 10);

            battleActor.parameter.speed.Is(speed + 10);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).speed.Is(0 + 10);

            battleActor.parameter -= new Actor.Parameter(0, 0, 0, speed: 100);

            battleActor.parameter.speed.Is(speed + 10 - 100);
            ((Actor.Parameter)fieldInfo.GetValue(battleActor)).speed.Is(0 + 10 - 100);
        }
    }
}
