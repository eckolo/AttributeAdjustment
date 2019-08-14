using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.View.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx.Async;

namespace Assets.Src.Controller
{
    public static class ChoiceController
    {
        /// <summary>
        /// 選択肢生成メソッド
        /// </summary>
        /// <param name="choiceList">選択肢</param>
        /// <param name="initialChoiced">デフォルトの選択位置</param>
        /// <returns>生成された選択肢情報</returns>
        public static async UniTask<ChoiceState> Setup(
            this List<string> choiceList,
            int initialChoiced = 0)
        {
            var state = choiceList.ToChoiceState(initialChoiced);
            await Wait.Until(1);

            return await state.ToView();
        }
        /// <summary>
        /// 選択肢の選択操作メソッド
        /// </summary>
        /// <param name="state">操作対象の選択肢状態オブジェクト</param>
        /// <param name="button">現在のボタン設定</param>
        /// <returns>操作完了後の選択肢状態オブジェクト</returns>
        public static async UniTask<ChoiceState> Choice(
            this ChoiceState state,
            Configs.Button button)
        {
            var ableKeyList = button.decide.Concat(button.vertical).ToList();
            var (inputKeys, keyTiming) = await Wait.Until(ableKeyList);

            while(!state.isFinish)
            {
                state = await state
                    .Update(button, inputKeys, keyTiming)
                    .ToView();
                await Wait.Until(1);
            }

            return await state.ToView();
        }
        /// <summary>
        /// 選択肢終了メソッド
        /// 基本的には<see cref="ChoiceState.Dispose"/>から呼ばれる想定
        /// </summary>
        /// <param name="state">終了処理対象の選択肢状態オブジェクト</param>
        /// <returns>終了処理完了後の選択肢状態オブジェクト</returns>
        public static async UniTask<ChoiceState> End(this ChoiceState state)
        {
            state = state.Finalize();
            await Wait.Until(1);

            return await state.ToView();
        }
    }
}
