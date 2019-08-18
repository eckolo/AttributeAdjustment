using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Factory
{
    /// <summary>
    /// <see cref="ViewAction"/>生成処理関連
    /// </summary>
    public static class ViewActionFactory
    {
        /// <summary>
        /// 単一ビューオブジェクトに対するアクションの作成
        /// </summary>
        /// <param name="view">アクション対象のビューオブジェクト</param>
        /// <param name="pattern">アクション種別</param>
        /// <returns>生成されたビューアクション</returns>
        public static ViewAction ToViewAction(this IViewKey view, ViewAction.Pattern pattern)
            => new ViewAction(pattern, view);
    }
}
