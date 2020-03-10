using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.View.Factory;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Service
{
    /// <summary>
    /// <see cref="TextMesh"/>操作サービス
    /// </summary>
    public static class TextMeshManager
    {
        public static TViewState UpdateText<TViewState>(
            this TViewState state,
            ViewDeployment deployment,
            ITextMeshKey textKey,
            ViewDeployment targetDeploy,
            string updateText)
            where TViewState : ViewState
        {
            if(state is null)
                throw new ArgumentNullException(nameof(state));

            var textValue = state.Pop<TextMesh>(deployment, textKey);
            if(textValue is null)
                return state;

            textValue.text = updateText;
            state.Save(targetDeploy, textKey, textValue);

            return state;
        }

        public static TViewState DestroyText<TViewState>(
            this TViewState state,
            ViewDeployment deployment,
            ITextMeshKey key)
            where TViewState : ViewState
        {
            if(state is null)
                throw new ArgumentNullException(nameof(state));

            var target = state.Pop<TextMesh>(deployment, key);
            if(target is null)
                return state;

            target.Destroy();

            return state;
        }
        /// <summary>
        /// システムテキストの削除
        /// </summary>
        /// <param name="textMesh">対象テキストオブジェクト</param>
        /// <returns>削除した文字列の内容</returns>
        public static TextMesh Destroy(this TextMesh textMesh)
        {
            UnityEngine.Object.Destroy(textMesh.gameObject);
            return textMesh;
        }
    }
}