using Assets.Src.Domain.Model.Value;
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
    public static class ChoiceSystem
    {
        /// <summary>
        /// 選択肢関数
        /// 結果の値はendProcessで返す
        /// </summary>
        /// <param name="choiceList">選択肢</param>
        /// <param name="buttom">ボタン設定</param>
        /// <param name="initialChoiced">デフォルトの選択位置</param>
        /// <param name="endProcess">終了時処理</param>
        /// <returns></returns>
        public static async UniTask<int?> Choice(
            this List<string> choiceList,
            Configs.Buttom buttom,
            int initialChoiced = 0,
            Action<int?> endProcess = null)
        {
            if(!choiceList.Any())
            {
                endProcess(initialChoiced);
                return initialChoiced;
            }

            int? choiced = initialChoiced;
            await Wait.Until(1);

            var textObject = "".SetText(Vector2.zero, size: 0.5f, alignment: TextAlignment.Left);
            var choiceCount = choiceList.Count;

            int keepUpTime = 0;
            int keepDownTime = 0;
            bool endChoice;
            do
            {
                choiced %= choiceCount;

                textObject.text = choiceList
                    .Select((choice, index) => (cursor: index == choiced ? ">" : "", choice))
                    .Select(line => $"{line.cursor}\t{line.choice}")
                    .Aggregate((line1, line2) => $"{line1}\r\n{line2}");

                var ableKeyList = buttom.decide.Concat(buttom.vertical).ToList();

                var (inputKeys, keyTiming) = await Wait.Until(ableKeyList);

                var inputDecisionKey = inputKeys.Judge(buttom.decide).Any() && keyTiming == KeyTiming.DOWN;
                var inputCancelKey = inputKeys.Judge(buttom.cancel).Any() && keyTiming == KeyTiming.DOWN;
                var inputUpKey = inputKeys.Judge(buttom.ups).Any() && keyTiming == KeyTiming.DOWN;
                var inputDownKey = inputKeys.Judge(buttom.downs).Any() && keyTiming == KeyTiming.DOWN;
                var keepUpKey = inputKeys.Judge(buttom.ups).Any() && keyTiming == KeyTiming.ON;
                var keepDownKey = inputKeys.Judge(buttom.downs).Any() && keyTiming == KeyTiming.ON;

                Debug.Log($"inputDecisionKey:{inputDecisionKey},inputCancelKey:{inputCancelKey},inputUpKey:{inputUpKey},keepUpKey:{keepUpKey},inputDownKey:{inputDownKey},keepDownKey:{keepDownKey}");

                keepUpTime = keepUpKey ? keepUpTime + 1 : 0;
                keepDownTime = keepDownKey ? keepDownTime + 1 : 0;
                if(inputUpKey || (keepUpKey && keepUpTime > 10))
                {
                    keepUpTime = 0;
                    choiced += choiceCount - 1;
                }
                if(inputDownKey || (keepDownKey && keepDownTime > 10))
                {
                    keepDownTime = 0;
                    choiced += 1;
                }
                if(inputCancelKey) choiced = null;

                endChoice = inputDecisionKey || inputCancelKey;
            }
            while(!endChoice);

            textObject.Destroy();

            endProcess?.Invoke(choiced);
            return choiced;
        }
    }
}
