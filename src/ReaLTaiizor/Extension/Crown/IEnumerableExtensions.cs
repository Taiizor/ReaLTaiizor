#region Imports

using System.Collections.Generic;
using System.Linq;

#endregion

namespace ReaLTaiizor.Extension.Crown
{
    #region IEnumerableExtensionsExtension

    internal static class IEnumerableExtensions
    {
        internal static bool IsLast<T>(this IEnumerable<T> items, T item)
        {
            T last = items.LastOrDefault();
            if (last == null)
            {
                return false;
            }

            return item.Equals(last);
        }

        internal static bool IsFirst<T>(this IEnumerable<T> items, T item)
        {
            T first = items.FirstOrDefault();
            if (first == null)
            {
                return false;
            }

            return item.Equals(first);
        }

        internal static bool IsFirstOrLast<T>(this IEnumerable<T> items, T item)
        {
            return items.IsFirst(item) || items.IsLast(item);
        }
    }

    #endregion
}