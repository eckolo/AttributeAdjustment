namespace Assets.Src.Domain.Model.Entity
{
    /// <summary>
    /// 戦闘処理時の行動主体
    /// </summary>
    public partial class BattleActor : Actor
    {
        public BattleActor(string name) : base(name)
        { }
        public BattleActor(Actor origin) : base(origin.name)
        {
            abilityList = origin.abilityList;
            experience = origin.experience;

            base.maxVitality = origin.maxVitality;
            base.vitality = origin.vitality;
            base.offense = origin.offense;
            base.defense = origin.defense;
            base.speed = origin.speed;

            _isPlayer = origin.isPlayer;
        }

        /// <summary>
        /// 変動パラメータを全て初期化する
        /// </summary>
        public BattleActor Initialize()
        {
            maxVitalityValue = maxVitality;
            offenseValue = offense;
            defenseValue = defense;
            speedValue = speed;
            state = new State();

            return this;
        }

        /// <summary>
        /// 現在の最大体力
        /// </summary>
        public int maxVitalityValue { get; set; }
        /// <summary>
        /// 現在の攻撃力
        /// </summary>
        public int offenseValue { get; set; }
        /// <summary>
        /// 現在の防御力
        /// </summary>
        public int defenseValue { get; set; }
        /// <summary>
        /// 現在の素早さ
        /// </summary>
        public int speedValue { get; set; }

        /// <summary>
        /// プレイヤー操作対象であるか否かのフラグ
        /// </summary>
        public override bool isPlayer => _isPlayer;
        /// <summary>
        /// プレイヤー操作対象であるか否かのフラグ
        /// </summary>
        readonly bool _isPlayer;

        /// <summary>
        /// 現在の戦闘状態
        /// </summary>
        public State state { get; set; }
    }
}
