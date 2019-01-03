using Assets.Src.Domain.Model.Abstract;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// オブジェクト複製クラス
    /// </summary>
    public static class DuplicateManager
    {
        /// <summary>
        /// 複製可能なオブジェクトの複製を行う
        /// Nullチェック付き
        /// </summary>
        /// <typeparam name="Duplicated">複製されるオブジェクト型</typeparam>
        /// <param name="origin">複製される雛形オブジェクト</param>
        /// <returns>複製されたオブジェクト</returns>
        public static Duplicated Duplicate<Duplicated>(this IDuplicatable<Duplicated> origin)
            where Duplicated : IDuplicatable<Duplicated>
            => origin != null ? origin.MemberwiseClonePublic() : default;

        /// <summary>
        /// 完全複製可能なオブジェクトの完全複製を行う
        /// Nullチェック付き
        /// </summary>
        /// <typeparam name="Duplicated">複製されるオブジェクト型</typeparam>
        /// <param name="origin">複製される雛形オブジェクト</param>
        /// <returns>複製されたオブジェクト</returns>
        public static Duplicated DuplicateFull<Duplicated>(this Duplicated origin)
            => JsonUtility.FromJson<Duplicated>(JsonUtility.ToJson(origin));
    }
}
