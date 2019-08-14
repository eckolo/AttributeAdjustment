using Assets.Src.Domain.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx.Async;

namespace Assets.Src.View.Factory
{
    public static class ChoiceViewFactory
    {
        public static async UniTask<ChoiceState> ToView(this ChoiceState stat)
        {
            throw new NotImplementedException(nameof(ToView));
        }
    }
}
