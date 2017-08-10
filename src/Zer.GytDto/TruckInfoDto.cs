using Zer.Entities;
using Zer.Framework.Attributes;

namespace Zer.GytDto
{
    public class TruckInfoDto : IValidateInputParameter
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
