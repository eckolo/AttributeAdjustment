using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using Assets.Src.Domain.Service;
using Assets.Src.View.Repository;
using Assets.Src.View.Service;
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
        readonly static ViewStateRepository viewStateRepository = new ViewStateRepository();

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

            return state.Indicate(viewStateRepository);
        }
        /// <summary>
        /// 選択肢の選択操作メソッド
        /// </summary>
        /// <param name="state">操作対象の選択肢状態オブジェクト</param>
        /// <param name="keyConfigs">現在のボタン設定</param>
        /// <returns>操作完了後の選択肢状態オブジェクト</returns>
        public static async UniTask<ChoiceState> Choice(
            this ChoiceState state,
            KeyConfigs keyConfigs)
        {
            var ableKeyList = keyConfigs.decide.Concat(keyConfigs.vertical).ToList();
            while(!state.isFinished)
            {
                var (inputKeys, keyTiming) = await Wait.Until(ableKeyList);
                state = state
                    .Update(keyConfigs, inputKeys, keyTiming)
                    .Indicate(viewStateRepository);
            }

            await Wait.Until(1);
            return state.Indicate(viewStateRepository);
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

            return state.Indicate(viewStateRepository);
        }
    }
}
