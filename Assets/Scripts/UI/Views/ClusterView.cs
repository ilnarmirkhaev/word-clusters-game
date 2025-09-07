using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace UI.Views
{
    public class ClusterView : MonoBehaviour
    {
        [SerializeField] private List<LetterCellView> _letterViews;

        protected Cluster Cluster;

        private Transform _transform;
        public Transform Transform => _transform ??= transform;

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
    }
}