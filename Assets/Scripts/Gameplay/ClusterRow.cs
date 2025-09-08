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
                _cells = new Cluster[rowLength];
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

            public bool RemoveCluster(Cluster cluster)
            {
                if (cluster == null) return false;

                var found = false;
                for (var i = 0; i < _cells.Length; i++)
                {
                    if (_cells[i] != cluster) continue;

                    _cells[i] = null;
                    found = true;
                }

                return found;
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