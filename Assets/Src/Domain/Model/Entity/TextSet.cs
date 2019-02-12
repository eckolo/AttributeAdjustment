﻿using Assets.Src.Domain.Model.Abstract;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 複数テキスト一括制御+テキスト背景画像
    /// </summary>
    public class TextSet : PrefabAbst
    {
        /// <summary>
        /// テキスト群
        /// </summary>
        public IEnumerable<TextMesh> texts { get; set; }

        /// <summary>
        /// 色設定
        /// </summary>
        public Color32 color
        {
            set {
                foreach(var text in texts) text.color = value;
            }
        }
    }
}
