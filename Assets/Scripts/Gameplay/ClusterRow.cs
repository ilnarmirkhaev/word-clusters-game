namespace Gameplay
{
    public partial class PlayingFieldState
    {
        private class ClusterRow
        {
            private readonly int _rowLength;
            private readonly Cluster[] _cells;

            public ClusterRow(int rowLength)
            {
                _rowLength = rowLength;
            }

            public Cluster this[int index] => _cells[index];

            public bool TryPlaceCluster(Cluster cluster, int startIndex)
            {
                if (!CanPlaceCluster(cluster, startIndex))
                    return false;

                for (var i = 0; i < cluster.Length; i++)
                {
                    _cells[startIndex + i] = cluster;
                }

                return true;
            }

            public void RemoveCluster(Cluster cluster)
            {
                for (var i = 0; i < _cells.Length; i++)
                {
                    if (_cells[i] == cluster)
                    {
                        _cells[i] = null;
                    }
                }
            }

            private bool CanPlaceCluster(Cluster cluster, int startIndex)
            {
                if (startIndex + cluster.Length > _rowLength)
                {
                    return false;
                }

                for (var i = 0; i < cluster.Length; i++)
                {
                    if (_cells[startIndex + i] != null)
                        return false;
                }

                return true;
            }
        }
    }
}