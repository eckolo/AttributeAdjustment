using Assets.Src.Domain.Model.Abstract;

namespace Assets.Src.Domain.Model.Value
{
    public abstract partial class ViewAction
    {
        /// <summary>
        /// 動作タイプ
        /// </summary>
        public enum ActionType
        {
            /// <summary>
            /// 生成処理
            /// </summary>
            GENERATE,
            /// <summary>
            /// ターゲット指定移動処理
            /// </summary>
            MOVE,
            /// <summary>
            /// 削除処理
            /// </summary>
            DELETE,
        }
    }
}
