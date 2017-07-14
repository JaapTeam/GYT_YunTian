using System;
using System.IO;
using log4net;
using log4net.Config;

namespace Zer.Framework.Logs
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _logger;
        public Log4NetLogger()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            _logger = LogManager.GetLogger(typeof(Log4NetLogger));
        }

        public Log4NetLogger(string path)
        {
            var logCfg = new FileInfo(path + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            _logger = LogManager.GetLogger(typeof(Log4NetLogger));
        }

        public void Error(object message)
        {
            _logger.ErrorFormat("{0}", message);
        }

        public void Info(object message)
        {
            _logger.InfoFormat("{0}", message);
        }

        public void Debug(object message)
        {
            _logger.DebugFormat("{0}", message);
        }
    }
}