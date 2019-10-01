using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// Viewに渡されることで画面描画処理の起点となる状態オブジェクト
    /// </summary>
    public abstract class ViewStateKey : IViewKey
    {
        /// <summary>
        /// ビューの更新内容キュー
        /// </summary>
        public Queue<ViewAction> viewActionQueue { get; } = new Queue<ViewAction>();
        /// <summary>
        /// 生成直後フラグ
        /// </summary>
        public bool isGenerated { get; set; } = false;
    }
}
