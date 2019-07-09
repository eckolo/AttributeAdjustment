using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using NUnit.Framework;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    public static class ActorTest
    {
        /// <summary>
        /// <see cref="Actor.Parameter"/>クラスのテスト
        /// </summary>
        public static class ParameterTest
        {
            [Test]
            public static void NegativeValueTest_正常動作()
            {
                var parameter = new Actor.Parameter(120, 20, -110, 5, -43);

                var result = -parameter;

                result.IsNotNull();
                result.maxVitality.Is(-parameter.maxVitality);
                result.vitality.Is(-parameter.vitality);
                result.offense.Is(-parameter.offense);
                result.defense.Is(-parameter.defense);
                result.speed.Is(-parameter.speed);
            }
            [Test]
            public static void NegativeValueTest_NULL()
            {
                var parameter = (Actor.Parameter)null;

                var result = -parameter;

                result.IsNull();
            }

            [Test]
            public static void AdditionTest_正常動作()
            {
                var parameter1 = new Actor.Parameter(120, 20, -110, 5, -43);
                var parameter2 = new Actor.Parameter(18, -32, 65, -3, 107);

                var result = parameter1 + parameter2;

                result.IsNotNull();
                result.maxVitality.Is(parameter1.maxVitality + parameter2.maxVitality);
                result.vitality.Is(parameter1.vitality + parameter2.vitality);
                result.offense.Is(parameter1.offense + parameter2.offense);
                result.defense.Is(parameter1.defense + parameter2.defense);
                result.speed.Is(parameter1.speed + parameter2.speed);
            }
            [Test]
            public static void AdditionTest_NULL_前方()
            {
                var parameter1 = (Actor.Parameter)null;
                var parameter2 = new Actor.Parameter(18, -32, 65, -3, 107);

                var result = parameter1 + parameter2;

                result.IsNotNull();
                result.maxVitality.Is(parameter2.maxVitality);
                result.vitality.Is(parameter2.vitality);
                result.offense.Is(parameter2.offense);
                result.defense.Is(parameter2.defense);
                result.speed.Is(parameter2.speed);
            }
            [Test]
            public static void AdditionTest_NULL_後方()
            {
                var parameter1 = new Actor.Parameter(120, 20, -110, 5, -43);
                var parameter2 = (Actor.Parameter)null;

                var result = parameter1 + parameter2;

                result.IsNotNull();
                result.maxVitality.Is(parameter1.maxVitality);
                result.vitality.Is(parameter1.vitality);
                result.offense.Is(parameter1.offense);
                result.defense.Is(parameter1.defense);
                result.speed.Is(parameter1.speed);
            }
            [Test]
            public static void AdditionTest_NULL_両方()
            {
                var parameter1 = (Actor.Parameter)null;
                var parameter2 = (Actor.Parameter)null;

                var result = parameter1 + parameter2;

                result.IsNull();
            }

            [Test]
            public static void SubtractionTest_正常動作()
            {
                var parameter1 = new Actor.Parameter(120, 20, -110, 5, -43);
                var parameter2 = new Actor.Parameter(18, -32, 65, -3, 107);

                var result = parameter1 - parameter2;

                result.IsNotNull();
                result.maxVitality.Is(parameter1.maxVitality - parameter2.maxVitality);
                result.vitality.Is(parameter1.vitality - parameter2.vitality);
                result.offense.Is(parameter1.offense - parameter2.offense);
                result.defense.Is(parameter1.defense - parameter2.defense);
                result.speed.Is(parameter1.speed - parameter2.speed);
            }
            [Test]
            public static void SubtractionTest_NULL_前方()
            {
                var parameter1 = (Actor.Parameter)null;
                var parameter2 = new Actor.Parameter(18, -32, 65, -3, 107);

                var result = parameter1 - parameter2;

                result.IsNotNull();
                result.maxVitality.Is(-parameter2.maxVitality);
                result.vitality.Is(-parameter2.vitality);
                result.offense.Is(-parameter2.offense);
                result.defense.Is(-parameter2.defense);
                result.speed.Is(-parameter2.speed);
            }
            [Test]
            public static void SubtractionTest_NULL_後方()
            {
                var parameter1 = new Actor.Parameter(120, 20, -110, 5, -43);
                var parameter2 = (Actor.Parameter)null;

                var result = parameter1 - parameter2;

                result.IsNotNull();
                result.maxVitality.Is(parameter1.maxVitality);
                result.vitality.Is(parameter1.vitality);
                result.offense.Is(parameter1.offense);
                result.defense.Is(parameter1.defense);
                result.speed.Is(parameter1.speed);
            }
            [Test]
            public static void SubtractionTest_NULL_両方()
            {
                var parameter1 = (Actor.Parameter)null;
                var parameter2 = (Actor.Parameter)null;

                var result = parameter1 - parameter2;

                result.IsNull();
            }
        }
    }
}
