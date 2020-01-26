using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Mock.Model.Value
{
    public class PartsFlameMock : PartsFlame
    {
        PartsFlameMock(int armor, int speed, int weaponSlot, IEnumerable<Weapon> weaponsFix)
        {
            this.armor = armor;
            this.speed = speed;
            this.weaponSlot = weaponSlot;
            this.weaponsFix = weaponsFix;
        }

        public static PartsFlameMock Generate(int armor, int speed, int weaponSlot, IEnumerable<Weapon> weaponsFix)
            => new PartsFlameMock(armor, speed, weaponSlot, weaponsFix);
        public static PartsFlameMock Generate(int armor, int speed)
            => new PartsFlameMock(armor, speed, default, Enumerable.Empty<Weapon>());
        public static PartsFlameMock Generate()
            => new PartsFlameMock(default, default, default, Enumerable.Empty<Weapon>());
    }
}
