using System.Configuration;
using Zer.Framework.Cache;

namespace com.gyt.ms
{
    public class CacheInitial
    {
        public static void ReadConfig()
        {
            int pageSize;

            if (!int.TryParse(ConfigurationManager.AppSettings["PageSize"], out pageSize))
            {
                pageSize = 20;
            }

            CacheHelper.SetCache("PageSize", pageSize);

            int homePageSize;

            if (!int.TryParse(ConfigurationManager.AppSettings["HomePageSize"], out homePageSize))
            {
                homePageSize = 15;
            }

            CacheHelper.SetCache("HomePageSize", homePageSize);
        }
    }
}