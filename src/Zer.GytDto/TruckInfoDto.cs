using Zer.Entities;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;

namespace Zer.GytDto
{
    public class TruckInfoDto : DtoBase
    {
        public int Id { get; set; }
        public string FrontTruckNo { get; set; }
        public string BehindTruckNo { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
       
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
        public TruckState State { get; set; }
    }
}
