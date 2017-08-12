using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class PeccancyInfo : EntityBase
    {
        [MaxLength(50)]
        public string PeccancyId { get; set; }
        public int CompanyId { get; set; }
        [MaxLength(100)]
        public string CompanyName { get; set; }

        public int TruckId { get; set; }

        [MaxLength(20)]
        public string FrontTruckNo { get; set; }
         [MaxLength(20)]
        public string BehindTruckNo { get; set; }
        [MaxLength(50)]
        public string DriverId { get; set; }
        [MaxLength(30)]
        public string DriverName { get; set; }

        public DateTime? PeccancyDate { get; set; }

        public string PeccancyMatter { get; set; }
        public decimal GrossWeight { get; set; }
        public int AxisNumber { get; set; }
         [MaxLength(50)]
        public string Source { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        未整改=1,
        已整改=4
    }
}
