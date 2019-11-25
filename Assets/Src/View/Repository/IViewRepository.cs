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
    /// <typeparam name="TView">ビュー実体</typeparam>
    public interface IViewRepository<TViewKey, TView>
    {
        TView Search<TKey>(TKey key) where TKey : TViewKey;

        TView Save<TKey, TValue>(TKey key, TValue state) where TKey : TViewKey where TValue : TView;
    }
}
