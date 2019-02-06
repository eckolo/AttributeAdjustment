using Assets.Src.Domain.Model.Value;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// 戦闘処理制御に関連する処理サービス
    /// </summary>
    public static class BattleManager
    {
        /// <summary>
        /// 戦闘開始処理
        /// </summary>
        /// <param name="state">戦闘開始時の戦闘状態</param>
        /// <returns>戦闘開始処理後の戦闘状態</returns>
        public static BattleState BattleStart(this BattleState state)
        {
            return state.SetupDeck().SetupBoard().SetupAllHandTips();
        }
    }
}
