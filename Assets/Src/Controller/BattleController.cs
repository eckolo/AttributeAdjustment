using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.View.Repository;
using Assets.Src.View.Service;
using System.Collections.Generic;
using UniRx.Async;

namespace Assets.Src.Controller
{
    /// <summary>
    /// 戦闘処理制御に関連する処理サービス
    /// </summary>
    public static class BattleController
    {
        readonly static ViewStateRepository viewStateRepository = new ViewStateRepository();

        /// <summary>
        /// 戦闘状態の生成
        /// </summary>
        /// <param name="state">戦闘処理開始前のゲーム状態</param>
        /// <param name="enemys">プレイヤー以外の戦闘参加者一覧</param>
        /// <param name="topography">戦闘処理開始時の地形状態</param>
        /// <returns>戦闘状態の生成処理</returns>
        public static async UniTask<BattleState> SetupBattle(
            this GameState state,
            IEnumerable<Actor> enemys,
            Topography topography)
        {
            var battleState = state
                .SetupBattleState(enemys, topography)
                .SetupDeck()
                .SetupBoard()
                .SetupAllHandTips();
            await Wait.Until(1);

            return battleState.Indicate(viewStateRepository);
        }

        /// <summary>
        /// 戦闘の中核処理制御
        /// </summary>
        /// <param name="state">処理前の戦闘状態</param>
        /// <returns>戦闘処理完了後のゲーム状態</returns>
        public static async UniTask<BattleState> ExecuteBattle(this BattleState state)
        //TODO ビュー（ビューコントローラ）呼び出し処理実装
        {
            while(!state.isEnd)
            {
                //TODO 戦闘中のターン毎処理の実装
                state = state.UpdateEnergy().SetNextActor().Indicate(viewStateRepository);
                await Wait.Until(1);
            }
            return state.Indicate(viewStateRepository);
        }

        /// <summary>
        /// 戦闘終了処理
        /// </summary>
        /// <param name="state">処理前の戦闘状態</param>
        /// <returns>戦闘終了処理完了後のゲーム状態</returns>
        public static async UniTask<BattleState> EndBattle(this BattleState state)
        {
            //TODO 戦闘終了処理の実装
            state = state.SetupDeck().SetupBoard().SetupAllHandTips();
            await Wait.Until(1);

            return state.Indicate(viewStateRepository);
        }
    }
}
