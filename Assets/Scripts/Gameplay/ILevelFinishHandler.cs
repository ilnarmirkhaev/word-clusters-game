using System.Collections.Generic;

namespace Gameplay
{
    public interface ILevelFinishHandler
    {
        void Handle(IReadOnlyList<string> guessedWords);
    }
}