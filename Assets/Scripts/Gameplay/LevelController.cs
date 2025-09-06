using Data;

namespace Gameplay
{
    public class LevelController
    {
        private LevelData _levelData;

        public LevelController(LevelDataProvider levelDataProvider)
        {
            _levelData = levelDataProvider.GetNextLevelData();
        }
    }
}