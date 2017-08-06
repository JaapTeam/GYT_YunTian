using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class OverloadInfo : EntityBase
    {
        public int CompanyId { get; set; }
        [MaxLength(20)]
        public string FrontTruckNo { get; set; }
         [MaxLength(20)]
        public string BehindTruckNo { get; set; }

        public int DriverId { get; set; }

        public DateTime OverloadDateTime { get; set; }

        public OverloadMatter OverloadMatter { get; set; }
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

    public enum OverloadMatter
    {
        深圳超限
    }
}
