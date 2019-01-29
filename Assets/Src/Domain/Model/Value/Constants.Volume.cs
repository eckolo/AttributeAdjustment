namespace Assets.Src.Domain.Model.Value
{
    public static partial class Constants
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
    }
}
