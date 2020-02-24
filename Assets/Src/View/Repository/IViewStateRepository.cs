using Assets.Src.Domain.Model.Entity;
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
        ViewState SearchOrGenerate(ViewStateKey key);
    }
}
