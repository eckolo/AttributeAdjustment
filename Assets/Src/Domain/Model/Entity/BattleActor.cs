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
            vitality = origin.vitality;

            base.parameter = origin.parameter;

            _isPlayer = origin.isPlayer;
        }

        public override Parameter parameter
        {
            get => base.parameter + _parameterAdjust;
            set => _parameterAdjust = value - base.parameter;
        }
        Parameter _parameterAdjust = new Parameter();

        /// <summary>
        /// 行動力
        /// </summary>
        public int energy { get; set; } = 0;

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
        public State state { get; set; } = new State();
    }
}
