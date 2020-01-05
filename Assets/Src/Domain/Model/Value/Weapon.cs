using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Value
{
    public class Weapon
    {
        public IEnumerable<Ability> abilities { get; }
    }
}
