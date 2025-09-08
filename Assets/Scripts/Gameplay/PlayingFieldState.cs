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
            return _rows[row].TryPlaceCluster(cluster, column);
        }

        public void RemoveCluster(Cluster cluster)
        {
            foreach (var row in _rows)
            {
                if (row.RemoveCluster(cluster))
                {
                    break;
                }
            }
        }
    }
}