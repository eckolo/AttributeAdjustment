using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.View.Repository
{
    /// <summary>
    /// ビューの類を扱うリポジトリの共通機能インターフェース
    /// </summary>
    /// <typeparam name="TViewKey">ビューの索引キー</typeparam>
    /// <typeparam name="TViewValue">ビュー実体</typeparam>
    public interface IViewRepository<TViewKey, TViewValue>
    {
        TViewValue Search(TViewKey key);

        TValue Save<TValue>(TViewKey key, TValue value)
            where TValue : TViewValue;
    }
}
