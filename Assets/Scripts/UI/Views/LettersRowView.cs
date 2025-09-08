using System.Collections.Generic;
using UnityEngine;

namespace UI.Views
{
    public class LettersRowView : ObjectWithOrderedTransformChildren
    {
        [SerializeField] private List<Transform> _letters;

        protected override IReadOnlyList<Transform> OrderedChildren => _letters;
    }
}