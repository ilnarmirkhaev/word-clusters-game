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

        [Inject] private LevelController _levelController;
        [Inject] private IObjectResolver _resolver;

        private void Start()
        {
            InstantiateClusters();
        }

        private void InstantiateClusters()
        {
            foreach (var cluster in _levelController.GetClustersForLevel())
            {
                var view = _resolver.Instantiate(_clusterViewPrototype, _clustersContainer);
                view.Setup(cluster);
            }
        }
    }
}