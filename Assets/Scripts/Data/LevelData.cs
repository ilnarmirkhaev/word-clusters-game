using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class LevelData
    {
        public readonly IReadOnlyList<Word> Words;

        public LevelData(List<Word> words)
        {
            Words = words;
        }
    }
}