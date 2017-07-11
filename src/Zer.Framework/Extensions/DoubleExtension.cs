using System;

namespace Zer.Framework.Extensions
{
    public static class DoubleExtension
    {
        public static int ToInt(this double value)
        {
            var val = Convert.ToInt32(value);
            return val;
        }
    }
}