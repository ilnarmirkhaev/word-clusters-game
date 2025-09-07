using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using VContainer.Unity;

namespace Gameplay
{
    public class LevelController : IStartable
    {
        private readonly LevelData _levelData;
        private readonly LevelState _levelState;
        private readonly Random _random = new();

        public LevelController(LevelDataProvider levelDataProvider)
        {
            _levelData = levelDataProvider.GetNextLevelData();
            _levelState = CreateLevelState(_levelData);
        }

        public void Start()
        {
        }

        public IReadOnlyList<Cluster> GetClustersForLevel()
        {
            return _levelState.Clusters.OrderBy(_ => _random.Next()).ToArray();
        }

        private LevelState CreateLevelState(LevelData levelData)
        {
            if (levelData.Words.Any(word => word.Length != levelData.Words[0].Length))
            {
                throw new ArgumentException("Words in LevelData must have the same length");
            }

            var wordCount = levelData.Words.Count;
            var wordLength = levelData.Words[0].Length;
            var clusters = levelData.Words.SelectMany(word => word.Clusters).ToList();
            return new LevelState(wordCount, wordLength, clusters);
        }
    }
}