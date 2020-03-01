using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Entity;
using Assets.Src.View.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.View.Factory
{
    public static class ViewStateFactory
    {
        /// <summary>
        /// <see cref="ViewState"/>の新規生成処理
        /// 既に存在していた場合は取得
        /// </summary>
        /// <typeparam name="TStateKey">生成元のキーの型</typeparam>
        /// <param name="stateKey">生成元のキー</param>
        /// <returns>生成された<see cref="ViewState"/></returns>
        public static ViewState GenerateViewState<TStateKey>(this IViewStateRepository repository, TStateKey stateKey)
            where TStateKey : ViewStateKey
        {
            if(repository is null)
                throw new ArgumentNullException(nameof(repository));
            if(stateKey is null)
                throw new ArgumentNullException(nameof(stateKey));

            var name = $"{nameof(ViewState)}_{stateKey.GetType().FullName}";
            var stateExisting = repository.Search(stateKey);

            if(!(stateExisting is null))
                return stateExisting;

            var stateNew = name.SetPrefab<ViewState>();
            stateNew.stateKey = stateKey;
            repository.Save(stateKey, stateNew);
            stateNew.Save(new ViewDeployment(), stateKey, stateNew);

            return stateNew;
        }
    }
}
