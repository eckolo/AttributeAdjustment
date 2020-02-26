using UnityEngine;

namespace Assets.Src.Mock.Model.Entity
{
    /// <summary>
    /// ユニットテスト用MonoBehaviour
    /// </summary>
    public class MonoBehaviourMock : MonoBehaviour
    {
        public static MonoBehaviourMock Generate(string name)
            => new GameObject($"{nameof(MonoBehaviourMock)}_{name}", typeof(MonoBehaviourMock))
                .GetComponent<MonoBehaviourMock>();
    }
}
