using Data;
using VContainer.Unity;

namespace Gameplay
{
    public class LevelController : IStartable
    {
        private LevelData _levelData;

        public LevelController(LevelDataProvider levelDataProvider)
        {
            _levelData = levelDataProvider.GetNextLevelData();
        }

        public void Start()
        {
        }
    }
}