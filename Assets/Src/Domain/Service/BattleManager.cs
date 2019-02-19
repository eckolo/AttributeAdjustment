using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using UniRx.Async;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// 戦闘処理制御に関連する処理サービス
    /// </summary>
    public static class BattleManager
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
        {
            var battleState = state.ToBattleState(enemys, topography).BattleStart();

            while(!battleState.isEnd)
            {
                battleState = battleState.BattleTurnByTurn();
            }

            battleState = battleState.BattleEnd();

            LogHub.DEBUG.LeaveLog($"{state} TurnByTurn", state.fileManager);
            await Wait.Until(120);

            return state;
        }

        /// <summary>
        /// 戦闘開始処理
        /// </summary>
        /// <param name="state">戦闘開始時のゲーム状態</param>
        /// <returns>戦闘開始処理後の戦闘状態</returns>
        static BattleState BattleStart(this BattleState state)
        {
            return state.SetupDeck().SetupBoard().SetupAllHandTips();
        }

        /// <summary>
        /// 戦闘1ターン毎の処理
        /// </summary>
        /// <param name="state">ターン開始前の戦闘状態</param>
        /// <returns>ターン終了時の戦闘状態</returns>
        static BattleState BattleTurnByTurn(this BattleState state)
        {
            return state.SetupDeck().SetupBoard().SetupAllHandTips();
        }

        /// <summary>
        /// 戦闘終了処理
        /// </summary>
        /// <param name="state">終了処理対象の戦闘状態</param>
        /// <returns>終了処理後のゲーム状態</returns>
        static BattleState BattleEnd(this BattleState state)
        {
            return state.SetupDeck().SetupBoard().SetupAllHandTips();
        }
    }
}
