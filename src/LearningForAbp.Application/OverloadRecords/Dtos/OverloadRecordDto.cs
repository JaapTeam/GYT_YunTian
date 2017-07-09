using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace LearningForAbp.OverloadRecords.Dtos
{
    [AutoMap(typeof(OverloadRecords))]
    public class OverloadRecordDto
    {
        public int Id { get; set; }
        public string TruckNo { get; set; }
    }
}
