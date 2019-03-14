using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.Infrastructure.Service;

namespace Assets.Src.Infrastructure.Model.Entity
{
    /// <summary>
    /// ゲーム基盤クラス
    /// シングルトンとして中身を保持する
    /// </summary>
    public partial class GameFoundation : IDuplicatable<GameFoundation>
    {
        /// <summary>
        /// 現在有効なゲーム基盤の実体
        /// </summary>
        public static GameFoundation entity { get; private set; } = null;

        /// <summary>
        /// インジェクション用メソッド定義のために初回生成時のみ起動
        /// </summary>
        static GameFoundation()
        {
        }

        /// <summary>
        /// インスタンス生成用のプライベートなコンストラクタ
        /// </summary>
        /// <param name="state">初期ゲーム基盤</param>
        GameFoundation(GameState state, ViewRoot viewRoot)
        {
            nowState = state ?? nowState.Duplicate();
            viewRoot = viewRoot ?? this.viewRoot;
        }

        /// <summary>
        /// 新規ゲーム基盤生成メソッド
        /// </summary>
        /// <param name="state">初期状態</param>
        /// <returns>生成されたゲーム基盤</returns>
        public static GameFoundation CreateNewState(GameState state)
        {
            var mainUiView = ViewRoot.CleateNew(Constants.MAIN_UI_NAME);
            if(entity == null) return entity = entity ?? new GameFoundation(state, mainUiView);
            entity.nowState = state;
            return entity;
        }
        /// <summary>
        /// 新規ゲーム基盤生成メソッド
        /// </summary>
        /// <param name="randamSeed">乱数の種</param>
        /// <returns>生成されたゲーム基盤</returns>
        public static GameFoundation CreateNewState(int randamSeed)
            => CreateNewState(new GameState(randamSeed, new FileManager()));

        /// <summary>
        /// パラメータ一括アクセス用プロパティ
        /// </summary>
        public GameState nowState { get; set; }

        /// <summary>
        /// 画面表示用オブジェクトルート
        /// </summary>
        public ViewRoot viewRoot { get; set; }

        /// <summary>
        /// シャローコピーメソッド
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        public GameFoundation MemberwiseClonePublic() => (GameFoundation)MemberwiseClone();
    }
}
