using Assets.Src.Domain.Model.Value;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Repository
{
    /// <summary>
    /// システム設定値リポジトリ
    /// </summary>
    [Serializable]
    public partial class ConfigsRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigsRepository() { }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">キーコンフィグ対応用可変ボタンコード</param>
        /// <param name="volume">音量関連のパラメータ</param>
        public ConfigsRepository(KeyConfigs key, VolumeConfigs volume)
        {
            _key = key;
            _volume = volume;
        }

        /// <summary>
        /// キーコンフィグ対応用可変ボタンコード
        /// </summary>
        [SerializeField]
        KeyConfigs _key = new KeyConfigs();
        /// <summary>
        /// キーコンフィグ対応用可変ボタンコード
        /// </summary>
        public KeyConfigs key => _key;

        /// <summary>
        /// 音量関連のパラメータ
        /// </summary>
        [SerializeField]
        VolumeConfigs _volume = new VolumeConfigs();
        /// <summary>
        /// 音量関連のパラメータ
        /// </summary>
        public VolumeConfigs volume => _volume;
    }
}
