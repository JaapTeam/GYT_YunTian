using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class SystemLogInfo : EntityBase
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}