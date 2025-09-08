using Core.Modules;
using Core.Serialization;
using Data;
using UnityEngine;

namespace Gameplay
{
    public interface ILevelDataProvider
    {
        LevelData GetLevelData();
    }

    public class LevelDataProvider : ILevelDataProvider
    {
        private readonly LevelsProgressionModule _progressionModule;
        private readonly ISerializer _serializer;

        public LevelDataProvider(LevelsProgressionModule progressionModule, ISerializer serializer)
        {
            _progressionModule = progressionModule;
            _serializer = serializer;
        }

        public LevelData GetLevelData()
        {
            var level = _progressionModule.GetNextLevelNumber();
            var json = Resources.Load<TextAsset>($"LevelConfigs/{level}");
            return _serializer.Deserialize<LevelData>(json.text);
        }
    }
}