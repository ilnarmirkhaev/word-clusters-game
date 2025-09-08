using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace UI.Views
{
    public class ClusterView : ObjectWithOrderedChildren
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

        public int GetLetterIndexClosestToPoint(Vector2 point) => GetChildIndexClosestToPoint(point);

        protected override IReadOnlyList<CachedMonoBehaviour> OrderedChildren => _letterViews;
        protected override int ActiveChildrenCount => Cluster.Length;
    }
}