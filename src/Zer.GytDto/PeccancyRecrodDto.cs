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

        [ExportSort(1)]
        [ExportDisplayName("处罚决定书编号")]
        [ImportSort(1)]
        public string PeccancyId { get; set; }

        [ExportIgnore]
        [ImprotIgnore]
        public int CompanyId { get; set; }

        [ExportSort(2)]
        [ExportDisplayName("公司名称")]
        [ImportSort(2)]
        public string CompanyName { get; set; }

        [ExportSort(3)]
        [ExportDisplayName("前车牌号")]
        [ImportSort(3)]
        public string FrontTruckNo { get; set; }

        [ExportSort(4)]
        [ImportSort(4)]
        [ExportDisplayName("前车牌号")]
        public string BehindTruckNo { get; set; }

        [ExportSort(5)]
        [ImportSort(5)]
        [ExportDisplayName("所属行业")]
        public string TraderRange { get; set; }
        
        [ExportIgnore]
        [ImprotIgnore]
        public string DriverId { get; set; }

        [ExportSort(6)]
        [ImportSort(6)]
        [ExportDisplayName("驾驶员姓名")]
        public string DriverName { get; set; }

        [ExportSort(7)]
        [ImportSort(7)]
        [ExportDisplayName("违法日期")]
        public string PeccancyDate { get; set; }

        [ExportSort(8)]
        [ImportSort(8)]
        [ExportDisplayName("违法事项")]
        public string PeccancyMatter { get; set; }

        [ExportSort(9)]
        [ImportSort(9)]
        [ExportDisplayName("总重")]
        public string GrossWeight { get; set; }
        [ExportSort(10)]
        [ImportSort(10)]
        [ExportDisplayName("轴数")]
        public int AxisNumber { get; set; }

        [ExportSort(11)]
        [ImportSort(11)]
        [ExportDisplayName("文件来源")]
        public string Source { get; set; }

        [ImportSort(12)]
        [ExportDisplayName("状态")]
        [ExportIgnore]
        public Status Status { get; set; }
    }
}
