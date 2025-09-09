using System.Collections.Generic;
using UnityEngine;

namespace UI.Views
{
    public class LettersRowView : ObjectWithOrderedTransformChildren
    {
        [SerializeField] private List<Transform> _letters;

        public Vector2 GetCentralizedPositionForLetters(int firstLetterIndex, int lettersCount)
        {
            var sum = Vector2.zero;
            for (var i = 0; i < lettersCount; i++)
            {
                sum += GetLetterPosition(i + firstLetterIndex);
            }
            return sum / lettersCount;
        }

        private Vector2 GetLetterPosition(int index) => _letters[index].position;

        protected override IReadOnlyList<Transform> OrderedChildren => _letters;
    }
}