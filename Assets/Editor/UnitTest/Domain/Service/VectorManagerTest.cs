using Assets.Src.Domain.Service;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.TextAnchor;


namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="VectorManager"/>クラスのテスト
    /// </summary>
    public static class VectorManagerTest
    {
        [Test]
        public static void Max()
        {
            var bigVector = new Vector2(5, 5);
            var smallVector = new Vector2(3, 4);

            VectorManager.Max(smallVector, bigVector).Is(new Vector2(5, 5));
        }
        [Test]
        public static void Max2()
        {
            var vector = new Vector2(3, 4);
            var smallScalar = 4f;
            var bigScalar = 10f;

            VectorManager.Max(vector, smallScalar).Is(new Vector2(3, 4));
            VectorManager.Max(vector, bigScalar).Is(new Vector2(6, 8));
        }
        [Test]
        public static void Min()
        {
            var bigVector = new Vector2(5, 5);
            var smallVector = new Vector2(3, 4);

            VectorManager.Min(smallVector, bigVector).Is(new Vector2(3, 4));
        }
        [Test]
        public static void Min2()
        {
            var vector = new Vector2(6, 8);
            var smallScalar = 5f;
            var bigScalar = 12f;

            VectorManager.Min(vector, smallScalar).Is(new Vector2(3, 4));
            VectorManager.Min(vector, bigScalar).Is(new Vector2(6, 8));
        }
        [Test]
        public static void Abs()
        {
            var vector = new Vector2(-3, -4);

            VectorManager.Abs(vector).Is(new Vector2(3, 4));
        }
        [Test]
        public static void Correct()
        {
            var vector1 = new Vector2(3, 4);
            var vector2 = new Vector2(5, 5);

            VectorManager.Correct(vector1, vector2).Is(new Vector2(4, 4.5f));
            VectorManager.Correct(vector1, vector2, 1).Is(new Vector2(3, 4));
            VectorManager.Correct(vector1, vector2, 0).Is(new Vector2(5, 5));
        }
        [Test]
        public static void Scaling()
        {
            var vector1 = new Vector2(3, 4);
            var vector2 = new Vector2(5, 5);

            VectorManager.Scaling(vector1, vector2).Is(new Vector2(15, 20));
        }
        [Test]
        public static void Scaling2()
        {
            var vector = new Vector2(3, 4);
            var scalar1 = 3f;
            var scalar2 = 7f;

            VectorManager.Scaling(vector, scalar1, scalar2).Is(new Vector2(9, 28));
        }
        [Test]
        public static void Rescaling()
        {
            var vector1 = new Vector2(3, 4);
            var vector2 = new Vector2(5, 5);

            VectorManager.Rescaling(vector1, vector2).Is(new Vector2(0.6f, 0.8f));
        }
        [Test]
        public static void Rescaling2()
        {
            var vector = new Vector2(3, 4);
            var scalar1 = 3f;
            var scalar2 = 10f;

            VectorManager.Rescaling(vector, scalar1, scalar2).Is(new Vector2(1, 0.4f));
        }
        [Test]
        public static void Rotate()
        {
            var vector1 = new Vector2(3, 4);
            var pivot1 = new Vector2(2, 5);
            var pivot2 = new Vector2(-4, 0);
            var angle1 = 90;
            var angle2 = -90;

            //どうしても有効桁数下2桁あたりで検算側に誤差が出るため手動で丸める
            (Mathf.Round(VectorManager.Rotate(vector1, pivot1, angle1).x * 1000) / 1000).Is(3);
            (Mathf.Round(VectorManager.Rotate(vector1, pivot1, angle1).y * 1000) / 1000).Is(6);
            (Mathf.Round(VectorManager.Rotate(vector1, pivot1, angle2).x * 1000) / 1000).Is(1);
            (Mathf.Round(VectorManager.Rotate(vector1, pivot1, angle2).y * 1000) / 1000).Is(4);
            (Mathf.Round(VectorManager.Rotate(vector1, pivot2, angle1).x * 1000) / 1000).Is(-8);
            (Mathf.Round(VectorManager.Rotate(vector1, pivot2, angle1).y * 1000) / 1000).Is(7);
            (Mathf.Round(VectorManager.Rotate(vector1, pivot2, angle2).x * 1000) / 1000).Is(0);
            (Mathf.Round(VectorManager.Rotate(vector1, pivot2, angle2).y * 1000) / 1000).Is(-7);
        }
        [Test]
        public static void ToVector1()
        {
            var vector = new Vector2(3, 4);
            var scalar = 3f;

            VectorManager.ToVector(vector).Is(new Vector2(0.6f, 0.8f));
            VectorManager.ToVector(vector, scalar).Is(new Vector2(1.8f, 2.4f));
            VectorManager.ToVector(Vector2.zero, scalar).Is(Vector2.zero);
        }
        [Test]
        public static void ToVector2()
        {
            var vector1 = new Vector2(3, 4);
            var vector2 = new Vector2(5, 12);

            VectorManager.ToVector(vector1, vector2).Is(new Vector2(7.8f, 10.4f));
        }
        [Test]
        public static void ToVector3()
        {
            var rotation = Quaternion.AngleAxis(60, Vector3.forward);
            var scalar = 3f;

            VectorManager.ToVector(rotation).Is(new Vector2(0.5f, 0.5f * Mathf.Sqrt(3)));
            VectorManager.ToVector(rotation, scalar).Is(new Vector2(1.5f, 1.5f * Mathf.Sqrt(3)));
        }
        [Test]
        public static void ToVector4()
        {
            var angle = 60f;
            var scalar = 3f;

            VectorManager.ToVector(angle).Is(new Vector2(0.5f, 0.5f * Mathf.Sqrt(3)));
            VectorManager.ToVector(angle, scalar).Is(new Vector2(1.5f, 1.5f * Mathf.Sqrt(3)));
        }
        [Test]
        public static void ToVector5()
        {
            var angle = 60f;
            var vector = new Vector2(3, 4);

            VectorManager.ToVector(angle, vector).Is(new Vector2(2.5f, 2.5f * Mathf.Sqrt(3)));
        }
        [Test]
        public static void Invert()
        {
            var vector1 = new Vector2(3, 4);
            var vector2 = new Vector2(-4, 5);
            var vector3 = new Vector2(0, 34);

            VectorManager.Invert(vector1).Is(new Vector2(-3, 4));
            VectorManager.Invert(vector2).Is(new Vector2(4, 5));
            VectorManager.Invert(vector3).Is(new Vector2(0, 34));
        }
        [Test]
        public static void GetIntXTest()
        {
            new Vector2(3, 4).GetIntX().Is(3);
            new Vector2(-4, 5).GetIntX().Is(-4);
            new Vector2(3.4f, 0).GetIntX().Is(3);
            new Vector2(7.6f, 0).GetIntX().Is(7);
            new Vector2(-37.4f, 3.4f).GetIntX().Is(-37);
            new Vector2(-45.8f, 3.4f).GetIntX().Is(-45);
        }
        [Test]
        public static void GetIntYTest()
        {
            new Vector2(3, 4).GetIntY().Is(4);
            new Vector2(4, -5).GetIntY().Is(-5);
            new Vector2(3.4f, 3.4f).GetIntY().Is(3);
            new Vector2(3.4f, 7.6f).GetIntY().Is(7);
            new Vector2(-4.3f, -37.4f).GetIntY().Is(-37);
            new Vector2(-4.3f, -45.8f).GetIntY().Is(-45);
        }
        [Test]
        public static void Within()
        {
            var vector1 = new Vector2(-2, -4);
            var vector2 = new Vector2(3, -4);
            var vector3 = new Vector2(8, -4);
            var vector4 = new Vector2(-2, 7);
            var vector5 = new Vector2(3, 7);
            var vector6 = new Vector2(8, 7);
            var vector7 = new Vector2(-2, 18);
            var vector8 = new Vector2(3, 18);
            var vector9 = new Vector2(8, 18);
            var underLeft = new Vector2(-1, -2);
            var upperRight = new Vector2(5, 12);

            VectorManager.Within(vector1, underLeft, upperRight).Is(new Vector2(-1, -2));
            VectorManager.Within(vector2, underLeft, upperRight).Is(new Vector2(3, -2));
            VectorManager.Within(vector3, underLeft, upperRight).Is(new Vector2(5, -2));
            VectorManager.Within(vector4, underLeft, upperRight).Is(new Vector2(-1, 7));
            VectorManager.Within(vector5, underLeft, upperRight).Is(new Vector2(3, 7));
            VectorManager.Within(vector6, underLeft, upperRight).Is(new Vector2(5, 7));
            VectorManager.Within(vector7, underLeft, upperRight).Is(new Vector2(-1, 12));
            VectorManager.Within(vector8, underLeft, upperRight).Is(new Vector2(3, 12));
            VectorManager.Within(vector9, underLeft, upperRight).Is(new Vector2(5, 12));
        }
        [Test]
        public static void GetAxis()
        {
            VectorManager.GetAxis(LowerLeft).Is(new Vector2(0, 0));
            VectorManager.GetAxis(LowerCenter).Is(new Vector2(0.5f, 0));
            VectorManager.GetAxis(LowerRight).Is(new Vector2(1, 0));
            VectorManager.GetAxis(MiddleLeft).Is(new Vector2(0, 0.5f));
            VectorManager.GetAxis(MiddleCenter).Is(new Vector2(0.5f, 0.5f));
            VectorManager.GetAxis(MiddleRight).Is(new Vector2(1, 0.5f));
            VectorManager.GetAxis(UpperLeft).Is(new Vector2(0, 1));
            VectorManager.GetAxis(UpperCenter).Is(new Vector2(0.5f, 1));
            VectorManager.GetAxis(UpperRight).Is(new Vector2(1, 1));

            VectorManager.GetAxis(LowerLeft, LowerLeft).Is(new Vector2(0, 0));
            VectorManager.GetAxis(LowerCenter, LowerLeft).Is(new Vector2(0.5f, 0));
            VectorManager.GetAxis(LowerRight, LowerLeft).Is(new Vector2(1, 0));
            VectorManager.GetAxis(MiddleLeft, LowerLeft).Is(new Vector2(0, 0.5f));
            VectorManager.GetAxis(MiddleCenter, LowerLeft).Is(new Vector2(0.5f, 0.5f));
            VectorManager.GetAxis(MiddleRight, LowerLeft).Is(new Vector2(1, 0.5f));
            VectorManager.GetAxis(UpperLeft, LowerLeft).Is(new Vector2(0, 1));
            VectorManager.GetAxis(UpperCenter, LowerLeft).Is(new Vector2(0.5f, 1));
            VectorManager.GetAxis(UpperRight, LowerLeft).Is(new Vector2(1, 1));

            VectorManager.GetAxis(LowerLeft, LowerCenter).Is(new Vector2(-0.5f, 0));
            VectorManager.GetAxis(LowerCenter, LowerCenter).Is(new Vector2(0, 0));
            VectorManager.GetAxis(LowerRight, LowerCenter).Is(new Vector2(0.5f, 0));
            VectorManager.GetAxis(MiddleLeft, LowerCenter).Is(new Vector2(-0.5f, 0.5f));
            VectorManager.GetAxis(MiddleCenter, LowerCenter).Is(new Vector2(0, 0.5f));
            VectorManager.GetAxis(MiddleRight, LowerCenter).Is(new Vector2(0.5f, 0.5f));
            VectorManager.GetAxis(UpperLeft, LowerCenter).Is(new Vector2(-0.5f, 1));
            VectorManager.GetAxis(UpperCenter, LowerCenter).Is(new Vector2(0, 1));
            VectorManager.GetAxis(UpperRight, LowerCenter).Is(new Vector2(0.5f, 1));

            VectorManager.GetAxis(LowerLeft, LowerRight).Is(new Vector2(-1, 0));
            VectorManager.GetAxis(LowerCenter, LowerRight).Is(new Vector2(-0.5f, 0));
            VectorManager.GetAxis(LowerRight, LowerRight).Is(new Vector2(0, 0));
            VectorManager.GetAxis(MiddleLeft, LowerRight).Is(new Vector2(-1, 0.5f));
            VectorManager.GetAxis(MiddleCenter, LowerRight).Is(new Vector2(-0.5f, 0.5f));
            VectorManager.GetAxis(MiddleRight, LowerRight).Is(new Vector2(0, 0.5f));
            VectorManager.GetAxis(UpperLeft, LowerRight).Is(new Vector2(-1, 1));
            VectorManager.GetAxis(UpperCenter, LowerRight).Is(new Vector2(-0.5f, 1));
            VectorManager.GetAxis(UpperRight, LowerRight).Is(new Vector2(0, 1));

            VectorManager.GetAxis(LowerLeft, MiddleLeft).Is(new Vector2(0, -0.5f));
            VectorManager.GetAxis(LowerCenter, MiddleLeft).Is(new Vector2(0.5f, -0.5f));
            VectorManager.GetAxis(LowerRight, MiddleLeft).Is(new Vector2(1, -0.5f));
            VectorManager.GetAxis(MiddleLeft, MiddleLeft).Is(new Vector2(0, 0));
            VectorManager.GetAxis(MiddleCenter, MiddleLeft).Is(new Vector2(0.5f, 0));
            VectorManager.GetAxis(MiddleRight, MiddleLeft).Is(new Vector2(1, 0));
            VectorManager.GetAxis(UpperLeft, MiddleLeft).Is(new Vector2(0, 0.5f));
            VectorManager.GetAxis(UpperCenter, MiddleLeft).Is(new Vector2(0.5f, 0.5f));
            VectorManager.GetAxis(UpperRight, MiddleLeft).Is(new Vector2(1, 0.5f));

            VectorManager.GetAxis(LowerLeft, MiddleCenter).Is(new Vector2(-0.5f, -0.5f));
            VectorManager.GetAxis(LowerCenter, MiddleCenter).Is(new Vector2(0, -0.5f));
            VectorManager.GetAxis(LowerRight, MiddleCenter).Is(new Vector2(0.5f, -0.5f));
            VectorManager.GetAxis(MiddleLeft, MiddleCenter).Is(new Vector2(-0.5f, 0));
            VectorManager.GetAxis(MiddleCenter, MiddleCenter).Is(new Vector2(0, 0));
            VectorManager.GetAxis(MiddleRight, MiddleCenter).Is(new Vector2(0.5f, 0));
            VectorManager.GetAxis(UpperLeft, MiddleCenter).Is(new Vector2(-0.5f, 0.5f));
            VectorManager.GetAxis(UpperCenter, MiddleCenter).Is(new Vector2(0, 0.5f));
            VectorManager.GetAxis(UpperRight, MiddleCenter).Is(new Vector2(0.5f, 0.5f));

            VectorManager.GetAxis(LowerLeft, MiddleRight).Is(new Vector2(-1, -0.5f));
            VectorManager.GetAxis(LowerCenter, MiddleRight).Is(new Vector2(-0.5f, -0.5f));
            VectorManager.GetAxis(LowerRight, MiddleRight).Is(new Vector2(0, -0.5f));
            VectorManager.GetAxis(MiddleLeft, MiddleRight).Is(new Vector2(-1, 0));
            VectorManager.GetAxis(MiddleCenter, MiddleRight).Is(new Vector2(-0.5f, 0));
            VectorManager.GetAxis(MiddleRight, MiddleRight).Is(new Vector2(0, 0));
            VectorManager.GetAxis(UpperLeft, MiddleRight).Is(new Vector2(-1, 0.5f));
            VectorManager.GetAxis(UpperCenter, MiddleRight).Is(new Vector2(-0.5f, 0.5f));
            VectorManager.GetAxis(UpperRight, MiddleRight).Is(new Vector2(0, 0.5f));

            VectorManager.GetAxis(LowerLeft, UpperLeft).Is(new Vector2(0, -1));
            VectorManager.GetAxis(LowerCenter, UpperLeft).Is(new Vector2(0.5f, -1));
            VectorManager.GetAxis(LowerRight, UpperLeft).Is(new Vector2(1, -1));
            VectorManager.GetAxis(MiddleLeft, UpperLeft).Is(new Vector2(0, -0.5f));
            VectorManager.GetAxis(MiddleCenter, UpperLeft).Is(new Vector2(0.5f, -0.5f));
            VectorManager.GetAxis(MiddleRight, UpperLeft).Is(new Vector2(1, -0.5f));
            VectorManager.GetAxis(UpperLeft, UpperLeft).Is(new Vector2(0, 0));
            VectorManager.GetAxis(UpperCenter, UpperLeft).Is(new Vector2(0.5f, 0));
            VectorManager.GetAxis(UpperRight, UpperLeft).Is(new Vector2(1, 0));

            VectorManager.GetAxis(LowerLeft, UpperCenter).Is(new Vector2(-0.5f, -1));
            VectorManager.GetAxis(LowerCenter, UpperCenter).Is(new Vector2(0, -1));
            VectorManager.GetAxis(LowerRight, UpperCenter).Is(new Vector2(0.5f, -1));
            VectorManager.GetAxis(MiddleLeft, UpperCenter).Is(new Vector2(-0.5f, -0.5f));
            VectorManager.GetAxis(MiddleCenter, UpperCenter).Is(new Vector2(0, -0.5f));
            VectorManager.GetAxis(MiddleRight, UpperCenter).Is(new Vector2(0.5f, -0.5f));
            VectorManager.GetAxis(UpperLeft, UpperCenter).Is(new Vector2(-0.5f, 0));
            VectorManager.GetAxis(UpperCenter, UpperCenter).Is(new Vector2(0, 0));
            VectorManager.GetAxis(UpperRight, UpperCenter).Is(new Vector2(0.5f, 0));

            VectorManager.GetAxis(LowerLeft, UpperRight).Is(new Vector2(-1, -1));
            VectorManager.GetAxis(LowerCenter, UpperRight).Is(new Vector2(-0.5f, -1));
            VectorManager.GetAxis(LowerRight, UpperRight).Is(new Vector2(0, -1));
            VectorManager.GetAxis(MiddleLeft, UpperRight).Is(new Vector2(-1, -0.5f));
            VectorManager.GetAxis(MiddleCenter, UpperRight).Is(new Vector2(-0.5f, -0.5f));
            VectorManager.GetAxis(MiddleRight, UpperRight).Is(new Vector2(0, -0.5f));
            VectorManager.GetAxis(UpperLeft, UpperRight).Is(new Vector2(-1, 0));
            VectorManager.GetAxis(UpperCenter, UpperRight).Is(new Vector2(-0.5f, 0));
            VectorManager.GetAxis(UpperRight, UpperRight).Is(new Vector2(0, 0));
        }
        [Test]
        public static void Log1()
        {
            var vector1 = Vector2.one.normalized * (Mathf.Exp(32) - 1);
            var vector2 = new Vector2(-Mathf.Exp(26.43f) + 1, 0);

            vector1.Log().magnitude.Is(32);
            vector1.Log().normalized.Is(Vector2.one.normalized);
            vector2.Log().Is(new Vector2(-26.43f, 0));
        }
        [Test]
        public static void Log2()
        {
            var vector1 = Vector2.one.normalized * (Mathf.Pow(2.6f, 11.3f) - 1);
            var vector2 = new Vector2(-Mathf.Pow(4.63f, 26.43f) + 1, 0);

            vector1.Log(2.6f).Is(Vector2.one.normalized * 11.3f);
            vector2.Log(4.63f).Is(new Vector2(-26.43f, 0));
        }
        [Test]
        public static void EasingElliptical()
        {
            var destination = new Vector2(6, 9);
            var now = 60;
            var max = 90;

            VectorManager.EasingV.Elliptical(destination, now, max, true).y.Is(4.5f * Mathf.Sqrt(3));
            VectorManager.EasingV.Elliptical(destination, now, max, false).x.Is(3 * Mathf.Sqrt(3));
        }
        [Test]
        public static void EasingElliptical2()
        {
            var start = new Vector2(-2, -3);
            var destination = new Vector2(4, 6);
            var now = 60;
            var max = 90;

            VectorManager.EasingV.Elliptical(start, destination, now, max, true).y.Is(4.5f * Mathf.Sqrt(3) - 3);
            VectorManager.EasingV.Elliptical(start, destination, now, max, false).x.Is(3 * Mathf.Sqrt(3) - 2);
        }
    }
}
