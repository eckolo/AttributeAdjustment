using Assets.Src.Domain.Model.Entity;

namespace Assets.Src.Mock
{
    public class BattleActorMock : BattleActor
    {
        BattleActorMock(string name) : base(name) { }

        public static BattleActorMock Generate(string name) => new BattleActorMock(name);
    }
}
