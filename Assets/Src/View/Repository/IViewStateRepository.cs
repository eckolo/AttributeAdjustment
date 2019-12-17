using Assets.Src.Domain.Model.Abstract;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.View.Repository
{
    public interface IViewStateRepository : IViewRepository<ViewStateKey, ViewState>
    {
        ViewState SearchOrGenerate<TKey>(TKey key) where TKey : ViewStateKey;
    }
}
