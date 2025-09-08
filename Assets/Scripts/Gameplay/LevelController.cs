using VContainer.Unity;

namespace Gameplay
{
    public class LevelController : IStartable
    {
        private readonly LevelState _levelState;

        public LevelController(LevelState levelState)
        {
            _levelState = levelState;
        }

        public void Start()
        {
        }
    }
}