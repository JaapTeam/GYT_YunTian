using Zer.Framework.Dto;

namespace Zer.GytDto.SearchFilters
{
    public class LngAllowanceSearchDto : SearchDtoBase
    {
        public string TruckNo { get; set; }
        public string EngineId { get; set; }
        public bool? IsAllowanced { get; set; }
    }
}