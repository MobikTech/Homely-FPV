using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FpvDroneSimulator.Common.Extensions
{
    public static class ListExtensions
    {
        public static (int index, TElement value) GetRandomElement<TElement>(this IEnumerable<TElement> array)
        {
            int index = Random.Range(0, array.Count());
            return (index, array.ElementAt(index));
        }
    }
}