using Zer.Framework.Attributes;
using Zer.Framework.Dto;

namespace Zer.GytDto
{
    public class SystemLogInfoDto : DtoBase
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}