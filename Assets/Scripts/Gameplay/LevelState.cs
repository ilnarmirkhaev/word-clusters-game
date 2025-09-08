using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
    public class LevelState
    {
        private readonly IPlayingField _playingField;
        private readonly List<Cluster> _clusters;

        public LevelState(IPlayingField playingField, List<string> clusters)
        {
            _playingField = playingField;
            _clusters = clusters.Select(c => new Cluster(c)).ToList();
        }

        public IReadOnlyList<Cluster> Clusters => _clusters;

        public bool TryPlaceCluster(Cluster cluster, int row, int column)
        {
            return _playingField.TryPlaceCluster(cluster, row, column);
        }

        public void ReturnCluster(Cluster cluster)
        {
            _playingField.RemoveCluster(cluster);
        }
    }
}