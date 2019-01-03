using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// ファイルの入出力制御
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// ファイル書き込み
        /// </summary>
        /// <param name="path">書き込み先ファイルパス</param>
        /// <param name="filename">書き込み先ファイル名</param>
        /// <param name="written">書き込み内容</param>
        /// <param name="append">上書き保存ではなく追加保存するフラグ</param>
        /// <returns>書き込み成否</returns>
        bool Write(string path, string filename, string written, bool append = false);
        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name="path">書き込み先ファイルパス</param>
        /// <param name="filename">読み込み先ファイル名</param>
        /// <returns>読み込んだファイル内容</returns>
        string Read(string path, string filename);
    }
}
