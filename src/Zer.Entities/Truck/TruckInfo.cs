using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer.Entities.Truck
{
    public class TruckInfo:EntityBase
    {
        public string FrontTruckNo { get; set; }
        public string RearTruckNo { get; set; }
        public int CompanyId { get; set; }
        public int DriverId {get;set ;}
    }

    public enum TruckState
    {
        Active = 0,
        Frozen = 1
    }
}
