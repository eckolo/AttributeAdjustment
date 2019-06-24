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
    }
}
