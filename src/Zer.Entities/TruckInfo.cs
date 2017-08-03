namespace Zer.Entities
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
