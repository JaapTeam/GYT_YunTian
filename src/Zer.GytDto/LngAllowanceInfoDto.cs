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
        [ExportSort(1)]
        [ImprotIgnore]
        [ExportDisplayName("���")]
        public int Id { get; set; }

        [ExportIgnore]
        [ImprotIgnore]
        public int CompanyId { get; set; }

        [ExportSort(2)]
        [ImportSort(1)]
        [ExportDisplayName("��˾����")]
        public string CompanyName { get; set; }

        [ExportSort(3)]
        [ImportSort(2)]
        [ExportDisplayName("����")]
        public int LotId { get; set; }

        [ExportSort(4)]
        [ImportSort(3)]
        [ExportDisplayName("���ƺ�")]
        public string TruckNo { get; set; }

        [ExportSort(5)]
        [ImportSort(4)]
        [ExportDisplayName("��������")]
        public string EngineId { get; set; }

        [ExportSort(6)]
        [ImportSort(5)]
        [ExportDisplayName("����1���")]
        public string CylinderDefaultId { get; set; }

        [ExportSort(7)]
        [ImportSort(6)]
        [ExportDisplayName("����2���")]
        public string CylinderSeconedId { get; set; }
    }
}