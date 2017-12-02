using System;
using Zer.Entities;
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

        [ExportSort(1)]
        [ExportDisplayName("日志编号")]
        public int Id { get; set; }

        [ExportIgnore]
        public int UserId { get; set; }

        [ExportSort(2)]
        [ExportDisplayName("用户")]
        public string DisplayName { get; set; }

        [ExportSort(3)]
        [ExportDisplayName("操作类型")]
        public ActionType ActionType { get; set; }

        [ExportSort(4)]
        [ExportDisplayName("操作模块")]
        public string ActionModel { get; set; }

        [ExportSort(5)]
        [ExportDisplayName("发生时间")]
        public DateTime CreateTime { get; set; }
        
        [ExportSort(7)]
        [ExportDisplayName("IP")]
        public string IP { get; set; }

        [ExportSort(8)]
        [ExportDisplayName("内容")]
        public string Content { get; set; }
    }
}
