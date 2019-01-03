namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// シャローコピー可能オブジェクトインターフェース
    /// </summary>
    /// <typeparam name="Duplicated">コピー対象オブジェクトクラス</typeparam>
    public interface IDuplicatable<Duplicated> where Duplicated : IDuplicatable<Duplicated>
    {
        /// <summary>
        /// シャローコピーメソッド
        /// 基本的にMemberwiseClone()メソッドをキャストだけしてそのまま返せばOK
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        Duplicated MemberwiseClonePublic();
    }
}
