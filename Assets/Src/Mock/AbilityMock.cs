using Assets.Src.Domain.Model.Value;

namespace Assets.Src.Mock
{
    public class AbilityMock: Ability
    {
        AbilityMock(string name) : base(name) { }

        public static AbilityMock Generate(string name) => new AbilityMock(name);
    }
}
