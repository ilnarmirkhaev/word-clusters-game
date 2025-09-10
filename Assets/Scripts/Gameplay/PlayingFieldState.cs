using System;
using System.Collections.Generic;
using System.Text;

namespace Gameplay
{
    public interface IPlayingField
    {
        bool TryPlaceCluster(Cluster cluster, int row, int column);
        void RemoveCluster(Cluster cluster);
        event Action<string> WordCreated;
        event Action<string> WordRemoved;
    }

    public class PlayingFieldState : IPlayingField
    {
        private readonly int _wordLength;
        private readonly int _wordCount;
        private readonly Cluster[,] _field;

        private readonly Dictionary<Cluster, ClusterPosition> _usedClusters = new();
        private readonly Dictionary<int, int> _filledRows = new();
        private readonly Dictionary<int, string> _builtWords = new();
        private readonly StringBuilder _sb = new();

        public PlayingFieldState(int wordCount, int wordLength)
        {
            _wordCount = wordCount;
            _wordLength = wordLength;
            _field = new Cluster[wordCount, wordLength];
        }

        public event Action<string> WordCreated;
        public event Action<string> WordRemoved;

        public bool TryPlaceCluster(Cluster cluster, int row, int column)
        {
            if (!IsRowValid(row)) return false;

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

                _filledRows[row] -= cluster.Length;
                if (_builtWords.Remove(row, out var brokenWord))
                {
                    WordRemoved?.Invoke(brokenWord);
                }
            }
        }

        private bool IsRowValid(int rowIndex)
        {
            return rowIndex >= 0 && rowIndex < _wordCount;
        }

        private bool IsRowFilled(int row)
        {
            return _filledRows.TryGetValue(row, out var length) && length == _wordLength;
        }

        private void PlaceCluster(Cluster cluster, int row, int column)
        {
            for (var i = 0; i < cluster.Length; i++)
            {
                _field[row, column + i] = cluster;
            }

            _usedClusters[cluster] = new ClusterPosition(row, column);

            AddLettersCountToRow(cluster, row);
            if (IsRowFilled(row))
            {
                var newWord = BuildWord(row);
                _builtWords[row] = newWord;
                WordCreated?.Invoke(newWord);
            }
        }

        private void AddLettersCountToRow(Cluster cluster, int row)
        {
            if (_filledRows.TryGetValue(row, out var currentLetters))
            {
                _filledRows[row] = currentLetters + cluster.Length;
            }
            else
            {
                _filledRows[row] = cluster.Length;
            }
        }

        private string BuildWord(int row)
        {
            _sb.Clear();
            Cluster lastCluster = null;
            var clusterStartIndex = 0;

            for (var i = 0; i < _wordLength; i++)
            {
                var cluster = _field[row, i];
                if (cluster == null) return null;

                if (cluster != lastCluster)
                {
                    lastCluster = cluster;
                    clusterStartIndex = i;
                }

                _sb.Append(cluster[i - clusterStartIndex]);
            }

            var word = _sb.ToString();
            _sb.Clear();
            return word;
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