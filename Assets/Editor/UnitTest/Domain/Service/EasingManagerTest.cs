using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="EasingManager"/>のテスト
    /// </summary>
    public static class EasingManagerTest
    {
        [Test]
        public static void InTest_正常系_Linear()
        {
            var easing = Easing.Pattern.Linear;
            var limit = 1000;
            var max = 100f;

            for(int time = 0; time <= limit; time++)
            {
                easing.In(time, limit).value.Is((float)time / limit, $"time={time}");
                easing.In(time, limit, max).value.Is(max * time / limit, $"time={time}");
            }
        }
        [Test]
        public static void InTest_正常系_Quadratic()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.In(0, limit).value.Is(0);
            easing.In(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.In(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.In(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.In(limit, limit).value.Is(1f);
            easing.In(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InTest_正常系_Cubic()
        {
            var easing = Easing.Pattern.Cubic;
            var limit = 1000;
            var max = 100f;

            easing.In(0, limit).value.Is(0);
            easing.In(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.In(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.In(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.In(limit, limit).value.Is(1f);
            easing.In(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InTest_正常系_Quartic()
        {
            var easing = Easing.Pattern.Quartic;
            var limit = 1000;
            var max = 100f;

            easing.In(0, limit).value.Is(0);
            easing.In(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.In(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.In(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.In(limit, limit).value.Is(1f);
            easing.In(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InTest_正常系_Quintic()
        {
            var easing = Easing.Pattern.Quintic;
            var limit = 1000;
            var max = 100f;

            easing.In(0, limit).value.Is(0);
            easing.In(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.In(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.In(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.In(limit, limit).value.Is(1f);
            easing.In(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InTest_正常系_Sinusoidal()
        {
            var easing = Easing.Pattern.Sinusoidal;
            var limit = 1000;
            var max = 100f;

            easing.In(0, limit).value.Is(0);
            easing.In(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.In(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.In(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.In(limit, limit).value.Is(1f);
            easing.In(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InTest_正常系_Exponential()
        {
            var easing = Easing.Pattern.Exponential;
            var limit = 1000;
            var max = 100f;

            //Exponentialは経過時間0の時の変動量が0じゃない
            //easing.In(0, limit).value.Is(0);
            //easing.In(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.In(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.In(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.In(limit, limit).value.Is(1f);
            easing.In(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InTest_正常系_Circular()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.In(0, limit).value.Is(0);
            easing.In(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.In(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.In(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.In(limit, limit).value.Is(1f);
            easing.In(limit, limit, max).value.Is(max);
        }

        [Test]
        public static void SubInTest_正常系_Linear()
        {
            var easing = Easing.Pattern.Linear;
            var limit = 1000;
            var max = 100f;

            for(int time = 0; time <= limit; time++)
            {
                easing.SubIn(time, limit).Is(1 - time.DividedBy(limit), $"time={time}");
                easing.SubIn(time, limit, max).Is(max - max * time.DividedBy(limit), $"time={time}");
            }
        }
        [Test]
        public static void SubInTest_正常系_Quadratic()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.SubIn(0, limit).value.Is(1f);
            easing.SubIn(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubIn(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubIn(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubIn(limit, limit).value.Is(0);
            easing.SubIn(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInTest_正常系_Cubic()
        {
            var easing = Easing.Pattern.Cubic;
            var limit = 1000;
            var max = 100f;

            easing.SubIn(0, limit).value.Is(1f);
            easing.SubIn(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubIn(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubIn(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubIn(limit, limit).value.Is(0);
            easing.SubIn(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInTest_正常系_Quartic()
        {
            var easing = Easing.Pattern.Quartic;
            var limit = 1000;
            var max = 100f;

            easing.SubIn(0, limit).value.Is(1f);
            easing.SubIn(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubIn(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubIn(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubIn(limit, limit).value.Is(0);
            easing.SubIn(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInTest_正常系_Quintic()
        {
            var easing = Easing.Pattern.Quintic;
            var limit = 1000;
            var max = 100f;

            easing.SubIn(0, limit).value.Is(1f);
            easing.SubIn(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubIn(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubIn(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubIn(limit, limit).value.Is(0);
            easing.SubIn(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInTest_正常系_Sinusoidal()
        {
            var easing = Easing.Pattern.Sinusoidal;
            var limit = 1000;
            var max = 100f;

            easing.SubIn(0, limit).value.Is(1f);
            easing.SubIn(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubIn(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubIn(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubIn(limit, limit).value.Is(0);
            easing.SubIn(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInTest_正常系_Exponential()
        {
            var easing = Easing.Pattern.Exponential;
            var limit = 1000;
            var max = 100f;

            //Exponentialは経過時間0の時の変動量が0じゃない
            //easing.SubIn(0, limit).value.Is(1f);
            //easing.SubIn(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubIn(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubIn(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubIn(limit, limit).value.Is(0);
            easing.SubIn(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInTest_正常系_Circular()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.SubIn(0, limit).value.Is(1f);
            easing.SubIn(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubIn(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubIn(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubIn(limit, limit).value.Is(0);
            easing.SubIn(limit, limit, max).value.Is(0);
        }

        [Test]
        public static void OutTest_正常系_Linear()
        {
            var easing = Easing.Pattern.Linear;
            var limit = 1000;
            var max = 100f;

            for(int time = 0; time <= limit; time++)
            {
                easing.Out(time, limit).value.Is((float)time / limit, $"time={time}");
                easing.Out(time, limit, max).value.Is(max * time / limit, $"time={time}");
            }
        }
        [Test]
        public static void OutTest_正常系_Quadratic()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.Out(0, limit).value.Is(0);
            easing.Out(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.Out(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.Out(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.Out(limit, limit).value.Is(1f);
            easing.Out(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void OutTest_正常系_Cubic()
        {
            var easing = Easing.Pattern.Cubic;
            var limit = 1000;
            var max = 100f;

            easing.Out(0, limit).value.Is(0);
            easing.Out(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.Out(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.Out(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.Out(limit, limit).value.Is(1f);
            easing.Out(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void OutTest_正常系_Quartic()
        {
            var easing = Easing.Pattern.Quartic;
            var limit = 1000;
            var max = 100f;

            easing.Out(0, limit).value.Is(0);
            easing.Out(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.Out(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.Out(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.Out(limit, limit).value.Is(1f);
            easing.Out(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void OutTest_正常系_Quintic()
        {
            var easing = Easing.Pattern.Quintic;
            var limit = 1000;
            var max = 100f;

            easing.Out(0, limit).value.Is(0);
            easing.Out(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.Out(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.Out(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.Out(limit, limit).value.Is(1f);
            easing.Out(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void OutTest_正常系_Sinusoidal()
        {
            var easing = Easing.Pattern.Sinusoidal;
            var limit = 1000;
            var max = 100f;

            easing.Out(0, limit).value.Is(0);
            easing.Out(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.Out(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.Out(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.Out(limit, limit).value.Is(1f);
            easing.Out(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void OutTest_正常系_Exponential()
        {
            var easing = Easing.Pattern.Exponential;
            var limit = 1000;
            var max = 100f;

            easing.Out(0, limit).value.Is(0);
            easing.Out(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.Out(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.Out(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            ////Exponentialは経過時間0の時の変動量が0じゃない
            //easing.Out(limit, limit).value.Is(1f);
            //easing.Out(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void OutTest_正常系_Circular()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.Out(0, limit).value.Is(0);
            easing.Out(0, limit, max).value.Is(0);
            for(int time = 1; time < limit; time++)
            {
                (easing.Out(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.Out(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.Out(limit, limit).value.Is(1f);
            easing.Out(limit, limit, max).value.Is(max);
        }

        [Test]
        public static void SubOutTest_正常系_Linear()
        {
            var easing = Easing.Pattern.Linear;
            var limit = 1000;
            var max = 100f;

            for(int time = 0; time <= limit; time++)
            {
                easing.SubOut(time, limit).Is(1 - time.DividedBy(limit), $"time={time}");
                easing.SubOut(time, limit, max).Is(max - max * time.DividedBy(limit), $"time={time}");
            }
        }
        [Test]
        public static void SubOutTest_正常系_Quadratic()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.SubOut(0, limit).value.Is(1f);
            easing.SubOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubOut(limit, limit).value.Is(0);
            easing.SubOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubOutTest_正常系_Cubic()
        {
            var easing = Easing.Pattern.Cubic;
            var limit = 1000;
            var max = 100f;

            easing.SubOut(0, limit).value.Is(1f);
            easing.SubOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubOut(limit, limit).value.Is(0);
            easing.SubOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubOutTest_正常系_Quartic()
        {
            var easing = Easing.Pattern.Quartic;
            var limit = 1000;
            var max = 100f;

            easing.SubOut(0, limit).value.Is(1f);
            easing.SubOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubOut(limit, limit).value.Is(0);
            easing.SubOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubOutTest_正常系_Quintic()
        {
            var easing = Easing.Pattern.Quintic;
            var limit = 1000;
            var max = 100f;

            easing.SubOut(0, limit).value.Is(1f);
            easing.SubOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubOut(limit, limit).value.Is(0);
            easing.SubOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubOutTest_正常系_Sinusoidal()
        {
            var easing = Easing.Pattern.Sinusoidal;
            var limit = 1000;
            var max = 100f;

            easing.SubOut(0, limit).value.Is(1f);
            easing.SubOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubOut(limit, limit).value.Is(0);
            easing.SubOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubOutTest_正常系_Exponential()
        {
            var easing = Easing.Pattern.Exponential;
            var limit = 1000;
            var max = 100f;

            easing.SubOut(0, limit).value.Is(1f);
            easing.SubOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            //Exponentialは経過時間0の時の変動量が0じゃない
            //easing.SubOut(limit, limit).value.Is(0);
            //easing.SubOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubOutTest_正常系_Circular()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.SubOut(0, limit).value.Is(1f);
            easing.SubOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit; time++)
            {
                (easing.SubOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubOut(limit, limit).value.Is(0);
            easing.SubOut(limit, limit, max).value.Is(0);
        }

        [Test]
        public static void InOutTest_正常系_Linear()
        {
            var easing = Easing.Pattern.Linear;
            var limit = 1000;
            var max = 100f;

            for(int time = 0; time <= limit; time++)
            {
                easing.InOut(time, limit).value.Is((float)time / limit, $"time={time}");
                easing.InOut(time, limit, max).value.Is(max * time / limit, $"time={time}");
            }
        }
        [Test]
        public static void InOutTest_正常系_Quadratic()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.InOut(0, limit).value.Is(0);
            easing.InOut(0, limit, max).value.Is(0);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.InOut(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit / 2, limit).value.Is(1 / 2f);
            easing.InOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.InOut(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit, limit).value.Is(1f);
            easing.InOut(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InOutTest_正常系_Cubic()
        {
            var easing = Easing.Pattern.Cubic;
            var limit = 1000;
            var max = 100f;

            easing.InOut(0, limit).value.Is(0);
            easing.InOut(0, limit, max).value.Is(0);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.InOut(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit / 2, limit).value.Is(1 / 2f);
            easing.InOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.InOut(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit, limit).value.Is(1f);
            easing.InOut(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InOutTest_正常系_Quartic()
        {
            var easing = Easing.Pattern.Quartic;
            var limit = 1000;
            var max = 100f;

            easing.InOut(0, limit).value.Is(0);
            easing.InOut(0, limit, max).value.Is(0);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.InOut(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit / 2, limit).value.Is(1 / 2f);
            easing.InOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.InOut(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit, limit).value.Is(1f);
            easing.InOut(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InOutTest_正常系_Quintic()
        {
            var easing = Easing.Pattern.Quintic;
            var limit = 1000;
            var max = 100f;

            easing.InOut(0, limit).value.Is(0);
            easing.InOut(0, limit, max).value.Is(0);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.InOut(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit / 2, limit).value.Is(1 / 2f);
            easing.InOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.InOut(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit, limit).value.Is(1f);
            easing.InOut(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InOutTest_正常系_Sinusoidal()
        {
            var easing = Easing.Pattern.Sinusoidal;
            var limit = 1000;
            var max = 100f;

            easing.InOut(0, limit).value.Is(0);
            easing.InOut(0, limit, max).value.Is(0);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.InOut(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit / 2, limit).value.Is(1 / 2f);
            easing.InOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.InOut(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit, limit).value.Is(1f);
            easing.InOut(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InOutTest_正常系_Exponential()
        {
            var easing = Easing.Pattern.Exponential;
            var limit = 1000;
            var max = 100f;

            ////Exponentialは経過時間0の時の変動量が0じゃない
            //easing.InOut(0, limit).value.Is(0);
            //easing.InOut(0, limit, max).value.Is(0);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.InOut(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit / 2, limit).value.Is(1 / 2f);
            easing.InOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.InOut(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            ////Exponentialは経過時間0の時の変動量が0じゃない
            //easing.InOut(limit, limit).value.Is(1f);
            //easing.InOut(limit, limit, max).value.Is(max);
        }
        [Test]
        public static void InOutTest_正常系_Circular()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.InOut(0, limit).value.Is(0);
            easing.InOut(0, limit, max).value.Is(0);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.InOut(time, limit) < (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) < max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit / 2, limit).value.Is(1 / 2f);
            easing.InOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.InOut(time, limit) > (float)time / limit).IsTrue($"time={time}");
                (easing.InOut(time, limit, max) > max * time / limit).IsTrue($"time={time}");
            }
            easing.InOut(limit, limit).value.Is(1f);
            easing.InOut(limit, limit, max).value.Is(max);
        }

        [Test]
        public static void SubInOutTest_正常系_Linear()
        {
            var easing = Easing.Pattern.Linear;
            var limit = 1000;
            var max = 100f;

            for(int time = 0; time <= limit; time++)
            {
                easing.SubInOut(time, limit).Is(1 - time.DividedBy(limit), $"time={time}");
                easing.SubInOut(time, limit, max).Is(max - max * time.DividedBy(limit), $"time={time}");
            }
        }
        [Test]
        public static void SubInOutTest_正常系_Quadratic()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.SubInOut(0, limit).value.Is(1f);
            easing.SubInOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.SubInOut(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit / 2, limit).value.Is(1 / 2f);
            easing.SubInOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.SubInOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit, limit).value.Is(0);
            easing.SubInOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInOutTest_正常系_Cubic()
        {
            var easing = Easing.Pattern.Cubic;
            var limit = 1000;
            var max = 100f;

            easing.SubInOut(0, limit).value.Is(1f);
            easing.SubInOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.SubInOut(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit / 2, limit).value.Is(1 / 2f);
            easing.SubInOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.SubInOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit, limit).value.Is(0);
            easing.SubInOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInOutTest_正常系_Quartic()
        {
            var easing = Easing.Pattern.Quartic;
            var limit = 1000;
            var max = 100f;

            easing.SubInOut(0, limit).value.Is(1f);
            easing.SubInOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.SubInOut(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit / 2, limit).value.Is(1 / 2f);
            easing.SubInOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.SubInOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit, limit).value.Is(0);
            easing.SubInOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInOutTest_正常系_Quintic()
        {
            var easing = Easing.Pattern.Quintic;
            var limit = 1000;
            var max = 100f;

            easing.SubInOut(0, limit).value.Is(1f);
            easing.SubInOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.SubInOut(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit / 2, limit).value.Is(1 / 2f);
            easing.SubInOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.SubInOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit, limit).value.Is(0);
            easing.SubInOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInOutTest_正常系_Sinusoidal()
        {
            var easing = Easing.Pattern.Sinusoidal;
            var limit = 1000;
            var max = 100f;

            easing.SubInOut(0, limit).value.Is(1f);
            easing.SubInOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.SubInOut(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit / 2, limit).value.Is(1 / 2f);
            easing.SubInOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.SubInOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit, limit).value.Is(0);
            easing.SubInOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInOutTest_正常系_Exponential()
        {
            var easing = Easing.Pattern.Exponential;
            var limit = 1000;
            var max = 100f;

            //Exponentialは経過時間0の時の変動量が0じゃない
            //easing.SubInOut(0, limit).value.Is(1f);
            //easing.SubInOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.SubInOut(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit / 2, limit).value.Is(1 / 2f);
            easing.SubInOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.SubInOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            //Exponentialは経過時間0の時の変動量が0じゃない
            //easing.SubInOut(limit, limit).value.Is(0);
            //easing.SubInOut(limit, limit, max).value.Is(0);
        }
        [Test]
        public static void SubInOutTest_正常系_Circular()
        {
            var easing = Easing.Pattern.Quadratic;
            var limit = 1000;
            var max = 100f;

            easing.SubInOut(0, limit).value.Is(1f);
            easing.SubInOut(0, limit, max).value.Is(max);
            for(int time = 1; time < limit / 2; time++)
            {
                (easing.SubInOut(time, limit) > 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) > max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit / 2, limit).value.Is(1 / 2f);
            easing.SubInOut(limit / 2, limit, max).value.Is(max / 2f);
            for(int time = limit / 2 + 1; time < limit; time++)
            {
                (easing.SubInOut(time, limit) < 1f - (float)time / limit).IsTrue($"time={time}");
                (easing.SubInOut(time, limit, max) < max - max * time / limit).IsTrue($"time={time}");
            }
            easing.SubInOut(limit, limit).value.Is(0);
            easing.SubInOut(limit, limit, max).value.Is(0);
        }
    }
}
