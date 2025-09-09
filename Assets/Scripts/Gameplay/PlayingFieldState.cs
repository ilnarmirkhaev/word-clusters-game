using System.Collections.Generic;

namespace Gameplay
{
    public interface IPlayingField
    {
        bool TryPlaceCluster(Cluster cluster, int row, int column);
        void RemoveCluster(Cluster cluster);
    }

    public class PlayingFieldState : IPlayingField
    {
        private readonly int _wordLength;
        private readonly int _wordCount;
        private readonly Cluster[,] _field;
        private readonly Dictionary<Cluster, ClusterPosition> _usedClusters = new();

        public PlayingFieldState(int wordCount, int wordLength)
        {
            _wordCount = wordCount;
            _wordLength = wordLength;
            _field = new Cluster[wordCount, wordLength];
        }

        public Cluster this[int row, int column] => _field[row, column];

        public Cluster ClusterAt(int row, int column) => _field[row, column];

        public bool TryPlaceCluster(Cluster cluster, int row, int column)
        {
            if (row < 0 || row >= _wordCount) return false;

            if (!CanPlaceCluster(cluster, row, column)) return false;

            RemoveCluster(cluster);
            PlaceCluster(cluster, row, column);
            return true;
        }

        public void RemoveCluster(Cluster cluster)
        {
            if (_usedClusters.Remove(cluster, out var clusterPosition))
            {
                var row = clusterPosition.Row;
                var column = clusterPosition.Column;
                for (var i = column; i < column + cluster.Length; i++)
                {
                    _field[row, i] = null;
                }
            }
        }

        private void PlaceCluster(Cluster cluster, int row, int column)
        {
            for (var i = 0; i < cluster.Length; i++)
            {
                _field[row, column + i] = cluster;
            }

            _usedClusters[cluster] = new ClusterPosition(row, column);
        }

        private bool CanPlaceCluster(Cluster cluster, int row, int column)
        {
            if (column < 0 || column + cluster.Length > _wordLength)
            {
                return false;
            }

            for (var i = 0; i < cluster.Length; i++)
            {
                var cell = _field[row, column + i];
                if (cell != null && cell != cluster)
                    return false;
            }

            return true;
        }

        private readonly struct ClusterPosition
        {
            public readonly int Row;
            public readonly int Column;

            public ClusterPosition(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }
    }
}