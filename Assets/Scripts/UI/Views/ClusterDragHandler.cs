using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Views
{
    public class ClusterDragHandler : MonoBehaviour
    {
        [SerializeField] private ClusterView _fakeCluster;
        [SerializeField] private ScrollRect _clustersScroll;

        private Cluster _cluster;
        private Vector2 _grabOffset;

        private void Start()
        {
            _fakeCluster.GameObject.SetActive(false);
        }

        public void BeginDrag(PointerEventData eventData, Cluster cluster, Vector2 grabOffset = default)
        {
            _cluster = cluster;
            _grabOffset = grabOffset;
            _fakeCluster.Setup(cluster);
            _fakeCluster.GameObject.SetActive(true);
            _clustersScroll.enabled = false;

            Drag(eventData);
        }

        public void Drag(PointerEventData eventData)
        {
            _fakeCluster.Transform.position = eventData.position - _grabOffset;
        }

        public bool TryEndDrag(PointerEventData eventData, out Vector2 newPosition)
        {
            var letterIndexUnderPointer = _fakeCluster.GetLetterIndexClosestToPoint(eventData.position);
            _fakeCluster.GameObject.SetActive(false);
            newPosition = eventData.position;
            _clustersScroll.enabled = true;
            return true;
        }
    }
}