using System.Collections.Generic;
using System.Linq;
using Webdiyer.WebControls.Mvc;

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

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> list, int pgIndex, int pgSize, int total)
        {
            return new PagedList<T>(list, pgIndex, pgSize, total);
        }

        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> list, int pgIndex, int pgSize, int total)
        {
            return new PagedList<T>(list, pgIndex, pgSize, total);
        }

        public static IPagedList<T> ToPagedList<T>(this IList<T> list, int pgIndex, int pgSize, int total)
        {
            return new PagedList<T>(list, pgIndex, pgSize, total);
        }
    }
}
