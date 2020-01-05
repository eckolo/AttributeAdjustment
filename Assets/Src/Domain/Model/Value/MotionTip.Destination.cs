using Assets.Src.Domain.Model.Entity;

namespace Assets.Src.Domain.Model.Value
{
    public partial class MotionTip
    {
        /// <summary>
        /// <see cref="MotionTip"/>の移動先パターン
        /// </summary>
        public enum Destination
        {
            /// <summary>
            /// <see cref="BattleState.deckTips"/>
            /// </summary>
            DECK,
            /// <summary>
            /// <see cref="BattleState.boardTips"/>
            /// </summary>
            BOARD,
        }
    }
}
