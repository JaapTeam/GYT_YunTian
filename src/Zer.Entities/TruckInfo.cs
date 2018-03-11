using System;
using System.ComponentModel.DataAnnotations;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class TruckInfo : EntityBase
    {
        /// <summary>
        /// 前车牌
        /// </summary>
        [StringLength(20)]
        public string FrontTruckNo { get; set; }
        /// <summary>
        /// 后车牌
        /// </summary>
        [StringLength(20)]
        public string BehindTruckNo { get; set; }

        public int CompanyId { get; set; }

        [StringLength(20)]
        public string DriverId { get; set; }
    }

    public enum TruckState
    {
        Active = 0,
        Frozen = 1
    }
}
