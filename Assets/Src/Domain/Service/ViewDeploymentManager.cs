using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    public static class ViewDeploymentManager
    {
        public static Vector2 ToPosition(this ViewDeployment deployment)
        {
            //TODO pivot加味したロジック考案する
            return deployment.pivotGap;
        }
    }
}
