using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
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
            if(choiceList is null)
                throw new ArgumentNullException(nameof(choiceList));

            if(!choiceList.ContainsIndex(initialChoiced))
                initialChoiced = 0;

            var state = new ChoiceState(choiceList, initialChoiced)
                .SetNewView(choiceList.ToChoiceText(initialChoiced));

            return state;
        }
    }
}
