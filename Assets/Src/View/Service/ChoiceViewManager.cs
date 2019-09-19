using Assets.Src.Domain.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx.Async;

namespace Assets.Src.View.Service
{
    public static class ChoiceViewManager
    {
        public static async UniTask<ChoiceState> Indicate(this ChoiceState state)
        {
            throw new NotImplementedException(nameof(Indicate));
        }
    }
}
