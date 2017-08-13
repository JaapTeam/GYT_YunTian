using System;
using Zer.Framework.Entities;

namespace Zer.Entities
{
    [Serializable]
    public class TruckInfo : EntityBase
    {
        /// <summary>
        /// 前车牌
        /// </summary>
        public string FrontTruckNo { get; set; }
        /// <summary>
        /// 后车牌
        /// </summary>
        public string BehindTruckNo { get; set; }
        public int CompanyId { get; set; }
        public int? DriverId { get; set; }
    }

    public enum TruckState
    {
        Active = 0,
        Frozen = 1
    }
}
