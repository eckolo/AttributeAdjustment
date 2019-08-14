using System;
using UnityEngine;

namespace Assets.Src.Domain.Repository
{
    /// <summary>
    /// システム設定値
    /// </summary>
    [Serializable]
    public partial class Configs
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Configs() { }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="button">キーコンフィグ対応用可変ボタンコード</param>
        /// <param name="volume">音量関連のパラメータ</param>
        public Configs(Button button, Volume volume)
        {
            _button = button;
            _volume = volume;
        }

        /// <summary>
        /// キーコンフィグ対応用可変ボタンコード
        /// </summary>
        [SerializeField]
        Button _button = new Button();
        /// <summary>
        /// キーコンフィグ対応用可変ボタンコード
        /// </summary>
        public Button button => _button;

        /// <summary>
        /// 音量関連のパラメータ
        /// </summary>
        [SerializeField]
        Volume _volume = new Volume();
        /// <summary>
        /// 音量関連のパラメータ
        /// </summary>
        public Volume volume => _volume;
    }
}
