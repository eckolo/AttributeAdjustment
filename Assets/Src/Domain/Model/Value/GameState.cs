using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// ゲーム状態クラス
    /// </summary>
    [Serializable]
    public partial class GameState : IDuplicatable<GameState>
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
        Configs _configs = new Configs();
        /// <summary>
        /// ゲーム設定
        /// </summary>
        public Configs configs { get { return _configs; } set { _configs = value; } }

        /// <summary>
        /// 現在の外部入力可否
        /// </summary>
        [SerializeField]
        bool _recievable = true;
        /// <summary>
        /// 現在の外部入力可否
        /// </summary>
        public bool recievable { get { return _recievable; } set { _recievable = value; } }

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
