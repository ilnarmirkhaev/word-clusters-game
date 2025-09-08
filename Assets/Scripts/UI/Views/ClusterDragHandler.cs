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
        [SerializeField] private PlayingFieldView _playingField;
        [SerializeField] private ClustersScrollView _clustersScrollView;

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
            // _playingField.DisplayDrag();
        }

        public bool TryEndDrag(PointerEventData eventData, out Vector2 newPosition, out Transform newParent)
        {
            var pointPosition = eventData.position;
            var letterIndexUnderPointer = _fakeCluster.GetLetterIndexClosestToPoint(pointPosition);

            var changedPlace = false;
            if (_playingField.TryPlaceClusterView(_cluster, pointPosition, letterIndexUnderPointer))
            {
                newPosition = pointPosition - _grabOffset;
                newParent = _playingField.PlacedClustersContainer;
                changedPlace = true;
            }
            else if (_clustersScrollView.TryReturnCluster(_cluster, pointPosition))
            {
                newPosition = pointPosition;
                newParent = _clustersScrollView.ClustersContainer;
                changedPlace = true;
            }
            else
            {
                newPosition = default;
                newParent = null;
            }

            _fakeCluster.GameObject.SetActive(false);
            _clustersScroll.enabled = true;

            return changedPlace;
        }
    }
}