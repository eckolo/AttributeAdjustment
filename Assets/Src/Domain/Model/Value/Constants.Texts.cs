using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    public static partial class Constants
    {
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
            /// <summary>
            /// 名無しオブジェクトにつけられる名称
            /// </summary>
            public const string ANONYMOUS_NAME = "NoNameObject";
            /// <summary>
            /// 通常の文字色
            /// </summary>
            public static readonly Color32 DEFAULT_COLOR = Color.black;
        }
    }
}
