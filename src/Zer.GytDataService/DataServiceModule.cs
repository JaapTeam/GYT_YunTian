using System.Reflection;
using Zer.Framework.Module;

namespace Zer.GytDataService
{
    public class DataServiceModule : ModuleBase
    {
        public DataServiceModule() : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}