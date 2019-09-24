using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Model.Entity
{
    public class ViewRoot : IViewRoot
    {
        public Dictionary<ViewStateKey, ViewState> viewStateMap { get; set; }
             = new Dictionary<ViewStateKey, ViewState>();
    }
}
