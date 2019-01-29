namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// プライヤー操作対象者クラス
    /// </summary>
    public class Player : Actor
    {
        public Player(string name) : base(name)
        {
        }

        public override bool isPlayer => true;
    }
}
