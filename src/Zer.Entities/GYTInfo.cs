using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class GYTInfo : EntityBase<string>
    {
        public BusinessType BusinessType { get; set; }

        public int? OriginalCompanyId { get; set; }

        [StringLength(50)]
        public string OriginalCompanyName { get; set; }

        [StringLength(10)]
        public string OriginalTruckNo { get; set; }

        public int BidCompanyId { get; set; }

        [StringLength(50)]
        public string BidCompanyName { get; set; }

        [StringLength(10)]
        public string BidTruckNo { get; set; }

        public DateTime? BidDate { get; set; }

        [StringLength(20)]
        public string BidName { get; set; }

        [StringLength(20)]
        public string BidDisplayName { get; set; }

        public BusinessState Status { get; set; }
        [Key]
        [StringLength(30)]
        public override string Id { get; set; }
    }

    /// <summary>
    /// 业务类型
    /// </summary>
    public enum BusinessType
    {
        天然气车辆 = 0,
        过户车辆 = 1,
        以旧换新车辆 = 2
    }

    /// <summary>
    /// 信息状态
    /// </summary>
    public enum BusinessState
    {
        已注销 = 4,
        已办理 = 8
    }
}
