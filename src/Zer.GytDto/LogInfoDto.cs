using System;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto
{
    public class LogInfoDto : DtoBase
    {
        public LogInfoDto()
        {
            CreateTime = DateTime.Now;
        }

        [Sort(1)]
        [ExportDisplayName("日志编号")]
        public int Id { get; set; }

        [ExportIgnore]
        public int UserId { get; set; }

        [Sort(2)]
        [ExportDisplayName("用户")]
        public string DisplayName { get; set; }

        [Sort(3)]
        [ExportDisplayName("操作类型")]
        public ActionType ActionType { get; set; }

        [Sort(4)]
        [ExportDisplayName("操作模块")]
        public string ActionModel { get; set; }

        [Sort(5)]
        [ExportDisplayName("发生时间")]
        public DateTime CreateTime { get; set; }
        
        [Sort(7)]
        [ExportDisplayName("IP")]
        public string IP { get; set; }

        [Sort(8)]
        [ExportDisplayName("内容")]
        public string Content { get; set; }
    }

    public enum ActionType
    {
        Add=0,
        Edit=1,
        Delete=2,
        Change=3,
        Query=4
    }
}
