using System.Collections.Generic;

namespace Gameplay
{
    public interface IPlayingField
    {
        bool TryPlaceCluster(Cluster cluster, int row, int column);
        void RemoveCluster(Cluster cluster);
    }

    public partial class PlayingFieldState : IPlayingField
    {
        private readonly ClusterRow[] _rows;
        private readonly Dictionary<Cluster, ClusterPosition> _usedClusters = new();

        public PlayingFieldState(int wordCount, int wordLength)
        {
            _rows = new ClusterRow[wordCount];
            for (var i = 0; i < wordCount; i++)
            {
                _rows[i] = new ClusterRow(wordLength);
            }
        }

        public Cluster this[int row, int column] => _rows[row][column];

        public Cluster ClusterAt(int row, int column) => _rows[row][column];

        public bool TryPlaceCluster(Cluster cluster, int row, int column)
        {
            var isPlaced = _rows[row].TryPlaceCluster(cluster, column);
            if (isPlaced)
            {
                _usedClusters[cluster] = new ClusterPosition(row, column);
            }
            return isPlaced;
        }

        public void RemoveCluster(Cluster cluster)
        {
            if (_usedClusters.Remove(cluster, out var clusterPosition))
            {
                _rows[clusterPosition.Row].RemoveCluster(cluster, clusterPosition.Column);
            }
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