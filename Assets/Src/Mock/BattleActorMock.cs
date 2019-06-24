using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;

namespace Assets.Src.Mock
{
    public partial class BattleActorMock : BattleActor
    {
        BattleActorMock(string name) : base(name) { }
        BattleActorMock(Actor origin) : base(origin) { }

        public static BattleActorMock Generate(string name) => new BattleActorMock(name);

        public static BattleActorMock Generate(Actor origin) => new BattleActorMock(origin);
    }
}
