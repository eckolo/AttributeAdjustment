namespace Assets.Src.Domain.Model.Entity
{
    public partial class BattleState
    {
        /// <summary>
        /// 戦闘状態中のビューアクションのターゲット種別
        /// </summary>
        public enum ActionTarget
        {
            /// <summary>
            /// <see cref="deckTips"/>
            /// </summary>
            DECK,
            /// <summary>
            /// <see cref="boardTips"/>
            /// </summary>
            BOARD,
        }
    }
}
