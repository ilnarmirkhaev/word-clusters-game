using UnityEngine;

namespace UI.Views
{
    public abstract class CachedMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;
        public Transform Transform => _transform ??= transform;

        private GameObject _gameObject;
        public GameObject GameObject => _gameObject ??= gameObject;
        
        private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform ??= (RectTransform)Transform;
    }
}