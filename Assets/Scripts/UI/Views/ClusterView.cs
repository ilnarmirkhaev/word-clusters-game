using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace UI.Views
{
    public class ClusterView : CachedMonoBehaviour
    {
        [SerializeField] private List<LetterCellView> _letterViews;

        protected Cluster Cluster;

        public void Setup(Cluster cluster)
        {
            Cluster = cluster;

            var index = 0;
            foreach (var view in _letterViews)
            {
                var isValid = index < cluster.Length;

                view.gameObject.SetActive(isValid);
                if (isValid)
                {
                    view.Setup(cluster[index].ToString());
                }

                index++;
            }
        }

        public int GetLetterIndexClosestToPoint(Vector2 point)
        {
            var minIndex = 0;
            var minDelta = DistanceToPoint(minIndex);

            for (var i = 1; i < Cluster.Length; i++)
            {
                var distance = DistanceToPoint(i);
                if (distance < minDelta)
                {
                    minIndex = i;
                    minDelta = distance;
                }
                else // дистанция начинает расти, значит меньше уже не будет
                {
                    return minIndex;
                }
            }

            return minIndex;

            float DistanceToPoint(int letterIndex)
            {
                var view = _letterViews[letterIndex];
                return Vector2.Distance(point, view.Transform.position);
            }
        }
    }
}