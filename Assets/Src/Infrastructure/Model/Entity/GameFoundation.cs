using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;

namespace Assets.Src.Infrastructure.Model.Entity
{
    /// <summary>
    /// ゲーム基盤クラス
    /// シングルトンとして中身を保持する
    /// </summary>
    public partial class GameFoundation : IDuplicatable<GameFoundation>
    {
        /// <summary>
        /// メインUIのビューオブジェクトの名称
        /// </summary>
        const string MAIN_UI_NAME = "MainUI";

        /// <summary>
        /// 現在有効なゲーム基盤の実体
        /// </summary>
        public static GameFoundation myself { get; private set; } = null;

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
        GameFoundation(GameState state, View viewRoot)
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
            var mainUiView = View.CleateNew(MAIN_UI_NAME);
            if(myself == null) return myself = myself ?? new GameFoundation(state, mainUiView);
            myself.nowState = state;
            return myself;
        }
        /// <summary>
        /// 新規ゲーム基盤生成メソッド
        /// </summary>
        /// <param name="randamSeed">乱数の種</param>
        /// <returns>生成されたゲーム基盤</returns>
        public static GameFoundation CreateNewState(int randamSeed) => CreateNewState(new GameState(randamSeed));

        /// <summary>
        /// パラメータ一括アクセス用プロパティ
        /// </summary>
        public GameState nowState { get; set; }

        /// <summary>
        /// 画面表示用オブジェクトルート
        /// </summary>
        public View viewRoot { get; set; }

        /// <summary>
        /// シャローコピーメソッド
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        public GameFoundation MemberwiseClonePublic() => (GameFoundation)MemberwiseClone();
    }
}
