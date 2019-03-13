using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    public static partial class Constants
    {
        public static partial class Energy
        {
            /// <summary>
            /// 対応カラー
            /// </summary>
            public static class Color
            {
                public static readonly Color32 FLAME = new Color32(r: 255, g: 000, b: 000, a: 255);//赤
                public static readonly Color32 ICE = new Color32(r: 000, g: 000, b: 255, a: 255);//青
                public static readonly Color32 WIND = new Color32(r: 000, g: 255, b: 000, a: 255);//黄緑
                public static readonly Color32 GRAVITY = new Color32(r: 120, g: 120, b: 120, a: 255);//灰色
                public static readonly Color32 LIGHT = new Color32(r: 255, g: 255, b: 255, a: 255);//白
                public static readonly Color32 DARKNESS = new Color32(r: 000, g: 000, b: 000, a: 255);//黒
                public static readonly Color32 THUNDER = new Color32(r: 255, g: 255, b: 000, a: 255);//黄色
                public static readonly Color32 EARTH = new Color32(r: 120, g: 000, b: 000, a: 255);//茶色
                public static readonly Color32 LIFE = new Color32(r: 000, g: 120, b: 000, a: 255);//緑
                public static readonly Color32 POISON = new Color32(r: 120, g: 000, b: 120, a: 255);//紫
                public static readonly Color32 SLASH = new Color32(r: 000, g: 255, b: 255, a: 255);//水色
                public static readonly Color32 BLOW = new Color32(r: 120, g: 120, b: 000, a: 255);//黄土色
                public static readonly Color32 IMPACT = new Color32(r: 000, g: 120, b: 120, a: 255);//青緑
                public static readonly Color32 PIERCING = new Color32(r: 000, g: 000, b: 120, a: 255);//紺
            }
        }
    }
}
