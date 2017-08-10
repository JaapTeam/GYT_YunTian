using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;

namespace Zer.GytDto
{
    public class OverloadRecrodDto : IValidateInputParameter
    {
        [Sort(1)]
        [ExportDisplayName("超限超载编号")]
        public int Id { get; set; }
        [ExportIgnore]
        public int CompanyId { get; set; }

        [Sort(2)]
        [ExportDisplayName("公司名称")]
        public string CompanName { get; set; }

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
        public int DriverId { get; set; }

        [Sort(6)]
        [ExportDisplayName("驾驶员名称")]
        public string DriverName { get; set; }

        [Sort(7)]
        [ExportDisplayName("违法日期")]
        public string OverloadDate { get; set; }

        [Sort(8)]
        [ExportDisplayName("违法事项")]
        public string OverloadMatter { get; set; }

        [Sort(8)]
        [ExportDisplayName("总重")]
        public string GrossWeight { get; set; }
        [Sort(9)]
        [ExportDisplayName("轴数")]
        public int AxisNumber { get; set; }
        [Sort(10)]
        [ExportDisplayName("文件来源")]
        public string Source { get; set; }
    }
}
