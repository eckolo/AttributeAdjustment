namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// ログのトレースレベル
    /// </summary>
    public enum LogHub
    {
        /// <summary>
        /// 状況を追跡するためのログ
        /// </summary>
        TRACE,
        /// <summary>
        /// 開発時に内部的な値などを追うためのログ
        /// </summary>
        DEBUG,
        /// <summary>
        /// 例外投げないけど実質不正動作ですログ
        /// </summary>
        ERROR
    }
}
