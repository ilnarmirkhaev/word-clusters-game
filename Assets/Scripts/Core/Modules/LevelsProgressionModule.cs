using System.Collections.Generic;

namespace Core.Modules
{
    public class LevelsProgressionState : StateBase
    {
        private readonly Dictionary<string, int> _passedLevels = new();

        public void MarkLevelPassed(string levelId)
        {
            _passedLevels.TryAdd(levelId, 0);
            _passedLevels[levelId]++;
        }

        public bool IsLevelPassed(string levelId) => _passedLevels.ContainsKey(levelId);
        
        public IReadOnlyDictionary<string, int> PassedLevels => _passedLevels;
    }

    public class LevelsProgressionModule : StateModuleBase<LevelsProgressionState>
    {
        public LevelsProgressionModule(LevelsProgressionState state) : base(state)
        {
        }

        public int GetPassedLevelsCount()
        {
            return State.PassedLevels.Count;
        }

        public int GetNextLevelNumber()
        {
            // TODO
            return 1;
        }
    }
}