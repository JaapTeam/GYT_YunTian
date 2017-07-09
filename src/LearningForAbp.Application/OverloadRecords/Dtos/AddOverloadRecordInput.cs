using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace LearningForAbp.OverloadRecords.Dtos
{
    [AutoMap(typeof(OverloadRecords))]
    public class AddOverloadRecordInput
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
