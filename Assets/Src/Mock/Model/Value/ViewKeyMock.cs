using Assets.Src.Domain.Model.Value;
using System;
using UnityEngine;

namespace Assets.Src.Mock.Model.Value
{
    public class ViewKeyMock : IViewKey, IEquatable<ViewKeyMock>
    {
        protected ViewKeyMock(int value)
        {
            this.value = value;
        }
        public int value { get; }

        public ulong hashCode => (ulong)GetHashCode();

        public Vector2 position => throw new NotImplementedException();

        public static ViewKeyMock Generate(int value = 0) => new ViewKeyMock(value);

        public override bool Equals(object other) => other is ViewKeyMock viewValue ? Equals(viewValue) : false;
        public bool Equals(ViewKeyMock other) => value == other?.value;
        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(ViewKeyMock mock1, ViewKeyMock mock2)
            => mock1?.Equals(mock2) ?? mock2 is null;
        public static bool operator !=(ViewKeyMock mock1, ViewKeyMock mock2)
            => !(mock1 == mock2);
    }
}
