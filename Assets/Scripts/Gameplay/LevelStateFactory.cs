using System;
using System.Linq;
using Data;

namespace Gameplay
{
    public interface ILevelStateFactory
    {
        LevelState Create(LevelData levelData);
    }

    public class LevelStateFactory : ILevelStateFactory
    {
        private readonly IPlayingFieldFactory _playingFieldFactory;
        private readonly Random _random = new();

        public LevelStateFactory(IPlayingFieldFactory playingFieldFactory)
        {
            _playingFieldFactory = playingFieldFactory;
        }

        public LevelState Create(LevelData levelData)
        {
            var wordCount = levelData.Words.Count;
            var wordLength = levelData.WordLength;
            var playingField = _playingFieldFactory.Create(wordCount, wordLength);

            var clusters = levelData.Words.SelectMany(word => word.Clusters)
                .Select(c => new Cluster(c))
                .OrderBy(_ => _random.Next())
                .ToList();

            return new LevelState(playingField, levelData.Words, clusters);
        }
    }
}