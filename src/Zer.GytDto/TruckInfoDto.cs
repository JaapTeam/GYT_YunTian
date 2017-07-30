using Zer.Entities;

namespace Zer.GytDto
{
    public class TruckInfoDto
    {
        public int Id { get; set; }
        public string FrontTruckNo { get; set; }
        public string RearTruckNo { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public TruckState State { get; set; }
    }
}
