using System;
using System.IO;
using System.Web.Hosting;

namespace Zer.Framework.Logs
{
    public class CommonLogger : ILogger
    {
        private readonly string _rootFolder;

        public string RootFolder
        {
            get { return _rootFolder; }
        }

        public CommonLogger()
        {
            _rootFolder = HostingEnvironment.ApplicationPhysicalPath;
        }

        protected virtual string GetFolder(LogSettings type)
        {
            var now = DateTime.Now;
            var path1 = string.Format("logs/" + type.ToString().ToLower() + "/{0}/{1}/", now.Year, now.Month);
            return Path.Combine(RootFolder, path1);
        }

        protected virtual string GetFilePath(LogSettings type)
        {
            var now = DateTime.Now;
            var folder = GetFolder(type);
            var path1 = string.Format("{0}.txt", now.Day);
            return Path.Combine(folder, path1);
        }

        protected virtual string GetLogContent(object message)
        {
            return message.ToString();
        }

        public void Error(object message)
        {
            this.Log(message, LogSettings.Errors);
        }

        public void Info(object message)
        {
            this.Log(message, LogSettings.Info);
        }

        public void Debug(object message)
        {
            this.Log(message, LogSettings.Debug);
        }

        protected void Log(object message, LogSettings type)
        {
            StreamWriter sw = null;
            try
            {
                var folder = GetFolder(type);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var file = GetFilePath(type);
                sw = !File.Exists(file) ? File.CreateText(file) : File.AppendText(file);
                sw.WriteLine(GetLogContent(message));
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            catch (Exception)
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}