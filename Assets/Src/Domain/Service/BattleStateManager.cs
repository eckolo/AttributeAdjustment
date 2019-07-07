using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
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
        /// <param name="state">山札初期化対象の戦闘状態</param>
        /// <returns>山札の初期化された戦闘状態</returns>
        public static BattleState SetupDeck(this BattleState state)
        {
            var deckStationery = state.deckStationeryMap?
                .SelectMany(tip => Enumerable.Range(0, tip.Value).Select(_ => tip.Key))
                .Shuffle();

            var result = state.SetDeckTips(deckStationery);
            return result;
        }
        /// <summary>
        /// 山札の強制取り出し
        /// 足りなければ補充して必要枚数取り出す
        /// </summary>
        /// <param name="popTipNumber">取り出し枚数</param>
        /// <returns>山札から取り出されたモーションチップ一覧</returns>
        public static IEnumerable<MotionTip> PopDeckTipsForced(this BattleState state, int popTipNumber)
            => state.deckTips.Count >= popTipNumber || (state.deckStationeryMap?.Any() ?? false)
                ? Enumerable.Empty<MotionTip>().JoinDeckTips(state, popTipNumber)
                : Enumerable.Empty<MotionTip>();
        static IEnumerable<MotionTip> JoinDeckTips(
            this IEnumerable<MotionTip> tips,
            BattleState state,
            int popTipNumber)
            => (tips.Count() < popTipNumber)
                ? tips.Concat(state.PopDeckTips(popTipNumber - tips.Count())).JoinDeckTips(state, popTipNumber)
                : tips;
        /// <summary>
        /// 場札の初期化
        /// </summary>
        /// <param name="state">場札初期化対象の戦闘状態</param>
        /// <param name="tipNumbers">初期化場札枚数</param>
        /// <returns>場札の初期化された戦闘状態</returns>
        public static BattleState SetupBoard(
            this BattleState state,
            int tipNumbers = Constants.Battle.DEFAULT_BOARD_TIP_NUMBERS)
            => state?.SetBoardTips(state.PopDeckTips(tipNumbers));
        /// <summary>
        /// 全行動主体の手札を総初期化する
        /// </summary>
        /// <param name="state">手札初期化対象の戦闘状態</param>
        /// <param name="tipNumbers">初期化手札枚数</param>
        /// <returns></returns>
        public static BattleState SetupAllHandTips(
            this BattleState state,
            int tipNumbers = Constants.Battle.DEFAULT_HAND_TIP_NUMBERS)
        {
            if(state is null)
                return state;

            foreach(var actor in state.battleActors)
                actor.state = actor.state.ClearHandTips().AddHandTips(state.PopDeckTipsForced(tipNumbers));

            return state;
        }
        public static BattleState UpdateEnergy(this BattleState state)
        {
            if(state is null)
                return state;
            var actors = state.battleActors;
            if(!(actors?.Any() ?? false))
                return state;

            var baseEnergy = actors.Min(actor => actor.energy);

            foreach(var actor in actors)
                actor.energy += actor.GetEnergyIncrease() - baseEnergy;

            return state;
        }
        public static BattleState SetNextActor(this BattleState state)
            => state?.SetThisTimeActor(state.battleActors?.MaxKeys(actor => actor.energy).FirstOrDefault());
    }
}
