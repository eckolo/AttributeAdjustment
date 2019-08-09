namespace Assets.Src.Domain.Model.Value
{
    public partial class ViewAction
    {
        /// <summary>
        /// 動作タイプ
        /// </summary>
        public enum Pattern
        {
            /// <summary>
            /// 生成処理
            /// </summary>
            GENERATE,
            /// <summary>
            /// ターゲット指定更新処理
            /// </summary>
            UPDATE,
            /// <summary>
            /// 削除処理
            /// </summary>
            DELETE,
        }
    }
}
