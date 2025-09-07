using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
    public class LevelState
    {
        private readonly PlayingFieldState _playingFieldState;
        private readonly List<Cluster> _clusters;

        public LevelState(int wordCount, int wordLength, List<string> clusters)
        {
            _playingFieldState = new PlayingFieldState(wordCount, wordLength);
            _clusters = clusters.Select(c => new Cluster(c)).ToList();
        }

        public IReadOnlyList<Cluster> Clusters => _clusters;
    }
}