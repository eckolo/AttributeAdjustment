using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Factory
{
    public static class ChoiceStateFactory
    {
        public static ChoiceState ToChoiceState(this List<string> choiceList, int initialChoiced = 0)
        {
            return new ChoiceState(choiceList, initialChoiced);
        }
    }
}
