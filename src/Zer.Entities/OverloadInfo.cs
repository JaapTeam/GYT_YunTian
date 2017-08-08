using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class OverloadInfo : EntityBase
    {
        [MaxLength(50)]
        public string PeccancyId { get; set; }
        public int CompanyId { get; set; }
        [MaxLength(20)]
        public string FrontTruckNo { get; set; }
         [MaxLength(20)]
        public string BehindTruckNo { get; set; }
        [MaxLength(50)]
        public string DriverId { get; set; }
        [MaxLength(30)]
        public string DriverName { get; set; }

        public DateTime? OverloadDate { get; set; }

        public string OverloadMatter { get; set; }
        public decimal GrossWeight { get; set; }
        public int AxisNumber { get; set; }
         [MaxLength(50)]
        public string Source { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        未整改,
        已整改
    }
}
