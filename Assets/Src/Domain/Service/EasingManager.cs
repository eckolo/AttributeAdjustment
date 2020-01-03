using Assets.Src.Domain.Model.Value;
using System;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    public static class EasingManager
    {
        public static Fraction In(this Easing.Pattern easingType, int time, int limit, Fraction max = null)
        {
            var maxNonNull = max ?? 1f;
            var diameter = time.DividedBy(limit);
            switch(easingType)
            {
                case Easing.Pattern.Linear:
                    return maxNonNull * diameter;
                case Easing.Pattern.Quadratic:
                    return maxNonNull * diameter * diameter;
                case Easing.Pattern.Cubic:
                    return maxNonNull * diameter * diameter * diameter;
                case Easing.Pattern.Quartic:
                    return maxNonNull * diameter * diameter * diameter * diameter;
                case Easing.Pattern.Quintic:
                    return maxNonNull * diameter * diameter * diameter * diameter * diameter;
                case Easing.Pattern.Sinusoidal:
                    return maxNonNull * (1 - Mathf.Cos(time * Mathf.PI / limit / 2));
                case Easing.Pattern.Exponential:
                    return maxNonNull * Mathf.Pow(2, (10 * (diameter - 1)).value);
                case Easing.Pattern.Circular:
                    return -maxNonNull * (Mathf.Sqrt((1 - diameter * diameter).value) - 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(easingType));
            }
        }
        public static Fraction SubIn(this Easing.Pattern easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.In(time, limit, max ?? 1f);

        public static Fraction Out(this Easing.Pattern easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.In(limit - time, limit, max ?? 1f);
        public static Fraction SubOut(this Easing.Pattern easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.Out(time, limit, max ?? 1f);

        public static Fraction InOut(this Easing.Pattern easingType, int time, int limit, Fraction max = null)
            => time < limit / 2
            ? easingType.In(time, limit / 2, (max ?? 1f) / 2)
            : easingType.Out(time - limit / 2, limit / 2, (max ?? 1f) / 2) + (max ?? 1f) / 2;
        public static Fraction SubInOut(this Easing.Pattern easingType, int time, int limit, Fraction max = null)
            => (max ?? 1f) - easingType.InOut(time, limit, max ?? 1f);
    }
}
