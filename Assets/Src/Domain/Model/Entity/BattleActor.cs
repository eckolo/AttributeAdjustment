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
            vitality = origin.vitality;
            base.offense = origin.offense;
            base.defense = origin.defense;
            base.speed = origin.speed;

            _isPlayer = origin.isPlayer;
        }

        /// <summary>
        /// 現在の最大体力
        /// </summary>
        public override int maxVitality
        {
            get => base.maxVitality + _maxVitalityAdjust;
            set => _maxVitalityAdjust = value - base.maxVitality;
        }
        /// <summary>
        /// 現在の最大体力補正値
        /// </summary>
        int _maxVitalityAdjust = 0;

        /// <summary>
        /// 現在の攻撃力
        /// </summary>
        public override int offense
        {
            get => base.offense + _offenseAdjust;
            set => _offenseAdjust = value - base.offense;
        }
        /// <summary>
        /// 現在の攻撃力補正値
        /// </summary>
        int _offenseAdjust = 0;

        /// <summary>
        /// 現在の防御力
        /// </summary>
        public override int defense
        {
            get => base.defense + _defenseAdjust;
            set => _defenseAdjust = value - base.defense;
        }
        /// <summary>
        /// 現在の防御力補正値
        /// </summary>
        int _defenseAdjust = 0;

        /// <summary>
        /// 現在の素早さ
        /// </summary>
        public override int speed
        {
            get => base.speed + _speedAdjust;
            set => _speedAdjust = value - base.speed;
        }
        /// <summary>
        /// 現在の素早さ補正値
        /// </summary>
        int _speedAdjust = 0;

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
