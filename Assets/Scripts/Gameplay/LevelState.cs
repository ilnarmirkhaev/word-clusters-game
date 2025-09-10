using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Gameplay
{
    public class LevelState
    {
        private readonly IPlayingField _playingField;
        private readonly HashSet<string> _hiddenWords;
        private readonly IReadOnlyList<Cluster> _clusters;
        private readonly List<string> _guessedWords = new();

        public event Action AllWordsSolved;

        public LevelState(IPlayingField playingField, IReadOnlyList<Word> words, IReadOnlyList<Cluster> clusters)
        {
            _playingField = playingField;
            _hiddenWords = words.Select(word => word.Value).ToHashSet();
            _clusters = clusters;

            _playingField.WordCreated += OnWordCreated;
            _playingField.WordRemoved += OnWordRemoved;
        }

        public IReadOnlyList<Cluster> Clusters => _clusters;
        public IReadOnlyList<string> GuessedWords => _guessedWords;

        public bool TryPlaceCluster(Cluster cluster, int row, int column)
        {
            return _playingField.TryPlaceCluster(cluster, row, column);
        }

        public void ReturnCluster(Cluster cluster)
        {
            _playingField.RemoveCluster(cluster);
        }

        private void OnWordCreated(string word)
        {
            if (_hiddenWords.Contains(word) && !_guessedWords.Contains(word)) // проверяем, что не собрали дубликат слова из других кластеров
            {
                _guessedWords.Add(word);
                CheckForLevelCompletion();
            }
        }

        private void OnWordRemoved(string word)
        {
            if (_hiddenWords.Contains(word))
            {
                _guessedWords.Remove(word);
            }
        }

        private void CheckForLevelCompletion()
        {
            if (_guessedWords.Count != _hiddenWords.Count) return;

            if (_guessedWords.All(word => _hiddenWords.Contains(word)))
            {
                AllWordsSolved?.Invoke();
            }
        }
    }
}