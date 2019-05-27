using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 正確な分数を表現するクラス
    /// </summary>
    public class Fraction : IComparable<Fraction>, IComparable<int>, IComparable<float>
    {
        /// <summary>
        /// 分数の初期化
        /// </summary>
        /// <param name="numer">分子</param>
        /// <param name="denom">分母</param>
        Fraction(float numer, float denom)
        {
            if(denom == 0)
                throw new DivideByZeroException();

            var sign = (denom < 0 ? -1 : 1);
            var numerInt = (int)numer;
            var denomInt = (int)denom;
            if(numer == numerInt && denom == denomInt)
            {
                var gcd = sign * numerInt.Euclidean(denomInt);
                this.numer = numerInt / gcd;
                this.denom = denomInt / gcd;
                return;
            }

            this.numer = numer * sign;
            this.denom = denom * sign;
        }

        /// <summary>
        /// 分数の初期化
        /// </summary>
        /// <param name="numer">分子</param>
        public Fraction(float numer) : this(numer, 1) { }

        /// <summary>
        /// 分子
        /// </summary>
        public virtual float numer { get; }
        /// <summary>
        /// 分母
        /// </summary>
        public virtual float denom { get; }
        /// <summary>
        /// 実数値
        /// </summary>
        public float value => numer / denom;

        /// <summary>
        /// ０と同値の分数型
        /// </summary>
        public static Fraction zero => new Fraction(0);
        /// <summary>
        /// １と同値の分数型
        /// </summary>
        public static Fraction one => new Fraction(1);

        public static Fraction operator +(Fraction x, Fraction y)
            => new Fraction(x.numer * y.denom + y.numer * x.denom, x.denom * y.denom);
        public static Fraction operator -(Fraction x, Fraction y)
            => new Fraction(x.numer * y.denom - y.numer * x.denom, x.denom * y.denom);
        public static Fraction operator *(Fraction x, Fraction y)
            => new Fraction(x.numer * y.numer, x.denom * y.denom);
        public static Fraction operator /(Fraction x, Fraction y)
            => new Fraction(x.numer * y.denom, x.denom * y.numer);
        public static Fraction operator -(Fraction x) => x * -1;
        public static Fraction operator +(float x, Fraction y) => new Fraction(x) + y;
        public static Fraction operator +(Fraction x, float y) => x + new Fraction(y);
        public static Fraction operator -(float x, Fraction y) => new Fraction(x) - y;
        public static Fraction operator -(Fraction x, float y) => x - new Fraction(y);
        public static Fraction operator *(float x, Fraction y) => new Fraction(x) * y;
        public static Fraction operator *(Fraction x, float y) => x * new Fraction(y);
        public static Fraction operator /(float x, Fraction y) => new Fraction(x) / y;
        public static Fraction operator /(Fraction x, float y) => x / new Fraction(y);

        public static bool operator ==(Fraction x, Fraction y) => x.CompareTo(y) == 0;
        public static bool operator !=(Fraction x, Fraction y) => x.CompareTo(y) != 0;
        public static bool operator ==(Fraction x, float y) => x.CompareTo(y) == 0;
        public static bool operator !=(Fraction x, float y) => x.CompareTo(y) != 0;
        public static bool operator ==(float x, Fraction y) => x.CompareTo(y) == 0;
        public static bool operator !=(float x, Fraction y) => x.CompareTo(y) != 0;
        public static bool operator ==(Fraction x, int y) => x.CompareTo(y) == 0;
        public static bool operator !=(Fraction x, int y) => x.CompareTo(y) != 0;
        public static bool operator ==(int x, Fraction y) => x.CompareTo(y) == 0;
        public static bool operator !=(int x, Fraction y) => x.CompareTo(y) != 0;

        public static bool operator <(Fraction x, Fraction y) => x.CompareTo(y) < 0;
        public static bool operator >(Fraction x, Fraction y) => x.CompareTo(y) > 0;
        public static bool operator <(Fraction x, float y) => x.CompareTo(y) < 0;
        public static bool operator >(Fraction x, float y) => x.CompareTo(y) > 0;
        public static bool operator <(float x, Fraction y) => x.CompareTo(y) < 0;
        public static bool operator >(float x, Fraction y) => x.CompareTo(y) > 0;
        public static bool operator <(Fraction x, int y) => x.CompareTo(y) < 0;
        public static bool operator >(Fraction x, int y) => x.CompareTo(y) > 0;
        public static bool operator <(int x, Fraction y) => x.CompareTo(y) < 0;
        public static bool operator >(int x, Fraction y) => x.CompareTo(y) > 0;

        public static bool operator <=(Fraction x, Fraction y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Fraction x, Fraction y) => x.CompareTo(y) >= 0;
        public static bool operator <=(Fraction x, float y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Fraction x, float y) => x.CompareTo(y) >= 0;
        public static bool operator <=(float x, Fraction y) => x.CompareTo(y) <= 0;
        public static bool operator >=(float x, Fraction y) => x.CompareTo(y) >= 0;
        public static bool operator <=(Fraction x, int y) => x.CompareTo(y) <= 0;
        public static bool operator >=(Fraction x, int y) => x.CompareTo(y) >= 0;
        public static bool operator <=(int x, Fraction y) => x.CompareTo(y) <= 0;
        public static bool operator >=(int x, Fraction y) => x.CompareTo(y) >= 0;

        public static implicit operator Fraction(float num) => new Fraction(num);

        public int CompareTo(Fraction other)
        {
            if(numer * other.denom > other.numer * denom)
                return 1;
            if(numer * other.denom < other.numer * denom)
                return -1;
            return 0;
        }
        public int CompareTo(int other) => CompareTo(new Fraction(other));
        public int CompareTo(float other)
        {
            if(numer > other * denom)
                return 1;
            if(numer < other * denom)
                return -1;
            return 0;
        }

        public override bool Equals(object obj)
        {
            var fraction = obj as Fraction;
            return CompareTo(fraction) == 0;
        }

        public override int GetHashCode()
        {
            const int NORM = -1521134295;
            var hashCode = 2124583152;
            hashCode = hashCode * NORM + numer.GetHashCode();
            hashCode = hashCode * NORM + numer.GetHashCode();
            hashCode = hashCode * NORM + denom.GetHashCode();
            hashCode = hashCode * NORM + denom.GetHashCode();
            hashCode = hashCode * NORM + value.GetHashCode();
            return hashCode;
        }
    }
}
