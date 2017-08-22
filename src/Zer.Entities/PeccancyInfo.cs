using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class PeccancyInfo : EntityBase<string>
    {
        [StringLength(30)]
        public string PeccancyId { get; set; }
        public int CompanyId { get; set; }
        [StringLength(50)]
        public string CompanyName { get; set; }

        public int TruckId { get; set; }

        [StringLength(20)]
        public string FrontTruckNo { get; set; }
        [StringLength(20)]
        public string BehindTruckNo { get; set; }
        [StringLength(20)]
        public string DriverId { get; set; }
        [StringLength(20)]
        public string DriverName { get; set; }

        public DateTime? PeccancyDate { get; set; }

        [StringLength(20)]
        public string PeccancyMatter { get; set; }

        public decimal GrossWeight { get; set; }
        public int AxisNumber { get; set; }

        [StringLength(50)]
        public string Source { get; set; }
        public Status Status { get; set; }
        [Key]
        [StringLength(30)]
        public override string Id { get; set; }
    }

    public enum Status
    {
        未整改=1,
        已整改=4
    }
}
