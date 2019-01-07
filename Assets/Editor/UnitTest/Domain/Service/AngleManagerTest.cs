using Assets.Src.Domain.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="AngleManager"/>サービスのテスト
    /// </summary>
    public static class AngleManagerTest
    {
        [Test]
        public static void Max()
        {
            var angle1 = 405;
            var angle2 = 60;
            var angle3 = -120;

            AngleManager.MaxAngle(angle1, angle2).Is(60);
            AngleManager.MaxAngle(angle2, angle3).Is(-120);
            AngleManager.MaxAngle(angle1, angle3).Is(-120);
        }
        [Test]
        public static void Min()
        {
            var angle1 = 405;
            var angle2 = 60;
            var angle3 = -120;

            AngleManager.MinAngle(angle1, angle2).Is(405);
            AngleManager.MinAngle(angle2, angle3).Is(60);
            AngleManager.MinAngle(angle1, angle3).Is(405);
        }
        [Test]
        public static void Acute()
        {
            var angle1 = 405;
            var angle2 = 60;
            var angle3 = -120;

            AngleManager.Acute(angle1).Is(45);
            AngleManager.Acute(angle2).Is(60);
            AngleManager.Acute(angle3).Is(120);
        }
        [Test]
        public static void Correct()
        {
            var angle1 = 405;
            var angle2 = 60;

            AngleManager.CorrectAngle(angle1, angle2).Is(52.5f);
            AngleManager.CorrectAngle(angle1, angle2, 1).Is(45);
            AngleManager.CorrectAngle(angle1, angle2, 0).Is(60);
        }
        [Test]
        public static void Compile()
        {
            var angle1 = 405;
            var angle2 = 60;
            var angle3 = -120;

            AngleManager.Compile(angle1).Is(45);
            AngleManager.Compile(angle2).Is(60);
            AngleManager.Compile(angle3).Is(240);
        }
        [Test]
        public static void ToAngle()
        {
            var vector1 = new Vector2(5, 5);
            var vector2 = new Vector2(-3, 3 * Mathf.Sqrt(3));
            var vector3 = new Vector2(-6, -2 * Mathf.Sqrt(3));
            var vector4 = new Vector2(12.3f, -12.3f);

            //どうしても有効桁数下2桁あたりで検算側に誤差が出るため手動で丸める
            (Mathf.Round(AngleManager.ToAngle(vector1) * 1000) / 1000).Is(45);
            (Mathf.Round(AngleManager.ToAngle(vector2) * 1000) / 1000).Is(120);
            (Mathf.Round(AngleManager.ToAngle(vector3) * 1000) / 1000).Is(210);
            (Mathf.Round(AngleManager.ToAngle(vector4) * 1000) / 1000).Is(315);
        }
        [Test]
        public static void ToAngle2()
        {
            var rotation = Quaternion.AngleAxis(120, Vector3.forward);

            (Mathf.Round(AngleManager.ToAngle(rotation) * 1000) / 1000).Is(120);
        }
        [Test]
        public static void ToRotation()
        {
            var angle = 120f;

            AngleManager.ToRotation(angle).Is(Quaternion.AngleAxis(120, Vector3.forward));
        }
        [Test]
        public static void ToRotation3()
        {
            var vector = new Vector2(5, 5);

            AngleManager.ToRotation(vector).Is(Quaternion.AngleAxis(45, Vector3.forward));
        }
        [Test]
        public static void Invert()
        {
            var angle1 = 405;
            var angle2 = 60;
            var angle3 = -120;

            AngleManager.Invert(angle1).Is(135f);
            AngleManager.Invert(angle2).Is(120f);
            AngleManager.Invert(angle3).Is(300f);
        }
        [Test]
        public static void Invert2()
        {
            var rotation1 = Quaternion.AngleAxis(405, Vector3.forward);
            var rotation2 = Quaternion.AngleAxis(60, Vector3.forward);
            var rotation3 = Quaternion.AngleAxis(-120, Vector3.forward);

            AngleManager.Invert(rotation1).Is(Quaternion.AngleAxis(135, Vector3.forward));
            AngleManager.Invert(rotation2).Is(Quaternion.AngleAxis(120, Vector3.forward));
            AngleManager.Invert(rotation3).Is(Quaternion.AngleAxis(300, Vector3.forward));
        }
    }
}
