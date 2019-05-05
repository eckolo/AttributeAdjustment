using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using System.Collections.Generic;
using UniRx.Async;

namespace Assets.Src.Controller
{
    /// <summary>
    /// 戦闘処理制御に関連する処理サービス
    /// </summary>
    public static class BattleController
    {
        /// <summary>
        /// タイムライン上の戦闘制御処理
        /// </summary>
        /// <param name="state">戦闘処理開始前のゲーム状態</param>
        /// <param name="enemys">プレイヤー以外の戦闘参加者一覧</param>
        /// <param name="topography">戦闘処理開始時の地形状態</param>
        /// <returns>戦闘処理完了後のゲーム状態</returns>
        public static async UniTask<GameState> ExecuteBattle(
            this GameState state,
            IEnumerable<Actor> enemys,
            Topography topography)
        //TODO ビュー（ビューコントローラ）呼び出し処理実装
        {
            var battleState = state
                .ToBattleState(enemys, topography)
                .SetupDeck()
                .SetupBoard()
                .SetupAllHandTips();

            while(!battleState.isEnd)
            {
                //TODO 戦闘中のターン毎処理の実装
                battleState = battleState.SetupDeck().SetupBoard().SetupAllHandTips();
            }

            //TODO 戦闘終了処理の実装
            battleState = battleState.SetupDeck().SetupBoard().SetupAllHandTips();

            LogHub.DEBUG.LeaveLog($"{state} TurnByTurn", state.fileManager);
            await Wait.Until(120);

            return state;
        }
    }
}
