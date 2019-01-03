using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Infrastructure.Service
{
    /// <summary>
    /// ファイルの入出力制御
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// エンコーダーを固定値として登録
        /// </summary>
        readonly Encoding characterCode = Encoding.GetEncoding("utf-8");

        /// <summary>
        /// ファイル書き込み
        /// </summary>
        /// <param name="path">書き込み先ファイルパス</param>
        /// <param name="filename">書き込み先ファイル名</param>
        /// <param name="written">書き込み内容</param>
        /// <param name="append">上書き保存ではなく追加保存するフラグ</param>
        /// <returns>書き込み成否</returns>
        public bool Write(string path, string filename, string written, bool append = false)
        {
            try
            {
                if(!Directory.Exists(path)) Directory.CreateDirectory(path);
                var fullpath = $"{path}/{filename}";

                using(var writer = new StreamWriter(fullpath, append, characterCode))
                {
                    writer.WriteLine(written);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name="path">書き込み先ファイルパス</param>
        /// <param name="filename">読み込み先ファイル名</param>
        /// <returns>読み込んだファイル内容</returns>
        public string Read(string path, string filename)
        {
            var fullpath = $"{path}/{filename}";
            if(File.Exists(fullpath))
            {
                using(var stream = new StreamReader(fullpath, characterCode))
                {
                    return stream.ReadToEnd();
                }
            }
            else
            {
                return null;
            }
        }
    }
}
