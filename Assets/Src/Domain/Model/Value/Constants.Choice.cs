namespace Assets.Src.Domain.Model.Value
{
    public static partial class Constants
    {
        /// <summary>
        /// 選択肢系関連のパラメータ
        /// </summary>
        public static class Choice
        {
            /// <summary>
            /// 上下押しっぱなしで連打判定に入るまでの猶予フレーム
            /// </summary>
            public const int KEEP_VERTICAL_LIMIT = 60;
            /// <summary>
            /// 連打判定時の連打間隔フレーム数
            /// </summary>
            public const int KEEP_VERTICAL_INTERVAL = 12;
            /// <summary>
            /// 選択肢ウィンドウアニメーション時間
            /// </summary>
            public const int WINDOW_MOTION_TIME = 48;
            /// <summary>
            /// 選択肢の決定操作時のSE音量補正値
            /// </summary>
            public const float DECISION_SE_VORUME = 0.8f;
            /// <summary>
            /// 選択肢のキャンセル操作時のSE音量補正値
            /// </summary>
            public const float CANCEL_SE_VORUME = 0.8f;
            /// <summary>
            /// 選択肢の選択操作時のSE音量補正値
            /// </summary>
            public const float SETECTING_SE_VORUME = 0.8f;
            /// <summary>
            /// メインメニューの項目最大数
            /// </summary>
            public const int MAX_MENU_CHOICE = 10;
        }
    }
}
