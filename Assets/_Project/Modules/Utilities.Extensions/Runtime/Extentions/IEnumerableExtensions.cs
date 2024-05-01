using System;
using System.Collections.Generic;
using System.Linq;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any() == false;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.IsEmpty();
        }

        public static IEnumerable<T> NotNulls<T>(this IEnumerable<T> self) where T : class
        {
            return self.Where(static element => element != null);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
                action(item);
        }

        public static bool ContainsDuplicates<T>(this IEnumerable<T> enumerable)
        {
            HashSet<T> hashSet = new HashSet<T>();
            return enumerable.Any(item => hashSet.Add(item) == false);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }
    }
}