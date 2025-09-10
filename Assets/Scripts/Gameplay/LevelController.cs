using System.Collections.Generic;
using VContainer.Unity;

namespace Gameplay
{
    public class LevelController : IStartable
    {
        private readonly LevelState _levelState;
        private readonly IReadOnlyList<ILevelFinishHandler> _handlers;

        public LevelController(LevelState levelState, IReadOnlyList<ILevelFinishHandler> handlers)
        {
            _levelState = levelState;
            _handlers = handlers;

            _levelState.AllWordsSolved += FinishLevel;
        }

        public void Start()
        {
        }

        private void FinishLevel()
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(_levelState.GuessedWords);
            }
        }
    }
}