using System;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// ログとか画面文字表示管轄クラス
    /// </summary>
    public static partial class MessageManager
    {
        /// <summary>
        /// ログを残す
        /// </summary>
        /// <param name="logger">ログの種類</param>
        /// <param name="logedText">ログに残されるべきテキスト</param>
        /// <returns>ログファイル名</returns>
        public static string LeaveLog(this LogHub logger, string logedText, IFileManager fileManager)
        {
            var displayedSentences = $"{DateTime.Now.ToLongTimeString()}\t【{logger.ToString()}】\t{logedText}";
            Debug.Log(displayedSentences);

            if(logger != LogHub.ERROR && !Debug.isDebugBuild) return "";

            var path = $"{Application.dataPath}/Logs";
            var filename = $"{DateTime.Today.ToString("yyyyMMdd")}.log";
            fileManager.Write(path, filename, displayedSentences, true);
            return filename;
        }

        /// <summary>
        /// システムテキストへの文字設定
        /// </summary>
        /// <param name="parent">描画先</param>
        /// <param name="setText">表示する文字列</param>
        /// <param name="position">表示位置</param>
        /// <param name="size">文字サイズ</param>
        /// <param name="color">文字色</param>
        /// <param name="pivot">表示位置基準点</param>
        /// <param name="alignment">文字の横位置</param>
        /// <param name="textName">描画文字列とは別の文字オブジェクト名称</param>
        /// <returns>生成された文字列オブジェクト</returns>
        public static TextMesh SetText(
            this MonoBehaviour parent,
            string setText,
            Vector2 position,
            float size = 1f,
            Color32? color = null,
            TextAnchor pivot = TextAnchor.MiddleCenter,
            TextAlignment alignment = TextAlignment.Center,
            string textName = null)
        {
            var nameModel = textName ?? setText;
            var _textName = nameModel;
            for(var index = 0; GameObject.Find(_textName) != null; index++) _textName = $"{nameModel}_{index}";

            var textObject = parent.SetPrefab<TextMesh>(_textName);

            textObject.text = setText;
            textObject.anchor = pivot;
            textObject.alignment = alignment;
            textObject.characterSize = size;
            textObject.color = color ?? Color.white;

            var transform = textObject.GetComponent<Transform>();
            transform.localPosition = position;

            return textObject;
        }
        /// <summary>
        /// システムテキストの削除
        /// </summary>
        /// <param name="textObject">対象テキストオブジェクト</param>
        /// <returns>削除した文字列の内容</returns>
        public static string Destroy(this TextMesh textObject)
        {
            if(textObject == null) return "";
            var result = textObject?.text ?? "";
            textObject.Destroy();
            return result;
        }
    }
}
