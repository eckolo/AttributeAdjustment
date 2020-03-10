using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Repository
{
    /// <summary>
    /// <see cref="ViewDeployment"/>に関する固定パラメータ取得元
    /// </summary>
    public static class ViewDeploymentProperties
    {
        /// <summary>
        /// 状態系オブジェクトに対する自身の配置先
        /// </summary>
        public static ViewDeployment stateMyself => new ViewDeployment(Vector2.zero, -1);
    }
}
