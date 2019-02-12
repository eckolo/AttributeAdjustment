using UnityEngine;

namespace Assets.Src.Domain.Model.Abstract
{
    /// <summary>
    /// Prefabの大元クラス
    /// </summary>
    public abstract class PrefabAbst : MonoBehaviour
    {
        public Vector2 position
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }
    }
}
