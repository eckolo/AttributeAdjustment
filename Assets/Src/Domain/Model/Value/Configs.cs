using System;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
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
        /// <param name="buttom">キーコンフィグ対応用可変ボタンコード</param>
        /// <param name="volume">音量関連のパラメータ</param>
        public Configs(Buttom buttom, Volume volume)
        {
            _buttom = buttom;
            _volume = volume;
        }

        /// <summary>
        /// キーコンフィグ対応用可変ボタンコード
        /// </summary>
        [SerializeField]
        Buttom _buttom = new Buttom();
        /// <summary>
        /// キーコンフィグ対応用可変ボタンコード
        /// </summary>
        public Buttom buttom => _buttom;

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
