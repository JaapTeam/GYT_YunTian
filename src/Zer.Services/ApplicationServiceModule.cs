using System.Reflection;
using Zer.Framework.Module;

namespace Zer.Services
{
    public class ApplicationServiceModule : ModuleBase
    {
        public ApplicationServiceModule() : base(Assembly.GetExecutingAssembly())
        {
        }
    }
}