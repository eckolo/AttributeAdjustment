using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// システムテキストの削除
        /// </summary>
        /// <param name="textObject">対象テキストオブジェクト</param>
        /// <returns>削除した文字列の内容</returns>
        public static TextMesh Destroy(this TextMesh textObject) => textObject?.Destroy<TextMesh>();

        /// <summary>
        /// システムテキストへの文字設定
        /// </summary>
        /// <param name="parent">描画先</param>
        /// <param name="setTexts">表示する文字列</param>
        /// <param name="position">表示位置</param>
        /// <param name="size">文字サイズ</param>
        /// <param name="color">文字色</param>
        /// <param name="pivot">表示位置基準点</param>
        /// <param name="alignment">文字の横位置</param>
        /// <param name="textName">描画文字列とは別の文字オブジェクト名称</param>
        /// <returns>生成された文字列オブジェクト</returns>
        public static TextSet SetTextSet(
            this MonoBehaviour parent,
            IEnumerable<string> setTexts,
            Vector2 position,
            float size = 1f,
            Color32? color = null,
            TextAlignment alignment = TextAlignment.Center,
            string textName = null)
        {
            var name = setTexts.Aggregate((text1, text2) => $"{text1} {text2}");
            var textSet = parent.SetPrefab<TextSet>(name);

            var texts = setTexts
                .Select(text => textSet.SetText(
                    setText: text,
                    position: Vector2.zero,
                    size: size,
                    color: color,
                    pivot: TextAnchor.MiddleCenter,
                    alignment: alignment))
                .ToList();

            textSet.texts = texts;
            textSet.position = position;

            return textSet;
        }
        /// <summary>
        /// システムテキストの削除
        /// </summary>
        /// <param name="textSet">対象テキストオブジェクト</param>
        /// <returns>削除した文字列の内容</returns>
        public static TextSet Destroy(this TextSet textSet)
        {
            if(textSet == null) return textSet;

            foreach(var text in textSet.texts) text.Destroy();

            return textSet.Destroy<TextSet>();
        }
    }
}
