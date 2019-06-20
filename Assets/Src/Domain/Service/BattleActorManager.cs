using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Linq;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// <see cref="BattleActor"/>クラスの制御関連サービス
    /// </summary>
    public static class BattleActorManager
    {
        /// <summary>
        /// 手札の初期化
        /// </summary>
        /// <param name="state">手札初期化対象の戦闘状態</param>
        /// <param name="actor">手札初期化対象の動作主体</param>
        /// <param name="tipNumbers">初期化手札枚数</param>
        /// <returns>所定の動作主体の手札が初期化された戦闘状態</returns>
        public static BattleState SetupHandTips(
            this BattleState state,
            BattleActor actor,
            int tipNumbers = Constants.Battle.DEFAULT_HAND_TIP_NUMBERS)
        {
            if(state is null)
                return state;
            if(actor is null)
                return state;

            if(!state.battleActors.Contains(actor))
                return state;

            actor.state = actor.state
                .ClearHandTips()
                .AddHandTips(state.PopDeckTips(tipNumbers));

            return state;
        }
    }
}
