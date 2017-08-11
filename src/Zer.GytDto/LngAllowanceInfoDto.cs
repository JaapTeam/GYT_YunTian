using System;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;
using Zer.Framework.Export.Attributes;
using Zer.Framework.Extensions;

namespace Zer.GytDto
{
    [Serializable]
    public class LngAllowanceInfoDto : DtoBase
    {
        [Sort(1)]
        [ImprotIgnore]
        [ExportDisplayName("编号")]
        public int Id { get; set; }

        [ExportIgnore]
        [ImprotIgnore]
        public int CompanyId { get; set; }

        [Sort(2)]
        [ExportDisplayName("公司名称")]
        public string CompanyName { get; set; }

        [Sort(3)]
        [ExportDisplayName("批次")]
        public int LotId { get; set; }

        [Sort(4)]
        [ExportDisplayName("车牌号")]
        public string TruckNo { get; set; }

        [Sort(5)]
        [ExportDisplayName("发动机号")]
        public string EngineId { get; set; }

        [Sort(6)]
        [ExportDisplayName("汽缸1编号")]
        public string CylinderDefaultId { get; set; }

        [Sort(7)]
        [ExportDisplayName("汽缸2编号")]
        public string CylinderSeconedId { get; set; }
    }
}