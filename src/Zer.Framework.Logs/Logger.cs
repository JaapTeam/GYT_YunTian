using System;

namespace Zer.Framework.Logs
{
    public class Logger
    {
        private static ILogger _errorLogger;

        static Logger()
        {
            _errorLogger = new Log4NetLogger();
        }

        public static void SiteLogConfig(string path)
        {
            _errorLogger = new Log4NetLogger(path);
        }

        public static void Error(object message)
        {
            _errorLogger.Error(message);
        }
        public static void ErrorFormat(string format, params object[] args)
        {
            _errorLogger.Info(string.Format(format, args));
        }

        public static void Error(Exception ex)
        {
            _errorLogger.Error(ex.ToString());
        }

        public static void Info(object message)
        {
            _errorLogger.Info(message);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            _errorLogger.Info(string.Format(format,args));
        }

        public static void Debug(object message)
        {
            _errorLogger.Debug(message);
        }
        public static void DebugFormat(string format, params object[] args)
        {
            _errorLogger.Info(string.Format(format, args));
        }
    }
}