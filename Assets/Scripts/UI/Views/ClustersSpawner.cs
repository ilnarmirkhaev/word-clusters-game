using Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI.Views
{
    public class ClustersSpawner : MonoBehaviour
    {
        [SerializeField] private ClusterView _clusterViewPrototype;
        [SerializeField] private RectTransform _clustersContainer;

        [Inject] private LevelState _levelState;
        [Inject] private IObjectResolver _resolver;

        private void Start()
        {
            InstantiateClusters();
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