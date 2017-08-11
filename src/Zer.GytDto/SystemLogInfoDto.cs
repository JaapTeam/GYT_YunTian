using Zer.Framework.Attributes;

namespace Zer.GytDto
{
    public class SystemLogInfoDto : IValidateInputParameter
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}