using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
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
        /// <param name="button">ボタン設定</param>
        /// <param name="initialChoiced">デフォルトの選択位置</param>
        /// <param name="startProcess">開始時処理</param>
        /// <param name="midProcess">途中処理</param>
        /// <param name="endProcess">終了時処理</param>
        /// <returns>選択結果</returns>
        public static async UniTask<int?> Choice(
            this List<string> choiceList,
            Configs.Button button,
            int initialChoiced = 0,
            Action<List<string>, int?> startProcess = null,
            Action<List<string>, int?> midProcess = null,
            Action<List<string>, int?> endProcess = null)
        {
            var _startProcess = startProcess ?? startProcessDefault;
            var _midProcess = midProcess ?? midProcessDefault;
            var _endProcess = endProcess ?? endProcessDefault;

            int? choiced = initialChoiced;

            _startProcess(choiceList, choiced);

            if(!choiceList.Any())
            {
                _endProcess(choiceList, choiced);
                return choiced;
            }

            await Wait.Until(1);

            var choiceCount = choiceList.Count;

            int keepUpTime = 0;
            int keepDownTime = 0;
            bool endChoice;
            do
            {
                choiced %= choiceCount;

                _midProcess(choiceList, choiced);

                var ableKeyList = button.decide.Concat(button.vertical).ToList();

                var (inputKeys, keyTiming) = await Wait.Until(ableKeyList);

                var inputDecisionKey = inputKeys.Judge(button.decide).Any() && keyTiming == KeyTiming.DOWN;
                var inputCancelKey = inputKeys.Judge(button.cancel).Any() && keyTiming == KeyTiming.DOWN;
                var inputUpKey = inputKeys.Judge(button.ups).Any() && keyTiming == KeyTiming.DOWN;
                var inputDownKey = inputKeys.Judge(button.downs).Any() && keyTiming == KeyTiming.DOWN;
                var keepUpKey = inputKeys.Judge(button.ups).Any() && keyTiming == KeyTiming.ON;
                var keepDownKey = inputKeys.Judge(button.downs).Any() && keyTiming == KeyTiming.ON;

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

            _endProcess(choiceList, choiced);
            return choiced;
        }

        static readonly Stack<ViewRoot> viewStack = new Stack<ViewRoot>();
        static readonly Stack<TextMesh> textMeshesStack = new Stack<TextMesh>();

        static readonly Action<List<string>, int?> startProcessDefault = (choiceList, choiced) => {
            var name = nameof(ChoiceSystem);

            var view = ViewRoot.CleateNew(name);
            viewStack.Push(view);

            var textMesh = view.SetText("", Vector2.zero, size: 0.5f, alignment: TextAlignment.Left, textName: name);
            textMeshesStack.Push(textMesh);
        };
        static readonly Action<List<string>, int?> midProcessDefault = (choiceList, choiced) => {
            var textMesh = textMeshesStack.Peek();
            textMesh.text = choiceList
                .Select((choice, index) => (cursor: index == choiced ? ">" : "", choice))
                .Select(line => $"{line.cursor}\t{line.choice}")
                .Aggregate((line1, line2) => $"{line1}\r\n{line2}");
        };
        static readonly Action<List<string>, int?> endProcessDefault = (choiceList, choiced) => {
            var textMesh = textMeshesStack.Pop();
            var view = viewStack.Pop();
            view.Destroy();
        };
    }
}
