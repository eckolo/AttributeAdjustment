using Assets.Src.Domain.Repository;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 音量関連のパラメータ
    /// </summary>
    [Serializable]
    public class VolumeConfigs
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VolumeConfigs() { }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="bgm">BGM音量</param>
        /// <param name="se">SE音量</param>
        public VolumeConfigs(float bgm, float se)
        {
            _bgm = bgm;
            _se = se;
        }

        /// <summary>
        /// BGM音量
        /// </summary>
        [SerializeField]
        float _bgm = Constants.Volume.BGM_DEFAULT;
        /// <summary>
        /// BGM音量
        /// </summary>
        public float bgm => _bgm;

        /// <summary>
        /// SE音量
        /// </summary>
        [SerializeField]
        float _se = Constants.Volume.SE_DEFAULT;
        /// <summary>
        /// SE音量
        /// </summary>
        public float se => _se;
    }
}
