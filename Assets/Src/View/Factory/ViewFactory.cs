using Assets.Src.Domain.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.View.Factory
{
    public static class ViewFactory
    {
        public static TState UpdateView<TState>(this TState state) where TState : ViewStateAbst
        {
            return state;
        }
    }
}
