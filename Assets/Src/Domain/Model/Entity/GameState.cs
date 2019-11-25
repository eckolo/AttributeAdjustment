using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Repository;
using Assets.Src.Domain.Service;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// ゲーム状態クラス
    /// </summary>
    [Serializable]
    public partial class GameState : ViewStateKey, IDuplicatable<GameState>
    {
        public GameState(int seedInt, IFileManager fileManager)
        {
            UnityEngine.Random.InitState(seedInt);
            _seed = UnityEngine.Random.state;
            this.fileManager = fileManager;
        }

        /// <summary>
        /// ゲーム設定
        /// </summary>
        [SerializeField]
        ConfigsRepository _configsRepository = new ConfigsRepository();
        /// <summary>
        /// ゲーム設定
        /// </summary>
        public ConfigsRepository configsRepository
        {
            get => _configsRepository;
            set => _configsRepository = value;
        }

        /// <summary>
        /// プレイヤー設定
        /// </summary>
        [SerializeField]
        Player _player = null;
        /// <summary>
        /// プレイヤー設定
        /// </summary>
        public Player player { get => _player; set => _player = value; }

        /// <summary>
        /// 現在の外部入力可否
        /// </summary>
        [SerializeField]
        bool _recievable = true;
        /// <summary>
        /// 現在の外部入力可否
        /// </summary>
        public bool recievable { get => _recievable; set => _recievable = value; }

        /// <summary>
        /// ファイル操作サービス
        /// </summary>
        public IFileManager fileManager { get; }

        /// <summary>
        /// 乱数の種
        /// </summary>
        [SerializeField]
        readonly UnityEngine.Random.State _seed;

        /// <summary>
        /// シャローコピーメソッド
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        public GameState MemberwiseClonePublic() => (GameState)MemberwiseClone();
    }
}
