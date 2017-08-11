using AutoMapper;
using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto
{
    public class PeccancyRecrodDto : DtoBase
    {
        [ExportIgnore]
        [ImprotIgnore]
        public int Id { get; set; }

        [Sort(1)]
        [ExportDisplayName("处罚决定书编号")]
        public string PeccancyId { get; set; }

        [ExportIgnore]
        [ImprotIgnore]
        public int CompanyId { get; set; }

        [Sort(2)]
        [ExportDisplayName("公司名称")]
        public string CompanyName { get; set; }

        [Sort(3)]
        [ExportDisplayName("前车牌号")]
        public string FrontTruckNo { get; set; }

        [Sort(4)]
        [ExportDisplayName("前车牌号")]
        public string BehindTruckNo { get; set; }

        [Sort(5)]
        [ExportDisplayName("所属行业")]
        public string TraderRange { get; set; }
        
        [ExportIgnore]
        [ImprotIgnore]
        public string DriverId { get; set; }

        [Sort(6)]
        [ExportDisplayName("驾驶员姓名")]
        public string DriverName { get; set; }

        [Sort(7)]
        [ExportDisplayName("违法日期")]
        public string PeccancyDate { get; set; }

        [Sort(8)]
        [ExportDisplayName("违法事项")]
        public string PeccancyMatter { get; set; }

        [Sort(9)]
        [ExportDisplayName("总重")]
        public string GrossWeight { get; set; }
        [Sort(10)]
        [ExportDisplayName("轴数")]
        public int AxisNumber { get; set; }
        [Sort(11)]
        [ExportDisplayName("文件来源")]
        public string Source { get; set; }

        [Sort(12)]
        [ExportDisplayName("状态")]
        [ExportIgnore]
        public Status Status { get; set; }
    }
}
