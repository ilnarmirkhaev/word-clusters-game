using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UI.Views
{
    public abstract class ObjectWithOrderedChildren<T> : CachedMonoBehaviour
    {
        protected abstract IReadOnlyList<T> OrderedChildren { get; }
        protected virtual int ActiveChildrenCount => OrderedChildren.Count;
        protected abstract Func<T, Transform> GetChildTransform { get; }

        public int GetChildIndexClosestToPoint(Vector3 point)
        {
            return TransformUtils.GetChildIndexClosestToPoint(OrderedChildren, ActiveChildrenCount, point,
                GetChildTransform);
        }
    }

    public abstract class ObjectWithOrderedChildren : ObjectWithOrderedChildren<CachedMonoBehaviour>
    {
        protected override Func<CachedMonoBehaviour, Transform> GetChildTransform { get; } = cached => cached.Transform;
    }

    public abstract class ObjectWithOrderedTransformChildren : ObjectWithOrderedChildren<Transform>
    {
        protected override Func<Transform, Transform> GetChildTransform { get; } = t => t;
    }
}