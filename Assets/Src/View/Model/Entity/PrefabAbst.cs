using UnityEngine;

namespace Assets.Src.View.Model.Entity
{
    /// <summary>
    /// Prefabの大元クラス
    /// </summary>
    public abstract class PrefabAbst : MonoBehaviour
    {
        /// <summary>
        /// ローカル位置座標
        /// </summary>
        public Vector2 position
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }
        /// <summary>
        /// ローカル移動先座標
        /// </summary>
        public Vector2 targetPosition
        {
            get => _targetPosition ?? position;
            set => _targetPosition = value;
        }
        Vector2? _targetPosition;
    }
}
