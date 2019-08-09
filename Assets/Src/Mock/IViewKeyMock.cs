using Assets.Src.Domain.Model.Abstract;
using System;

namespace Assets.Src.Mock
{
    public class IViewKeyMock : IViewKey, IEquatable<IViewKeyMock>
    {
        public IViewKeyMock(int value)
        {
            this.value = value;
        }
        public int value { get; }
        public static IViewKeyMock Generate(int value = 0) => new IViewKeyMock(value);

        public override bool Equals(object other) => other is IViewKeyMock viewValue ? Equals(viewValue) : false;
        public bool Equals(IViewKeyMock other) => value == other?.value;
        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(IViewKeyMock mock1, IViewKeyMock mock2)
            => mock1?.Equals(mock2) ?? mock2 is null;
        public static bool operator !=(IViewKeyMock mock1, IViewKeyMock mock2)
            => !(mock1 == mock2);
    }
}
