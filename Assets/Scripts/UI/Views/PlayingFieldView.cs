using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using Utils;
using VContainer;

namespace UI.Views
{
    public class PlayingFieldView : ObjectWithOrderedChildren
    {
        [SerializeField] private List<LettersRowView> _rows;
        [SerializeField] private Transform _placedClustersContainer;

        [Inject] private LevelState _levelState;

        public Transform PlacedClustersContainer => _placedClustersContainer;

        public bool TryPlaceClusterView(Cluster cluster, Vector2 pointPosition, int letterIndexUnderPointer)
        {
            if (!RectTransform.IsPointInside(pointPosition)) return false;

            var row = GetClosestRow(pointPosition);
            var column = _rows[row].GetChildIndexClosestToPoint(pointPosition) - letterIndexUnderPointer;

            return _levelState.TryPlaceCluster(cluster, row, column);
        }

        private int GetClosestRow(Vector2 point) => GetChildIndexClosestToPoint(point);

        protected override IReadOnlyList<CachedMonoBehaviour> OrderedChildren => _rows;
    }
}