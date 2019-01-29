namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// 名前付きオブジェクト
    /// </summary>
    public abstract class Named
    {
        public Named(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; }
    }
}
