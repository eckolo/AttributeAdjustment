using Assets.Src.Mock.Model.Abstract;
using Assets.Src.Mock.Model.Entity;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Model.Abstract
{
    /// <summary>
    /// <see cref="PrefabAbst"/>クラス関係のテスト
    /// </summary>
    public static class PrefabAbstTest
    {
        [Test]
        public static void GetterTest_position_正常系()
        {
            var parent = MonoBehaviourMock.Generate($"{nameof(MonoBehaviourMock)}_{nameof(GetterTest_position_正常系)}");
            parent.transform.position = Vector2.one;

            var vector = new Vector2(23, -555);
            var prefabAbst = PrefabAbstMock.Generate(nameof(GetterTest_position_正常系), parent, vector);

            prefabAbst.position.IsNotNull();
            prefabAbst.position.x.Is(vector.x);
            prefabAbst.position.y.Is(vector.y);
        }

        [Test]
        public static void SetterTest_position_正常系()
        {
            var parent = MonoBehaviourMock.Generate($"{nameof(MonoBehaviourMock)}_{nameof(SetterTest_position_正常系)}");
            parent.transform.position = Vector2.one;

            var vector = new Vector2(23, -555);
            var prefabAbst = PrefabAbstMock.Generate(nameof(SetterTest_position_正常系), parent);

            prefabAbst.position = vector;

            prefabAbst.position.IsNotNull();
            prefabAbst.position.x.Is(vector.x);
            prefabAbst.position.y.Is(vector.y);
        }
    }
}
