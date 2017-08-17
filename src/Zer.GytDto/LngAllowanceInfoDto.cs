using System;
using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;
using Zer.Framework.Export.Attributes;
using Zer.Framework.Extensions;

namespace Zer.GytDto
{
    [Serializable]
    public class LngAllowanceInfoDto : DtoBase
    {
        [ExportSort(1)]
        [ImprotIgnore]
        [ExportDisplayName("±àºÅ")]
        public int Id { get; set; }

        [ExportIgnore]
        [ImprotIgnore]
        public int CompanyId { get; set; }

        [ExportSort(2)]
        [ImportSort(1)]
        [ExportDisplayName("¹«Ë¾Ãû³Æ")]
        public string CompanyName { get; set; }

        [ExportSort(3)]
        [ImportSort(2)]
        [ExportDisplayName("Åú´Î")]
        public int LotId { get; set; }

        [ExportSort(4)]
        [ImportSort(3)]
        [ExportDisplayName("³µÅÆºÅ")]
        public string TruckNo { get; set; }

        [ExportSort(5)]
        [ImportSort(4)]
        [ExportDisplayName("·¢¶¯»úºÅ")]
        public string EngineId { get; set; }

        [ExportSort(6)]
        [ImportSort(5)]
        [ExportDisplayName("Æû¸×1±àºÅ")]
        public string CylinderDefaultId { get; set; }

        [ExportSort(7)]
        [ImportSort(6)]
        [ExportDisplayName("Æû¸×2±àºÅ")]
        public string CylinderSeconedId { get; set; }

        [ExportSort(8)]
        [ImportSort(7)]
        [ExportDisplayName("²¹Ìù×´Ì¬")]
        public LngStatus Status { get; set; }
    }
}