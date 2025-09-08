using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class TransformUtils
    {
        public static int GetChildIndexClosestToPoint<T>(IReadOnlyList<T> transformProviders, int count, Vector3 point,
            Func<T, Transform> func)
        {
            var minIndex = 0;
            var minDelta = float.MaxValue;
            var index = 0;

            for (var i = 0; i < count; i++)
            {
                var child = transformProviders[i];
                var distance = Vector3.Distance(point, func(child).position);
                if (distance < minDelta)
                {
                    minIndex = index;
                    minDelta = distance;
                }
                else // дистанция начинает расти, значит меньше уже не будет
                {
                    return minIndex;
                }

                index++;
            }

            return minIndex;
        }

        public static bool IsPointInside(this RectTransform rectTransform, Vector2 point)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, point);
        }
    }
}