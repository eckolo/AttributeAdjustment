using Assets.Src.Domain.Model.Value;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    public static class EasingManager
    {
        public static Fraction In(this Easing easingType, int time, int limit, Fraction max = null)
        {
            var maxNonNull = max ?? 1f;
            var diameter = time.DividedBy(limit);
            switch(easingType)
            {
                case Easing.Linear:
                    return maxNonNull * diameter;
                case Easing.Quadratic:
                    return maxNonNull * diameter * diameter;
                case Easing.Cubic:
                    return maxNonNull * diameter * diameter * diameter;
                case Easing.Quartic:
                    return maxNonNull * diameter * diameter * diameter * diameter;
                case Easing.Quintic:
                    return maxNonNull * diameter * diameter * diameter * diameter * diameter;
                case Easing.Sinusoidal:
                    return maxNonNull * (1 - Mathf.Cos(time * Mathf.PI / limit / 2));
                case Easing.Exponential:
                    return maxNonNull * Mathf.Pow(2, (10 * (diameter - 1)).value);
                case Easing.Circular:
                    return -maxNonNull * (Mathf.Sqrt((1 - diameter * diameter).value) - 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(easingType));
            }
        }
        public static Fraction SubIn(this Easing easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.In(time, limit, max ?? 1f);

        public static Fraction Out(this Easing easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.In(limit - time, limit, max ?? 1f);
        public static Fraction SubOut(this Easing easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.Out(time, limit, max ?? 1f);

        public static Fraction InOut(this Easing easingType, int time, int limit, Fraction max = null)
            => time < limit / 2
            ? easingType.In(time, limit / 2, (max ?? 1f) / 2)
            : easingType.Out(time - limit / 2, limit / 2, (max ?? 1f) / 2) + (max ?? 1f) / 2;
        public static Fraction SubInOut(this Easing easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.InOut(time, limit, max ?? 1f);
    }
}
