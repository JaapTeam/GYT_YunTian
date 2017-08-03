using System.Collections.Generic;
using System.Linq;

namespace Zer.Framework.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return !(list != null && list.Any());
        }

        public static bool IsNullOrEmpty<T>(this IQueryable<T> list)
        {
            return !(list != null && list.Any());
        }

        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return !(list != null && list.Any());
        }
    }
}
