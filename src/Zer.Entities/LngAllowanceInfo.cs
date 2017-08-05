using Zer.Framework.Entities;

namespace Zer.Entities
{
    public class LngAllowanceInfo : EntityBase
    {
        public string CompanyName { get; set; }

        public int LotId { get; set; }

        public string TruckNo { get; set; }

        public string EngineId { get; set; }

        public string CylinderDefaultId { get; set; }

        public string CylinderSeconedId { get; set; }
    }
}