using Assets.Src.Domain.Model.Value;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// <see cref="Energy"/>のプロパティのような振る舞いをする値取得メソッド群
    /// </summary>
    public static class EnergyProperties
    {
        /// <summary>
        /// 名称取得
        /// </summary>
        /// <param name="energy">取得元オブジェクト</param>
        /// <returns>名称文字列</returns>
        public static string GetName(this Energy energy)
        {
            switch(energy)
            {
                case Energy.FLAME: return Constants.Energy.Name.FLAME;
                case Energy.ICE: return Constants.Energy.Name.ICE;
                case Energy.WIND: return Constants.Energy.Name.WIND;
                case Energy.GRAVITY: return Constants.Energy.Name.GRAVITY;
                case Energy.LIGHT: return Constants.Energy.Name.LIGHT;
                case Energy.DARKNESS: return Constants.Energy.Name.DARKNESS;
                case Energy.THUNDER: return Constants.Energy.Name.THUNDER;
                case Energy.EARTH: return Constants.Energy.Name.EARTH;
                case Energy.LIFE: return Constants.Energy.Name.LIFE;
                case Energy.POISON: return Constants.Energy.Name.POISON;
                case Energy.SLASH: return Constants.Energy.Name.SLASH;
                case Energy.BLOW: return Constants.Energy.Name.BLOW;
                case Energy.IMPACT: return Constants.Energy.Name.IMPACT;
                case Energy.PIERCING: return Constants.Energy.Name.PIERCING;
                default: throw new ArgumentOutOfRangeException(nameof(energy));
            }
        }
        /// <summary>
        /// 色取得
        /// </summary>
        /// <param name="energy">取得元オブジェクト</param>
        /// <returns>名称文字列</returns>
        public static Color32 GetColor(this Energy energy)
        {
            switch(energy)
            {
                case Energy.FLAME: return Constants.Energy.Color.FLAME;
                case Energy.ICE: return Constants.Energy.Color.ICE;
                case Energy.WIND: return Constants.Energy.Color.WIND;
                case Energy.GRAVITY: return Constants.Energy.Color.GRAVITY;
                case Energy.LIGHT: return Constants.Energy.Color.LIGHT;
                case Energy.DARKNESS: return Constants.Energy.Color.DARKNESS;
                case Energy.THUNDER: return Constants.Energy.Color.THUNDER;
                case Energy.EARTH: return Constants.Energy.Color.EARTH;
                case Energy.LIFE: return Constants.Energy.Color.LIFE;
                case Energy.POISON: return Constants.Energy.Color.POISON;
                case Energy.SLASH: return Constants.Energy.Color.SLASH;
                case Energy.BLOW: return Constants.Energy.Color.BLOW;
                case Energy.IMPACT: return Constants.Energy.Color.IMPACT;
                case Energy.PIERCING: return Constants.Energy.Color.PIERCING;
                default: throw new ArgumentOutOfRangeException(nameof(energy));
            }
        }
    }
}
