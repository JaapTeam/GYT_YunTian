using System;

namespace Zer.Framework.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum t)
        {
            return Convert.ToInt32(t);
        }
    }
}
