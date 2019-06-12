using Assets.Src.Domain.Model.Abstract;
using System;

namespace Assets.Src.Mock
{
    public class ViewValueMock : IViewValue, IEquatable<ViewValueMock>
    {
        public ViewValueMock(int value)
        {
            this.value = value;
        }
        public int value { get; }
        public static ViewValueMock Generate(int value = 0) => new ViewValueMock(value);

        public override bool Equals(object other) => other is ViewValueMock viewValue ? Equals(viewValue) : false;
        public bool Equals(ViewValueMock other) => value == other?.value;
        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(ViewValueMock mock1, ViewValueMock mock2)
            => mock1?.Equals(mock2) ?? mock2 is null;
        public static bool operator !=(ViewValueMock mock1, ViewValueMock mock2)
            => !(mock1 == mock2);
    }
}
