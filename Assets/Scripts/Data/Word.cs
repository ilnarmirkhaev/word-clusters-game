using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Data
{
    [Serializable]
    public class Word
    {
        public readonly string Value;
        public readonly IReadOnlyList<string> Clusters;

        [JsonIgnore] public int Length => Value.Length;

        [JsonConstructor]
        public Word(string value, List<string> clusters = null)
        {
            Value = value.ToUpper();
            Clusters = clusters ?? CreateRandomClusters(Value);
        }

        public Word(string word, params int[] lengths)
        {
            Value = word.ToUpper();
            Clusters = CreateClustersFromLengths(Value, lengths);
        }

        public override string ToString()
        {
            return Value;
        }

        private List<string> CreateRandomClusters(string word)
        {
            // если не передан список кластеров, то разбить слово случайно
            return new();
        }

        private static List<string> CreateClustersFromLengths(string word, params int[] lengths)
        {
            var clusters = new List<string>();

            var index = 0;
            foreach (var length in lengths)
            {
                if (index + length > word.Length)
                {
                    throw new ArgumentException("Clusters are bigger than the word length");
                }

                clusters.Add(word.Substring(index, length));
                index += length;
            }

            if (index != word.Length)
            {
                throw new ArgumentException("Clusters are smaller than the word length");
            }

            return clusters;
        }
    }
}