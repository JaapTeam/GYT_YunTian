using log4net;

namespace Zer.Framework.Mvc.Logs
{
    public static class Log4NetLogger
    {
        public static ILog Logger { get; }

        static Log4NetLogger()
        {
            Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}