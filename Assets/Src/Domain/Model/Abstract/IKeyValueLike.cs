namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// インスペクタから管理するための辞書型の要素っぽいインターフェース
    /// </summary>
    /// <typeparam name="TKey">キー型</typeparam>
    /// <typeparam name="TValue">内容型</typeparam>
    public interface IKeyValueLike<TKey, TValue>
    {
        /// <summary>
        /// キーとなる値
        /// </summary>
        TKey key { get; }
        /// <summary>
        /// 内容値
        /// </summary>
        TValue value { get; }
    }
}
