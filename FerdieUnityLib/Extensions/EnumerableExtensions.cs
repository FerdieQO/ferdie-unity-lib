using System;
using System.Collections.Generic;
using System.Text;

namespace FerdieUnityLib.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Gets the instance of a IEnumerable{T} with the maximum value for the
        /// provided property. 
        /// </summary>
        /// <typeparam name="T">The type of the IEnumerable</typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentNullException">source</exception>
        /// <exception cref="InvalidOperationException">Sequence is empty.</exception>
        /// <example>
        /// <code>
        /// CooperativeSeasonBook mostRecentSeason = this.cooperativeSeasonBooks.MaxBy(x => x.SeasonNumber);
        /// </code>
        /// </example>
        public static T MaxBy<T, U>(this IEnumerable<T> source, Func<T, U> selector)
              where U : IComparable<U>
        {
            if (source == null) throw new ArgumentNullException("source");
            bool first = true;
            T maxObj = default(T);
            U maxKey = default(U);
            foreach (var item in source)
            {
                if (first)
                {
                    maxObj = item;
                    maxKey = selector(maxObj);
                    first = false;
                }
                else
                {
                    U currentKey = selector(item);
                    if (currentKey.CompareTo(maxKey) > 0)
                    {
                        maxKey = currentKey;
                        maxObj = item;
                    }
                }
            }
            if (first) throw new InvalidOperationException("Sequence is empty.");
            return maxObj;
        }

        public static T MinBy<T, U>(this IEnumerable<T> source, Func<T, U> selector)
              where U : IComparable<U>
        {
            if (source == null) throw new ArgumentNullException("source");
            bool first = true;
            T minObj = default(T);
            U minKey = default(U);
            foreach (var item in source)
            {
                if (first)
                {
                    minObj = item;
                    minKey = selector(minObj);
                    first = false;
                }
                else
                {
                    U currentKey = selector(item);
                    if (currentKey.CompareTo(minKey) < 0)
                    {
                        minKey = currentKey;
                        minObj = item;
                    }
                }
            }
            if (first) throw new InvalidOperationException("Sequence is empty.");
            return minObj;
        }
    }
}
