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
    public class ViewEntity : IViewRoot
    {
        public ViewEntity(IViewValue value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IViewValue value { get; }

        public IViewRoot parent { get; protected set; }

        public ViewEntity SetParent(IViewRoot parent)
        {
            this.parent = parent ?? this.parent;
            return this;
        }
    }
}
