using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// 値比較用のハッシュ値取得可能なデータ型
    /// </summary>
    public interface IHashable
    {
        /// <summary>
        /// ハッシュコード値
        /// </summary>
        ulong hashCode { get; }
    }
}
