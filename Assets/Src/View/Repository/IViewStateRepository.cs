using Assets.Src.Domain.Model.Abstract;
using Assets.Src.View.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.View.Repository
{
    public interface IViewStateRepository
    {
        /// <summary>
        /// <see cref="ViewState"/>の検索
        /// </summary>
        /// <typeparam name="TKey">検索キーの型</typeparam>
        /// <param name="key">検索キー</param>
        /// <returns>検索結果</returns>
        ViewState SearchViewState<TKey>(TKey key) where TKey : ViewStateKey;

        ViewState SaveViewState<TKey>(TKey key, ViewState state) where TKey : ViewStateKey;
    }
}
