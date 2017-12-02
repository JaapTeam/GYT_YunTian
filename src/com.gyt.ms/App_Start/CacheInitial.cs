using System.Configuration;
using Zer.Framework.Cache;
using Zer.Framework.Extensions;

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

            string provinceString = ConfigurationManager.AppSettings["Province"];

            if (provinceString.IsNullOrEmpty())
            {
                provinceString = "京,津,沪,渝,冀,豫,云,辽,黑,湘,皖,鲁,新,苏,浙,赣,鄂,桂,甘,晋,蒙,陕,吉,闽,贵,粤,青,藏,川,宁,琼";
            }

            CacheHelper.SetCache("Province", provinceString);

            string characterString = ConfigurationManager.AppSettings["Character"];

            if (characterString.IsNullOrEmpty())
            {
                characterString = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            }

            CacheHelper.SetCache("Character", characterString);

            string webHost = ConfigurationManager.AppSettings["WebHost"];

            if (webHost.IsNullOrEmpty())
            {
                webHost = "localhost";
            }

            CacheHelper.SetCache("WebHost", webHost);
        }
    }
}