using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    [Serializable]
    public class LevelData
    {
        public readonly IReadOnlyList<Word> Words;

        public LevelData(List<Word> words)
        {
            var length = words[0].Length;
            if (length <= 0 || words.Any(word => word.Length != length))
            {
                throw new ArgumentException("Words in LevelData must have the same length");
            }

            Words = words;
        }
    }
}