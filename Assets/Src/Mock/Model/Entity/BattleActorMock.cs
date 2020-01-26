using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Value;

namespace Assets.Src.Mock.Model.Entity
{
    public partial class BattleActorMock : BattleActor
    {
        BattleActorMock(string name) : base(name) { }
        BattleActorMock(Actor origin) : base(origin) { }

        public static BattleActorMock Generate(string name) => new BattleActorMock(name);

        public static BattleActorMock Generate(Actor origin) => new BattleActorMock(origin);

        public static BattleActorMock Generate(Parameter parameter)
        {
            var engineMock = PartsEngineMock.Generate(power: parameter.offense);
            var flameMock = PartsFlameMock.Generate(armor: parameter.maxVitality, speed: parameter.speed);
            return new BattleActorMock(nameof(BattleActorMock))
            {
                engine = engineMock,
                flame = flameMock,
            };
        }
    }
}
