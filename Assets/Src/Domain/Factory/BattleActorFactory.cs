using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;

namespace Assets.Src.Domain.Factory
{
    /// <summary>
    /// <see cref="BattleActor"/>クラスの生成処理
    /// </summary>
    public static class BattleActorFactory
    {
        /// <summary>
        /// 雛形を基に戦闘処理用行動主体オブジェクトを生成する
        /// </summary>
        /// <param name="stationery">雛形となる行動主体オブジェクト</param>
        /// <returns></returns>
        public static BattleActor ConvertForBattle(this Actor stationery)
            => ((BattleActor)stationery.Duplicate()).Initialize();
    }
}
