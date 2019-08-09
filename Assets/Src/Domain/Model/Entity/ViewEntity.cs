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
    public class ViewEntity : IViewKey
    {
        public ViewEntity(IViewKey value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IViewKey value { get; }

        public IViewKey parent { get; protected set; }

        public ViewEntity SetParent(IViewKey parent)
        {
            this.parent = parent ?? this.parent;
            return this;
        }
    }
}
