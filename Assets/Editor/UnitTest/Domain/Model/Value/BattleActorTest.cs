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

            var maxVitality = 2;
            var vitality = 3;
            var offense = 4;
            var defense = 5;
            var speed = 6;

            var actor = new Actor($"{nameof(Actor)}_{nameof(ConstructorTest_Actor)}")
            {
                abilityList = abilityList,
                experience = experience,
                maxVitality = maxVitality,
                vitality = vitality,
                offense = offense,
                defense = defense,
                speed = speed,
            };

            var result = new BattleActor(actor);

            result.IsNotNull();
            result.abilityList.IsNotNull();
            result.abilityList.Count.Is(abilityList.Count);
            result.abilityList[0].IsSameReferenceAs(abilityList[0]);
            result.abilityList[1].IsSameReferenceAs(abilityList[1]);
            result.experience = experience;
            result.maxVitality = maxVitality;
            result.vitality = vitality;
            result.offense = offense;
            result.defense = defense;
            result.speed = speed;
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

            var maxVitality = 2;
            var vitality = 3;
            var offense = 4;
            var defense = 5;
            var speed = 6;

            var actor = new Player($"{nameof(Player)}_{nameof(ConstructorTest_Player)}")
            {
                abilityList = abilityList,
                experience = experience,
                maxVitality = maxVitality,
                vitality = vitality,
                offense = offense,
                defense = defense,
                speed = speed,
            };

            var result = new BattleActor(actor);

            result.IsNotNull();
            result.abilityList.IsNotNull();
            result.abilityList.Count.Is(abilityList.Count);
            result.abilityList[0].IsSameReferenceAs(abilityList[0]);
            result.abilityList[1].IsSameReferenceAs(abilityList[1]);
            result.experience = experience;
            result.maxVitality = maxVitality;
            result.vitality = vitality;
            result.offense = offense;
            result.defense = defense;
            result.speed = speed;
            result.isPlayer.Is(true);
        }

        [Test]
        public static void MaxVitalityTest()
        {
            const string MEMBER_NAME = "_maxVitalityAdjust";
            var maxVitality = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(MaxVitalityTest))
            {
                maxVitality = maxVitality
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.maxVitality.Is(maxVitality);
            fieldInfo.GetValue(battleActor).Is(0);

            battleActor.maxVitality += 10;

            battleActor.maxVitality.Is(maxVitality + 10);
            fieldInfo.GetValue(battleActor).Is(0 + 10);

            battleActor.maxVitality -= 100;

            battleActor.maxVitality.Is(maxVitality + 10 - 100);
            fieldInfo.GetValue(battleActor).Is(0 + 10 - 100);
        }
        [Test]
        public static void OffenseTest()
        {
            const string MEMBER_NAME = "_offenseAdjust";
            var offense = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(OffenseTest))
            {
                offense = offense
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.offense.Is(offense);
            fieldInfo.GetValue(battleActor).Is(0);

            battleActor.offense += 10;

            battleActor.offense.Is(offense + 10);
            fieldInfo.GetValue(battleActor).Is(0 + 10);

            battleActor.offense -= 100;

            battleActor.offense.Is(offense + 10 - 100);
            fieldInfo.GetValue(battleActor).Is(0 + 10 - 100);
        }
        [Test]
        public static void DefenseTest()
        {
            const string MEMBER_NAME = "_defenseAdjust";
            var defense = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(DefenseTest))
            {
                defense = defense
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.defense.Is(defense);
            fieldInfo.GetValue(battleActor).Is(0);

            battleActor.defense += 10;

            battleActor.defense.Is(defense + 10);
            fieldInfo.GetValue(battleActor).Is(0 + 10);

            battleActor.defense -= 100;

            battleActor.defense.Is(defense + 10 - 100);
            fieldInfo.GetValue(battleActor).Is(0 + 10 - 100);
        }
        [Test]
        public static void SpeedTest()
        {
            const string MEMBER_NAME = "_speedAdjust";
            var speed = 2;
            var battleActor = BattleActorMock.Generate(new Actor(nameof(SpeedTest))
            {
                speed = speed
            });

            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            var fieldInfo = typeof(BattleActor).GetField(MEMBER_NAME, bindingFlags);

            battleActor.speed.Is(speed);
            fieldInfo.GetValue(battleActor).Is(0);

            battleActor.speed += 10;

            battleActor.speed.Is(speed + 10);
            fieldInfo.GetValue(battleActor).Is(0 + 10);

            battleActor.speed -= 100;

            battleActor.speed.Is(speed + 10 - 100);
            fieldInfo.GetValue(battleActor).Is(0 + 10 - 100);
        }
    }
}
