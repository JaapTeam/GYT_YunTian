using System.Collections.Generic;

namespace Zer.Framework.Export
{
    public static class ExportExtension
    {
        public static byte[] GetBuffer<T>(this IEnumerable<T> list)
            where T : class
        {
            return Export.GetBuffer(list);
        }
    }
}