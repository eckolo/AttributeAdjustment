using Assets.Src.View.Model.Entity;
using System;
using UnityEngine;

namespace Assets.Src.View.Service
{
    public static class TextSetManager
    {
        /// <summary>
        /// システムテキストの削除
        /// </summary>
        /// <param name="textObject">対象テキストオブジェクト</param>
        /// <returns>削除した文字列の内容</returns>
        public static TextMesh Destroy(this TextMesh textObject)
        {
            UnityEngine.Object.Destroy(textObject);
            return textObject;
        }

        /// <summary>
        /// システムテキストの削除
        /// </summary>
        /// <param name="textSet">対象テキストオブジェクト</param>
        /// <returns>削除した文字列の内容</returns>
        public static TextSet Destroy(this TextSet textSet)
        {
            if(textSet == null)
                return textSet;

            foreach(var text in textSet.texts)
                text.Destroy();

            UnityEngine.Object.Destroy(textSet);
            return textSet;
        }
    }
}
