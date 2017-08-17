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
        [Sort(1)]
        [ImprotIgnore]
        [ExportDisplayName("±àºÅ")]
        public int Id { get; set; }

        [ExportIgnore]
        [ImprotIgnore]
        public int CompanyId { get; set; }

        [Sort(2)]
        [ExportDisplayName("¹«Ë¾Ãû³Æ")]
        public string CompanyName { get; set; }

        [Sort(3)]
        [ExportDisplayName("Åú´Î")]
        public int LotId { get; set; }

        [Sort(4)]
        [ExportDisplayName("³µÅÆºÅ")]
        public string TruckNo { get; set; }

        [Sort(5)]
        [ExportDisplayName("·¢¶¯»úºÅ")]
        public string EngineId { get; set; }

        [Sort(6)]
        [ExportDisplayName("Æû¸×1±àºÅ")]
        public string CylinderDefaultId { get; set; }

        [Sort(7)]
        [ExportDisplayName("Æû¸×2±àºÅ")]
        public string CylinderSeconedId { get; set; }

        [Sort(8)]
        [ExportDisplayName("²¹Ìù×´Ì¬")]
        public LngStatus Status { get; set; }
    }
}