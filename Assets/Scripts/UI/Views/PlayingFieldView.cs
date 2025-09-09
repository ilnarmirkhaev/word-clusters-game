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

        public bool TryPlaceClusterView(Cluster cluster, Vector2 pointPosition, int clusterLetterIndex,
            out Vector2 placedPosition)
        {
            if (!RectTransform.IsPointInside(pointPosition))
            {
                placedPosition = default;
                return false;
            }

            var row = GetClosestRow(pointPosition);
            var pointedLetterIndex = _rows[row].GetChildIndexClosestToPoint(pointPosition);
            var firstLetterIndex = pointedLetterIndex - clusterLetterIndex;

            if (_levelState.TryPlaceCluster(cluster, row, firstLetterIndex))
            {
                placedPosition = _rows[row].GetCentralizedPositionForLetters(firstLetterIndex, cluster.Length);
                return true;
            }

            placedPosition = default;
            return false;
        }

        private int GetClosestRow(Vector2 point) => GetChildIndexClosestToPoint(point);

        protected override IReadOnlyList<CachedMonoBehaviour> OrderedChildren => _rows;
    }
}