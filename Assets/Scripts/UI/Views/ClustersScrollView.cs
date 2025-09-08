using Gameplay;
using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;

namespace UI.Views
{
    public class ClustersScrollView : MonoBehaviour
    {
        [SerializeField] private ClusterView _clusterViewPrototype;
        [SerializeField] private RectTransform _clustersContainer;
        [SerializeField] private RectTransform _scrollBounds;

        [Inject] private LevelState _levelState;
        [Inject] private IObjectResolver _resolver;

        public RectTransform ClustersContainer => _clustersContainer;

        private void Start()
        {
            InstantiateClusters();
        }

        public bool TryReturnCluster(Cluster cluster, Vector2 pointPosition)
        {
            if (!_scrollBounds.IsPointInside(pointPosition)) return false;

            _levelState.ReturnCluster(cluster);
            return true;
        }

        private void InstantiateClusters()
        {
            foreach (var cluster in _levelState.Clusters)
            {
                var view = _resolver.Instantiate(_clusterViewPrototype, _clustersContainer);
                view.Setup(cluster);
            }
        }
    }
}