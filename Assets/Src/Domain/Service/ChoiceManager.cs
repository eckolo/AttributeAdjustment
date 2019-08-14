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
        /// 選択肢生成メソッド
        /// </summary>
        /// <param name="choiceList">選択肢</param>
        /// <param name="button">ボタン設定</param>
        /// <param name="initialChoiced">デフォルトの選択位置</param>
        /// <returns>生成された選択肢情報</returns>
        public static ChoiceState Update(
            this ChoiceState state,
            Configs.Button button,
            IEnumerable<KeyCode> inputKeys,
            KeyTiming keyTiming)
        {
            var choiceCount = state.choiceList.Count;
            state.choiced %= choiceCount;

            var inputDecisionKey = inputKeys.Judge(button.decide).Any() && keyTiming == KeyTiming.DOWN;
            var inputCancelKey = inputKeys.Judge(button.cancel).Any() && keyTiming == KeyTiming.DOWN;
            var inputUpKey = inputKeys.Judge(button.ups).Any() && keyTiming == KeyTiming.DOWN;
            var inputDownKey = inputKeys.Judge(button.downs).Any() && keyTiming == KeyTiming.DOWN;
            var keepUpKey = inputKeys.Judge(button.ups).Any() && keyTiming == KeyTiming.ON;
            var keepDownKey = inputKeys.Judge(button.downs).Any() && keyTiming == KeyTiming.ON;

            state.keepUpTime = keepUpKey ? state.keepUpTime + 1 : 0;
            state.keepDownTime = keepDownKey ? state.keepDownTime + 1 : 0;
            if(inputUpKey || (keepUpKey && state.keepUpTime > 10))
            {
                state.keepUpTime = 0;
                state.choiced += choiceCount - 1;
            }
            if(inputDownKey || (keepDownKey && state.keepDownTime > 10))
            {
                state.keepDownTime = 0;
                state.choiced += 1;
            }
            if(inputCancelKey)
                state.choiced = null;

            state.isFinish = inputDecisionKey || inputCancelKey;
            state.viewActionQueue.Enqueue(state.ToViewAction(ViewAction.Pattern.UPDATE));

            return state;
        }
        public static ChoiceState Finalize(this ChoiceState state)
        {
            state.viewActionQueue.Enqueue(state.ToViewAction(ViewAction.Pattern.DELETE));
            return state;
        }
    }
}
