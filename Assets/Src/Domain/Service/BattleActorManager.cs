using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using System;
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
        /// <param name="actor">手札初期化対象の戦闘者</param>
        /// <returns>手札の初期化された戦闘者</returns>
        public static BattleActor ReloadHandTips(this BattleActor actor)
        {
            if(actor is null)
                throw new ArgumentNullException(nameof(actor));

            var addTips = actor.engine.defaultHandTipMap.Embody();
            actor.state = actor.state.ClearHandTips().AddHandTips(addTips);
            actor.state.SetNewView(MotionTip.Destination.HAND.GetCenterPosition(), addTips);

            return actor;
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
