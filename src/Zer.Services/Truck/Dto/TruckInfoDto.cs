using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Zer.Entities.Truck;

namespace Zer.Services.Truck.Dto
{
    public class TruckInfoDto
    {
        public int Id { get; set; }
        public string FrontTruckNo { get; set; }
        public string RearTruckNo { get; set; }
        public string ConpanyName { get; set; }
        public string DriverName { get; set; }
        public TruckState State { get; set; }
    }
}
