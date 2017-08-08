using System.Reflection;
using Zer.Framework.Module;

namespace Zer.Framework.Mvc.Logs
{
    public class MvcLoggerModule:ModuleBase
    {
        public MvcLoggerModule() : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}