using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx.Async;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// 選択肢システム
    /// </summary>
    public static class ChoiceManager
    {
        /// <summary>
        /// 選択肢状態更新メソッド
        /// </summary>
        /// <param name="state">更新対象の選択状態オブジェクト</param>
        /// <param name="keyConfigs">ボタン設定</param>
        /// <param name="inputKeys">入力キー一覧</param>
        /// <param name="keyTiming">キーの入力タイミング</param>
        /// <returns>更新された選択肢状態オブジェクト</returns>
        public static ChoiceState Update(
            this ChoiceState state,
            KeyConfigs keyConfigs,
            IEnumerable<KeyCode> inputKeys,
            KeyTiming keyTiming)
        {
            var inputDecisionKey = inputKeys.Judge(keyConfigs.decide).Any() && keyTiming == KeyTiming.DOWN;
            var inputCancelKey = inputKeys.Judge(keyConfigs.cancel).Any() && keyTiming == KeyTiming.DOWN;
            var inputUpKey = inputKeys.Judge(keyConfigs.ups).Any() && keyTiming == KeyTiming.DOWN;
            var inputDownKey = inputKeys.Judge(keyConfigs.downs).Any() && keyTiming == KeyTiming.DOWN;
            var keepUpKey = inputKeys.Judge(keyConfigs.ups).Any() && keyTiming == KeyTiming.ON;
            var keepDownKey = inputKeys.Judge(keyConfigs.downs).Any() && keyTiming == KeyTiming.ON;

            state.keepUpTime = keepUpKey ? state.keepUpTime + 1 : 0;
            state.keepDownTime = keepDownKey ? state.keepDownTime + 1 : 0;
            if(inputUpKey || (keepUpKey && state.keepUpTime > Constants.Choice.KEEP_VERTICAL_LIMIT))
            {
                state.keepUpTime = !inputUpKey
                    ? Constants.Choice.KEEP_VERTICAL_LIMIT - Constants.Choice.KEEP_VERTICAL_INTERVAL
                    : 0;
                state.choiced -= 1;
            }
            if(inputDownKey || (keepDownKey && state.keepDownTime > Constants.Choice.KEEP_VERTICAL_LIMIT))
            {
                state.keepDownTime = !inputDownKey
                    ? Constants.Choice.KEEP_VERTICAL_LIMIT - Constants.Choice.KEEP_VERTICAL_INTERVAL
                    : 0;
                state.choiced += 1;
            }
            if(inputCancelKey)
                state.choiced = null;

            state.isFinished = inputDecisionKey || inputCancelKey;
            state.viewActionQueue.Enqueue(state.ToViewAction(ViewAction.Pattern.UPDATE));

            return state;
        }
        /// <summary>
        /// 選択肢終了処理メソッド
        /// </summary>
        /// <param name="state">終了処理対象の選択肢状態オブジェクト</param>
        /// <returns>終了処理のなされた選択肢状態オブジェクト</returns>
        public static ChoiceState Finalize(this ChoiceState state)
        {
            state.viewActionQueue.Enqueue(state.ToViewAction(ViewAction.Pattern.DELETE));
            return state;
        }

        public static TextMeshStationeryValue ToChoiceText(this ChoiceState state)
            => state?.choiceList.ToChoiceText(state.choiced) ?? throw new ArgumentNullException(nameof(state));
        public static TextMeshStationeryValue ToChoiceText(this IList<string> choiceList, int? choiced)
        {
            var text = (choiceList?.Any() ?? false)
                ? choiceList
                .Select((choice, index) => (cursor: index == choiced ? ">" : "", choice))
                .Select(line => $"{line.cursor}\t{line.choice}")
                .Aggregate((line1, line2) => $"{line1}\r\n{line2}")
                : string.Empty;
            var textMesh = new TextMeshStationeryValue(text);

            return textMesh;
        }
    }
}
