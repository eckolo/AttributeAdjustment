using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Value;
using System.Collections.Generic;

namespace Assets.Src.Mock.Model.Entity
{
    public partial class BattleActorMock : BattleActor
    {
        BattleActorMock(string name) : base(name) { }
        BattleActorMock(Actor origin) : base(origin) { }

        public static BattleActorMock Generate(string name) => new BattleActorMock(name)
        {
            brain = new PartsBrain(),
            engine = new PartsEngine(),
            flame = new PartsFlame(),
        };

        public static BattleActorMock Generate(Actor origin)
        {
            origin.brain = origin.brain ?? new PartsBrain();
            origin.engine = origin.engine ?? new PartsEngine();
            origin.flame = origin.flame ?? new PartsFlame();
            return new BattleActorMock(origin);
        }

        public static BattleActorMock Generate(Parameter parameter)
        {
            var engineMock = PartsEngineMock.Generate(power: parameter.offense);
            var flameMock = PartsFlameMock.Generate(armor: parameter.maxVitality, speed: parameter.speed);

            return new BattleActorMock(nameof(BattleActorMock))
            {
                brain = new PartsBrain(),
                engine = engineMock,
                flame = flameMock,
            };
        }
        public static BattleActorMock Generate(
            Dictionary<MotionTip, int> defaultDeckTipMap = null,
            Dictionary<MotionTip, int> defaultHandTipMap = null)
        {
            var brainMock = PartsBrainMock
                .Generate(defaultDeckTipMap: defaultDeckTipMap ?? new Dictionary<MotionTip, int>());
            var engineMock = PartsEngineMock
                .Generate(defaultHandTipMap: defaultHandTipMap ?? new Dictionary<MotionTip, int>());

            return new BattleActorMock(nameof(BattleActorMock))
            {
                brain = brainMock,
                engine = engineMock,
                flame = new PartsFlame(),
            };
        }
    }
}
