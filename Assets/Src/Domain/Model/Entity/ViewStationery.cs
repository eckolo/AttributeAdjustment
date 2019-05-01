using Assets.Src.Domain.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 画面表示されるオブジェクト類の雛形クラス
    /// </summary>
    public class ViewStationery
    {
        public ViewStationery(IViewValue value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IViewValue value { get; }
    }
}
