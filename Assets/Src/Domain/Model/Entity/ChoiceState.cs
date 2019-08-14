using Assets.Src.Controller;
using Assets.Src.Domain.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Entity
{
    public class ChoiceState : ViewStateAbst, IViewKey, IDisposable
    {
        public ChoiceState(List<string> choiceList, int? choiced)
        {
            this.choiceList = choiceList;
            this.choiced = choiced;
        }

        public List<string> choiceList { get; }
        public int? choiced { get; set; }
        public int keepUpTime { get; set; } = 0;
        public int keepDownTime { get; set; } = 0;
        public bool isFinish { get; set; } = false;

        public async void Dispose() => await this.End();
    }
}
