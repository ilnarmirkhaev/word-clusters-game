using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace UI.Views
{
    public class DraggableClusterView : ClusterView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Inject] private ClusterDragHandler _dragHandler;

        private Vector2 _dragStartPosition;
        private Vector2 _grabOffset;
        private bool _isDragging;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragStartPosition = eventData.position;
            _grabOffset = eventData.position - (Vector2)Transform.position;
            _isDragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging)
            {
                var delta = eventData.position - _dragStartPosition;

                if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
                {
                    StartClusterDrag(eventData);
                }
                else return;
            }

            if (_isDragging)
            {
                _dragHandler.Drag(eventData);
            }
        }

        private void StartClusterDrag(PointerEventData eventData)
        {
            _isDragging = true;

            _dragHandler.BeginDrag(eventData, Cluster, _grabOffset);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDragging) return;

            if (_dragHandler.TryEndDrag(eventData, out var newPosition))
            {
                Transform.position = newPosition;
            }
            
            _isDragging = false;
        }
    }
}