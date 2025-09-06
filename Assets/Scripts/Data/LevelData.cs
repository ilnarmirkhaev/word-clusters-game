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

        public static LevelData Test => new LevelData(new List<Word>()
        {
            new Word("ПРИМЕР", 2, 2, 2),
            new Word("РАБОТА", 3, 3),
            new Word("ПРАВЫЙ", 2, 2, 2),
            new Word("КАЛОША", 2, 4),
        });
    }
}