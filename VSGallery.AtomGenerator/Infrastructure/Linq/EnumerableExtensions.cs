using System;
using System.Collections;
using System.Collections.Generic;

namespace VSGallery.AtomGenerator.Infrastructure.Linq
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes the given <paramref name="sideEffect"/> when the enumerable is evaluated.
        /// </summary>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> sideEffect)
        {
            foreach (var item in source)
            {
                sideEffect(item);
                yield return item;
            }
        }
    }
}
