using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace LearningForAbp.OverloadRecords
{
    public class OverloadRecords : Entity, IHasCreationTime
    {
        public string CompanyName { get; set; }
        public string TruckNo { get; set; }
        public string TraderName { get; set; }
        public string DriverName { get; set; }
        public DateTime LogDate { get; set; }
        public decimal TotalWeight { get; set; }
        public int AxleQuantity { get; set; }
        public string Source { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
