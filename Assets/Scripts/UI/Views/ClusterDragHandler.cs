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

        private void Start()
        {
            _fakeCluster.gameObject.SetActive(false);
        }

        public void BeginDrag(PointerEventData eventData, Cluster cluster)
        {
            _cluster = cluster;
            _fakeCluster.Setup(cluster);
            _fakeCluster.gameObject.SetActive(true);
            _clustersScroll.enabled = false;

            Drag(eventData);
        }

        public void Drag(PointerEventData eventData)
        {
            _fakeCluster.Transform.position = eventData.position;
        }

        public bool TryEndDrag(PointerEventData eventData, out Vector2 newPosition)
        {
            _fakeCluster.gameObject.SetActive(false);
            newPosition = eventData.position;
            _clustersScroll.enabled = true;
            return true;
        }
    }
}