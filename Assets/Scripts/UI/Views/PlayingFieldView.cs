using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using VContainer;

namespace UI.Views
{
    public class PlayingFieldView : ObjectWithOrderedChildren
    {
        [SerializeField] private List<LettersRowView> _rows;
        [SerializeField] private Transform _placedClustersContainer;

        [Inject] private LevelState _levelState;

        public Transform PlacedClustersContainer => _placedClustersContainer;

        public bool TryPlaceClusterView(int letterIndexUnderPointer, Cluster cluster, Vector2 pointPosition)
        {
            if (!IsPointInsideOfRect(pointPosition)) return false;

            var row = GetClosestRow(pointPosition);
            var column = _rows[row].GetChildIndexClosestToPoint(pointPosition) - letterIndexUnderPointer;

            return _levelState.PlayingField.TryPlaceCluster(cluster, row, column);
        }

        private bool IsPointInsideOfRect(Vector2 pointPosition)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(RectTransform, pointPosition);
        }

        private int GetClosestRow(Vector2 point) => GetChildIndexClosestToPoint(point);

        protected override IReadOnlyList<CachedMonoBehaviour> OrderedChildren => _rows;
    }
}