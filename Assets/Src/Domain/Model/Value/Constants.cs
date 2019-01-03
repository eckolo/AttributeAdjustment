using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 定数値クラス
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// 音量関連のパラメータ
        /// </summary>
        public static class Volume
        {
            /// <summary>
            /// BGM音量基礎値
            /// </summary>
            public const float BASE_BGM = 0.01f;
            /// <summary>
            /// BGM音量初期値
            /// </summary>
            public const float BGM_DEFAULT = 50;
            /// <summary>
            /// SE音量基礎値
            /// </summary>
            public const float BASE_SE = 0.01f;
            /// <summary>
            /// SE音量初期値
            /// </summary>
            public const float SE_DEFAULT = 50;

            /// <summary>
            /// 最大音量
            /// </summary>
            public const float MAX = 100;
            /// <summary>
            /// 最小音量
            /// </summary>
            public const float MIN = 0;
        }

        /// <summary>
        /// フェードイン・アウトのデフォルト所要時間
        /// </summary>
        public const int DEFAULT_FADE_TIME = 108;

        /// <summary>
        /// ウィンドウ系関連のパラメータ
        /// </summary>
        public static class Window
        {
            public const int DEFAULT_MOTION_TIME = 48;
            /// <summary>
            /// メインウィンドウの文字表示間隔
            /// </summary>
            public const int MAIN_WINDOW_INTERVAL = 24;
        }

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

        /// <summary>
        /// システムテキスト関連のパラメータ
        /// </summary>
        public static class Texts
        {
            /// <summary>
            /// システムテキストのデフォルト文字サイズ
            /// </summary>
            public const int CHAR_SIZE = 13;
            /// <summary>
            /// システムテキストのデフォルト行間幅
            /// </summary>
            public const float LINE_SPACE = 0.1f;
        }
    }
}
