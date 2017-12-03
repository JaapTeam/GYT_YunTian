using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class LngAllowanceInfo : EntityBase<string>,ICreateTime
    {
        public int CompanyId { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        public int LotId { get; set; }

        [StringLength(10)]
        public string TruckNo { get; set; }

        [StringLength(15)]
        public string EngineId { get; set; }

        [StringLength(20)]
        public string CylinderDefaultId { get; set; }

        [StringLength(20)]
        public string CylinderSeconedId { get; set; }

        public LngStatus Status { get; set; }
        
        public bool IsForceImport { get; set; }

        [MaxLength]
        public string Description { get; set; }

        [Key]
        [StringLength(30)]
        public override string Id { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public enum LngStatus
    {
        未补贴=1,
        已补贴=4
    }
}