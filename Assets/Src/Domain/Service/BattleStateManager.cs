using Assets.Src.Domain.Model.Value;
using System.Linq;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// <see cref="BattleState"/>クラスの制御関連サービス
    /// </summary>
    public static class BattleStateManager
    {
        /// <summary>
        /// 山札の初期化
        /// </summary>
        /// <returns>山札の初期化された戦闘状態</returns>
        public static BattleState SetupDeck(this BattleState state)
        {
            var deckStationery = state.deckStationeryMap?
                .SelectMany(tip => Enumerable.Range(0, tip.Value).Select(_ => tip.Key))
                .Shuffle();
            state.SetDeckTip(deckStationery);
            return state;
        }
    }
}
