namespace Assets.Src.Domain.Model.Entity
{
    public partial class Actor
    {
        /// <summary>
        /// パラメータクラス
        /// </summary>
        public class Parameter
        {
            public Parameter()
            { }
            public Parameter(int maxVitality, int vitality, int offense, int defense, int speed)
            {
                this.maxVitality = maxVitality;
                this.vitality = vitality;
                this.offense = offense;
                this.defense = defense;
                this.speed = speed;
            }

            /// <summary>
            /// 最大体力
            /// </summary>
            public int maxVitality { get; } = 0;
            /// <summary>
            /// 現在の体力
            /// </summary>
            public int vitality { get; } = 0;
            /// <summary>
            /// 攻撃力
            /// </summary>
            public int offense { get; } = 0;
            /// <summary>
            /// 防御力
            /// </summary>
            public int defense { get; } = 0;
            /// <summary>
            /// 素早さ
            /// </summary>
            public int speed { get; } = 0;

            public static Parameter operator +(Parameter x, Parameter y)
                => x != null || y != null
                ? new Parameter(
                    maxVitality: (x?.maxVitality ?? 0) + (y?.maxVitality ?? 0),
                    vitality: (x?.vitality ?? 0) + (y?.vitality ?? 0),
                    offense: (x?.offense ?? 0) + (y?.offense ?? 0),
                    defense: (x?.defense ?? 0) + (y?.defense ?? 0),
                    speed: (x?.speed ?? 0) + (y?.speed ?? 0))
                : null;
            public static Parameter operator -(Parameter x, Parameter y)
                => x != null || y != null
                ? new Parameter(
                    maxVitality: (x?.maxVitality ?? 0) - (y?.maxVitality ?? 0),
                    vitality: (x?.vitality ?? 0) - (y?.vitality ?? 0),
                    offense: (x?.offense ?? 0) - (y?.offense ?? 0),
                    defense: (x?.defense ?? 0) - (y?.defense ?? 0),
                    speed: (x?.speed ?? 0) - (y?.speed ?? 0))
                : null;
            public static Parameter operator -(Parameter x)
                => x is Parameter xNotNull
                ? new Parameter(
                    maxVitality: -xNotNull.maxVitality,
                    vitality: -xNotNull.vitality,
                    offense: -xNotNull.offense,
                    defense: -xNotNull.defense,
                    speed: -xNotNull.speed)
                : null;
        }
    }
}
