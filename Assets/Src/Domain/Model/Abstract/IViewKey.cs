﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// ビューオブジェクトが紐づくルート
    /// </summary>
    public interface IViewKey : IHashable
    {
        Vector2 position { get; }
    }
}
