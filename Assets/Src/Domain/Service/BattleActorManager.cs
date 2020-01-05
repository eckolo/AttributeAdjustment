using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using System.Linq;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// <see cref="BattleActor"/>クラスの制御関連サービス
    /// </summary>
    public static class BattleActorManager
    {
        /// <summary>
        /// 手札の補充
        /// 所定の枚数以上になるまで補充する
        /// </summary>
        /// <param name="state">手札補充対象の戦闘状態</param>
        /// <param name="actor">手札補充元の動作主体</param>
        /// <param name="tipNumbers">補充後の手札枚数</param>
        /// <returns>所定の動作主体の手札が初期化された戦闘状態</returns>
        public static BattleState ReloadHandTips(
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

            var replenishment = tipNumbers - actor.state.handTips.Count;
            if(replenishment <= 0)
                return state;

            var addTips = state.PopDeckTipsForced(replenishment);
            actor.state = actor.state.AddHandTips(addTips);
            actor.state.SetTipMoving(addTips, MotionTip.Destination.HAND);

            return state;
        }

        /// <summary>
        /// 行動力増加量を計算する
        /// </summary>
        /// <param name="actor">計算対象行動主体</param>
        /// <returns>行動力の増加量</returns>
        public static int GetEnergyIncrease(this BattleActor actor)
            => Constants.Battle.ENERGY_NORM + actor.parameter.speed - actor.state.selfTips.Count();
    }
}
