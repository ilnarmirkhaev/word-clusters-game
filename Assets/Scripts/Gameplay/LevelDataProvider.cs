using System;
using System.Collections.Generic;
using System.Linq;
using Core.Modules;
using Core.Serialization;
using Data;
using UnityEngine;

namespace Gameplay
{
    public class LevelDataProvider
    {
        private readonly LevelsProgressionModule _progressionModule;
        private readonly ISerializer _serializer;
        // private readonly IReadOnlyList<LazyLevelData> _levels;

        public LevelDataProvider(LevelsProgressionModule progressionModule, ISerializer serializer)
        {
            _progressionModule = progressionModule;
            _serializer = serializer;

            // _levels = Resources.LoadAll<TextAsset>("LevelConfigs")
            //     .Select(asset => new LazyLevelData(asset, serializer)).ToList();
        }

        public LevelData GetNextLevelData()
        {
            // return LevelData.Test;
            var level = _progressionModule.GetNextLevelNumber();
            var json = Resources.Load<TextAsset>($"LevelConfigs/{level}");
            return _serializer.Deserialize<LevelData>(json.text);
        }


        private class LazyLevelData : Lazy<LevelData>
        {
            public LazyLevelData(TextAsset text, ISerializer serializer) :
                base(() => serializer.Deserialize<LevelData>(text.text))
            {
            }
        }
    }
}