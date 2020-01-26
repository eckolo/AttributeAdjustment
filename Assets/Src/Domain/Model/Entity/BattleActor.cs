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
            experience = origin.experience;
            vitality = origin.vitality;

            brain = origin.brain;
            engine = origin.engine;
            flame = origin.flame;

            weaponsFree = origin.weaponsFree;

            _isPlayer = origin.isPlayer;
        }

        public override Parameter parameter => parameterVariable;
        public Parameter parameterVariable
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
