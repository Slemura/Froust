using System;
using System.Collections.Generic;
using System.Linq;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        private static Random _prng;

        public static void ExceptWith<T>(this IList<T> list, IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (list.Count == 0)
                return;

            if (ReferenceEquals(other, list))
            {
                list.Clear();
            }
            else
            {
                foreach (T obj in other)
                    list.Remove(obj);
            }
        }

        public static void UnionWith<T>(this IList<T> list, IEnumerable<T> other)
        {
            if (other == null)
                throw new AggregateException(nameof(other));

            if (ReferenceEquals(other, list))
                return;

            foreach (T obj in other)
            {
                if (list.Contains(obj))
                    continue;

                list.Add(obj);
            }
        }

        public static T Pop<T>(this IList<T> list, int index)
        {
            if (list.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(list));

            T item = list[index];
            list.RemoveAt(index);

            return item;
        }

        public static T PopFirst<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(list));

            T first = list[0];
            list.RemoveAt(0);

            return first;
        }

        public static T PopLast<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(list));

            T last = list.Last();
            list.RemoveAt(list.Count - 1);

            return last;
        }

        public static void Shuffle<T>(this IList<T> list, int? seed = null)
        {
            Random prng = seed == null ? new Random() : new Random(seed.Value);

            int count = list.Count;

            while (count > 1)
            {
                count--;
                int k = prng.Next(count + 1);
                (list[k], list[count]) = (list[count], list[k]);
            }
        }

        public static T GetRandomElement<T>(this IList<T> collection)
        {
            if (collection == null)
                throw new ArgumentException($"Collection cannot be null.");

            if (collection.Count == 0)
                throw new ArgumentException($"Collection cannot be empty.");

            return collection[(_prng ??= new Random()).Next(collection.Count)];
        }
    }
}