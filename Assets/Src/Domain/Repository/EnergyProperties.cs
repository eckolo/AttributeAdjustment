using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Assets.Src.Domain.Service;

namespace Assets.Src.Domain.Repository
{
    /// <summary>
    /// <see cref="Energy"/>のプロパティのような振る舞いをする値取得メソッド群
    /// </summary>
    public static class EnergyProperties
    {
        /// <summary>
        /// 名称
        /// </summary>
        static readonly Dictionary<Energy, string> nameMap = new Dictionary<Energy, string>
        {
            { Energy.FLAME, "炎" },
            { Energy.ICE, "氷" },
            { Energy.WIND, "風" },
            { Energy.GRAVITY, "重" },
            { Energy.LIGHT, "光" },
            { Energy.DARKNESS, "闇" },
            { Energy.THUNDER, "雷" },
            { Energy.EARTH, "地" },
            { Energy.LIFE, "命" },
            { Energy.POISON, "毒" },
            { Energy.SLASH, "斬" },
            { Energy.BLOW, "打" },
            { Energy.IMPACT, "衝" },
            { Energy.PIERCING, "突" },
        };
        /// <summary>
        /// 名称取得
        /// </summary>
        /// <param name="energy">取得元オブジェクト</param>
        /// <returns>名称文字列</returns>
        public static string GetName(this Energy energy)
            => nameMap.GetOrDefault(energy, null) ?? throw new ArgumentOutOfRangeException(nameof(energy));
        /// <summary>
        /// 対応カラー
        /// </summary>
        static readonly Dictionary<Energy, Color32?> colorMap = new Dictionary<Energy, Color32?>
        {
            { Energy.FLAME, new Color32(r: 255, g: 000, b: 000, a: 255) },//赤
            { Energy.ICE, new Color32(r: 000, g: 000, b: 255, a: 255) },//青
            { Energy.WIND, new Color32(r: 000, g: 255, b: 000, a: 255) },//黄緑
            { Energy.GRAVITY, new Color32(r: 120, g: 120, b: 120, a: 255) },//灰色
            { Energy.LIGHT, new Color32(r: 255, g: 255, b: 255, a: 255) },//白
            { Energy.DARKNESS, new Color32(r: 000, g: 000, b: 000, a: 255) },//黒
            { Energy.THUNDER, new Color32(r: 255, g: 255, b: 000, a: 255) },//黄色
            { Energy.EARTH, new Color32(r: 120, g: 000, b: 000, a: 255) },//茶色
            { Energy.LIFE, new Color32(r: 000, g: 120, b: 000, a: 255) },//緑
            { Energy.POISON, new Color32(r: 120, g: 000, b: 120, a: 255) },//紫
            { Energy.SLASH, new Color32(r: 000, g: 255, b: 255, a: 255) },//水色
            { Energy.BLOW, new Color32(r: 120, g: 120, b: 000, a: 255) },//黄土色
            { Energy.IMPACT, new Color32(r: 000, g: 120, b: 120, a: 255) },//青緑
            { Energy.PIERCING, new Color32(r: 000, g: 000, b: 120, a: 255) },//紺
        };
        /// <summary>
        /// 色取得
        /// </summary>
        /// <param name="energy">取得元オブジェクト</param>
        /// <returns>名称文字列</returns>
        public static Color32 GetColor(this Energy energy)
            => colorMap.GetOrDefault(energy, null) ?? throw new ArgumentOutOfRangeException(nameof(energy));
    }
}
