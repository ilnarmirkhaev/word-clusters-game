namespace Gameplay
{
    public partial class PlayingFieldState
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
        }
    }
}