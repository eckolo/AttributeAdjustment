using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Repository
{
    public interface IComponentRepository : IViewRepository<(ViewDeployment deploy, IViewKey view), Component>
    {
        Component Pop((ViewDeployment deploy, IViewKey view) key);
    }
}
